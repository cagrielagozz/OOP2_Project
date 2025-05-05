using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalOrganizerApp.ReminderClasses
{
    // Represents the types of reminders that can be created.
    // Used to distinguish between different reminder categories.
    public enum ReminderType
    {
        // A reminder related to a scheduled meeting or event.
        Meeting,
        // A reminder related to a task or to-do item.
        Task
    }
}

