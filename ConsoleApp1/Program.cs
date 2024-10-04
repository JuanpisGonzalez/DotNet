namespace ConsoleApp1
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var task1 = new Task(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("Asyncronous task 1");
            });

            task1.Start();


            var task2 = new Task(() =>
            {
                Thread.Sleep(100);
                Console.WriteLine("Asyncronous task 2");
            });

            task2.Start();
            Console.WriteLine("Doing another things...");
            await task1;
            await task2;

            int resultRandom = await RandomAsync(4);
            Console.WriteLine($"{resultRandom}");
            Console.WriteLine("Task finished");
            Console.ReadLine();

        }
        public static async Task<int> RandomAsync(int num)
        {
            var task = new Task<int>(() =>

                new Random().Next() * num
            );

            task.Start();
            int result = await task;
            return result;
        }
    }
}
