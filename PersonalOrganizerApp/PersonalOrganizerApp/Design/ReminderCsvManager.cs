using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using PersonalOrganizerApp.ReminderClasses;

namespace PersonalOrganizerApp.ReminderClasses
{
    /// Manages the loading and saving of reminders to a CSV file.
    internal class ReminderCsvManager
    {
        private static readonly string filePath = "reminder.csv";

        // Creates the CSV file if it doesn't exist
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

        // Loads reminders from the CSV file for a specific user
        public static List<IReminder> LoadReminders(string username)
        {
            var reminders = new List<IReminder>();

            if (!File.Exists(filePath))
                return reminders;

            var lines = File.ReadAllLines(filePath);

            foreach (var line in lines)
            {
                var parts = line.Split(',');

                if (parts.Length < 7 || parts[0] != username)
                    continue;

                // Parse the reminder type
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

        // Saves all reminders for a specific user to the CSV file
        public static void SaveAllReminders(List<IReminder> updatedList, string username)
        {
            var allLines = File.Exists(filePath)
                ? File.ReadAllLines(filePath).ToList()
                : new List<string>();

            // Remove existing lines for the user
            allLines = allLines.Where(line => line.Split(',')[0] != username).ToList();

            // Create new lines for the updated list
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

            // Write all lines back to the file
            File.WriteAllLines(filePath, allLines);
        }
    }
}