namespace CalculateTimeAngle.Services;

public class TimeAngleService
{
    public virtual double Calculate(int hour, int minute)
    {
        // static hour
        int anglePerHour = 360 / 12;
        int hourAngle = (hour % 12) * anglePerHour;

        int anglePerMinute = anglePerHour / 5;
        int minuteAngle = minute * anglePerMinute;

        //in between hours 
        double lastHour = minute * .5;

        return hourAngle + minuteAngle + lastHour;
    }
}
