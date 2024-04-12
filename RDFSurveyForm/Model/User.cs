using RDFSurveyForm.Model.Unit_SubUnit;

namespace RDFSurveyForm.Model
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; }
        public int? RoleId { get; set; }
        public virtual Role Role { get; set; }
        public int? DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        public string EditedBy { get; set; }
        public bool UpdatePass { get; set; } = false;
        public DateTime EditedAt { get; set; }
        public  int? UnitId { get; set; }
        public int? SubunitId { get; set; }
        public virtual Unit Unit { get; set; }
        public virtual Subunit Subunit { get; set; }

    }
}
