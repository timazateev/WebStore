using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Reflection;
using System.Xml;

namespace WebStore.Logger
{
    public static class Log4NetLoggerFactoryExtensions
    {
        private static string CheckFilePath(string FilePath)
        {
            //if(!(FilePath != null && FilePath.Length > 0))
            if (FilePath is not { Length: > 0 })
                throw new ArgumentException("Указан некорректный путь к файлу", nameof(FilePath));

            if (Path.IsPathRooted(FilePath)) return FilePath;

            var assembly = Assembly.GetEntryAssembly();
            var dir = Path.GetDirectoryName(assembly!.Location);
            return Path.Combine(dir!, FilePath);
        }

        public class Log4NetLoggerProvider : ILoggerProvider
        {
            private readonly string _ConfigurationFile;
            private readonly ConcurrentDictionary<string, Log4NetLogger> _Loggers = new();

            public Log4NetLoggerProvider(string ConfigurationFile) => _ConfigurationFile = ConfigurationFile;

            public ILogger CreateLogger(string Category) =>
                _Loggers.GetOrAdd(Category, category =>
                {
                    var xml = new XmlDocument();
                    xml.Load(_ConfigurationFile);
                    return new Log4NetLogger(category, xml["log4net"]);
                });

            public void Dispose() => _Loggers.Clear();
        }
    }
}
