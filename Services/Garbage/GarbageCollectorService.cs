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
                var nextRunTime = now.Date.Add(new TimeSpan(12, 45, 0)); // Каждый день в 20:00
                if (nextRunTime < now)
                {
                    nextRunTime = nextRunTime.AddDays(1); // Если текущее время после 20:00, то следующая дата - завтра
                }

                var delay = nextRunTime - now; // Рассчитываем время до следующего запуска
                _logger.LogInformation($"Следующая очистка памяти запланирована на {nextRunTime}");

                await Task.Delay(delay, stoppingToken); // Ожидаем до 20:00

                // Запуск сборки мусора
                _logger.LogInformation("Запуск сборки мусора.");
                GC.Collect();
                GC.WaitForPendingFinalizers(); // Ожидаем завершения финализаторов

                _logger.LogInformation("Сборка мусора завершена.");
            }
        }
    }
}
