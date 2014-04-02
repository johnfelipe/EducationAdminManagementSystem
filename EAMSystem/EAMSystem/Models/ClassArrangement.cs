using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EAMSystem.Models
{
    public class ClassArrangement
    {
        public class ClassTime
        {
            public int Hour{get;set;}

            public int Minute {get;set;}
        }

        public ClassTime StartTime { get; set; }

        public ClassTime EndTime { get; set; }

        public int Period { get; set; }

        public static readonly ClassArrangement FirstClass
            = new ClassArrangement { Period = 2, StartTime = new ClassTime { Hour = 8, Minute = 20 }, EndTime = new ClassTime { Hour = 9, Minute = 55 } };

        public static readonly ClassArrangement SecondClass
            = new ClassArrangement { Period = 2, StartTime = new ClassTime { Hour = 10, Minute = 15 }, EndTime = new ClassTime { Hour = 11, Minute = 50 } };

        public static readonly ClassArrangement ThirdClass
            = new ClassArrangement { Period = 2, StartTime = new ClassTime { Hour = 13, Minute = 10 }, EndTime = new ClassTime { Hour = 14, Minute = 45 } };

        public static readonly ClassArrangement ForthClass
            = new ClassArrangement { Period = 2, StartTime = new ClassTime { Hour = 15, Minute = 5 }, EndTime = new ClassTime { Hour = 16, Minute = 40 } };

        public static readonly ClassArrangement FifthClass
            = new ClassArrangement { Period = 3, StartTime = new ClassTime { Hour = 18, Minute = 0 }, EndTime = new ClassTime { Hour = 20, Minute = 25 } };
    }

    public class CourseSchedule
    {
        public int Day { get; set; }

        public ClassArrangement arrangement { get; set; }
    }
}