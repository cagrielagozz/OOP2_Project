using PersonalOrganizerApp.Models;
using PersonalOrganizerApp.ReminderClasses;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PersonalOrganizerApp
{
    public class Util
    {
        //Asla bir nesne üretilmesini istemiyoruz ortak bir class 
        private Util() { }
        public static string ComputeStringToSha256Hash(string plainText)
        {
            // Create a SHA256 hash from string   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Computing Hash - returns here byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(plainText));
                // now convert byte array to a string   
                StringBuilder stringbuilder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    stringbuilder.Append(bytes[i].ToString("x2"));
                }
                return stringbuilder.ToString();
            }
        }
        private readonly static string EMAIL_PATTERN = @"^[A-Z0-9._%+-]+@[A-Z0-9.-]+.(com|org|net|edu|gov|mil|biz|info|mobi)(.[A-Z]{2})?$";
        public static bool isEmailValid(string emailInput)
        {
            Regex regex = new Regex(EMAIL_PATTERN, RegexOptions.IgnoreCase);
            return regex.IsMatch(emailInput);
        }
        public static string ImageToBase64(string path)
        {
            try
            {
                byte[] imageArray = System.IO.File.ReadAllBytes(path);
                string base64String = Convert.ToBase64String(imageArray);
                return base64String;
            }
            catch (Exception)
            {
                return null;
            }

        }
        public static Image Base64ToImage(string base64String)
        {
            // Convert base 64 string to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            // Convert byte[] to Image
            using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                Image image = Image.FromStream(ms, true);
                return image;
            }
        }
        
        
        
        public static void LoadCsv(List<User> userList, string path)
        {
            userList.Clear();
            try
            {
                using (var reader = new StreamReader(path))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        var values = line.Split(',');
                        string username = values[0];
                        string password = values[1];
                        bool rememberme = (values[2]).Equals("1") ? true : false;
                        string usertypes = values[3];
                        string name = values[4];
                        string surname = values[5];
                        string phonenumber = values[6];
                        string address = values[7];
                        if (address.Contains("#"))
                        {
                            address = address.Replace("#", ",");
                        }
                        string email = values[8];
                        string photo = values[9];
                        string salary = values[10];
                        userList.Add(new User
                        {
                            Username = username,
                            Password = password,
                            Name = name,
                            Surname = surname,
                            PhoneNumber = phonenumber,
                            Address = address,
                            Email = email,
                            Base64Photo = photo,
                            RememberMe = rememberme,
                            UserTypes = usertypes

                        });
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        public static void SaveCsv(List<User> userList, string path)
        {
            using (var writer = new StreamWriter(path))
            {
                foreach (User user in userList)
                {
                    if (user.Address.Contains(","))
                    {
                        user.Address = user.Address.Replace(",", "#");
                    }
                    writer.WriteLine($"{user.Username},{user.Password},{(user.RememberMe ? "1" : "0")},{user.UserTypes},{user.Name},{user.Surname},{user.PhoneNumber},{user.Address},{user.Email},{user.Base64Photo}");
                }
            }
        }
        
    }
}