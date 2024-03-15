namespace RDFSurveyForm.Dto.SetupDto.CategoryDto
{
    public class GetCategoryDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public decimal CategoryPercentage { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }

    }
}
