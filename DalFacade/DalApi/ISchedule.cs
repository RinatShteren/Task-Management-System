namespace DalApi
{
    public interface ISchedule
    {

        public DateTime? SetStartPro(DateTime startPro);
        public DateTime? GetStartPro();

        public DateTime? SetEndPro(DateTime endPro);
        public DateTime? GetEndPro();

     
    }
}