namespace Quartz.Cron
{
    public class MinuteSegment : Segment
    {
        private readonly string _minuteSegment = "*";

        public MinuteSegment()
        {
            _minuteSegment = "*";
        }

        public MinuteSegment(int minute)
        {
            if (minute < 0 || minute > 59)
            {
                throw new ArgumentException($"参数【{nameof(minute)}】的取值范围为[0, 59]。");
            }

            _minuteSegment = minute.ToString();
        }

        public MinuteSegment(int fromMinute, int toMinute)
        {
            if (fromMinute < 1 || fromMinute > 58)
            {
                throw new ArgumentException($"参数【{nameof(fromMinute)}】的取值范围为[1, 58]。");
            }

            if (toMinute < 2 || toMinute > 59)
            {
                throw new ArgumentException($"参数【{nameof(toMinute)}】的取值范围为[2, 59]。");
            }

            _minuteSegment = fromMinute + "-" + toMinute;
        }

        public MinuteSegment(TimeSpan interval)
        {
            var intervalMinute = (int)interval.TotalMinutes;

            if (intervalMinute < 1 || intervalMinute > 59)
            {
                throw new ArgumentException($"参数【{nameof(interval)}】的分钟间隔取值范围为[1, 59]。");
            }

            _minuteSegment = "*/" + intervalMinute;
        }

        public MinuteSegment(int fromMinute, TimeSpan interval)
        {
            if (fromMinute < 0 || fromMinute > 59)
            {
                throw new ArgumentException($"参数【{nameof(fromMinute)}】的取值范围为[0, 59]。");
            }

            var intervalMinute = (int)interval.TotalMinutes;

            if (intervalMinute < 1 || intervalMinute > 59)
            {
                throw new ArgumentException($"参数【{nameof(interval)}】的分钟间隔取值范围为[1, 59]。");
            }

            _minuteSegment = fromMinute + "/" + intervalMinute;
        }

        public MinuteSegment(int[] minutes)
        {
            if (minutes.Length < 1)
            {
                throw new ArgumentNullException(nameof(minutes), "参数不能为空。");
            }

            if (Array.FindIndex(minutes, s => s < 0 || s > 59) > 0)
            {
                throw new ArgumentException($"数组【{nameof(minutes)}】中参数的取值范围为[0, 59]。");
            }

            _minuteSegment = string.Join(",", minutes.Distinct());
        }

        public override string ToString()
        {
            return _minuteSegment;
        }
    }
}
