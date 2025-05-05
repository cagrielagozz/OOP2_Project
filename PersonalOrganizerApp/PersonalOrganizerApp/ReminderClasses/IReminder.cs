using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalOrganizerApp.ReminderClasses
{

    // Interface for all reminder types (e.g., Meeting, Task).
    // Defines the common properties and behavior expected from a reminder.
    // This interface is part of the Abstract Factory pattern for reminder creation.
    public interface IReminder
    {
        // Title of the reminder, used as a brief identifier.
        string Title { get; set; }

        // Detailed description of the reminder content.
        string Description { get; set; }

        // Summary of the reminder, typically a short note or highlight.
        string Summary { get; set; }

        // Date and time when the reminder is set to occur.
        DateTime Date { get; set; }

        // Time of day when the reminder is set to occur.
        TimeSpan Time { get; set; }

        // Type of the reminder, indicating its category (e.g., Meeting, Task).
        ReminderType Type { get; set; }

        // Method to notify the user about the reminder.
        void Notify();
    }
}
