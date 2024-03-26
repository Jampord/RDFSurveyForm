using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using RDFSurveyForm.Data;
using RDFSurveyForm.DATA_ACCESS_LAYER.HELPERS;
using RDFSurveyForm.DataAccessLayer.IR_Setup.Interface;
using RDFSurveyForm.Dto.ModelDto.UserDto;
using RDFSurveyForm.Dto.SetupDto.GroupDto;
using RDFSurveyForm.Dto.SetupDto.GroupSurveyDto;
using RDFSurveyForm.Model.Setup;
using System.Reflection.Emit;


namespace RDFSurveyForm.DataAccessLayer.IR_Setup.Repository
{
    public class GroupSurveyRepository : IGroupSurveyRepository
    {
        private readonly StoreContext _context;

        public GroupSurveyRepository(StoreContext context)
        {
            _context = context;
        }


        public async Task<bool> AddSurvey(AddGroupSurveyDto survey)
        {
            var newGenerator = new SurveyGenerator { };
            await _context.SurveyGenerator.AddAsync(newGenerator);
            await _context.SaveChangesAsync();


            var addGroupId = new GroupSurvey
            {
                GroupsId = survey.GroupsId,
                CreatedAt = DateTime.Now,
                CreatedBy = survey.CreatedBy,
                SurveyGeneratorId = newGenerator.Id,

            };

            await _context.GroupSurvey.AddAsync(addGroupId);

            var categoryList = await _context.Category.Where(x => x.IsActive).ToListAsync();

             foreach (var items in categoryList)
             {

                var addSurveyScore = new SurveyScore
                {
                    CategoryName = items.CategoryName,
                    CategoryPercentage = items.CategoryPercentage,
                    Limit = items.Limit,
                    SurveyGeneratorId = newGenerator.Id,
                    CreatedBy = items.CreatedBy,
                   
                };

                await _context.SurveyScores.AddAsync(addSurveyScore);
            }

            return true;
        }

        public async Task<bool> GroupIdDoesnotExist(int? Id)
        {
            var groupExist = await _context.Groups.AnyAsync(x => x.Id == Id);
            if(groupExist == true)
            {
                return true;
            }
            return false;
        }

        public async Task<PagedList<GetGroupSurveyDto>> GroupSurveyPagination(UserParams userParams, bool? status, string search)
        {
            var totalScore = _context.SurveyScores
            .GroupBy(x => new
            {
                SurveyGeneratorId = x.SurveyGeneratorId,
                Id = x.Id,
                Score = x.Score,
                Limit = x.Limit,
            }).Select(x => new ScoreDto
            {
                SurveyGeneratorId = x.Key.SurveyGeneratorId,
                Id = x.Key.Id,
                Score = x.Key.Score / x.Key.Limit,
                Limit = x.Key.Limit,
                ActualScore = x.Key.Score
            });


            var categoryPercentage = _context.SurveyScores
              .GroupJoin(totalScore, total => total.Id, percentage => percentage.Id, (total, percentage) => new { total, percentage })
              .SelectMany(x => x.percentage.DefaultIfEmpty(), (x, percentage) => new { x.total, percentage })
              .GroupBy(x => new
              {
                  x.total.SurveyGeneratorId,
                  x.total.Id,
                  x.total.CategoryPercentage,
              }).Select(x => new ScoreDto
              {
                  SurveyGeneratorId = x.Key.SurveyGeneratorId,
                  Id = x.Key.Id,
                  Score = x.Sum(x => x.percentage.Score) * x.Key.CategoryPercentage,
                  CategoryPercentage = x.Key.CategoryPercentage,
              });


            var users = _context.GroupSurvey
                .GroupJoin(categoryPercentage, score => score.SurveyGeneratorId, percentage => percentage.SurveyGeneratorId, (score, percentage) => new { score, percentage })
                .SelectMany(x => x.percentage.DefaultIfEmpty(), (x, percentage) => new { x.score, percentage })
                .GroupBy(x => x.score.GroupsId)
                .Select(x => new GetGroupSurveyDto
                {
                    SurveyGeneratorId = x.Key,
                    BranchName = x.First().score.Groups.Branch.BranchName,
                    IsActive = x.First().score.IsActive,
                    CreatedBy = x.First().score.CreatedBy,
                    CreatedAt = x.First().score.CreatedAt,
                    GroupName = x.First().score.Groups.GroupName,
                    IsTransacted = x.First().score.IsTransacted,
                    FinalScore = x.Sum(x => x.percentage.Score) * 100
                });

            if (status != null)
            {
                users = users.Where(x => x.IsActive == status);
            }

            if (!string.IsNullOrEmpty(search))
            {
                users = users.Where(x => x.SurveyGeneratorId.ToString().Contains(search)
                || Convert.ToString(x.BranchName).ToLower().Contains(search.Trim().ToLower())
                || Convert.ToString(x.GroupName).ToLower().Contains(search.Trim().ToLower())
                );
            }

            users = users.OrderByDescending(x => x.FinalScore);

            return await PagedList<GetGroupSurveyDto>.CreateAsync(users, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<IReadOnlyList<ViewSurveyDto>> ViewSurvey(int? id)
        {
            var results = _context.SurveyScores.Where(x => x.SurveyGeneratorId == id)
                .GroupBy(x => x.SurveyGeneratorId).Select(x => new ViewSurveyDto
                {
                    SurveyGeneratorId = x.Key,
                    Categories = x.Select(x => new ViewSurveyDto.Category
                    {
                        Id = x.Id,
                        CategoryName = x.CategoryName,
                        CategoryPercentage = x.CategoryPercentage * 100,
                        Score = x.Score,
                        Limit = x.Limit,
                        SurveyPercentage =  (x.Score / x.Limit) * x.CategoryPercentage,

                    }).ToList()

                }).ToListAsync();

            return await results;
        }

        public async Task<bool> ScoreLimit(UpdateSurveyScoreDto limit)
        {
            
            foreach (var items in limit.UpdateSurveyScores)
            {
                var lim = await _context.SurveyScores.FirstOrDefaultAsync(x => x.Id == items.Id);
                var limits = items.Score;

                
                if (limits > lim.Limit || limits < 0)
                {
                    return false;
                }
                
            }
            return true;
        }


        public async Task<bool> UpdateScore(UpdateSurveyScoreDto score)
        {
            
            var updateScore = await _context.GroupSurvey.FirstOrDefaultAsync(x => x.SurveyGeneratorId == score.SurveyGeneratorId);
           

            foreach (var items in score.UpdateSurveyScores)
            {
                var scores = await _context.SurveyScores.FirstOrDefaultAsync(x => x.Id == items.Id);
                if (scores != null)
                {                    
                    scores.Score = items.Score;
                    scores.UpdatedBy = score.UpdatedBy;
                    scores.UpdatedAt = score.UpdatedAt;
                    updateScore.UpdatedBy = score.UpdatedBy;
                    updateScore.UpdatedAt = score.UpdatedAt;
                    updateScore.IsTransacted = true;                    
                }
            }
            await _context.SaveChangesAsync();

            return true;

        }
    }
}
