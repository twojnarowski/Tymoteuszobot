namespace Tymoteuszobot.Model;

public class Reminder
{
    public string UserName { get; set; }
    public int HoursBetween { get; set; }

    public Reminder(string userName, int hours)
    {
        this.UserName = userName;
        this.HoursBetween = hours;
    }
}