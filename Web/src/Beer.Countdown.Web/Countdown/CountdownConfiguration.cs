using System;

namespace Beer.Countdown.Web.Countdown
{
    public record CountdownConfiguration
    {
        public DateTime SchoolsOpeningDate { get; init; }
        public DateTime SixPeopleOutsideDate { get; init; }
        public DateTime NonEssentialShopsOpeningDate { get; init; }
        public DateTime SixPeopleInsideDate { get; init; }
        public DateTime RestrictionsLifted { get; init; }
    }
}
