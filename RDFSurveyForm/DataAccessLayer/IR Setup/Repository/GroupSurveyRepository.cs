using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using RDFSurveyForm.Data;
using RDFSurveyForm.DATA_ACCESS_LAYER.HELPERS;
using RDFSurveyForm.DataAccessLayer.IR_Setup.Interface;
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

            var categoryList = await _context.Category.ToListAsync();

             foreach (var items in categoryList)
             {

                var addSurveyScore = new SurveyScore
                {
                    CategoryName = items.CategoryName,
                    CategoryPercentage = items.CategoryPercentage,
                    Limit = 100,
                    SurveyGeneratorId = newGenerator.Id,
                   
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
            var users = _context.GroupSurvey.Select(x => new GetGroupSurveyDto
            {

                SurveyGeneratorId = x.SurveyGeneratorId,       
                BranchName = x.Groups.Branch.BranchName,
                IsActive = x.IsActive, 
                CreatedBy = x.CreatedBy,
                CreatedAt = x.CreatedAt,
                GroupName = x.Groups.GroupName,                                                              
                IsTransacted = x.IsTransacted,
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
                        CategoryName = x.CategoryName,
                        CategoryPercentage = x.CategoryPercentage * 100,
                        Score = x.Score,
                        Limit = x.Limit,
                        SurveyPercentage = x.Score == null ? (x.Score * 0.01M) + x.CategoryPercentage : 0,

                    }).ToList()

                }).ToListAsync();

            return await results;
        }            
    }
}
