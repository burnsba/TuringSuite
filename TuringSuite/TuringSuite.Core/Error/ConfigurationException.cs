using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace TuringSuite.Core.Error
{
    /// <summary>
    /// Exception thrown during initial setup.
    /// </summary>
    public class ConfigurationException : Exception
    {
        public int ErrorCode { get; set; }

        public ConfigurationException()
        {
        }

        public ConfigurationException(string message) : base(message)
        {
        }

        public ConfigurationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ConfigurationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
