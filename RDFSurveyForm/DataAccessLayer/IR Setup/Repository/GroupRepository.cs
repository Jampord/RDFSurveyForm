using Microsoft.EntityFrameworkCore;
using RDFSurveyForm.Data;
using RDFSurveyForm.DATA_ACCESS_LAYER.HELPERS;
using RDFSurveyForm.DataAccessLayer.IR_Setup.Interface;
using RDFSurveyForm.Dto.SetupDto.BranchDto;
using RDFSurveyForm.Dto.SetupDto.GroupDto;
using RDFSurveyForm.Migrations;
using RDFSurveyForm.Setup;

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
                Id = group.Id,
                GroupName = group.GroupName,                
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

                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<PagedList<GetGroupDto>> CustomerListPagnation(UserParams userParams, bool? status, string search)
        {

            var result = _context.Groups.Select(x => new GetGroupDto
            {
                Id = x.Id,
                GroupName = x.GroupName,
                CreatedAt = x.CreatedAt,
                CreatedBy = x.CreatedBy,
                IsActive = x.IsActive,
                UpdatedAt = x.UpdatedAt,
                UpdatedBy = x.UpdatedBy,
            });

            if (status != null)
            {
                result = result.Where(x => x.IsActive == status);
            }

            if (!string.IsNullOrEmpty(search))
            {
                result = result.Where(x => Convert.ToString(x.Id).ToLower().Contains(search.Trim().ToLower())
                || Convert.ToString(x.GroupName).ToLower().Contains(search.Trim().ToLower()));
            }

            return await PagedList<GetGroupDto>.CreateAsync(result, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<bool> DeleteGroup(int Id)
        {
            var deletegroup = await _context.Groups.FirstOrDefaultAsync(x => x.Id == Id);
            if (deletegroup != null)
            {
                _context.Groups.Remove(deletegroup);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> SetInactive(int Id)
        {
            var setInactive = await _context.Groups.FirstOrDefaultAsync(x => x.Id == Id);
            if (setInactive != null)
            {
                setInactive.IsActive = !setInactive.IsActive;
            }
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
