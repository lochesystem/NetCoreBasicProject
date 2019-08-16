using Microsoft.IdentityModel.Tokens;

namespace BasicProject.API.Security
{
    public class SigningConfigurations
    {
        const string sec = "jrphnaolsg48k8cq3c5ln04rhv7oxzok9qt8d0uy3ow1bxsx4udbt87wmdk8wbvzojaog713o1qtpzaqj5wqx12v9nuamm9l1gjtgllos5cyu8rq8ozzikn272yuyfeh";

        public SecurityKey Key { get; }
        public SigningCredentials SigningCredentials { get; }

        public SigningConfigurations()
        {

            Key = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(sec));
            SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256Signature);

        }
    }
}
