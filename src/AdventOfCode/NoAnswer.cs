using SmartAss;
using System;
using System.Runtime.Serialization;

namespace Advent_of_Code
{
    public class NoAnswer : InvalidOperationException
    {
        public NoAnswer() : this("No answer was found.") => Do.Nothing();

        public NoAnswer(string message)
            : base(message) => Do.Nothing();

        public NoAnswer(string message, Exception innerException)
            : base(message, innerException) => Do.Nothing();

        protected NoAnswer(SerializationInfo info, StreamingContext context)
            : base(info, context) => Do.Nothing();
    }
}
