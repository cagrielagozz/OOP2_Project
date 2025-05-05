using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using PersonalOrganizerApp.ReminderClasses;


namespace PersonalOrganizerApp.ReminderClasses
{
    // Manages observer registrations and notifies them of reminder-related events.
    // Implements the Observer design pattern.
    public class ReminderNotifier
    {
        // List of registered observers
        private readonly List<IReminderObserver> observers;

        // Constructor: Initializes the list of observers
        public ReminderNotifier()
        {
            observers = new List<IReminderObserver>();
        }

        // Registers an observer to be notified.
        public void Attach(IReminderObserver observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);
        }

        // Unregisters an observer from notifications.
        public void Detach(IReminderObserver observer)
        {
            if (observers.Contains(observer))
                observers.Remove(observer);
        }

        // Notifies all registered observers using the basic reminder object.
        public void NotifyObservers(IReminder reminder)
        {
            foreach (var observer in observers)
            {
                observer.Update(reminder);
            }
        }

        // Notifies all registered observers using the form and reminder object.
        public void NotifyObservers(Form form, IReminder reminder)
        {
            foreach (var observer in observers)
            {
                observer.Notify(form, reminder);
            }
        }

    }
}