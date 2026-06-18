
#region Постановка Task в очередь

//Task t1 = new Task(() => Console.WriteLine("Vasia"));
//t1.Start();

//Task t2 = Task.Factory.StartNew(() => Console.WriteLine("Petya"));

//Task t3 = Task.Run(() => Console.WriteLine("Dima"));

////
////
////

//t1.Wait();          // BLOCKING
//t2.Wait();          // BLOCKING
//t3.Wait();          // BLOCKING

#endregion

#region RunSynchronously()

Task t = new Task(() =>
{
    Console.WriteLine("Start");
    Thread.Sleep(1000);
    Console.WriteLine("End");
});

// t.Start();                       // async call
t.RunSynchronously();               // sync call

Console.WriteLine("FROM MAIN");
Console.ReadLine();

#endregion
