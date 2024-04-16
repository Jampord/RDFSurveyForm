namespace RDFSurveyForm.Dto.ModelDto.DepartmentDto
{
    public class GetDepartmentDto
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public int? DepartmentNo { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public string EditedBy { get; set; }
    }
}
