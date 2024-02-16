using System.Runtime.Serialization;

namespace BO
{
    [Serializable]
    internal class BlNotFitSchedule : Exception
    {
        public BlNotFitSchedule()
        {
        }

        public BlNotFitSchedule(string? message) : base(message)
        {
        }

        public BlNotFitSchedule(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected BlNotFitSchedule(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}