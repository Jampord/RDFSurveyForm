

namespace RDFSurveyForm.Dto.SetupDto.GroupSurveyDto
{
    public class GetGroupSurveyDto
    {
        public int? SurveyGeneratorId { get; set; }
       
        public string BranchName { get; set; }
        public string GroupName { get; set; }       
        public bool IsTransacted { get; set; }
        public decimal FinalScore { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        
    }
}
