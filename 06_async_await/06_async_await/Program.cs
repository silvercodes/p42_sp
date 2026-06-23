
// return: Task, Task<T>, void (:-(((), ValueTask, ValueTask<T>, IAsyncAnumerable<T>, IAcyncEnumerator<T>....

//async Task MethodAsync()
//{
//    Console.WriteLine("Start");

//    Task t = new Task(() => Thread.Sleep(1000));
//    t.Start();

//    // t.Wait();
//    await t;

//    Console.WriteLine("End");
//}

//Console.WriteLine("Main start");

//Task t = MethodAsync();

//Console.WriteLine("CONTINUE");
////
////
////
//await t;






async Task DownloadAsync (string url)
{
    HttpClient client = new HttpClient();
    string content = await client.GetStringAsync(url);
    //
    //
    Console.WriteLine(content);
}

_ = DownloadAsync("https://habr.com/ru/feed/");

Console.ReadLine();

