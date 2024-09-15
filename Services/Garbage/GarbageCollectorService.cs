namespace CRMEngSystem.Services.Garbage
{
    public class GarbageCollectorService : BackgroundService
    {
        private readonly ILogger<GarbageCollectorService> _logger;

        public GarbageCollectorService(ILogger<GarbageCollectorService> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Сервис очистки мусора запущен.");

            while (!stoppingToken.IsCancellationRequested)
            {
                var now = DateTime.Now;
                var nextRunTime = now.Date.Add(new TimeSpan(12, 45, 0));
                if (nextRunTime < now)
                {
                    nextRunTime = nextRunTime.AddDays(1);
                }

                var delay = nextRunTime - now;
                _logger.LogInformation($"Следующая очистка памяти запланирована на {nextRunTime}");

                await Task.Delay(delay, stoppingToken);

                _logger.LogInformation("Запуск сборки мусора.");
                GC.Collect();
                GC.WaitForPendingFinalizers();

                _logger.LogInformation("Сборка мусора завершена.");
            }
        }
    }
}
