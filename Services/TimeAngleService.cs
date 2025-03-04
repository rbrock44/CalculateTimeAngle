namespace CalculateTimeAngle.Services;

public class TimeAngleService
{
    public virtual double Calculate(int hour, int minute)
    {
        int anglePerHour = 360 / 12;
        int hourAngle = (hour % 12) * anglePerHour;

        int anglePerMinute = anglePerHour / 5;
        int minuteAngle = minute * anglePerMinute;

        double lastHour = minute * .5;

        return hourAngle + minuteAngle + lastHour;
    }
}
