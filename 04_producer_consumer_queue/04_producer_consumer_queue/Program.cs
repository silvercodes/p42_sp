using _04_producer_consumer_queue.Jobs;
using PCQ;

const int emailsCount = 10000;
const int workersCount = 50;

QueueManager qm = new QueueManager(workersCount);

for (int i = 0; i < emailsCount; i++)
{
    qm.AddJob(new SendEmailJob() { Email = $"user_{i}@mail.com" });
}

for (int i = 0; i < 200; ++i)
{
    Thread.Sleep(100);
    Console.WriteLine($"MAIN: {i}");
}
