namespace RDFSurveyForm.Dto.ModelDto.DepartmentDto
{
    public class UpdateDepartmentDto
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public int? DepartmentNo { get; set; }
        public string EditedBy { get; set; }
        public DateTime EditedAt { get; set; }
    }
}
