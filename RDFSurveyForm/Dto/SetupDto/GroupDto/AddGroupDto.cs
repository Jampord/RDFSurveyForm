namespace RDFSurveyForm.Dto.SetupDto.GroupDto
{
    public class AddGroupDto
    {

        public string GroupName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public int? BranchId { get; set; }
    }
}
