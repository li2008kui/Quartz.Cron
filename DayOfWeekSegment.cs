namespace Quartz.Cron
{
    public class DayOfWeekSegment : Segment
    {
        private readonly string _weekSegment = "?";

        public DayOfWeekSegment(bool unspecified = true)
        {
            _weekSegment = unspecified ? "?" : "*";
        }

        public DayOfWeekSegment(int week, bool isLastWeek = false)
        {
            if (week < 1 || week > 7)
            {
                throw new ArgumentException($"参数【{nameof(week)}】的取值范围为[1, 7]。");
            }

            _weekSegment = isLastWeek ? week + "L" : week.ToString();
        }

        public DayOfWeekSegment(int fromWeek, int toWeek)
        {
            if (fromWeek < 1 || fromWeek > 7)
            {
                throw new ArgumentException($"参数【{nameof(fromWeek)}】的取值范围为[1, 7]。");
            }

            if (toWeek < 2 || toWeek > 7)
            {
                throw new ArgumentException($"参数【{nameof(toWeek)}】的取值范围为[2, 7]。");
            }

            _weekSegment = fromWeek + "-" + toWeek;
        }

        public DayOfWeekSegment(int week, WeekOfMonth weekOfMonth)
        {
            if (week < 1 || week > 7)
            {
                throw new ArgumentException($"参数【{nameof(week)}】的天数间隔取值范围为[1, 7]。");
            }

            if (!Enum.IsDefined(weekOfMonth))
            {
                throw new ArgumentException($"参数【{nameof(weekOfMonth)}】的取值范围为[1, 5]。");
            }

            _weekSegment = week + "#" + (int)weekOfMonth;
        }

        public DayOfWeekSegment(DayOfWeekSpan interval)
        {
            var intervalMonth = interval.TotalDayOfWeeks;

            if (intervalMonth < 1 || intervalMonth > 7)
            {
                throw new ArgumentException($"参数【{nameof(interval)}】每周的天数间隔取值范围为[1, 7]。");
            }

            _weekSegment = "*/" + intervalMonth;
        }

        public DayOfWeekSegment(int fromMonth, DayOfWeekSpan interval)
        {
            if (fromMonth < 1 || fromMonth > 7)
            {
                throw new ArgumentException($"参数【{nameof(fromMonth)}】的取值范围为[1, 7]。");
            }

            var intervalMonth = interval.TotalDayOfWeeks;

            if (intervalMonth < 1 || intervalMonth > 7)
            {
                throw new ArgumentException($"参数【{nameof(interval)}】每周的天数间隔取值范围为[1, 7]。");
            }

            _weekSegment = fromMonth + "/" + intervalMonth;
        }

        public DayOfWeekSegment(int[] weeks)
        {
            if (weeks.Length < 1)
            {
                throw new ArgumentNullException(nameof(weeks), "参数不能为空。");
            }

            if (Array.FindIndex(weeks, s => s < 1 || s > 7) > 0)
            {
                throw new ArgumentException($"数组【{nameof(weeks)}】中参数的取值范围为[1, 7]。");
            }

            _weekSegment = string.Join(",", weeks.Distinct());
        }

        public override string ToString()
        {
            return _weekSegment;
        }
    }

    public enum WeekOfMonth
    {
        First = 1,
        Second = 2,
        Third = 3,
        Fourth = 4,
        Fifth = 5
    }

    public readonly struct DayOfWeekSpan
    {
        private DayOfWeekSpan(int value)
        {
            TotalDayOfWeeks = value;
        }

        public int TotalDayOfWeeks { get; }

        public static DayOfWeekSpan FromDayOfWeeks(int value)
        {
            return new DayOfWeekSpan(value);
        }
    }
}
