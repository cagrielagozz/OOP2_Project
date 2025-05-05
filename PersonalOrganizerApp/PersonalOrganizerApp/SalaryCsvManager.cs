using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PersonalOrganizerApp
{
    internal static class SalaryCsvManager
    {
        private static readonly string filePath = "user_salary_data.csv";

        // CSV başlığını oluşturur (sadece bir kez çalışır)
        public static void InitializeCsv()
        {
            if (!File.Exists(filePath))
            {
                string headers = "Username,Experience,City,Education,ManagementRole,EnglishCert,EnglishEdu,OtherLang,SpouseWorks,Child0_6,Child7_18,ChildOver18,CalculatedSalary,Date";
                File.WriteAllText(filePath, headers + Environment.NewLine);
            }
        }

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
            List<string> allLines = new List<string>();

            if (File.Exists(filePath))
                allLines = File.ReadAllLines(filePath).ToList();

            // Başlık satırı varsa koru
            string header = "Username,Experience,City,Education,ManagementRole,EnglishCert,EnglishEdu,OtherLang,SpouseWorks,Child0_6,Child7_18,ChildOver18,CalculatedSalary,Date";

            // Varsa eski satırı sil
            allLines = allLines.Where(line => !line.StartsWith(username + ",")).ToList();

            // Güncel satırı oluştur
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

            // Header satırını başa ekle (eğer eksikse)
            if (allLines.Count == 0 || !allLines[0].StartsWith("Username"))
                allLines.Insert(0, header);

            // Yeni satırı sona ekle
            allLines.Add(newLine);

            // Dosyaya yaz
            File.WriteAllLines(filePath, allLines);
        }
        public static string[] LoadUserData(string username)
        {
            if (!File.Exists(filePath)) return null;

            string[] allLines = File.ReadAllLines(filePath);
            for (int i = 1; i < allLines.Length; i++) // 0. satır başlık
            {
                string[] parts = allLines[i].Split(',');
                if (parts.Length >= 14 && parts[0] == username)
                {
                    return parts;
                }
            }
            return null;
        }

    }
}
