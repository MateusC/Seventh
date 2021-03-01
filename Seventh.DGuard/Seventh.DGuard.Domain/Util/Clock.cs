using System;

namespace Seventh.DGuard.Domain.Util
{
    public class Clock
    {
        private static DateTime? _now;

        public static DateTime Now { 
            get
            {
                return _now.HasValue ? _now.Value : DateTime.UtcNow;
            }
            private set
            {
                _now = value;
            }
        } 

        public static void Adjust(DateTime date)
        {
            Now = date;
        }

        public static void Add(TimeSpan time)
        {
            Now = Now.Add(time);
        }

        public static void Subtract(TimeSpan time)
        {
            Now = Now.Subtract(time);
        }

        public static void Reset()
        {
            Now = DateTime.UtcNow;
        }
    }
}
