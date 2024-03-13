using DZ_Lab1;

namespace Dz_Lab1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter the seed: ");

            int seed = int.Parse(Console.ReadLine());
            Console.Write("Enter backpack size: ");
            int backback_size = int.Parse(Console.ReadLine());
            Console.Write("Enter number of items: ");
            int number_of_items = int.Parse(Console.ReadLine());

            Backpack backpack = new Backpack(seed, backback_size, number_of_items);
            backpack.Display_items();
            Console.WriteLine(backpack);
            backpack.Solve();
            Console.WriteLine(backpack);




        }

    }
}