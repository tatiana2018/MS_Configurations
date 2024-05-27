using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace Shared.Logging
{
    public class SqlLogger : ILogger
    {
        private readonly string  _connectionString;

        public SqlLogger(string connectionString)
        {
            _connectionString = connectionString;
        }
        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (formatter != null)
            {
                // Aquí es donde guardas la excepción en la base de datos
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = @"
                    INSERT INTO Log (Message, Level, EventId, EventName, Timestamp) 
                    VALUES (@message, @level, @eventId, @eventName, @timestamp)";
                        command.Parameters.AddWithValue("@message", formatter(state, exception));
                        command.Parameters.AddWithValue("@level", logLevel.ToString());
                        command.Parameters.AddWithValue("@eventId", eventId.Id);
                        if (string.IsNullOrEmpty(eventId.Name))
                        {
                            command.Parameters.AddWithValue("@eventName", DBNull.Value);
                        } else
                        {
                            command.Parameters.AddWithValue("@eventName", eventId.Name);
                        }
                        command.Parameters.AddWithValue("@timestamp", DateTime.UtcNow);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
