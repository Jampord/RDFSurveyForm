using RDFSurveyForm.Model;

namespace RDFSurveyForm.Dto.Unit_SubUnitDto
{
    public class UpdateUnitDto
    {
        public int Id { get; set; }
        public string UnitName { get; set; }
        public string EditedBy { get; set; }
        public DateTime? EditedAt { get; set; } = DateTime.Now;
    }
}
