namespace RDFSurveyForm.Dto.SetupDto.BranchDto
{
    public class UpdateBranchDto
    {
        public int Id { get; set; }
        public string BranchName { get; set; }
        public string BranchCode { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }
}
