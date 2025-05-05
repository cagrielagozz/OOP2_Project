using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalOrganizerApp.ReminderClasses
{
    // Factory interface used to create reminder objects.
    // This is part of the Abstract Factory design pattern.
    // Each concrete factory will produce a specific type of reminder
    // (e.g., MeetingReminder or TaskReminder).
    public interface IReminderFactory
    {
        // Creates and returns an instance of a concrete IReminder.
        IReminder CreateReminder();
    }
}
