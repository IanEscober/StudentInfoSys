namespace StudentInfoSys.Domain.Interfaces.Logging
{
    public interface IBaseLogger<T>
    {
        void LogInfo(string message, params object[] args);
        void LogWarn(string message, params object[] args);
    }
}
