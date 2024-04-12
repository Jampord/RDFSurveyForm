using Microsoft.EntityFrameworkCore;
using RDFSurveyForm.Data;
using RDFSurveyForm.DATA_ACCESS_LAYER.HELPERS;
using RDFSurveyForm.DataAccessLayer.IR_Setup.Interface;
using RDFSurveyForm.Dto.SetupDto.BranchDto;
using RDFSurveyForm.Dto.SetupDto.GroupDto;
using RDFSurveyForm.Migrations;
using RDFSurveyForm.Model.Setup;


namespace RDFSurveyForm.DataAccessLayer.IR_Setup.Repository
{
    public class GroupRepository : IGroupRepository
    {
        private readonly StoreContext _context;

        public GroupRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<bool> GroupAlreadyExist(string groupName)
        {
            var groupAlreadyExist = await _context.Groups.AnyAsync(x => x.GroupName == groupName);
            if (groupAlreadyExist)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> AddGroup(AddGroupDto group)
        {
            var addGroup = new Groups
            {
                
                GroupName = group.GroupName,    
                BranchId = group.BranchId,
                CreatedAt = DateTime.Now,
                CreatedBy = group.CreatedBy, 
            };
            await _context.Groups.AddAsync(addGroup);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateGroup(UpdateGroupDto group)
        {

            var updateGroup = await _context.Groups.FirstOrDefaultAsync(x => x.Id == group.Id); 
            if (updateGroup != null)
            {
                updateGroup.GroupName = group.GroupName;
                updateGroup.UpdatedAt = DateTime.Now;
                updateGroup.UpdatedBy = group.UpdatedBy;
                updateGroup.BranchId = group.BranchId;

                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<PagedList<GetGroupDto>> GroupListPagnation(UserParams userParams, bool? status, string search)
        {
            var users = _context.Groups.Select(x => new GetGroupDto
            {
                Id = x.Id,
                GroupName = x.GroupName,
                CreatedAt = x.CreatedAt,
                CreatedBy = x.CreatedBy,
                IsActive = x.IsActive,
                UpdatedAt = x.UpdatedAt,
                UpdatedBy = x.UpdatedBy,
                BranchId = x.BranchId,
                BranchName = x.Branch.BranchName,

            });

            if (status != null)
            {
                users = users.Where(x => x.IsActive == status);
            }

            if (!string.IsNullOrEmpty(search))
            {
                users = users.Where(x => Convert.ToString(x.Id).ToLower().Contains(search.Trim().ToLower())
                || Convert.ToString(x.GroupName).ToLower().Contains(search.Trim().ToLower())
                || Convert.ToString(x.BranchName).ToLower().Contains(search.Trim().ToLower()));
            }

            return await PagedList<GetGroupDto>.CreateAsync(users, userParams.PageNumber, userParams.PageSize);
        }

       
        
        public async Task<bool> SetIsactive(int Id)
        {
            var setIsactive = await _context.Groups.FirstOrDefaultAsync(x => x.Id == Id);
            if (setIsactive != null)
            {
                setIsactive.IsActive = !setIsactive.IsActive;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
