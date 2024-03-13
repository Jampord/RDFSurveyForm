namespace RDFSurveyForm.Dto.ModelDto.RoleDto
{
    public class UpdateRoleDto
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public ICollection<string> Permission { get; set; }
        public string EditedBy { get; set; }

    }
}
