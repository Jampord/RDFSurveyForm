namespace RDFSurveyForm.Dto.SetupDto.GroupDto
{
    public class UpdateGroupDto
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public int? BranchId { get; set; }
    }
}
