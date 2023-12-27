
namespace stage0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            welcome2159();
            welcome6722();
            Console.ReadKey();
        }

        private static void Welcome6722()
        {
            throw new NotImplementedException();
        }

        private static void welcome2159()
        {
            Console.WriteLine("Enter your name");
            string name = Console.ReadLine();
            Console.WriteLine("{0},welcome to my first console application", name);
        }
    }
}
 