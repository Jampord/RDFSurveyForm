using Microsoft.EntityFrameworkCore;
using RDFSurveyForm.Data;
using RDFSurveyForm.DATA_ACCESS_LAYER.HELPERS;
using RDFSurveyForm.Dto.ModelDto.DepartmentDto;
using RDFSurveyForm.Dto.Unit_SubUnitDto;
using RDFSurveyForm.Model;
using RDFSurveyForm.Model.Unit_SubUnit;

namespace RDFSurveyForm.DataAccessLayer.IR_Unit_Subunit.Repository
{
    public class UnitRepository
    {
        private readonly StoreContext _context;

        public UnitRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistingUnit(string unit)
        {
            var existingUnit = await _context.Units.FirstOrDefaultAsync(x => x.UnitName == unit);
            if (existingUnit == null)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> AddUnit(AddUnitDto unit)
        {
            var addunit = new Unit
            {
                UnitName = unit.UnitName,
                CreatedAt = DateTime.Now,
            };

            await _context.Units.AddAsync(addunit);


            return true;
        }

        public async Task<bool> UpdateUnit(UpdateUnitDto unit)
        {
            var updateunit = await _context.Units.FirstOrDefaultAsync(d => d.Id == unit.Id);
            if (updateunit != null)
            {
                updateunit.UnitName = unit.UnitName;
                updateunit.EditedBy = unit.EditedBy;
                updateunit.EditedAt = unit.EditedAt;
                await _context.SaveChangesAsync();
                return true;
            };
            return false;
        }


        public async Task<bool> SetIsActive(int Id)
        {
            var setIsactive = await _context.Units.FirstOrDefaultAsync(x => x.Id == Id);
            if (setIsactive != null)
            {
                setIsactive.IsActive = !setIsactive.IsActive;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<PagedList<GetUnitDto>> UnitListPagination(UserParams userParams, bool? status, string search)
        {

            var result = _context.Units.Select(x => new GetUnitDto
            {
                Id = x.Id,
                UnitName = x.UnitName,
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
                || Convert.ToString(x.UnitName).ToLower().Contains(search.Trim().ToLower()));
            }


            return await PagedList<GetUnitDto>.CreateAsync(result, userParams.PageNumber, userParams.PageSize);


        }
    }
}
