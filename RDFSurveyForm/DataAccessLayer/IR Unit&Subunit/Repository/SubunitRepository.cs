using Microsoft.EntityFrameworkCore;
using RDFSurveyForm.Data;
using RDFSurveyForm.DATA_ACCESS_LAYER.HELPERS;
using RDFSurveyForm.DataAccessLayer.IR_Unit_Subunit.Interface;
using RDFSurveyForm.Dto.Unit_SubUnitDto;
using RDFSurveyForm.Model.Unit_SubUnit;

namespace RDFSurveyForm.DataAccessLayer.IR_Unit_Subunit.Repository
{
    public class SubunitRepository : ISubunitRepository
    {
        private readonly StoreContext _context;

        public SubunitRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistingUnit(string subunit)
        {
            var existingSubunit = await _context.Subunits.FirstOrDefaultAsync(x => x.SubunitName == subunit);
            if (existingSubunit == null)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> AddSubunit(AddSubunitDto subunit)
        {
            var addsubunit = new Subunit
            {
                SubunitName = subunit.SubunitName,
                CreatedBy = subunit.CreatedBy,
                UnitId = subunit.UnitId,
                CreatedAt = DateTime.Now,
            };

            await _context.Subunits.AddAsync(addsubunit);


            return true;
        }

        public async Task<bool> UpdateSubunit(UpdateSubunitDto subunit)
        {
            var updatesubunit = await _context.Subunits.FirstOrDefaultAsync(d => d.Id == subunit.Id);
            if (updatesubunit != null)
            {
                updatesubunit.SubunitName = subunit.SubunitName;
                updatesubunit.UnitId = subunit.UnitId;
                updatesubunit.EditedBy = subunit.EditedBy;
                updatesubunit.EditedAt = subunit.EditedAt;
                await _context.SaveChangesAsync();
                return true;
            };
            return false;
        }


        public async Task<bool> SetIsActive(int Id)
        {
            var setIsactive = await _context.Subunits.FirstOrDefaultAsync(x => x.Id == Id);
            if (setIsactive != null)
            {
                setIsactive.IsActive = !setIsactive.IsActive;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<PagedList<GetSubunitDto>> SubunitListPagination(UserParams userParams, bool? status, string search)
        {

            var result = _context.Subunits.Select(x => new GetSubunitDto
            {
                Id = x.Id,
                SubunitName = x.SubunitName,
                UnitId = x.UnitId,
                CreatedAt = DateTime.Now,
                IsActive = x.IsActive,
                EditedBy = x.EditedBy,

            });

            if (status != null)
            {
                result = result.Where(x => x.IsActive == status);
            }

            if (!string.IsNullOrEmpty(search))
            {
                result = result.Where(x => Convert.ToString(x.Id).ToLower().Contains(search.Trim().ToLower())
                || Convert.ToString(x.SubunitName).ToLower().Contains(search.Trim().ToLower()));
            }


            return await PagedList<GetSubunitDto>.CreateAsync(result, userParams.PageNumber, userParams.PageSize);


        }
    }
}
