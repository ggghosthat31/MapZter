using MapZter.Contracts.Interfaces.Logger;

using NLog;

namespace MapZter.Logger;
//These class represents logging logic with NLog library
public class LoggerManager : ILoggerManager
{
	private static ILogger logger = LogManager.GetCurrentClassLogger();

	public LoggerManager()
    {}

	public void LogDebug(string message) => 
		logger.Debug(message);

	public void LogError(string message) => 
		logger.Error(message);

	public void LogInfo(string message) =>
		logger.Info(message);

	public void LogWarn(string message) =>
		logger.Warn(message);
}