namespace CRMEngSystem.Services.Garbage
{
    public class GarbageCollectorService : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("Сервис очистки мусора запущен.");

            while (!stoppingToken.IsCancellationRequested)
            {
                var now = DateTime.Now;
                var nextRunTime = now.Date.Add(new TimeSpan(13, 7, 0));
                if (nextRunTime < now)
                {
                    nextRunTime = nextRunTime.AddDays(1);
                }

                var delay = nextRunTime - now;
                Console.WriteLine($"Следующая очистка памяти запланирована на {nextRunTime}");

                await Task.Delay(delay, stoppingToken);

                Console.WriteLine("Запуск сборки мусора.");
                GC.Collect();
                GC.WaitForPendingFinalizers();

                Console.WriteLine("Сборка мусора завершена.");
            }
        }
    }
}
