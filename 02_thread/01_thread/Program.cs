
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





bool done = false;

void Run()
{
    if (!done)
    {
        // Console.WriteLine("+");
        done = true;
        Console.WriteLine("DONE");
    }
}

new Thread(Run).Start();
Run();


#endregion



