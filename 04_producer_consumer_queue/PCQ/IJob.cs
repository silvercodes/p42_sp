namespace PCQ;

public interface IJob
{
    public void Execute();
    public string GetInfo();            // FIXME: for debug
}
