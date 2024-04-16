namespace RDFSurveyForm.Dto.ModelDto.UserDto
{
    public class UpdateUserDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public int? RoleId { get; set; }
        public int? GroupsId { get; set; }
        public string EditedBy { get; set; }
        public int? DepartmentId { get; set; }
    }
}
