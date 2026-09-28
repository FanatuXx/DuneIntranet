using System;
using System.Collections.Generic;
using System.Text;

namespace PFF.Domain.Model.Enum
{
    public enum ConsumptionFrequencyEnum
    {
        multipleTimesADay = 0,
        onceADay = 1,
        betweenOnceADayAndOnceAWeek = 2,
        onceAWeek = 3,
        betweenOnceAWeekAndOnceAMonth = 4,
        onceAMonth = 5,
        lessThanOnceAMonth = 6
    }
}
