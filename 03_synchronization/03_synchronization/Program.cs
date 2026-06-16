
// Инструменты сихронизации

// 1. Простые методы блокировки Thread.Sleep(), Thread.Join(), Task.Wait()....

// 2. Контроль критической секции lock, Monitor(20нс), Mutex(1000нс), SpinLock, Semaphore, SemaphoreSlim.....

// 3. Инструменты сигнализации Monitor.Pulse, Monitor.PulseAll(), AutoResetEvent, ManualResetEvent, CountdownResetEvent....

// 4. Неблокирующие инструменты Thread.MemoryBarrier, Interlocked, Thread.VlatileRead....



// Причины вывода потока из состояния блокировки
// 1. Выполнение условий блокировки
// 2. Таймер (таймаут)
// 3. Thread.Interruped()
// 4. Thread.Abort()




//new Thread(ThreadSafe.Run).Start();
//ThreadSafe.Run();
//class ThreadSafe 
//{ 
//   static int a = 10; 
//   static int b = 20; 
//   static object locker = new object(); 

//   public static void Run() 
//   { 
//       int c = 0; 

//       // FIFO 
//       lock (locker)                      // Monitor.Enter(locker, ref flag);
//       { 
//           if (b != 0) 
//           { 
//               c = a / b; 
//           } 

//           b = 0;  
//       }                                  // Monitor.Exit(locker);
//   }
//}




//class ThreadSafe
//{
//    static int a = 10;
//    static int b = 20;
//    static object locker = new object();

//    public static void Run()
//    {
//        int c = 0;
//        bool flag = false;

//        try
//        {
//            Monitor.Enter(locker, ref flag);        // Попытка взятия блокировки у locker 

//            if (b != 0)
//            {
//                c = a / b;
//            }

//            b = 0;
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"ERROR: {ex.Message}");
//        }
//        finally
//        {
//            if (flag)
//                Monitor.Exit(locker);               // Освобождение блокировки 
//        }

//    }

//}





//object locker = new object();
//int val = 0;

//void Run()
//{
//    bool flag = false;

//    try
//    {
//        flag = Monitor.TryEnter(locker, 3000);

//        if (flag)
//        {
//            for (int i = 0; i < 10; i++)
//            {
//                Console.WriteLine($"{Thread.CurrentThread.Name}: {val++}");
//                Thread.Sleep(200);
//            }
//        }
//        else
//        {
//            Console.WriteLine($"{Thread.CurrentThread.Name} is looser");
//        }
//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine($"ERROR: {ex.Message}");
//    }
//    finally
//    {
//        if (flag)
//            Monitor.Exit(locker);               // Освобождение блокировки 
//    }

//}


//for (int i = 0; i < 5; ++i)
//{
//    Thread t = new Thread(Run)
//    {
//        Name = $"thread_{i}"
//    };
//    t.Start();
//}





//int count = 0;
//Mutex mutex = new Mutex();

//void UseResource()
//{
//    if (mutex.WaitOne(500))            // Попытка взять блокировку 
//    {
//        Console.WriteLine($"{Thread.CurrentThread.Name} take the mutex");

//        Thread.Sleep(200);
//        count++;

//        Console.WriteLine($"{Thread.CurrentThread.Name} done the work");

//        Console.WriteLine($"{Thread.CurrentThread.Name} release mutex");

//        mutex.ReleaseMutex();           // Освоюождение mutex 
//    }
//    else
//    {
//        Console.WriteLine($"{Thread.CurrentThread.Name} is looser");
//    }
//}

//void StartThreads()
//{
//    for (int i = 0; i < 5; ++i)
//    {
//        Thread t = new Thread(UseResource)
//        {
//            Name = $"thread_{i}",
//        };
//        t.Start();
//    }
//}

//StartThreads();



Semaphore semaphore = new Semaphore(0, 3);
object locker = new object();
int executionTime = 0;

void Run(int id)
{
    Console.WriteLine($"Thread {id} statrted");

    semaphore.WaitOne();                        // Попытка взять блокировку 

    Console.WriteLine($"Thread {id} passed semaphore");

    int time;
    lock (locker)
    {
        executionTime += 200;
        time = executionTime;
    }

    Thread.Sleep(time + 2000);

    Console.WriteLine($"Thread {id} released semaphore");
    semaphore.Release();                            // "Освободить 1 место" 
}

for (int i = 1; i <= 5; ++i)
{
    int x = i;
    Thread t = new Thread(() => Run(x));
    t.Start();
}

Thread.Sleep(3000);
semaphore.Release(3);