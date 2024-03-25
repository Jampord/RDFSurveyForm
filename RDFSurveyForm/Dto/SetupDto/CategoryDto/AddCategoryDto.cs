namespace RDFSurveyForm.Dto.SetupDto.CategoryDto
{
    public class AddCategoryDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public decimal CategoryPercentage { get; set; }
        public decimal Limit { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }

    }
}
