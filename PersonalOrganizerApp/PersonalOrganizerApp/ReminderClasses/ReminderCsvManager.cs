using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using PersonalOrganizerApp.ReminderClasses;

namespace PersonalOrganizerApp.ReminderClasses
{
    internal class ReminderCsvManager
    {
        private static readonly string filePath = "reminder.csv";

        // CSV'ye yeni bir reminder ekler (ekleme işlemi için)
        public static void SaveReminder(string username, IReminder reminder)
        {
            string csvLine = string.Join(",",
                username,
                reminder.Type,
                reminder.Title,
                reminder.Description,
                reminder.Summary,
                reminder.Date.ToShortDateString(),
                reminder.Time.ToString(@"hh\:mm"));

            File.AppendAllText(filePath, csvLine + Environment.NewLine);
        }

        // Belirli kullanıcıya ait tüm reminder'ları yükler
        public static List<IReminder> LoadReminders(string username)
        {
            var reminders = new List<IReminder>();

            if (!File.Exists(filePath))
                return reminders;

            var lines = File.ReadAllLines(filePath); // Başlık yoksa Skip(1) gereksiz

            foreach (var line in lines)
            {
                var parts = line.Split(',');

                if (parts.Length < 7 || parts[0] != username)
                    continue;

                ReminderType type = (ReminderType)Enum.Parse(typeof(ReminderType), parts[1]);

                IReminderFactory factory;
                if (type == ReminderType.Meeting)
                    factory = new MeetingReminderFactory();
                else
                    factory = new TaskReminderFactory();

                IReminder reminder = factory.CreateReminder();

                reminder.Title = parts[2];
                reminder.Description = parts[3];
                reminder.Summary = parts[4];
                reminder.Date = DateTime.Parse(parts[5]);
                reminder.Time = TimeSpan.Parse(parts[6]);

                reminders.Add(reminder);
            }

            return reminders;
        }

        // Tüm kullanıcıya ait reminder listesini dosyaya yazar (güncelleme ve silme sonrası)
        public static void SaveAllReminders(List<IReminder> updatedList, string username)
        {
            var allLines = File.Exists(filePath)
                ? File.ReadAllLines(filePath).ToList()
                : new List<string>();

            // Eski kullanıcı satırlarını sil
            allLines = allLines.Where(line => line.Split(',')[0] != username).ToList();

            // Yeni listeyi satır satır ekle
            foreach (var reminder in updatedList)
            {
                string line = string.Join(",",
                    username,
                    reminder.Type,
                    reminder.Title,
                    reminder.Description,
                    reminder.Summary,
                    reminder.Date.ToShortDateString(),
                    reminder.Time.ToString(@"hh\:mm"));

                allLines.Add(line);
            }

            // Dosyaya tüm satırları tekrar yaz
            File.WriteAllLines(filePath, allLines);
        }
    }
}