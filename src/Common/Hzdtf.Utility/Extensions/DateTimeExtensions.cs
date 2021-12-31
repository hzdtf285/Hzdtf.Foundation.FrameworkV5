using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace System
{
    /// <summary>
    /// 日期时间扩展类
    /// @ 黄振东
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// 最大日期
        /// </summary>
        public static readonly DateTime MAX_DATE = DateTime.MaxValue.Date;

        /// <summary>
        /// 1970的日期
        /// </summary>
        public static readonly DateTime DATE_1970 = new DateTime(1970, 1, 1);

        /// <summary>
        /// 转换为全部日期时间字符串 yyyy-M-d H:m:s.fff
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>全部日期时间字符串</returns>
        public static string ToFullDateTime(this DateTime dateTime) => dateTime.ToString("yyyy-M-d H:m:s.fff");

        /// <summary>
        /// 转换为固定长度的全部日期时间字符串 yyyy-MM-dd HH:mm:ss.fff
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>全部日期时间字符串</returns>
        public static string ToFullFixedDateTime(this DateTime dateTime) => dateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");

        /// <summary>
        /// 转换为日期时间字符串 yyyy-M-d H:m:s
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>日期时间字符串</returns>
        public static string ToDateTime(this DateTime dateTime) => dateTime.ToString("yyyy-M-d H:m:s");

        /// <summary>
        /// 转换为固定长度的日期时间字符串 yyyy-MM-dd HH:mm:ss
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>日期时间字符串</returns>
        public static string ToFixedDateTime(this DateTime dateTime) => dateTime.ToString("yyyy-MM-dd HH:mm:ss");

        /// <summary>
        /// 转换为日期字符串 yyyy-M-d
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>日期字符串</returns>
        public static string ToDate(this DateTime dateTime) => dateTime.ToString("yyyy-M-d");

        /// <summary>
        /// 转换为固定长度的日期字符串 yyyy-MM-dd
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>日期字符串</returns>
        public static string ToFixedDate(this DateTime dateTime) => dateTime.ToString("yyyy-MM-dd");

        /// <summary>
        /// 转换为小于日期，对于SQL查询则需要用小于，而不是小于或等于
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>小于日期</returns>
        public static DateTime ToLessThanDate(this DateTime dateTime)
        {
            var temp = dateTime.AddDays(1);
            return new DateTime(temp.Year, temp.Month, temp.Day);
        }

        /// <summary>
        /// 转换为小于日期，对于SQL查询则需要用小于，而不是小于或等于
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>小于日期</returns>
        public static DateTime? ToLessThanDate(this DateTime? dateTime)
        {
            if (dateTime == null)
            {
                return null;
            }
            var temp = ((DateTime)dateTime).AddDays(1);
            return new DateTime(temp.Year, temp.Month, temp.Day);
        }

        /// <summary>
        /// 转换为固定长度的小于日期字符串，对于SQL查询则需要用小于，而不是小于或等于
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>小于日期字符串</returns>
        public static string ToLessThanFixedDateString(this DateTime dateTime) => dateTime.ToLessThanDate().ToFixedDate();

        /// <summary>
        /// 转换为年月字符串 yyyy-M
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>年月字符串</returns>
        public static string ToYM(this DateTime dateTime) => dateTime.ToString("yyyy-M");

        /// <summary>
        /// 转换为固定长度的年月字符串 yyyy-MM
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>年月字符串</returns>
        public static string ToFixedYM(this DateTime dateTime) => dateTime.ToString("yyyy-MM");

        /// <summary>
        /// 转换为年月字符串 yyyy-M
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>年月字符串</returns>
        public static string ToYM(this DateTime? dateTime) => dateTime != null ? ToYM((DateTime)dateTime) : null;

        /// <summary>
        /// 转换为固定长度的年月字符串 yyyy-MM
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>年月字符串</returns>
        public static string ToFixedYM(this DateTime? dateTime) => dateTime != null ? ToFixedYM((DateTime)dateTime) : null;

        /// <summary>
        /// 转换为紧凑简短的日期字符串 yyMd
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>日期字符串</returns>
        public static string ToCompactShortDate(this DateTime dateTime) => dateTime.ToString("yyMd");

        /// <summary>
        /// 转换为紧凑简短固定长度的日期字符串 yyMMdd
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>日期字符串</returns>
        public static string ToCompactFixedShortDate(this DateTime dateTime) => dateTime.ToString("yyMMdd");

        /// <summary>
        /// 转换为紧凑简短的年月字符串 yyM
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>年月字符串</returns>
        public static string ToCompactShortYM(this DateTime dateTime) => dateTime.ToString("yyM");

        /// <summary>
        /// 转换为紧凑简短固定长度的年月字符串 yyMM
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>年月字符串</returns>
        public static string ToCompactFixedShortYM(this DateTime dateTime) => dateTime.ToString("yyMM");

        /// <summary>
        /// 转换为紧凑简短的年月字符串 yyM
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>年月字符串</returns>
        public static string ToCompactShortYM(this DateTime? dateTime) => dateTime != null ? ToCompactShortYM((DateTime)dateTime) : null;

        /// <summary>
        /// 转换为紧凑简短固定长度的年月字符串 yyMM
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>年月字符串</returns>
        public static string ToCompactFixedShortYM(this DateTime? dateTime) => dateTime != null ? ToCompactFixedShortYM((DateTime)dateTime) : null;

        /// <summary>
        /// 如果时分秒毫秒都为0，则添加到本日的23:59:59.999
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>日期时间</returns>
        public static DateTime? AddThisDayLastTime(this DateTime? dateTime)
        {
            if (dateTime == null)
            {
                return null;
            }

            return AddThisDayLastTime((DateTime)dateTime);
        }

        /// <summary>
        /// 如果时分秒毫秒都为0，则添加到本日的23:59:59.999
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>日期时间</returns>
        public static DateTime AddThisDayLastTime(this DateTime dateTime)
        {
            if (dateTime.Hour == 0 && dateTime.Minute == 0 && dateTime.Second == 0 && dateTime.Millisecond == 0)
            {
                return dateTime.AddMilliseconds(86399999);
            }

            return dateTime;
        }

        /// <summary>
        /// 获取本月第1天日期
        /// </summary>
        /// <returns>本月第1天日期</returns>
        public static DateTime ThisMonthFristDay() => DateTime.Now.MonthFristDay();

        /// <summary>
        /// 获取指定日期的月份第1天日期
        /// </summary>
        /// <returns>月份第1天日期</returns>
        public static DateTime MonthFristDay(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1);
        }

        /// <summary>
        /// 获取本月最后1天日期
        /// </summary>
        /// <returns>本月最后1天日期</returns>
        public static DateTime ThisMonthLastDay() => DateTime.Now.MonthLastDay();

        /// <summary>
        /// 获取指定日期的月份最后1天日期
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>月份最后1天日期</returns>
        public static DateTime MonthLastDay(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.PlaceMonthOfDay(), 23, 59, 59, 999);
        }

        /// <summary>
        /// 获取给定日期距离1900-01-01的天数
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>日期距离1900-01-01的天数</returns>
        public static int GetDis1900Day(this DateTime dateTime)
        {
            TimeSpan ts1 = new TimeSpan(dateTime.Ticks);
            TimeSpan ts2 = new TimeSpan(Convert.ToDateTime("1900-01-01").Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();
            return ts.Days;
        }

        /// <summary>
        /// 获取指定日期的当前季度最后1天日期
        /// </summary>
        /// <returns>当前日期的季度最后1天日期</returns>
        public static DateTime CurrQuarterLastDay() => DateTime.Now.QuarterLastDay();

        /// <summary>
        /// 获取指定日期的季度最后1天日期
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>日期的季度最后1天日期</returns>
        public static DateTime QuarterLastDay(this DateTime dateTime)
        {
            switch (dateTime.GetQuarter())
            {
                case 0:
                    return new DateTime(dateTime.Year, 3, 31, 23, 59, 59, 999);

                case 1:
                    return new DateTime(dateTime.Year, 6, 30, 23, 59, 59, 999);

                case 2:
                    return new DateTime(dateTime.Year, 9, 30, 23, 59, 59, 999);

                default:
                    return new DateTime(dateTime.Year, 12, 31, 23, 59, 59, 999);
            }
        }

        /// <summary>
        /// 获取当前日期的季度第1天日期
        /// </summary>
        /// <returns>当前日期的季度第1天日期</returns>
        public static DateTime QuarterFirstDay() => DateTime.Now.QuarterFirstDay();

        /// <summary>
        /// 获取指定日期的季度第1天日期
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>日期的季度第1天日期</returns>
        public static DateTime QuarterFirstDay(this DateTime dateTime)
        {
            switch (dateTime.GetQuarter())
            {
                case 0:
                    return new DateTime(dateTime.Year, 1, 1);

                case 1:
                    return new DateTime(dateTime.Year, 4, 1);

                case 2:
                    return new DateTime(dateTime.Year, 7, 1);

                default:
                    return new DateTime(dateTime.Year, 10, 1);
            }
        }

        /// <summary>
        /// 获取当前季度，从0开始
        /// </summary>
        /// <returns>当前季度</returns>
        public static byte GetCurrQuarter() => DateTime.Now.GetQuarter();

        /// <summary>
        /// 获取季度，从0开始
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>季度</returns>
        public static byte GetQuarter(this DateTime dateTime)
        {
            if (dateTime.Month < 4)
            {
                return 0;
            }
            if (dateTime.Month > 3 && dateTime.Month < 7)
            {
                return 1;
            }
            if (dateTime.Month > 6 && dateTime.Month < 10)
            {
                return 2;
            }

            return 3;
        }
        
        #region 普通闰年

        /// <summary>
        /// 判断当前日期是否为普通闰年
        /// 普通闰年：能被4整除但不能被100整除
        /// </summary>
        /// <returns>当前日期是否为普通闰年</returns>
        public static bool IsOrdinaryLeapYear() => DateTime.Now.Year.IsOrdinaryLeapYear();

        /// <summary>
        /// 判断日期是否为普通闰年
        /// 普通闰年：能被4整除但不能被100整除
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>日期是否为普通闰年</returns>
        public static bool IsOrdinaryLeapYear(this DateTime dateTime) => dateTime.Year.IsOrdinaryLeapYear();

        /// <summary>
        /// 判断年份是否为普通闰年
        /// 普通闰年：能被4整除但不能被100整除
        /// </summary>
        /// <param name="year">年份</param>
        /// <returns>年份是否为普通闰年</returns>
        public static bool IsOrdinaryLeapYear(this int year) => year % 4 == 0 && year % 100 != 0;

        #endregion

        #region 世纪闰年

        /// <summary>
        /// 判断当前日期是否为世纪闰年
        /// 世纪闰年：能被400整除
        /// </summary>
        /// <returns>当前日期是否为世纪闰年</returns>
        public static bool IsCenturyLeapYear() => DateTime.Now.Year.IsCenturyLeapYear();

        /// <summary>
        /// 判断日期是否为世纪闰年
        /// 世纪闰年：能被400整除
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>日期是否为世纪闰年</returns>
        public static bool IsCenturyLeapYear(this DateTime dateTime) => dateTime.Year.IsCenturyLeapYear();

        /// <summary>
        /// 判断年份是否为世纪闰年
        /// 世纪闰年：能被400整除
        /// </summary>
        /// <param name="year">年份</param>
        /// <returns>年份是否为世纪闰年</returns>
        public static bool IsCenturyLeapYear(this int year) => year % 400 == 0;

        #endregion

        #region 闰年

        /// <summary>
        /// 判断当前日期是否为闰年
        /// 闰年：能被4整除但不能被100整除或能被400整除
        /// </summary>
        /// <returns>当前日期是否为闰年</returns>
        public static bool IsLeapYear() => DateTime.Now.Year.IsLeapYear();

        /// <summary>
        /// 判断日期是否为闰年
        /// 闰年：能被4整除但不能被100整除或能被400整除
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>日期是否为世纪闰年</returns>
        public static bool IsLeapYear(this DateTime dateTime) => dateTime.Year.IsLeapYear();

        /// <summary>
        /// 判断年份是否为闰年
        /// 闰年：能被4整除但不能被100整除或能被400整除
        /// </summary>
        /// <param name="year">年份</param>
        /// <returns>年份是否为闰年</returns>
        public static bool IsLeapYear(this int year) => year.IsOrdinaryLeapYear() || year.IsCenturyLeapYear();

        #endregion

        /// <summary>
        /// 获取当前日期的所在的月份的天数
        /// </summary>
        /// <returns>当前日期的所在的月份的天数</returns>
        public static int PlaceMonthOfDay() => DateTime.Now.PlaceMonthOfDay();

        /// <summary>
        /// 获取日期的所在的月份的天数
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>日期的所在的月份的天数</returns>
        public static int PlaceMonthOfDay(this DateTime dateTime)
        {
            int day = 30;
            switch (dateTime.Month)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:
                    day = 31;

                    break;

                case 2:
                    day = dateTime.IsLeapYear() ? 29 : 28;

                    break;
            }

            return day;
        }

        /// <summary>
        /// 时间范围内所过的月份数
        /// </summary>
        /// <param name="start">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <returns>时间范围内所过的月份数</returns>
        public static int RangeMonths(this DateTime start, DateTime end) => (end.Year - start.Year) * 12 + (end.Month - start.Month + 1);

        /// <summary>
        /// 取出月份存在区间
        /// </summary>
        /// <param name="rangeStartMonth">区间开始月份</param>
        /// <param name="rangeEndMonth">区间结束月份</param>
        /// <param name="startMonth">开始月份</param>
        /// <param name="endMonth">结束月份</param>
        /// <returns>存在的月份集合(yyyy-M)</returns>
        public static string[] MonthExistsRange(DateTime rangeStartMonth, DateTime rangeEndMonth, DateTime startMonth, DateTime endMonth)
        {
            List<string> result = new List<string>();
            if (rangeStartMonth > rangeEndMonth || startMonth > endMonth)
            {
                return null;
            }

            // 取出区间经历的月份数
            int rangeMonthLength = RangeMonths(rangeStartMonth, rangeEndMonth);
            // 取出经历的月份数
            int monthLength = RangeMonths(startMonth, endMonth);

            for (int i = 0; i < rangeMonthLength; i++)
            {
                DateTime d1 = rangeStartMonth.AddMonths(i);
                for (int j = 0; j < monthLength; j++)
                {
                    DateTime d2 = startMonth.AddMonths(j);
                    if (d1.Year == d2.Year && d1.Month == d2.Month)
                    {
                        result.Add(d2.ToFixedYM());
                        break;
                    }
                }
            }

            return result.ToArray();
        }

        /// <summary>
        /// 当前年的第一天
        /// </summary>
        /// <returns>当前年的第一天</returns>
        public static DateTime FristDay() => DateTime.Now.FristDay();

        /// <summary>
        /// 指定日期的年的第一天
        /// </summary>
        /// <returns>年的第一天</returns>
        public static DateTime FristDay(this DateTime dateTime)
        {
            return new DateTime(DateTime.Now.Year, 1, 1);
        }

        /// <summary>
        /// 当前年的最后一天
        /// </summary>
        /// <returns>当前年的最后一天</returns>
        public static DateTime LastDay() => DateTime.Now.LastDay();

        /// <summary>
        /// 指定日期的年的最后一天
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>年的最后一天</returns>
        public static DateTime LastDay(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, 12, 31, 23, 59, 59, 999);
        }

        /// <summary>
        /// 将当前日期时间转换为长数字字符串
        /// </summary>
        /// <returns>长数字字符串</returns>
        public static string ToLongDateTimeNumString() => DateTime.Now.ToLongDateTimeNumString();

        /// <summary>
        /// 将日期时间转换为长数字字符串
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>长数字字符串</returns>
        public static string ToLongDateTimeNumString(this DateTime dateTime) => dateTime.ToString("yyMMddHHmmssfff");

        /// <summary>
        /// 转换为周一日期
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>周一日期</returns>
        public static DateTime ToMonday(this DateTime dateTime)
        {
            var weekNum = (int)dateTime.DayOfWeek;
            if (weekNum == 0)
            {
                weekNum = 7;
            }
            var newDateTime = weekNum == 1 ? dateTime : dateTime.AddDays(1 - weekNum);

            return new DateTime(newDateTime.Year, newDateTime.Month, newDateTime.Day);
        }

        /// <summary>
        /// 转换为周日日期
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>周日日期</returns>
        public static DateTime ToSunday(this DateTime dateTime)
        {
            var weekNum = (int)dateTime.DayOfWeek;
            var newDateTime = weekNum == 0 ? dateTime : dateTime.AddDays(7 - weekNum);

            return new DateTime(newDateTime.Year, newDateTime.Month, newDateTime.Day);
        }

        /// <summary>
        /// 过滤掉时间
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>日期</returns>
        public static DateTime FilterTime(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
        }

        /// <summary>
        /// 将时间戳转换成日期时间
        /// </summary>
        /// <param name="timeStamp">时间戳</param>
        /// <returns>日期时间</returns>
        public static DateTime ToDateTimeFromTimeStamp(this string timeStamp)
        {
#pragma warning disable CS0618 // 类型或成员已过时
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(DATE_1970);
#pragma warning restore CS0618 // 类型或成员已过时
            long lTime = long.Parse(timeStamp + "0000");
            TimeSpan toNow = new TimeSpan(lTime);
            DateTime targetDt = dtStart.Add(toNow);

            return dtStart.Add(toNow);
        }

        /// <summary>
        /// 将时间戳转换成日期时间
        /// </summary>
        /// <param name="timeStamp">时间戳</param>
        /// <returns>日期时间</returns>
        public static DateTime ToDateTimeFromTimeStamp(this int timeStamp)
        {
            return DATE_1970.AddSeconds(timeStamp).ToLocalTime();
        }

        /// <summary>  
        /// 将DateTime时间格式转换为Unix时间戳格式  
        /// </summary>  
        /// <param name="dateTime">日期时间</param>  
        /// <returns>时间戳</returns>  
        public static long ToTimeStampFromDateTime(this DateTime dateTime)
        {
            var startTime = TimeZone.CurrentTimeZone.ToLocalTime(DATE_1970);
            long t = (dateTime.Ticks - startTime.Ticks) / 10000;   //除10000调整为13位      

            return t;
        }

        /// <summary>
        /// 设置对象所有日期时间属性为本地时间
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="depth">深度</param>
        public static void SetDateTimeToLocalTime(this object obj, byte depth = 3)
        {
            SetDateTimeToLocalTimeFrorCurrDepth(obj, 1, depth);
        }

        /// <summary>
        /// 设置对象所有日期时间属性为本地时间
        /// </summary>
        /// <param name="currDepth">当前深度</param>
        /// <param name="depth">深度</param>
        /// <param name="obj">对象</param>
        private static void SetDateTimeToLocalTimeFrorCurrDepth(object obj, byte currDepth, byte depth)
        {
            if (obj == null || currDepth > depth)
            {
                return;
            }
            var type = obj.GetType();
            if (type.IsArray)
            {
                var array = obj as object[];
                if (array == null)
                {
                    return;
                }
                foreach (var a in array)
                {
                    if (a == null)
                    {
                        continue;
                    }

                    SetDateTimeToLocalTimeFrorCurrDepth(a, currDepth, depth);
                }
                return;
            }
            var thisFullName = type.FullName;
            var nextDepth = Convert.ToByte(currDepth + 1);
            if (thisFullName.Contains("System.Collections.Generic.IList") || thisFullName.Contains("System.Collections.Generic.List")
                || thisFullName.Contains("System.Collections.Generic.ICollection"))
            {
                var list = obj as IEnumerable<object>;
                if (list == null)
                {
                    return;
                }
                foreach (var a in list)
                {
                    if (a == null)
                    {
                        continue;
                    }

                    SetDateTimeToLocalTimeFrorCurrDepth(a, nextDepth, depth);
                }

                return;
            }

            var propertys = obj.GetType().GetProperties();
            foreach (var item in propertys)
            {
                if (item.CanRead)
                {
                    if (item.PropertyType.IsEnum || item.PropertyType == typeof(string))
                    {
                        continue;
                    }

                    if (item.PropertyType == typeof(DateTime) || item.PropertyType == typeof(DateTime?))
                    {
                        if (item.CanWrite)
                        {
                            var value = item.GetValue(obj);
                            if (value == null)
                            {
                                continue;
                            }
                            var dateTime = ((DateTime)value).ToLocalTime();
                            item.SetValue(obj, dateTime);
                        }

                        continue;
                    }
                    if (item.PropertyType.IsValueType)
                    {
                        continue;
                    }

                    var fullName = item.PropertyType.FullName;
                    if (fullName.Contains("System.Collections.Generic.IList") || fullName.Contains("System.Collections.Generic.List")
                        || fullName.Contains("System.Collections.Generic.ICollection"))
                    {
                        var objValue = item.GetValue(obj);
                        if (objValue == null)
                        {
                            continue;
                        }

                        var list = objValue as IEnumerable<object>;
                        if (list == null)
                        {
                            continue;
                        }
                        foreach (var a in list)
                        {
                            if (a == null)
                            {
                                continue;
                            }

                            SetDateTimeToLocalTimeFrorCurrDepth(a, nextDepth, depth);
                        }
                    }

                    if (item.PropertyType.IsClass)
                    {
                        var objValue = item.GetValue(obj);
                        if (objValue == null)
                        {
                            continue;
                        }
                        if (item.PropertyType.IsArray)
                        {
                            var array = objValue as object[];
                            if (array == null)
                            {
                                continue;
                            }
                            foreach (var a in array)
                            {
                                if (a == null)
                                {
                                    continue;
                                }

                                SetDateTimeToLocalTimeFrorCurrDepth(a, nextDepth, depth);
                            }
                        }
                        else
                        {
                            SetDateTimeToLocalTimeFrorCurrDepth(objValue, nextDepth, depth);
                        }
                    }
                }
            }
        }
    }
}
