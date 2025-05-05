using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PersonalOrganizerApp.ReminderClasses
{
    public class ShakeObserver : IReminderObserver
    {
        public void Update(IReminder reminder)
        {
            // Şu an için bir şey yapmıyoruz (gerekirse genişletilir)
        }

        public void Notify(Form form, IReminder reminder)
        {
            if (form == null || form.WindowState == FormWindowState.Minimized)
                return;

            var originalLocation = form.Location;

            int shakeAmplitude = 10;
            int shakeCount = 10;
            int shakeDelay = 20;

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

                form.Invoke((Action)(() => form.Location = originalLocation));

                // Başlığı güncelle
                form.Invoke((Action)(() =>
                {
                    form.Text = $"🔔 {reminder.Summary}";
                }));
            });
        }

    }
}