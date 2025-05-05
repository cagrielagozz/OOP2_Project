using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PersonalOrganizerApp.ReminderClasses
{
    // Base abstract class for all reminders, implements IReminder interface
    public abstract class ReminderBase : IReminder
    {
        // Title of the reminder
        public string Title { get; set; }

        // Detailed description of the reminder
        public string Description { get; set; }

        // Summary of the reminder
        public string Summary { get; set; }

        // Date and time when the reminder is set to occur
        public DateTime Date { get; set; }

        // Time of day when the reminder is set to occur
        public TimeSpan Time { get; set; }


        // Type of the reminder, indicating its category (e.g., Meeting, Task)
        public abstract ReminderType Type { get; set; }

        // Method to notify the user about the reminder
        public virtual void Notify()
        {
            string message = $"[Reminder] {Summary}";
            Console.Title = message;

            // Shake the window to draw attention
            Form activeForm = Application.OpenForms["ReminderForm"];
            if (activeForm != null)
            {
                ShakeWindow(activeForm);
            }
        }

        // Method to shake the window to draw attention
        protected void ShakeWindow(Form form)
        {
            var originalLocation = form.Location;
            Random rnd = new Random();
            int shakeAmplitude = 10;

            for (int i = 0; i < 20; i++) // Shake the window 20 times
            {
                int offsetX = rnd.Next(-shakeAmplitude, shakeAmplitude);
                int offsetY = rnd.Next(-shakeAmplitude, shakeAmplitude);
                form.Location = new Point(originalLocation.X + offsetX, originalLocation.Y + offsetY);
                form.Refresh();
                System.Threading.Thread.Sleep(100);
            }

            form.Location = originalLocation;
        }
    }
}
