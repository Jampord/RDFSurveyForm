namespace RDFSurveyForm.Dto.ModelDto.RoleDto
{
    public class GetRoleDto
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
        public ICollection<string> Permission { get; set; }
        public string EditedBy { get; set; }

    }
}
