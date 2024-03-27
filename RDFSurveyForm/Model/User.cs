namespace RDFSurveyForm.Model
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public int? RoleId { get; set; }
        public virtual Role Role { get; set; }
        public int? DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        public string EditedBy { get; set; }
        public bool UpdatePass { get; set; } = false;

    }
}
