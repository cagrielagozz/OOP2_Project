using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PersonalOrganizerApp.ReminderClasses
{
    // Observer class that triggers a visual shake effect on the form when notified.
    // Implements the IReminderObserver interface.
    public class ShakeObserver : IReminderObserver
    {
        public void Update(IReminder reminder)
        {
        }

        // Triggers a shake animation on the given form and updates the form's title.
        // <param name="form">The Windows Form to apply the shake effect.</param>
        // <param name="reminder">The reminder that triggered the notification.</param>
        public void Notify(Form form, IReminder reminder)
        {
            if (form == null || form.WindowState == FormWindowState.Minimized)
                return;

            var originalLocation = form.Location;

            int shakeAmplitude = 10;    // How far the form shakes left and right
            int shakeCount = 10;        // Number of shakes
            int shakeDelay = 20;        // Delay between shakes in milliseconds

            Task.Run(() =>
            {
                for (int i = 0; i < shakeCount; i++)
                {
                    form.Invoke((Action)(() =>
                    {
                        form.Location = new System.Drawing.Point(
                            originalLocation.X + ((i % 2 == 0) ? shakeAmplitude : -shakeAmplitude),
                            originalLocation.Y);
                    }));

                    System.Threading.Thread.Sleep(shakeDelay);
                }

                // Restore the original position
                form.Invoke((Action)(() => form.Location = originalLocation));

                // Update the form title to display the reminder
                form.Invoke((Action)(() =>
                {
                    form.Text = $"🔔 {reminder.Summary}";
                }));
            });
        }

    }
}