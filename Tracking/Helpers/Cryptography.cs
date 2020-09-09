using BCryptInstance = BCrypt.Net.BCrypt;

namespace Tracking.Helpers
{
    public class Cryptography
    {
        private static string mySalt = "J@H!";
        public static string encryptPassword(string password)
        {
            string bcyptGeneratedSalt = BCryptInstance.GenerateSalt(7);

            string encryptedPassword = BCryptInstance.HashPassword(password + mySalt, bcyptGeneratedSalt);

            return encryptedPassword;

        }



        public static bool verifyPassword(string hashedPwdFromDatabase, string userEnteredPassword)
        {
            return BCryptInstance.Verify(userEnteredPassword + mySalt, hashedPwdFromDatabase);

        }

    }
}
