using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalOrganizerApp.ReminderClasses
{
    public class MeetingReminderFactory : IReminderFactory
    {
        public IReminder CreateReminder()
        {
            return new MeetingReminder();
        }
    }
}
