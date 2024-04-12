namespace RDFSurveyForm.Model
{
    public class Role
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
        public  ICollection<string> Permission {  get; set; }  
        public virtual ICollection<User> Users { get; set; }
        public string EditedBy { get; set; }
        public DateTime EditedAt { get; set; } 

    }
}
