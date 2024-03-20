namespace RDFSurveyForm.Dto.SetupDto.GroupSurveyDto
{
    public class GetSurveyGeneratorIdDto
    {
        public int? SurveyGeneratorId { get; set; }
        public string BranchName { get; set; }
        public string GroupsName { get; set; }        
        public bool IsTransacted { get; set; }
        public decimal FinalScore { get; set; }
    }
}
