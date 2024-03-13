namespace RDFSurveyForm.Dto.ModelDto.RoleDto
{
    public class AddRoleDto
    {
        public int Id { get; set; }
        public string RoleName { get; set; }

        public DateTime CreatedAt { get; set; }

        public ICollection<string> Permission { get; set; }
    }
}
