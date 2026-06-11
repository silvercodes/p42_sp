
#region intro

//void ShowPlus()
//{
//    for (int i = 0; i < 1000; ++i)
//        Console.Write('+');
//}

//Thread t = new Thread(ShowPlus);
//t.Start();

//Console.WriteLine(t.IsAlive);

//for (int i = 0; i < 1000; ++i)
//    Console.Write('0');




//void Run()
//{
//    for (int i = 0; i < 5; ++i)
//        Console.Write('0');
//}
//new Thread(Run).Start();
//Run();
//Console.WriteLine("Main end");





//bool done = false;

//void Run()
//{
//    if (!done)
//    {
//        Console.WriteLine("+");
//        done = true;
//        Console.WriteLine("DONE");
//    }
//}

//Thread t = new Thread(Run);
//t.Start();
//Run();






//bool done = false;
//object locker = new object();

//void Run()
//{
//    lock(locker)
//    {
//        if (!done)
//        {
//            Console.WriteLine("+");
//            done = true;
//            Console.WriteLine("DONE");
//        }
//    }
//}

//Thread t = new Thread(Run);
//t.Start();
//Run();





//void Run()
//{
//    for (int i = 0; i < 1000; ++i)
//        Console.Write('*');
//    Console.WriteLine();
//}

//Console.WriteLine("Main started");

//Thread t = new Thread(Run);
//t.Start();

////Thread.Sleep(1000);                 // Блокировка текущего потока (таймер)
//t.Join();                             // Блокировка текущего потока (событие -> завершение потока t)

//Console.WriteLine("Main finished");


#endregion


#region Create / Start

//void Run()
//{
//    Console.WriteLine("hello from Run");
//}

//Thread t = new Thread(Run);
//t.Start();



//Thread t = new Thread(() => Console.WriteLine("Hello from lambda"));
//t.Start();



//string email = "vasia@mail.com";
//Thread t = new Thread(() => Console.WriteLine($"EMAIL: {email}"));
//t.Start();




//void Sum(int a, int b)
//{
//    Console.WriteLine($"a + b = {a + b}");
//}

//int a = 4;
//int b = 5;

//Thread t = new Thread(() => Sum(a, b));




//void Render(string message, ConsoleColor color)
//{
//    Console.ForegroundColor = color;
//    Console.WriteLine(message);
//    Console.ResetColor();
//}

//string message = "Vasia";
//ConsoleColor color = ConsoleColor.Green;

//Thread t = new Thread(() => Render(message, color));
//t.Start();
////
////




//// :-(
//for (int i = 0; i < 10; ++i)
//    new Thread(() => Console.WriteLine(i)).Start();


// :-)
//for (int i = 0; i < 10; ++i)
//{
//    int n = i;
//    new Thread(() => Console.WriteLine(n)).Start();
//}


//int i;
//List<Thread> threads = new List<Thread>();

//for (i = 0; i < 10; ++i)
//    threads.Add(new Thread(() => Console.WriteLine(i)));

//threads.ForEach(t => t.Start());




#endregion



