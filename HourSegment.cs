namespace Quartz.Cron
{
    public class HourSegment : Segment
    {
        private readonly string _hourSegment = "*";

        public HourSegment()
        {
            _hourSegment = "*";
        }

        public HourSegment(int hour)
        {
            if (hour < 0 || hour > 23)
            {
                throw new ArgumentException($"参数【{nameof(hour)}】的取值范围为[0, 23]。");
            }

            _hourSegment = hour.ToString();
        }

        public HourSegment(int fromHour, int toHour)
        {
            if (fromHour < 0 || fromHour > 23)
            {
                throw new ArgumentException($"参数【{nameof(fromHour)}】的取值范围为[0, 23]。");
            }

            if (toHour < 2 || toHour > 23)
            {
                throw new ArgumentException($"参数【{nameof(toHour)}】的取值范围为[2, 23]。");
            }

            _hourSegment = fromHour + "-" + toHour;
        }

        public HourSegment(TimeSpan interval)
        {
            var intervalHour = (int)interval.TotalHours;

            if (intervalHour < 1 || intervalHour > 23)
            {
                throw new ArgumentException($"参数【{nameof(interval)}】的小时间隔取值范围为[1, 23]。");
            }

            _hourSegment = "*/" + intervalHour;
        }

        public HourSegment(int fromHour, TimeSpan interval)
        {
            if (fromHour < 0 || fromHour > 23)
            {
                throw new ArgumentException($"参数【{nameof(fromHour)}】的取值范围为[0, 23]。");
            }

            var intervalHour = (int)interval.TotalHours;

            if (intervalHour < 1 || intervalHour > 23)
            {
                throw new ArgumentException($"参数【{nameof(interval)}】的小时间隔取值范围为[1, 23]。");
            }

            _hourSegment = fromHour + "/" + intervalHour;
        }

        public HourSegment(int[] hours)
        {
            if (hours.Length < 1)
            {
                throw new ArgumentNullException(nameof(hours), "参数不能为空。");
            }

            if (Array.FindIndex(hours, s => s < 0 || s > 23) > 0)
            {
                throw new ArgumentException($"数组【{nameof(hours)}】中参数的取值范围为[0, 23]。");
            }

            _hourSegment = string.Join(",", hours.Distinct());
        }

        public override string ToString()
        {
            return _hourSegment;
        }
    }
}
