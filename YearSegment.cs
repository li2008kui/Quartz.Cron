namespace Quartz.Cron
{
    public class YearSegment : Segment
    {
        private readonly string _yearSegment = string.Empty;

        public YearSegment(bool unspecified = true)
        {
            _yearSegment = unspecified ? string.Empty : "*";
        }

        public YearSegment(int year)
        {
            var cntYear = DateTime.UtcNow.Year;

            if (year < cntYear)
            {
                throw new ArgumentException($"参数【{nameof(year)}】的取值应不小于{cntYear}。");
            }

            _yearSegment = year.ToString();
        }

        public YearSegment(int fromYear, int toYear)
        {
            var year = DateTime.UtcNow.Year;

            if (fromYear < year)
            {
                throw new ArgumentException($"参数【{nameof(fromYear)}】的取值应不小于{year}。");
            }

            if (fromYear < toYear)
            {
                throw new ArgumentException($"参数【{nameof(fromYear)}】的取值应不大于【{nameof(toYear)}】。");
            }

            _yearSegment = fromYear + "-" + toYear;
        }

        public YearSegment(YearSpan interval)
        {
            var intervalYear = interval.TotalYears;

            if (intervalYear < 1)
            {
                throw new ArgumentException($"参数【{nameof(interval)}】的年数间隔取值应大于0。");
            }

            _yearSegment = "*/" + intervalYear;
        }

        public YearSegment(int fromYear, YearSpan interval)
        {
            var year = DateTime.UtcNow.Year;

            if (fromYear < year)
            {
                throw new ArgumentException($"参数【{nameof(fromYear)}】的取值应不小于{year}。");
            }

            var intervalYear = interval.TotalYears;

            if (intervalYear < 1)
            {
                throw new ArgumentException($"参数【{nameof(interval)}】的年数间隔取值应大于0。");
            }

            _yearSegment = fromYear + "/" + intervalYear;
        }

        public YearSegment(int[] years)
        {
            if (years.Length < 1)
            {
                throw new ArgumentNullException(nameof(years), "参数不能为空。");
            }

            var cntYear = DateTime.UtcNow.Year;

            if (Array.FindIndex(years, s => s < cntYear) > 0)
            {
                throw new ArgumentException($"数组【{nameof(years)}】中参数的取值应不小于{cntYear}。");
            }

            _yearSegment = string.Join(",", years.Distinct());
        }

        public override string ToString()
        {
            return _yearSegment;
        }
    }

    public readonly struct YearSpan
    {
        private YearSpan(int value)
        {
            TotalYears = value;
        }

        public int TotalYears { get; }

        public static YearSpan FromYears(int value)
        {
            return new YearSpan(value);
        }
    }
}
