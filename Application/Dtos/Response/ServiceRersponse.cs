namespace Application.Dtos.Responses
{
    public class ServiceRersponse
    {
        public record class LoginResponse(bool Success = false , string[] Message = null ,
            string Token = null );
        public record class RegisterResponse(bool Success = false ,string[] Message = null );
    }
}
