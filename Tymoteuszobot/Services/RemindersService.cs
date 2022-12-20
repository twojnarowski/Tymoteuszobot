using Tymoteuszobot.Model;

namespace Tymoteuszobot.Services;

public class RemindersService
{
    private static readonly List<Reminder> reminders = new();

    public static void AddReminder(string userName, int hours)
    {
        if (Exists(userName))
        {
            reminders.First(x => x.UserName.Equals(userName, StringComparison.InvariantCultureIgnoreCase)).HoursBetween = hours;
        }
        else
        {
            Reminder newReminder = new(userName, hours);
            reminders.Add(newReminder);
        }
    }

    public static void DeleteReminder(string userName)
    {
        if (Exists(userName))
        {
            var reminder = reminders.First(x => x.UserName.Equals(userName, StringComparison.InvariantCultureIgnoreCase));
            reminders.Remove(reminder);
        }
    }

    public static Reminder GetReminder(string userName)
    {
        Reminder reminder = null;
        if (Exists(userName))
        {
            reminder = reminders.First(x => x.UserName.Equals(userName, StringComparison.InvariantCultureIgnoreCase));
        }

        return reminder;
    }

    public static bool Exists(string userName)
    {
        return reminders.Any(x => x.UserName.Equals(userName, StringComparison.InvariantCultureIgnoreCase));
    }
}