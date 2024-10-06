using System.Linq;

namespace Lambda
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //delegate>functionality of something
            Func<int, int> multi = (a) => a * 2;
            Console.WriteLine(multi(1));

            Func<int, int, int> sum = (a, b) => a + b;
            Console.WriteLine(sum(1, 2));

            Func<int, int, int> maj = (a, b) =>
            {
                if (a > b)
                {
                    return a;
                }
                else
                {
                    return b;
                }
            };
            Console.WriteLine(maj(1, 2));

            var numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            Func<int, bool> getPair = (number) => number % 2 == 0;

            var pairs = numbers.Where(getPair);
            var pairs2 = numbers.Where(n => n % 2 == 0);

            Func<Func<int, int>, int> test = (num) => num(10) + 5;
            Console.WriteLine(test(x => x * 2));

            Func<Func<int, int>, int, int> test2 = (owo, num2) => owo(num2) + 5;
            Console.WriteLine(test2(x => x * 2, 2));

            Action<int> print = (number) => Console.Write(number + ",");//Action is a void lambda function
            print(int.Parse("452"));

            Func<List<int>, List<int>> getPairs = (list) =>
            {
                return list.Where(x => x % 2 == 0).ToList();
            };

            Console.WriteLine();
            foreach (int num in getPairs(new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 10 }))
            {
                //Console.WriteLine(num);
                print(num);
            }

            Func<Func<int, int>, int> test3 = (owo) =>
            {
                return owo(19) + 5;
                };
            Console.WriteLine(test2(x => x * 2, 2));

        }
    }
}
