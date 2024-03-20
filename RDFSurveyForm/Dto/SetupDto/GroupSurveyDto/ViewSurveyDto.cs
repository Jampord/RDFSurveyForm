namespace RDFSurveyForm.Dto.SetupDto.GroupSurveyDto
{
    public class ViewSurveyDto
    {
        public int ? SurveyGeneratorId { get; set; }

        public List<Category> Categories { get; set; }
        public class Category
        {
            public string CategoryName { get; set; }
            public int Score { get; set; }
            public int Limit { get; set; }
            public decimal CategoryPercentage { get; set; }
            public decimal SurveyPercentage { get; set; }
        }

    }
}
