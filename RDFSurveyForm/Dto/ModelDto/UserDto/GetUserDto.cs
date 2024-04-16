namespace RDFSurveyForm.Dto.ModelDto.UserDto
{
    public class GetUserDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string CreatedBy { get; set; }
        public int? RoleId { get; set; }
        public string RoleName { get; set; }
        public int? GroupsId { get; set; }
        public string GroupName { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool InActive { get; set; }
        public string EditedBy { get; set; }
        public int? DepartmentId { get; set; }
        public string DepartmentName { get; set; }

    }
}
