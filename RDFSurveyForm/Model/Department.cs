using RDFSurveyForm.Model.Unit_SubUnit;

namespace RDFSurveyForm.Model
{
    public class Department
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
        public string EditedBy { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public DateTime? EditedAt { get; set; } 
        public DateTime SyncDate { get; set; }
        public string StatusSync { get; set; }
        public int? DepartmentNo { get; set; }
        public int? UnitId { get; set; }
        public int? SubunitId { get; set; }
        public virtual Unit Unit { get; set; }
        public virtual Subunit Subunit { get; set; }
    }
}
