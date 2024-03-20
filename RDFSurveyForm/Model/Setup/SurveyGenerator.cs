namespace RDFSurveyForm.Model.Setup
{
    public class SurveyGenerator
    {
        public int Id { get; set; }
        public bool IsActive { get; set; } = true;
       
        public ICollection<GroupSurvey> GroupSurveys { get; set; }
        public ICollection<SurveyScore> SurveyScores { get; set; }
    }
}
