using OrderConsole.Services;
using System;

namespace OrderConsole
{
    public class Program
    {
        public static Guid AgentId;
        public static int MagicNumber;
        public static void Main(string[] args)
        {
            AgentId = Guid.NewGuid();
            MagicNumber = new Random().Next(1, 10);

            Console.WriteLine($"I’m agent {AgentId}, my magic number is {MagicNumber}");

            OrderService orderService = new OrderService(AgentId, MagicNumber);
            orderService.ProcessAndSaveOrder().ConfigureAwait(false);
            
            Console.WriteLine("Done Processing Orders in queue");
            Console.ReadKey();

        }
    }
}
