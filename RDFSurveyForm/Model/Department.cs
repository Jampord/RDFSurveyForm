namespace RDFSurveyForm.Model
{
    public class Department
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; } = true;
        public string EditedBy { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public DateTime? EditedAt { get; set; }
        public DateTime SyncDate { get; set; }
        public string StatusSync { get; set; }
        public int? DepartmentNo { get; set; }
    }
}
