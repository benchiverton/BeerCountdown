using System;

namespace Beer.Countdown.Web.Countdown
{
    public record CountdownConfiguration
    {
        public DateTime TwoPeopleOutdoorsDate { get; init; }
        public DateTime SixPeopleOutdoorsDate { get; init; }
        public DateTime NonEssentialShopsOpeningDate { get; init; }
        public DateTime SixPeopleIndoorsDate { get; init; }
        public DateTime RestrictionsLifted { get; init; }
    }
}
