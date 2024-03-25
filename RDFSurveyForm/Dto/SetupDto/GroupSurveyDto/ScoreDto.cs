namespace RDFSurveyForm.Dto.SetupDto.GroupSurveyDto
{
    public class ScoreDto
    {
        public int? SurveyGeneratorId { get; set; }


            public int? Id { get; set; }
            public string CategoryName { get; set; }
            public decimal Score { get; set; }
            public decimal Limit { get; set; }
            public decimal CategoryPercentage { get; set; }
            public decimal SurveyPercentage { get; set; }
            public decimal ActualScore { get; set; }
        
    }
}
