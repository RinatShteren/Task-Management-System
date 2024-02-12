namespace BlApi;
public static class Factory
{
    static IBl Get() => new BlImplementation.Bl();
}
