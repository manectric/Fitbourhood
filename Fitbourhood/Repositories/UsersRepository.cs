using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Dapper;
using Fitbourhood.Models;

namespace Fitbourhood.Repositories
{
    public static class UsersRepository
    {
        public static string ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB";
        public static List<string> ErrorList = new List<string>();
        public static bool AddUser(User user)
        {
            ErrorList = new List<string>();
            bool result = false;
            if (user != null)
            {
                if(string.IsNullOrWhiteSpace(user.Name))
                    ErrorList.Add("Pole Imię nie może być puste");
                if(string.IsNullOrWhiteSpace(user.Password))
                    ErrorList.Add("Pole Hasło nie może być puste");
                if (string.IsNullOrWhiteSpace(user.PhoneNumber))
                    ErrorList.Add("Pole Numer telefonu nie może być puste");
                if (string.IsNullOrWhiteSpace(user.PasswordRepeat))
                    ErrorList.Add("Pole Powtórz hasło nie może być puste");
                if (string.IsNullOrWhiteSpace(user.Email))
                    ErrorList.Add("Pole E-mail nie może być puste");

                if (!string.IsNullOrWhiteSpace(user.Name) && !string.IsNullOrWhiteSpace(user.Password) &&
                    !string.IsNullOrWhiteSpace(user.PhoneNumber) && !string.IsNullOrWhiteSpace(user.Email))
                {
                    string sqlUserInsert = "INSERT INTO [FitbourhoodDB].[dbo].[Users] (Name, Password, PhoneNumber, Email, Description) Values (@Name, @Password, @PhoneNumber, @Email, @Description)";
                    string sqlSelectUserByEmail = "SELECT * FROM [FitbourhoodDB].[dbo].[Users] WHERE Email = @Email";
                    int affectedRows = 0;
                    using (var connection = new SqlConnection(ConnectionString))
                    {
                        var selectedRows = connection.QueryFirstOrDefault(sqlSelectUserByEmail, new { user.Email });
                        if(selectedRows != null)
                            ErrorList.Add("Podany adres E-Mail jest już zajęty");
                        else
                            affectedRows = connection.Execute(sqlUserInsert, new { user.Name, Password = GenerateHash(user.Password), user.PhoneNumber, user.Email, Description = "" });
                    }
                    if (affectedRows > 0)
                        result = true;
                }
            }

            return result;
        }

        private static string GenerateHash(string password)
        {
            MD5CryptoServiceProvider algorithm = new MD5CryptoServiceProvider();
            Byte[] inputBytes = Encoding.UTF8.GetBytes(password);
            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);

            return BitConverter.ToString(hashedBytes);
        }

        public static bool ValidatePasswordIsCorrect(string email, string password)
        {
            ErrorList = new List<string>();
            bool result = false;

            string sqlSelectUserByEmail = "SELECT * FROM [FitbourhoodDB].[dbo].[Users] WHERE Email = @Email";
            int affectedRows = 0;
            using (var connection = new SqlConnection(ConnectionString))
            {
                var selectedRows = connection.QueryFirstOrDefault<User>(sqlSelectUserByEmail, new { email });
                if (selectedRows != null)
                {
                    string hashedPassword = GenerateHash(password);
                    string hashedPasswordFromDB = selectedRows.Password;
                    if (hashedPassword == hashedPasswordFromDB)
                    {
                        result = true;
                    }
                }
            }
            if(!result)
                ErrorList.Add("Niepoprawny adres E-mail lub hasło");
            
            return result;
        }
    }
}