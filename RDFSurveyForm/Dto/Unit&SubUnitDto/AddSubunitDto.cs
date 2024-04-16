using RDFSurveyForm.Model;

namespace RDFSurveyForm.Dto.Unit_SubUnitDto
{
    public class AddSubunitDto
    {
        public int Id { get; set; }
        public string SubunitName { get; set; }
        public int? UnitId { get; set; }
        public string CreatedBy { get; set; }


    }
}
