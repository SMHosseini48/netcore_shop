namespace ncorep.Dtos;

public class UserLoginResponseDto : AuthenticationResultDto
{
    public string Token { get; set; }
}