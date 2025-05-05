using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalOrganizerApp.ReminderClasses
{
    // Factory class for creating TaskReminder instances.
    public class TaskReminderFactory : IReminderFactory
    {
        // Creates and returns a new instance of TaskReminder.
        public IReminder CreateReminder()
        {
            // Here we can add any specific initialization logic for TaskReminder if needed
            return new TaskReminder();
        }
    }
}

