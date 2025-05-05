using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PersonalOrganizerApp.ReminderClasses
{
    public interface IReminderObserver
    {
        void Update(IReminder reminder);

        void Notify(Form form, IReminder reminder);


    }
}

