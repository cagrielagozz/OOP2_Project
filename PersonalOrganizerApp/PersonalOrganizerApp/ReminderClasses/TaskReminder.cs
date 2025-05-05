using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalOrganizerApp.ReminderClasses
{
    public class TaskReminder : ReminderBase
    {
        public override ReminderType Type { get; set; } = ReminderType.Task;

        public override void Notify()
        {
            // Task için özel bildirim davranışı
            string message = $"[TASK] {Summary}";
            Console.Title = message;

            // Active form'u salla ve başlığı değiştir (Observer tarafından yapılacak)
        }
    }
}