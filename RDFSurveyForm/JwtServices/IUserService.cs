using RDFSurveyForm.JWT.AUTHENTICATION;

namespace RDFSurveyForm.JwtServices
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest request);
    }
}
