using System;
using System.Timers;
using Microsoft.Extensions.Logging;

namespace Beer.Countdown.Web.Countdown
{
    public interface ICountdownService
    {
        TimeSpan TimeUntilBeerWithSixFriendsOutside { get; }
        TimeSpan TimeUntilBeerWithSixFriendsInside { get; }
        TimeSpan TimeUntilBeerWithUnlimitedFriendsAnywhere { get; }
        event Action OnChange;
    }

    public class CountdownService : ICountdownService, IDisposable
    {
        private readonly ILogger _logger;
        private readonly CountdownConfiguration _configuration;
        private readonly Timer _timer;

        public CountdownService(ILogger<CountdownService> logger, CountdownConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;

            // set initial values
            Update();

            // configure + start timer
            _timer = new Timer
            {
                Interval = 1000
            };
            _timer.Elapsed += OnTimerElapsed;
            _timer.Start();
        }

        public TimeSpan TimeUntilBeerWithSixFriendsOutside { get; private set; }
        public TimeSpan TimeUntilBeerWithSixFriendsInside { get; private set; }
        public TimeSpan TimeUntilBeerWithUnlimitedFriendsAnywhere { get; private set; }

        public event Action OnChange;

        private void OnTimerElapsed(object sender, ElapsedEventArgs e) => Update();

        private void Update()
        {
            var now = DateTime.Now;
            TimeUntilBeerWithSixFriendsOutside = (_configuration.SixPeopleOutsideDate - now);
            TimeUntilBeerWithSixFriendsInside = (_configuration.SixPeopleInsideDate - now);
            TimeUntilBeerWithUnlimitedFriendsAnywhere = (_configuration.RestrictionsLifted - now);

            OnChange?.Invoke();
        }

        public void Dispose()
        {
            _timer?.Stop();
            _timer?.Dispose();
        }
    }
}
