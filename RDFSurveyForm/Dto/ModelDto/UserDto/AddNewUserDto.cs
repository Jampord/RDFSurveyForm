namespace RDFSurveyForm.Dto.ModelDto.UserDto
{
    public class AddNewUserDto
    {

        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int? GroupsId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? RoleId { get; set; }
        public int? DepartmentId { get; set; }
    }
}
