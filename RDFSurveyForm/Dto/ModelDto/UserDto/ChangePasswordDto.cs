namespace RDFSurveyForm.Dto.ModelDto.UserDto
{
    public class ChangePasswordDto
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
