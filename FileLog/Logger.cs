using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace FileLog
{
    public static class Logger
    {
        readonly static ReaderWriterLockSlim LogWriteLock =
            new ReaderWriterLockSlim();

        public static string FilePath { get; set; } = "log.txt";

        public static void Init(string filePath)
        {
            FilePath = filePath;
        }


        public static void Trace(string content) =>
            Writer(Model.LogLevel.Trace, content);

        public static void Information(string content)
            => Writer(Model.LogLevel.Information, content);

        public static void Critical(string content)
            => Writer(Model.LogLevel.Critical, content);

        public static void Debug(string content)
            => Writer(Model.LogLevel.Debug, content);

        public static void Warning(string content)
            => Writer(Model.LogLevel.Warning, content);

        public static void Error(string content)
            => Writer(Model.LogLevel.Error, content);


        public static void Writer(Model.LogLevel level, string content)
        {
            LogWriteLock.EnterWriteLock();
            var logText = string.Format("[{0}] [{1}] {2}\n",
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:dd"),
                    level.ToString(),
                    content
                );
            try
            {
                File.AppendAllText(FilePath, logText);
            }
            catch (Exception)
            {

            }
            finally
            {
                LogWriteLock.ExitWriteLock();
            }
        }

    }
}
