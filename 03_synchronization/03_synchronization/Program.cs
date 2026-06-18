
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



#region Критические секции
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






//Semaphore semaphore = new Semaphore(0, 3);
//object locker = new object();
//int executionTime = 0;

//void Run(int id)
//{
//    Console.WriteLine($"Thread {id} statrted");

//    semaphore.WaitOne();                        // Попытка взять блокировку 

//    Console.WriteLine($"Thread {id} passed semaphore");

//    int time;
//    lock (locker)
//    {
//        executionTime += 200;
//        time = executionTime;
//    }

//    Thread.Sleep(time + 2000);

//    Console.WriteLine($"Thread {id} released semaphore");
//    semaphore.Release();                            // "Освободить 1 место" 
//}

//for (int i = 1; i <= 5; ++i)
//{
//    int x = i;
//    Thread t = new Thread(() => Run(x));
//    t.Start();
//}

//Thread.Sleep(3000);
//semaphore.Release(3);

#endregion


#region Сигнализация

//object locker = new Object();

//void First()
//{
//    try
//    {
//        Monitor.Enter(locker);

//        for (int i = 1; i <= 10; i += 2)
//        {
//            Thread.Sleep(200);
//            Console.Write($"{i} ");
//            Monitor.Pulse(locker);          // Перевод locker в сигнальное состояние 
//            Monitor.Wait(locker);           // Ожидание следующего сигнального состояния 
//        }
//    }
//    finally
//    {
//        Monitor.Exit(locker);
//    }
//}
//void Second()
//{
//    try
//    {
//        Monitor.Enter(locker);

//        for (int i = 0; i <= 10; i += 2)
//        {
//            Thread.Sleep(200);
//            Console.Write($"{i} ");
//            Monitor.Pulse(locker);
//            Monitor.Wait(locker);
//        }
//    }
//    finally
//    {
//        Monitor.Exit(locker);
//    }
//}

//Thread t1 = new Thread(First);
//Thread t2 = new Thread(Second);

//t2.Start();
//Thread.Sleep(3000);
//t1.Start();







// FIXME ?????
//object locker1 = new Object();
//object locker2 = new Object();
//object mainLocker = new object();


//Thread t1 = new Thread(() =>
//{
//    Console.WriteLine("t1 started");
//    Monitor.Enter(locker1);
//    Console.WriteLine("t1 unblocked");
//    Thread.Sleep(10000);
//    Console.WriteLine("Hello from worker_1");
//    Monitor.Pulse(locker1);
//});

//Thread t2 = new Thread(() =>
//{
//    Console.WriteLine("t2 started");
//    Monitor.Enter(locker2);
//    Console.WriteLine("t2 unblocked");
//    Thread.Sleep(10000);
//    Console.WriteLine("Hello from worker_2");
//    Monitor.Pulse(locker2);
//});

//Monitor.Enter(locker1);
//Monitor.Enter(locker2);

//t1.Start();
//t2.Start();

////
////
//Thread.Sleep(3000);
//Monitor.Exit(locker1);
//Thread.Sleep(3000);
//Monitor.Exit(locker2);

//lock (mainLocker)
//{
//    Monitor.Wait(locker1);
//    Console.WriteLine("MAIN: t1 finished");
//    Monitor.Wait(locker2);
//    Console.WriteLine("MAIN: t2 finished"); 
//}





//SimpleWaitHandle.Run();
//static class SimpleWaitHandle
//{
//    static EventWaitHandle wh = new AutoResetEvent(false);

//    public static void Run()
//    {
//        new Thread(Work).Start();
//        Thread.Sleep(3000);
//        wh.Set();                   // Перевод в сигнальное состояние 
//    }

//    public static void Work()
//    {
//        Console.WriteLine("Work(): waiting...");
//        wh.WaitOne();               // Ожидание сигнального состояние 
//        Console.WriteLine("Working...");
//    }
//}




//AutoResetEvent are = new AutoResetEvent(false);

//for (int i = 0; i < 5; ++i)
//{
//    Thread t = new Thread(Render)
//    {
//        Name = $"thread_{i}",
//    };
//    t.Start();
//}
//Thread.Sleep(3000);
//are.Set();

//void Render()
//{
//    are.WaitOne();
//    for (int i = 0; i < 10; i++)
//    {
//        Console.WriteLine($"{Thread.CurrentThread.Name}: {i}");
//        Thread.Sleep(200);
//    }
//    are.Set();
//}






//ManualResetEvent mre = new ManualResetEvent(false);

//UserThread ut1 = new UserThread("first", mre);
//Console.WriteLine("Waiting");
//mre.WaitOne();
//Console.WriteLine("first finished");
//mre.Reset();

//UserThread ut2 = new UserThread("second", mre);
//mre.WaitOne();
//Console.WriteLine("second finished");
//mre.Reset();

//class UserThread
//{
//    private ManualResetEvent mre;
//    public Thread Thread { get; set; }

//    public UserThread(string name, ManualResetEvent mre)
//    {
//        this.mre = mre;

//        Thread = new Thread(Run)
//        {
//            Name = name,
//        };

//        Thread.Start();
//    }

//    private void Run()
//    {
//        Console.WriteLine($"{Thread.Name} started...");

//        for (int i = 0; i < 5; ++i)
//        {
//            Console.WriteLine($"{Thread.Name}: {i}");
//            Thread.Sleep(200);
//        }

//        mre.Set();
//    }
//}

#endregion



#region Interlocked

int count = 0;


//
//
Interlocked.Add(ref count, 10);


#endregion
