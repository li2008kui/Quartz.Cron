namespace Quartz.Cron
{
    public class MonthSegment : Segment
    {
        private readonly string _monthSegment = "*";

        public MonthSegment(bool unspecified = false)
        {
            _monthSegment = unspecified ? "?" : "*";
        }

        public MonthSegment(int month)
        {
            if (month < 1 || month > 12)
            {
                throw new ArgumentException($"参数【{nameof(month)}】的取值范围为[1, 12]。");
            }

            _monthSegment = month.ToString();
        }

        public MonthSegment(int fromMonth, int toMonth)
        {
            if (fromMonth < 1 || fromMonth > 12)
            {
                throw new ArgumentException($"参数【{nameof(fromMonth)}】的取值范围为[1, 12]。");
            }

            if (toMonth < 2 || toMonth > 12)
            {
                throw new ArgumentException($"参数【{nameof(toMonth)}】的取值范围为[2, 12]。");
            }

            _monthSegment = fromMonth + "-" + toMonth;
        }

        public MonthSegment(MonthSpan interval)
        {
            var intervalMonth = interval.TotalMonths;

            if (intervalMonth < 1 || intervalMonth > 12)
            {
                throw new ArgumentException($"参数【{nameof(interval)}】的月数间隔取值范围为[1, 12]。");
            }

            _monthSegment = "*/" + intervalMonth;
        }

        public MonthSegment(int fromMonth, MonthSpan interval)
        {
            if (fromMonth < 1 || fromMonth > 12)
            {
                throw new ArgumentException($"参数【{nameof(fromMonth)}】的取值范围为[1, 12]。");
            }

            var intervalMonth = interval.TotalMonths;

            if (intervalMonth < 1 || intervalMonth > 12)
            {
                throw new ArgumentException($"参数【{nameof(interval)}】的月数间隔取值范围为[1, 12]。");
            }

            _monthSegment = fromMonth + "/" + intervalMonth;
        }

        public MonthSegment(int[] months)
        {
            if (months.Length < 1)
            {
                throw new ArgumentNullException(nameof(months), "参数不能为空。");
            }

            if (Array.FindIndex(months, s => s < 1 || s > 12) > 0)
            {
                throw new ArgumentException($"数组【{nameof(months)}】中参数的取值范围为[1, 12]。");
            }

            _monthSegment = string.Join(",", months.Distinct());
        }

        public override string ToString()
        {
            return _monthSegment;
        }
    }

    public readonly struct MonthSpan
    {
        private MonthSpan(int value)
        {
            TotalMonths = value;
        }

        public int TotalMonths { get; }

        public static MonthSpan FromMonths(int value)
        {
            return new MonthSpan(value);
        }
    }
}
