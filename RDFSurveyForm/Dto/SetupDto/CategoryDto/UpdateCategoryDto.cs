namespace RDFSurveyForm.Dto.SetupDto.CategoryDto
{
    public class UpdateCategoryDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public decimal CategoryPercentage { get; set; }
        public decimal Limit {  get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }
}
