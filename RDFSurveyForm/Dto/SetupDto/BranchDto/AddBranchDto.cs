namespace RDFSurveyForm.Dto.SetupDto.BranchDto
{
    public class AddBranchDto
    {
        public int Id { get; set; }
        public string BranchName { get; set; }
        public string BranchDescription { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }

    }
}
