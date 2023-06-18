namespace Quartz.Cron
{
    public static class CronExpressionExtensions
    {
        public static CronExpression SetSegment<T>(this CronExpression cronExpression, Func<T> func) where T : Segment
        {
            var t = typeof(T);

            if (t == typeof(SecondSegment))
            {
                cronExpression.SecondSegment = (func as Func<SecondSegment>)?.Invoke();
            }
            else if (t == typeof(MinuteSegment))
            {
                cronExpression.MinuteSegment = (func as Func<MinuteSegment>)?.Invoke();
            }
            else if (t == typeof(HourSegment))
            {
                cronExpression.HourSegment = (func as Func<HourSegment>)?.Invoke();
            }
            else if (t == typeof(DayOfMonthSegment))
            {
                cronExpression.DayOfWeekSegment = new DayOfWeekSegment(true);
                cronExpression.DayOfMonthSegment = (func as Func<DayOfMonthSegment>)?.Invoke();
            }
            else if (t == typeof(MonthSegment))
            {
                cronExpression.MonthSegment = (func as Func<MonthSegment>)?.Invoke();
            }
            else if (t == typeof(DayOfWeekSegment))
            {
                cronExpression.DayOfMonthSegment = new DayOfMonthSegment(true);
                cronExpression.DayOfWeekSegment = (func as Func<DayOfWeekSegment>)?.Invoke();
            }
            else if (t == typeof(YearSegment))
            {
                cronExpression.YearSegment = (func as Func<YearSegment>)?.Invoke();
            }
            else { }

            return cronExpression;
        }

        public static CronExpression SetSecondSegment(this CronExpression cronExpression, Func<SecondSegment> func)
        {
            cronExpression.SecondSegment = func.Invoke();
            return cronExpression;
        }

        public static CronExpression SetMinuteSegment(this CronExpression cronExpression, Func<MinuteSegment> func)
        {
            cronExpression.MinuteSegment = func.Invoke();
            return cronExpression;
        }

        public static CronExpression SetHourSegment(this CronExpression cronExpression, Func<HourSegment> func)
        {
            cronExpression.HourSegment = func.Invoke();
            return cronExpression;
        }

        public static CronExpression SetDayOfMonthSegment(this CronExpression cronExpression, Func<DayOfMonthSegment> func)
        {
            cronExpression.DayOfWeekSegment = new DayOfWeekSegment(true);

            cronExpression.DayOfMonthSegment = func.Invoke();
            return cronExpression;
        }

        public static CronExpression SetMonthSegment(this CronExpression cronExpression, Func<MonthSegment> func)
        {
            cronExpression.MonthSegment = func.Invoke();
            return cronExpression;
        }

        public static CronExpression SetDayOfWeekSegment(this CronExpression cronExpression, Func<DayOfWeekSegment> func)
        {
            cronExpression.DayOfMonthSegment = new DayOfMonthSegment(true);

            cronExpression.DayOfWeekSegment = func.Invoke();
            return cronExpression;
        }

        public static CronExpression SetYearSegment(this CronExpression cronExpression, Func<YearSegment> func)
        {
            cronExpression.YearSegment = func.Invoke();
            return cronExpression;
        }
    }

    public class CronExpression
    {
        public SecondSegment? SecondSegment { get; set; }
        public MinuteSegment? MinuteSegment { get; set; }
        public HourSegment? HourSegment { get; set; }
        public DayOfMonthSegment? DayOfMonthSegment { get; set; }
        public MonthSegment? MonthSegment { get; set; }
        public DayOfWeekSegment? DayOfWeekSegment { get; set; }
        public YearSegment? YearSegment { get; set; }

        public CronExpression()
        {
            SecondSegment = new SecondSegment();
            MinuteSegment = new MinuteSegment();
            HourSegment = new HourSegment();
            DayOfMonthSegment = new DayOfMonthSegment();
            MonthSegment = new MonthSegment();
            DayOfWeekSegment = new DayOfWeekSegment(true);
            YearSegment = new YearSegment();
        }

        public CronExpression(SecondSegment secondSegment, MinuteSegment minuteSegment, HourSegment hourSegment, MonthSegment monthSegment, DayOfWeekSegment dayOfWeekSegment, YearSegment? yearSegment = null)
        {
            SecondSegment = secondSegment;
            MinuteSegment = minuteSegment;
            HourSegment = hourSegment;
            DayOfMonthSegment = new DayOfMonthSegment(true);
            MonthSegment = monthSegment;
            DayOfWeekSegment = dayOfWeekSegment;
            YearSegment = yearSegment;
        }

        public CronExpression(SecondSegment secondSegment, MinuteSegment minuteSegment, HourSegment hourSegment, DayOfMonthSegment dayOfMonthSegment, MonthSegment monthSegment, YearSegment? yearSegment = null)
        {
            SecondSegment = secondSegment;
            MinuteSegment = minuteSegment;
            HourSegment = hourSegment;
            DayOfMonthSegment = dayOfMonthSegment;
            MonthSegment = monthSegment;
            DayOfWeekSegment = new DayOfWeekSegment(true);
            YearSegment = yearSegment;
        }

        public override string ToString()
        {
            var doms = DayOfMonthSegment?.ToString();
            var dows = DayOfWeekSegment?.ToString();

            if (doms == "?" && dows == "?")
            {
                dows = "*";
            }

            if (doms != "?" && dows != "?")
            {
                dows = "?";
            }

            return (SecondSegment + " "
                + MinuteSegment + " "
                + HourSegment + " "
                + doms + " "
                + MonthSegment + " "
                + dows + " "
                + YearSegment).TrimEnd();
        }
    }

    public class Segment { }
}