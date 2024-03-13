using DZ_Lab1;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;

namespace Lab1_UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test_if_at_lest_one_added_when_possible()
        {
            

            
            const int backpackSize = 11; // Be sure its more than upper weight limit in init_items
            const int numberOfItems = 10;
            

            List<int> seed = new List<int> { 1234, 255, 345, 455, 456, 666, 667, 999, 123456, 76543, 3456, 87654, 2345, 123456, 4567, 678};



            for (int i = 0; i < seed.Count; i++)
            {
                Backpack backpack = new Backpack(seed[i], backpackSize, numberOfItems);

                backpack.Solve();

                try
                {
                    Assert.IsTrue(backpack.Result.Count >= 1);
                    Console.WriteLine($"Test {i + 1} passed");
                }
                catch (AssertFailedException ex)
                {
                    Console.WriteLine($"Test {i + 1} failed");
                    throw ex;
                }
            }

        }

        [TestMethod]
        public void Test_if_return_empty_when_cant_add_item()
        {



            const int backpackSize = 0; // Be sure its less than lower weight limit in init_items
            const int numberOfItems = 10; 


            List<int> seed = new List<int> { 1234, 255, 345, 455, 456, 666, 667, 999, 123456, 76543, 3456, 87654, 2345, 123456, 4567, 678 };



            for (int i = 0; i < seed.Count; i++)
            {
                Backpack backpack = new Backpack(seed[i], backpackSize, numberOfItems);

                backpack.Solve();

                try
                {
                    Assert.IsTrue(backpack.Result.Count == 0);
                    Console.WriteLine($"Test {i + 1} passed");
                }
                catch (AssertFailedException ex)
                {
                    Console.WriteLine($"Test {i + 1} failed");
                    throw ex;
                }
            }

        }
        [TestMethod]
        public void Check_if_order_of_items_changes_result()
        {



            const int backpackSize = 20; // Be sure its more than upper weight limit in init_items
            const int numberOfItems = 10;


            List<int> seed = new List<int> { 1234, 255, 345, 455, 456, 666, 667, 999, 123456, 76543, 3456, 87654, 2345, 123456, 4567, 678 };



            for (int i = 0; i < seed.Count; i++)
            {
                Backpack backpack1 = new Backpack(seed[i], backpackSize, numberOfItems);
                Backpack backpack2 = new Backpack(seed[i], backpackSize, numberOfItems);

                FieldInfo fieldInfo = typeof(Backpack).GetField("Items", BindingFlags.NonPublic | BindingFlags.Instance);

                List<(int id, int weight, int value)> privateFieldValue = (List<(int id, int weight, int value)>)fieldInfo.GetValue(backpack1);

                var tempVal = privateFieldValue[privateFieldValue.Count() - 1];

                privateFieldValue[privateFieldValue.Count() - 1] = privateFieldValue[privateFieldValue.Count()/2];
                privateFieldValue[privateFieldValue.Count() / 2] = tempVal;

                tempVal = privateFieldValue[privateFieldValue.Count() / 2];

                privateFieldValue[privateFieldValue.Count() / 2] = privateFieldValue[0];
                privateFieldValue[0] = tempVal;



                backpack1.Solve();
                backpack2.Solve();


                try
                {
                    CollectionAssert.AreEqual(backpack1.Result, backpack2.Result);
                    Console.WriteLine($"Test {i + 1} passed");
                }
                catch (AssertFailedException ex)
                {
                    Console.WriteLine($"Test {i + 1} failed code: " + ex );
                    Console.WriteLine(backpack1);
                    Console.WriteLine(backpack2);
                    throw ex;
                }
            }

        }
        [TestMethod]
        public void Check_results_result_for_specific_values()
        {
            const int backpackSize = 5; // Be sure its more than upper weight limit in init_items
            const int numberOfItems = 5;
            const int seed = 1234; // not important we will asign items by force


            Backpack backpack = new Backpack(seed, backpackSize, numberOfItems);

            FieldInfo fieldInfo = typeof(Backpack).GetField("Items", BindingFlags.NonPublic | BindingFlags.Instance);

            List<(int id, int weight, int value)> privateFieldValue = (List<(int id, int weight, int value)>)fieldInfo.GetValue(backpack);

            privateFieldValue[0] = (2, 1, 9);
            privateFieldValue[1] = (1, 1, 9);
            privateFieldValue[2] = (3, 5, 5);
            privateFieldValue[3] = (4, 9, 1);
            privateFieldValue[4] = (5, 4, 9);

            backpack.Solve();
            

            try
            {
                Assert.IsTrue(backpack.Result.Count == 2);
                Assert.IsTrue(backpack.Result[0].id == 1);
                Assert.IsTrue(backpack.Result[1].id == 2);
                Console.WriteLine("Test passed");
            }
            catch (AssertFailedException ex)
            {
                Console.WriteLine("Test failed");
                throw ex;
            }
        }

        [TestMethod]
        public void Check_if_negative_backpack_size_crashes()
        {
            const int numberOfItems = 5;
            const int seed = 1234; // not important we will asign items by force

            for (int backpackSize = -100; backpackSize < 1; backpackSize+= 3)
            {
                Backpack backpack = new Backpack(seed, backpackSize, numberOfItems);

                try
                {
                    backpack.Solve();
                    Console.WriteLine("Test passed");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Test failed");
                    throw ex;
                }
            }
        }

        [TestMethod]
        public void Check_if_negative_number_of_items_crashes()
        {
            const int backpackSize = 5;
            const int seed = 1234; // not important we will asign items by force

            for (int numberOfItems = -100; numberOfItems < 1; numberOfItems ++)
            {
                Backpack backpack = new Backpack(seed, backpackSize, numberOfItems);

                try
                {
                    backpack.Solve();
                    Console.WriteLine("Test passed");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Test failed");
                    throw ex;
                }
            }
        }

        [TestMethod]
        public void Check_if_seed_gives_repdoducible_results()
        {
            const int backpackSize = 20;
            const int numberOfItems = 20;
            List<int> seed = new List<int> { 1234, 255, 345, 455, 456, 666, 667, 999, 123456, 76543, 3456, 87654, 2345, 123456, 4567, 678 };

            for(int i = 0; i<seed.Count(); i++)
            {
                Backpack backpack1 = new Backpack(seed[i], backpackSize, numberOfItems);
                Backpack backpack2 = new Backpack(seed[i], backpackSize, numberOfItems);

                FieldInfo fieldInfo1 = typeof(Backpack).GetField("Items", BindingFlags.NonPublic | BindingFlags.Instance);

                List<(int id, int weight, int value)> privateFieldValue1 = (List<(int id, int weight, int value)>)fieldInfo1.GetValue(backpack1);

                FieldInfo fieldInfo2 = typeof(Backpack).GetField("Items", BindingFlags.NonPublic | BindingFlags.Instance);

                List<(int id, int weight, int value)> privateFieldValue2 = (List<(int id, int weight, int value)>)fieldInfo2.GetValue(backpack2);


                try
                {
                    CollectionAssert.AreEqual(privateFieldValue2, privateFieldValue1);
                    Console.WriteLine($"Test {i + 1} passed");
                }
                catch (AssertFailedException ex)
                {
                    Console.WriteLine($"Test {i + 1} failed code: " + ex);
                    Console.WriteLine(backpack1);
                    Console.WriteLine(backpack2);
                    throw ex;
                }
            }
        }
        [TestMethod]
        public void ToString_ReturnsCorrectFormat()
        {
            
            var backpack = new Backpack(seed: 123, backpack_size: 10, number_of_items: 0);

            backpack.Solve(); 

            var expectedResultForEmpty = "Backpack Results:\nNo result\n";

            var resultForEmpty = backpack.ToString();
            try
            {
                Assert.AreEqual(expectedResultForEmpty, resultForEmpty);
                Console.WriteLine("Test passed");
            }
            catch (AssertFailedException ex)
            {
                Console.WriteLine("Test failed");
                throw ex;
            }
            

            backpack = new Backpack(seed: 123, backpack_size: 5, number_of_items: 3);

            FieldInfo fieldInfo = typeof(Backpack).GetField("Items", BindingFlags.NonPublic | BindingFlags.Instance);

            List<(int id, int weight, int value)> privateFieldValue = (List<(int id, int weight, int value)>)fieldInfo.GetValue(backpack);

            privateFieldValue[0] = (2, 1, 9);
            privateFieldValue[1] = (1, 1, 9);
            privateFieldValue[2] = (3, 5, 5);
            
            
            backpack.Solve();
            
            var expectedResultForSelectedItems = "Backpack Results:\n" +
                                            "Item 1: Weight = 1, Value = 9\n" +
                                            "Item 2: Weight = 1, Value = 9\n" +
                                            "Total Weight Stored: 2\n" +
                                            "Total Value Stored: 18\n";
            var resultForSelectedItems = backpack.ToString();

            try
            {
                Assert.AreEqual(expectedResultForSelectedItems, resultForSelectedItems);
                Console.WriteLine("Test passed");
            }
            catch (AssertFailedException ex)
            {
                Console.WriteLine("Test failed");
                throw ex;
            }
            

        }



    }
}