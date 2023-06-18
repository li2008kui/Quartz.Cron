namespace Quartz.Cron
{
    public class DayOfMonthSegment : Segment
    {
        private readonly string _daySegment = "*";

        public DayOfMonthSegment(bool unspecified = false, bool isLastDay = false)
        {
            _daySegment = unspecified ? "?" : (isLastDay ? "L" : "*");
        }

        public DayOfMonthSegment(int day, bool isClosestWorkingDay = false)
        {
            if (day < 1 || day > 31)
            {
                throw new ArgumentException($"参数【{nameof(day)}】的取值范围为[1, 31]。");
            }

            _daySegment = isClosestWorkingDay ? day + "W" : day.ToString();
        }

        public DayOfMonthSegment(int fromDay, int toDay)
        {
            if (fromDay < 1 || fromDay > 31)
            {
                throw new ArgumentException($"参数【{nameof(fromDay)}】的取值范围为[1, 31]。");
            }

            if (toDay < 2 || toDay > 31)
            {
                throw new ArgumentException($"参数【{nameof(toDay)}】的取值范围为[2, 31]。");
            }

            _daySegment = fromDay + "-" + toDay;
        }

        public DayOfMonthSegment(TimeSpan interval)
        {
            var intervalDay = (int)interval.TotalDays;

            if (intervalDay < 1 || intervalDay > 31)
            {
                throw new ArgumentException($"参数【{nameof(interval)}】每月的天数间隔取值范围为[1, 31]。");
            }

            _daySegment = "*/" + intervalDay;
        }

        public DayOfMonthSegment(int fromDay, TimeSpan interval)
        {
            if (fromDay < 1 || fromDay > 31)
            {
                throw new ArgumentException($"参数【{nameof(fromDay)}】的取值范围为[1, 31]。");
            }

            var intervalDay = (int)interval.TotalDays;

            if (intervalDay < 1 || intervalDay > 31)
            {
                throw new ArgumentException($"参数【{nameof(interval)}】每月的天数间隔取值范围为[1, 31]。");
            }

            _daySegment = fromDay + "/" + intervalDay;
        }

        public DayOfMonthSegment(int[] days)
        {
            if (days.Length < 1)
            {
                throw new ArgumentNullException(nameof(days), "参数不能为空。");
            }

            if (Array.FindIndex(days, s => s < 1 || s > 31) > 0)
            {
                throw new ArgumentException($"数组【{nameof(days)}】中参数的取值范围为[1, 31]。");
            }

            _daySegment = string.Join(",", days.Distinct());
        }

        public override string ToString()
        {
            return _daySegment;
        }
    }
}
