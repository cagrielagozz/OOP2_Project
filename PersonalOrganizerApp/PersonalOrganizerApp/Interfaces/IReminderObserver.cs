using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PersonalOrganizerApp.ReminderClasses
{

    // Observer interface for receiving updates from a reminder subject.
    // Implements the Observer design pattern to decouple reminder logic from the UI layer.
    public interface IReminderObserver
    {
        // Called when a reminder state is updated or triggered.
        // This allows observers (e.g., forms or components) to react to reminder changes.
        void Update(IReminder reminder);

        // Called when a reminder is triggered, passing the form and reminder details.
        void Notify(Form form, IReminder reminder);


    }
}

