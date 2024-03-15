namespace RDFSurveyForm.Dto.SetupDto.QuestionsDto
{
    public class UpdateQuestionsDto
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public int? CategoryId { get; set; }
    }
}
