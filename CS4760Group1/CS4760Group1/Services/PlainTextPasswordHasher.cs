using Microsoft.AspNetCore.Identity;

namespace CS4760Group1.Services
{
    public class PlainTextPasswordHasher<TUser> : IPasswordHasher<TUser> where TUser : class
    {
        public string HashPassword(TUser user, string password)
        {
            return password;
        }

        public PasswordVerificationResult VerifyHashedPassword(TUser user, string hashedPassword, string providedPassword)
        {
            if (hashedPassword == providedPassword)
            {
                return PasswordVerificationResult.Success;
            }

            return PasswordVerificationResult.Failed;
        }
    }
}

