namespace RDFSurveyForm.Dto.SetupDto.QuestionsDto
{
    public class AddQuestionsDto
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public int? CategoryId { get; set; }
    }
}
