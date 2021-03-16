using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using BeerPartner.Application.Interfaces;

namespace BeerPartner.Application
{
    public class Logger : ILogger, IDisposable
    {
        private readonly TextWriter _defaultConsoleWriter;

        public Logger() 
        { 
            _defaultConsoleWriter = Console.Out;
        }

        public Logger(TextWriter writer)
        {
            _defaultConsoleWriter = Console.Out;
            Console.SetOut(writer);
        }

        public void Info(string message, object data = null)
        {
            Console.WriteLine(FormatLog(message, LogLevel.Info, data));
        }
        
        public void Error(string message, Exception exception = null)
        {
            var data = exception != null 
                ? new 
                    {
                        Message = exception.Message,
                        StackTrace = exception.StackTrace
                    }
                : null;

            Console.WriteLine(FormatLog($"ERROR: {message}", LogLevel.Error, data));
        }

        private string FormatLog(string message, LogLevel level, object data)
        {
            var obj = new 
            {
                Level = level.ToString(),
                Message = message,
                Data = data
            };
            
            var option = new JsonSerializerOptions 
            { 
                WriteIndented = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            return JsonSerializer.Serialize(obj, option);
        }

        public void Dispose()
        {
            Console.SetOut(_defaultConsoleWriter);
        }
    }

    enum LogLevel
    {
        Info,
        Error
    }
}