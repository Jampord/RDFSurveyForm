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
    }
}
