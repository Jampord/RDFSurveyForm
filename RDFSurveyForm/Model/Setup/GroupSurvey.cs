namespace RDFSurveyForm.Model.Setup
{
    public class GroupSurvey
    {
        public int Id { get; set; }
        public int? GroupsId { get; set; }
        public virtual Groups Groups { get; set; }
        public int? SurveyGeneratorId { get; set; }
        public virtual SurveyGenerator SurveyGenerator { get; set; }
        public bool IsActive { get; set; } = true;
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; } 
        public bool IsTransacted { get; set; }



    }
}
