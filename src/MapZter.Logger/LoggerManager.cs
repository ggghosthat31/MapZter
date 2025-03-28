using MapZter.Contracts.Interfaces.Logger;

using NLog;

namespace MapZter.Logger;
//These class represents logging logic with NLog library
public class LoggerManager : ILoggerManager
{
	private static ILogger logger = LogManager.GetCurrentClassLogger();

	public LoggerManager()
    {}

    public void LogInfo(string message) =>
		logger.Info(message);

	public void LogDebug(string message) => 
		logger.Debug(message);

	public void LogWarn(string message) =>
		logger.Warn(message);

	public void LogError(string message) => 
		logger.Error(message);

    public void LogError(Exception exception)
    {
        string message = $"Error message: {exception.Message};\n\nError trace:\n";
    
		foreach(var entry in exception.Data)
			message += $"{entry} : {exception.Data[entry]}";
		
		message += "\n";
		logger.Error(message);
	}
}