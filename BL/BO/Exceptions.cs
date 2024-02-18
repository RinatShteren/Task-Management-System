
using System.Runtime.Serialization;

namespace BO;
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

[Serializable]
public class BlNotVaildException : Exception
{
    public BlNotVaildException() : base() { }
    public BlNotVaildException(string message) : base(message) { }
    public BlNotVaildException(string message, Exception innerException) : base(message, innerException) { }
    //protected BlNotVaildException(SerializationInfo info, StreamingContext contex) : base(info, contex) { }
}

[Serializable]
public class BlAlreadyExistsException : Exception
{
    public BlAlreadyExistsException() : base() { }
    public BlAlreadyExistsException(string message) : base(message) { }
    public BlAlreadyExistsException(string message, Exception innerException) : base(message, innerException) { }
  //  protected BlAlreadyExistsException(SerializationInfo info, StreamingContext contex) : base(info, contex) { }
}

[Serializable]
public class BlDoesNotExistException : Exception
{
    public BlDoesNotExistException() : base() { }
    public BlDoesNotExistException(string message) : base(message) { }
    public BlDoesNotExistException(string message, Exception innerException) : base(message, innerException) { }
    //protected BlDoesNotExistException(SerializationInfo info, StreamingContext contex) : base(info, contex) { }
}


[Serializable]
public class BlDeletionImpossible : Exception
{
    public BlDeletionImpossible() : base() { }
    public BlDeletionImpossible(string message) : base(message) { }
    public BlDeletionImpossible(string message, Exception innerException) : base(message, innerException) { }
    //protected BlDeletionImpossible(SerializationInfo info, StreamingContext contex) : base(info, contex) { }
}
/// <summary>
/// The exception will be thrown when attempting to do an operation prohibited by the schedule.
/// </summary>
[Serializable]


internal class DependenceTasksStartDateIsStillNull : Exception
{
    public DependenceTasksStartDateIsStillNull() { }
    public DependenceTasksStartDateIsStillNull(string? message) : base(message) { }
    public DependenceTasksStartDateIsStillNull(string? message, Exception? innerException) : base(message, innerException) { }
    protected DependenceTasksStartDateIsStillNull(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
   /* public class BlNotFitSchedule : Exception
    {
        public BlNotFitSchedule() : base() { }
        public BlNotFitSchedule(string message) : base(message) { }
        public BlNotFitSchedule(string message, Exception innerException) : base(message, innerException) { }
        // protected BlException(SerializationInfo info, StreamingContext contex) : base(info, contex) { }
    }*/

    [Serializable]
    public class BlXMLFileLoadCreateException : Exception
    {
        public BlXMLFileLoadCreateException() : base() { }
        public BlXMLFileLoadCreateException(string message) : base(message) { }
        public BlXMLFileLoadCreateException(string message, Exception innerException) : base(message, innerException) { }
        //protected BlXMLFileLoadCreateException(SerializationInfo info, StreamingContext contex) : base(info, contex) { }
    }


