using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalOrganizerApp.ReminderClasses
{
    public interface IReminder
    {
        string Title { get; set; }
        string Description { get; set; }
        string Summary { get; set; }
        DateTime Date { get; set; }
        TimeSpan Time { get; set; }
        ReminderType Type { get; set; }

        void Notify(); // Örnek: Windows başlığına summary yazarak bildirim yapma
    }
}
