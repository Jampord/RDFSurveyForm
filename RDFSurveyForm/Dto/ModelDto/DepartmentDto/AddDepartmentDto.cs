namespace RDFSurveyForm.Dto.ModelDto.DepartmentDto
{
    public class AddDepartmentDto
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ? DepartmentNo { get; set; }
        public bool IsActive { get; set; }
        public string StatusSync { get; set; }
        
    }
}
