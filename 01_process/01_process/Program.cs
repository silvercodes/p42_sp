
#region Process
// 1. Memory scope (Адресное пространство)
// 2. Thread (Поток)
// 3. Системные ресурсы
// 4. Исполняемый код
// 5. Контекст безопасности
// 6. Идентификатор
// 7. Переменные окружения
// 8. Приоритет



using System.Diagnostics;


//Process[] processes = Process.GetProcesses();
//var proc = processes.OrderBy(p => p.Id);

//foreach (Process p in proc)
//{
//    Console.WriteLine($"pid: {p.Id} {p.ProcessName}");
//}



//try
//{
//	Process p = GetProcessFromUser();
//	Console.WriteLine($"{p.Id}\t{p.ProcessName}\t{p.BasePriority}\t{p.StartTime}");
//}
//catch (Exception ex)
//{
//    Console.WriteLine($"ERROR: {ex.Message}");
//}


try
{
    Process p = GetProcessFromUser();
    ProcessThreadCollection threads = p.Threads;
    foreach(ProcessThread t in threads)
    {
        Console.WriteLine($"{t.Id}\t{t.StartTime.ToShortTimeString()}\t{t.PriorityLevel}\t{p.VirtualMemorySize64}");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"ERROR: {ex.Message}");
}








Process GetProcessFromUser()
{
    Console.Write("Enter PID: ");
    string? input = Console.ReadLine();

    if (input is null)
        throw new Exception("Invalid input");

    int pid = int.Parse(input);

    return Process.GetProcessById(pid);
}



#endregion


