
namespace DO;

public class DalDoesNotExistException : Exception //חריגה עבור ישות עם מספר מזהה שלא קיים ברשימה
{
    public DalDoesNotExistException(string? message) : base(message) { }
}

public class DalAlreadyExistsException : Exception //חריגה עבור ישות עם מספר מזהה שכבר קיים ברשימה
{
    public DalAlreadyExistsException(string? message) : base(message) { }
}
public class DalDeletionImpossible : Exception //חריגה עבור ניסיון למחיקת אובייקט שאסור במחיקה

{
    public DalDeletionImpossible(string? message) : base(message) { }
}
public class NullReferenceException : Exception //object can not be null 

{
    public NullReferenceException(string? message) : base(message) { }
}
