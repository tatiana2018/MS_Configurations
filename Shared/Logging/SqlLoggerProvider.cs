using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Logging
{
    public class SqlLoggerProvider : ILoggerProvider
    {
        private readonly string _connectionString;

        public  SqlLoggerProvider(string connectionString)
        {
            _connectionString = connectionString;
        }
        public ILogger CreateLogger(string categoryName)
        {
            return new SqlLogger(_connectionString);
        }

        public void Dispose() {}
    }
}
