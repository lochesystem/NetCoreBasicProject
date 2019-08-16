using BasicProject.Service.DTOs.Login;

namespace BasicProject.Service.Interface
{
    public interface IServiceLogin
    {
        /// <summary>
        ///  Auth user
        /// </summary>
        bool Auth(LoginRequestDto login);
    }
}
