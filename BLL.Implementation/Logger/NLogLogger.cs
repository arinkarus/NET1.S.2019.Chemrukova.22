using BLL.Interface.Logger;

namespace BLL.Implementation.Logger
{
    /// <summary>
    /// NLogger Adapter.
    /// </summary>
    public class NLogLogger : ILogger
    {
        /// <summary>
        /// NLog logger.
        /// </summary>
        private readonly NLog.Logger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="NLogLogger" /> class.
        /// </summary>
        public NLogLogger()
        {
            logger = NLog.LogManager.GetCurrentClassLogger();
        }

        public void Debug(string message)
        {
            logger.Debug(message);
        }

        public void Error(string message)
        {
            logger.Debug(message);
        }

        public void Fatal(string message)
        {
            logger.Debug(message);
        }

        public void Info(string message)
        {
            logger.Info(message);
        }

        public void Warn(string message)
        {
            logger.Info(message);
        }
    }
}
