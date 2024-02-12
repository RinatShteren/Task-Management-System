namespace BlImplementation;
using BlApi;

internal class Bl : IBl
{
    public IEngineer Engineer => new EngineerImplementation();

    public ITask Task => new TaskImplementation();

    public IEngineer Student => throw new NotImplementedException();

    public ITask Course => throw new NotImplementedException();
}
