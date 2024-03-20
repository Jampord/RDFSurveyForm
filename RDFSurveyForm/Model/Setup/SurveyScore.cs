

namespace RDFSurveyForm.Model.Setup
{
    public class SurveyScore
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public decimal CategoryPercentage { get; set; }
        public int Score { get; set; }
        public int Limit { get; set; }
        public int? SurveyGeneratorId { get; set; }
 
        public virtual SurveyGenerator SurveyGenerator { get; set; }
        public bool IsActive { get; set; } = true;
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }    
        
    }
}
