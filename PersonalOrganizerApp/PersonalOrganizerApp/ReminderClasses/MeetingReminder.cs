using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalOrganizerApp.ReminderClasses
{
    public class MeetingReminder : ReminderBase
    {
        public override ReminderType Type { get; set; } = ReminderType.Meeting;

        public override void Notify()
        {
            // Meeting için özel bildirim davranışı
            string message = $"[MEETING] {Summary}";
            Console.Title = message;

            // Active form'u salla ve başlığı değiştir (Observer için eklenecek)
        }
    }
}
