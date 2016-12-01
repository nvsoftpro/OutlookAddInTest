namespace SecuredMail.Logger
{
    /// <summary>
    /// Extension for ILogger
    /// </summary>
    public static class LoggerExtensionMethod
    {
        public static void Message(this ILogger logger, string message, params object[] arg)
        {
            Logger.AppendLogMessage(message, arg);
        }
    }
}