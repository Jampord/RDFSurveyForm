namespace RDFSurveyForm.Model.Unit_SubUnit
{
    public class Subunit
    {
        public int Id { get; set; }
        public string SubunitName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
        public string EditedBy { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
        public DateTime? EditedAt { get; set; }
        public int? UnitId { get; set; }
        public virtual Unit Unit { get; set; }
    }
}
