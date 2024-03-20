

namespace RDFSurveyForm.Dto.SetupDto.GroupSurveyDto
{
    public class UpdateSurveyScoreDto
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
