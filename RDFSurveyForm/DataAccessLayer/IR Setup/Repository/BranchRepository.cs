using Microsoft.EntityFrameworkCore;
using RDFSurveyForm.Data;
using RDFSurveyForm.DATA_ACCESS_LAYER.HELPERS;
using RDFSurveyForm.DataAccessLayer.IR_Setup.Interface;
using RDFSurveyForm.Dto.ModelDto.UserDto;
using RDFSurveyForm.Dto.SetupDto.BranchDto;
using RDFSurveyForm.Model.Setup;


namespace RDFSurveyForm.DataAccessLayer.IR_Setup.Repository
{
    public class BranchRepository : IBranchRepository
    {
        private readonly StoreContext _context;

        public BranchRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<bool> BranchAlreadyExist(string  branchName)
        {
            var branchAlreadyExist = await _context.Branches.AnyAsync(x=>x.BranchName == branchName);
            if(branchAlreadyExist)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> BranchCodeExist(string branchCode)
        {
            var codeAlreadyExist = await _context.Branches.AnyAsync(x => x.BranchCode == branchCode);
            if(codeAlreadyExist)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> AddBranch(AddBranchDto branch)
        {
            var addBranch = new Branch
            {
                Id = branch.Id,
                BranchName = branch.BranchName,
                BranchCode = branch.BranchCode,
                CreatedAt = DateTime.Now,
                CreatedBy = branch.CreatedBy,
            };
            await _context.Branches.AddAsync(addBranch);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateBranch(UpdateBranchDto branch)
        {

            var updateBranch = await _context.Branches.FirstOrDefaultAsync(x => x.Id == branch.Id);
            if(updateBranch != null) 
            {
                updateBranch.BranchName = branch.BranchName;
                updateBranch.BranchCode = branch.BranchCode;
                updateBranch.UpdatedAt = DateTime.Now;
                updateBranch.UpdatedBy = branch.UpdatedBy;
                
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<PagedList<GetBranchDto>> BranchListPagnation(UserParams userParams, bool? status, string search)
        {           

            var result = _context.Branches.Select(x => new GetBranchDto
            {
                Id = x.Id,
                BranchName = x.BranchName,
                BranchCode = x.BranchCode,
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
                || Convert.ToString(x.BranchName).ToLower().Contains(search.Trim().ToLower())
                || Convert.ToString(x.BranchCode).ToLower().Contains(search.Trim().ToLower()));
            }

            return await PagedList<GetBranchDto>.CreateAsync(result, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<bool> IsActiveValidation(int Id)
        {
            var userList = await _context.Groups.Where(x => x.IsActive == true).ToListAsync();
            var validation = userList.FirstOrDefault(x => x.BranchId == Id);
            if (validation == null)
            {
                return false;
            };
            return true;

        }

        public async Task<bool> SetIsactive(int Id)
        {
            var setIsactive = await _context.Branches.FirstOrDefaultAsync(x => x.Id == Id);
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
