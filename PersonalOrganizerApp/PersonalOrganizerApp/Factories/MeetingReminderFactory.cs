using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalOrganizerApp.ReminderClasses
{
    // Concrete factory responsible for creating MeetingReminder instances.
    // Implements the IReminderFactory interface as part of the Abstract Factory pattern.
    // This allows the system to generate meeting-type reminders in a decoupled and extensible manner.
    public class MeetingReminderFactory : IReminderFactory
    {

        // Creates and returns a new instance of MeetingReminder.
        public IReminder CreateReminder()
        {
            return new MeetingReminder();
        }
    }
}
