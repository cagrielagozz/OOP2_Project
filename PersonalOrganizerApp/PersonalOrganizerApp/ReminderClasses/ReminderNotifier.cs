using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PersonalOrganizerApp.ReminderClasses;


namespace PersonalOrganizerApp.ReminderClasses
{
    public class ReminderNotifier
    {
        private readonly List<IReminderObserver> observers;

        public ReminderNotifier()
        {
            observers = new List<IReminderObserver>();
        }

        // Observer ekle
        public void Attach(IReminderObserver observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);
        }

        // Observer çıkar
        public void Detach(IReminderObserver observer)
        {
            if (observers.Contains(observer))
                observers.Remove(observer);
        }

        // Tüm observer'lara reminder üzerinden bildir
        public void NotifyObservers(IReminder reminder)
        {
            foreach (var observer in observers)
            {
                observer.Update(reminder);
            }
        }

        // Tüm observer'lara form üzerinden bildir (Shake için)
        public void NotifyObservers(Form form, IReminder reminder)
        {
            foreach (var observer in observers)
            {
                observer.Notify(form, reminder);
            }
        }

    }
}