using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PersonalOrganizerApp.ReminderClasses
{
    public abstract class ReminderBase : IReminder
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Summary { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }

        public abstract ReminderType Type { get; set; }

        public virtual void Notify()
        {
            string message = $"[Reminder] {Summary}";
            Console.Title = message;

            // Aktif formu bul ve salla
            Form activeForm = Application.OpenForms["ReminderForm"];
            if (activeForm != null)
            {
                ShakeWindow(activeForm);
            }
        }

        protected void ShakeWindow(Form form)
        {
            var originalLocation = form.Location;
            Random rnd = new Random();
            int shakeAmplitude = 10;

            for (int i = 0; i < 20; i++) // 2 saniye (20 x 100ms)
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
