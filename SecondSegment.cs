namespace Quartz.Cron
{
    public class SecondSegment : Segment
    {
        private readonly string _secondSegment = "*";

        public SecondSegment()
        {
            _secondSegment = "*";
        }

        public SecondSegment(int second)
        {
            if (second < 0 || second > 59)
            {
                throw new ArgumentException($"参数【{nameof(second)}】的取值范围为[0, 59]。");
            }

            _secondSegment = second.ToString();
        }

        public SecondSegment(int fromSecond, int toSecond)
        {
            if (fromSecond < 1 || fromSecond > 58)
            {
                throw new ArgumentException($"参数【{nameof(fromSecond)}】的取值范围为[1, 58]。");
            }

            if (toSecond < 2 || toSecond > 59)
            {
                throw new ArgumentException($"参数【{nameof(toSecond)}】的取值范围为[2, 59]。");
            }

            _secondSegment = fromSecond + "-" + toSecond;
        }

        public SecondSegment(TimeSpan interval)
        {
            var intervalSecond = (int)interval.TotalSeconds;

            if (intervalSecond < 1 || intervalSecond > 59)
            {
                throw new ArgumentException($"参数【{nameof(interval)}】的秒数间隔取值范围为[1, 59]。");
            }

            _secondSegment = "*/" + intervalSecond;
        }

        public SecondSegment(int fromSecond, TimeSpan interval)
        {
            if (fromSecond < 0 || fromSecond > 59)
            {
                throw new ArgumentException($"参数【{nameof(fromSecond)}】的取值范围为[0, 59]。");
            }

            var intervalSecond = (int)interval.TotalSeconds;

            if (intervalSecond < 1 || intervalSecond > 59)
            {
                throw new ArgumentException($"参数【{nameof(interval)}】的秒数间隔取值范围为[1, 59]。");
            }

            _secondSegment = fromSecond + "/" + intervalSecond;
        }

        public SecondSegment(int[] seconds)
        {
            if (seconds.Length < 1)
            {
                throw new ArgumentNullException(nameof(seconds), "参数不能为空。");
            }

            if (Array.FindIndex(seconds, s => s < 0 || s > 59) > 0)
            {
                throw new ArgumentException($"数组【{nameof(seconds)}】中参数的取值范围为[0, 59]。");
            }

            _secondSegment = string.Join(",", seconds.Distinct());
        }

        public override string ToString()
        {
            return _secondSegment;
        }
    }
}
