public static class Logger
{
    private static readonly string LogFilePath = "./LOG.json";

    public static void LogException(string exceptionMessage)
    {
        Log($"EXCEPTION: {exceptionMessage}");
    }

    public static void Log(string message)
    {
        try
        {
            if (!File.Exists(LogFilePath))
            {
                using (StreamWriter writer = File.CreateText(LogFilePath))
                {
                    writer.WriteLine("LOG FILE CREATED");
                }
            }

            
            using (StreamWriter writer = File.AppendText(LogFilePath))
            {
                writer.WriteLine($"{DateTime.Now}: {message}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to write to log file: {ex.Message}");
        }
    }
}