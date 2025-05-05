using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalOrganizerApp.ReminderClasses
{
    // Represents a task-type reminder.
    // Inherits from ReminderBase and overrides its behavior specific to tasks.
    public class TaskReminder : ReminderBase
    {
        // Specifies the type of the reminder. Default is Task.
        public override ReminderType Type { get; set; } = ReminderType.Task;

        // Executes the custom notification logic for a task.
        public override void Notify()
        {
            // Sets the console title to a formatted message indicating a task reminder.
            string message = $"[TASK] {Summary}";
            Console.Title = message;
        }
    }
}