namespace DalApi
{
    public interface ISchedule
    {
         DateTime? StartProject { set; get; }

        void ResetDep();

    }
}