namespace RDFSurveyForm.Model
{
    public class Role
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; } = true;
        public  ICollection<string> Permission {  get; set; }  
        public virtual ICollection<User> Users { get; set; }
        public string EditedBy { get; set; }

    }
}
