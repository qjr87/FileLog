# FileLog
C#轻量级文件日志

初始化
FileLog.Logger.Init("log.txt");

使用
Logger.Writer(Model.LogLevel.Error, "Error")
Logger.Information("info");
