namespace CRMEngSystem.Services.Garbage
{
    public class GarbageCollectorService : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var now = DateTime.Now;
                var nextRunTime = now.Date.Add(new TimeSpan(13, 20, 0)); // Каждый день в 20:00

                if (nextRunTime < now)
                {
                    nextRunTime = nextRunTime.AddDays(1); // Если текущее время после 20:00, переход на следующий день
                }

                var delay = nextRunTime - now;

                if (delay.TotalMinutes > 10) // Если до 20:00 осталось больше 10 минут
                {
                    await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken); // Ожидаем 10 минут
                }
                else
                {
                    await Task.Delay(delay, stoppingToken); // Ожидаем до 20:00
                    GC.Collect(); // Принудительная сборка мусора
                    GC.WaitForPendingFinalizers(); // Ожидание завершения
                }
            }
        }
    }
}
