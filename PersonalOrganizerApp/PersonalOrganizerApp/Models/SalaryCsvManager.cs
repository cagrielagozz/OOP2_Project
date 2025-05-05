using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PersonalOrganizerApp
{
    internal static class SalaryCsvManager
    {
        // Path to the CSV file
        private static readonly string filePath = "user_salary_data.csv";

        // Create the CSV file if it doesn't exist
        public static void InitializeCsv()
        {
            if (!File.Exists(filePath))
            {
                string headers = "Username,Experience,City,Education,ManagementRole,EnglishCert,EnglishEdu,OtherLang,SpouseWorks,Child0_6,Child7_18,ChildOver18,CalculatedSalary,Date";
                File.WriteAllText(filePath, headers + Environment.NewLine);
            }
        }

        // Save user salary data to the CSV file
        public static void SaveUserSalaryData(
            string username,
            string experience,
            string city,
            string education,
            string managementRole,
            bool englishCert,
            bool englishEdu,
            bool otherLang,
            bool spouseWorks,
            bool child0_6,
            bool child7_18,
            bool childOver18,
            double calculatedSalary
 )
        {
            // Initialize the CSV file if it doesn't exist
            List<string> allLines = new List<string>();

            if (File.Exists(filePath))
                allLines = File.ReadAllLines(filePath).ToList();

            // If the file is empty, create a new one with headers
            string header = "Username,Experience,City,Education,ManagementRole,EnglishCert,EnglishEdu,OtherLang,SpouseWorks,Child0_6,Child7_18,ChildOver18,CalculatedSalary,Date";

            // If the file is empty, add headers
            allLines = allLines.Where(line => !line.StartsWith(username + ",")).ToList();

            // Create a new line with the user data
            string newLine = string.Join(",",
                username,
                experience,
                city,
                education,
                managementRole,
                englishCert,
                englishEdu,
                otherLang,
                spouseWorks,
                child0_6,
                child7_18,
                childOver18,
                calculatedSalary.ToString("F2"),
                DateTime.Now.ToString("yyyy-MM-dd")
            );

            // Add the header line to the beginning (if missing)
            if (allLines.Count == 0 || !allLines[0].StartsWith("Username"))
                allLines.Insert(0, header);

            // Add newline to the end
            allLines.Add(newLine);

            // Write all lines back to the file
            File.WriteAllLines(filePath, allLines);
        }
        public static string[] LoadUserData(string username)
        {
            if (!File.Exists(filePath)) return null;

            string[] allLines = File.ReadAllLines(filePath);
            for (int i = 1; i < allLines.Length; i++)
            {
                string[] parts = allLines[i].Split(',');
                if (parts.Length >= 14 && parts[0] == username)
                {
                    return parts;
                }
            }
            return null; // User not found
        }

    }
}
