namespace API_Sicily.Service
{
    public class PasswordHash
    {
        // Méthode pour hacher un mot de passe lors de l'inscription (pas utilisée pour l'instant)
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        // Méthode pour vérifier un mot de passe lors du login
        public static bool VerifyPassword(string enteredPassword, string storedHashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(enteredPassword, storedHashedPassword);
        }
    }
}
