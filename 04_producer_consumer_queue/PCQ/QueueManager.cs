namespace PCQ;

public class QueueManager
{
    private Queue<IJob> jobs = new Queue<IJob>();
    private int workersCount;
    private List<Thread> threads = new List<Thread>();
    private EventWaitHandle wh = new AutoResetEvent(false);

    public QueueManager(int workersCount)
    {
        this.workersCount = workersCount;

        Init();
    }

    private void Init()
    {
        for (int i = 0; i < workersCount; i++)
        {
            Thread t = new Thread(HandleJob)
            {
                Name = $"worker_{i}"
            };
            threads.Add(t);

            t.Start();
        }
    }

    public void AddJob(IJob job)
    {
        lock(jobs)
            jobs.Enqueue(job);

        wh.Set();
    }

    private void HandleJob()
    {
        while(true)
        {
            IJob? job = null;

            lock(jobs)
            {
                if (jobs.Count > 0)
                    job = jobs.Dequeue();
            }

            if (job is not null)
            {
                job.Execute();
                Console.WriteLine($"{Thread.CurrentThread.Name} HNDLES {job.GetInfo()}");   // FIXME: for debug
            }
            else
            {
                wh.WaitOne();
            }
        }
    }

}
