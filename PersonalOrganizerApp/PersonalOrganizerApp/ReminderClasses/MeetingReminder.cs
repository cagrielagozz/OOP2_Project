using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalOrganizerApp.ReminderClasses
{
    // Represents a concrete reminder type for meetings.
    // Inherits shared behavior from ReminderBase and defines custom notification logic.
    // Used in the Abstract Factory pattern to create meeting-specific reminders.
    public class MeetingReminder : ReminderBase
    {

        // Specifies the type of this reminder as 'Meeting'.
        public override ReminderType Type { get; set; } = ReminderType.Meeting;

        // Executes the custom notification logic for a meeting.
        // For now, it sets the Console window title to a formatted message.
        // Observer behavior (e.g., shaking form, UI title update) will be added separately.
        public override void Notify()
        {
            // Specifies the type of this reminder as 'Meeting'.
            string message = $"[MEETING] {Summary}";
            Console.Title = message;
        }
    }
}
