using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("Lab1_UnitTest"), InternalsVisibleTo("Lab1_GUI")]
namespace DZ_Lab1
{
    internal class Backpack
    {
        List<(int id, int weight, int value)> Items = new List<(int id, int weight, int value)>();
        public List<(int id, int weight, int value)> Result = new List<(int id, int weight, int value)>();
        int number_items;
        int backpack_capacity;
        int value_stored;
        int weight_stored;


        public Backpack(int seed, int backpack_size, int number_of_items) {

            Random random = new Random(seed);
            number_items = number_of_items;
            backpack_capacity = backpack_size;

            Init_items(ref random, number_of_items);
        }


        private void Init_items(ref Random random, int number_of_items)
        {
            for (int i = 0; i < number_of_items; i++)
            {
                int weight = random.Next(1, 10);
                int value = random.Next(1, 10);
                Items.Add(( i+1 ,weight, value));
            }
        }
        public void Display_items()
        {
            for (int i = 0; i < Items.Count; i++)
            {
                Console.Write("Item id: ");
                Console.Write(Items[i].id);
                Console.Write("   weight: ");
                Console.Write(Items[i].weight);
                Console.Write("   value: ");
                Console.Write(Items[i].value);
                Console.Write("\n");
            }
            
        }

        public string Items_string()
        {
            string result = "";

            for (int i = 0; i < Items.Count; i++)
            {
                result += $"Item id: {Items[i].id}   weight: {Items[i].weight}   value: {Items[i].value}\n";
            }

            return result;
        }

        public override string ToString()
        {
            string result = "Backpack Results:\n";
            if (Result.Count() == 0)
            {
                result += "No result\n";
                return result;
            }
            for (int i = 0; i < Result.Count; i++)
            {
                result += $"Item {Result[i].id}: Weight = {Result[i].weight}, Value = {Result[i].value}\n";
            }
            result += $"Total Weight Stored: {weight_stored}\n";
            result += $"Total Value Stored: {value_stored}\n";
            return result;
        }

        public void Solve()
        {
            //Items = Items.OrderBy(x => x.id).ToList();
            List<(int index, double efficiency)> itemEfficiency = new List<(int index, double efficiency)>();
            for (int i = 0; i < number_items; i++)
            {
                itemEfficiency.Add(( i , (double)Items[i].value / Items[i].weight));
            }

            itemEfficiency.Sort((x, y) => y.efficiency.CompareTo(x.efficiency));

            for (int i = 0; i < number_items; i++)
            {
                if (backpack_capacity - Items[itemEfficiency[i].index].weight >= 0)
                {
                    backpack_capacity -= Items[itemEfficiency[i].index].weight;
                    Result.Add((Items[itemEfficiency[i].index].id, Items[itemEfficiency[i].index].weight, Items[itemEfficiency[i].index].value));
                    value_stored += Items[itemEfficiency[i].index].value;
                    weight_stored += Items[itemEfficiency[i].index].weight;
                }
            }

            Result = Result.OrderByDescending(x => x.value).ThenBy(x => x.id).ToList();
        }
    }
}
