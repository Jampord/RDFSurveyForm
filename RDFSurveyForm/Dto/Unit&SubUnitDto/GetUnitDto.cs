using RDFSurveyForm.Model;

namespace RDFSurveyForm.Dto.Unit_SubUnitDto
{
    public class GetUnitDto
    {
        public int Id { get; set; }
        public string UnitName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
        public string EditedBy { get; set; }
        public DateTime? EditedAt { get; set; }
    }
}
