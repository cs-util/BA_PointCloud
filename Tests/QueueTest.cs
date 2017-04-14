﻿using System;
using NUnit.Framework;
using NUnitLite;
using DataStructures;

namespace tests
{

    [TestFixture]
    class QueueTest
    {

        public static void Main(String[] args)
        {
            new AutoRun().Execute(args);
            Console.ReadKey();
        }

        [Test]
        public void TestExampleDictionaryQueue()
        {
            PriorityQueue<double, int> queue = new DictionaryQueue<double, int>();
            int[] elements = new int[] { 4, 9, 8, 7, 3, 22, 4, 9, -20, -40, -100, 102, 4, 5 };
            for (int i = 0; i < elements.Length; i++)
            {
                queue.Enqueue(elements[i], elements[i]);
            }
            Assert.AreEqual(queue.Count, elements.Length);
            int[] expected = new int[] { 102, 22, 9, 9, 8, 7, 5, 4, 4, 4, 3, -20, -40, -100 };
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], queue.Peek());
                Assert.AreEqual(expected[i], queue.Dequeue());
            }
            Assert.AreEqual(queue.Count, 0);
        }

        [Test]
        public void TestEmptyDictionaryQueue()
        {
            PriorityQueue<double, string> queue = new DictionaryQueue<double, string>();
            queue.Enqueue("1", 1);
            queue.Enqueue("2", 2);
            Assert.AreEqual(queue.Dequeue(), "2");
            Assert.AreEqual(queue.Dequeue(), "1");
            Assert.AreEqual(queue.Dequeue(), null);
            Assert.AreEqual(queue.Dequeue(), null);
            queue.Enqueue("3", 3);
            Assert.AreEqual(queue.Dequeue(), "3");
        }

        [Test]
        public void TestEnumeratorDictionaryQueue()
        {
            PriorityQueue<double, int> queue = new DictionaryQueue<double, int>();
            int[] elements = new int[] { 4, 9, 8, 7, 3, 22, 4, 9, -20, -40, -100, 102, 4, 5 };
            for (int i = 0; i < elements.Length; i++)
            {
                queue.Enqueue(elements[i], elements[i]);
            }
            int[] expected = new int[] { 102, 22, 9, 9, 8, 7, 5, 4, 4, 4, 3, -20, -40, -100 };
            int j = 0;
            foreach (int element in queue)
            {
                Assert.AreEqual(expected[j], element);
                j++;
            }
        }

        [Test]
        public void TestEnumeratorWhileChangingDictionaryQueue()
        {
            PriorityQueue<double, int> queue = new DictionaryQueue<double, int>();
            int[] elements = new int[] { 4, 9, 8, 7, 3, 22, 4, 9, -20, -40, -100, 102, 4, 5 };
            for (int i = 0; i < elements.Length; i++)
            {
                queue.Enqueue(elements[i], elements[i]);
            }
            int[] expected = new int[] { 102, 22, 9, 9, 8, 7, 5, 4, 4, 4, 3, -20, -40, -100 };
            int j = 0;
            foreach (int element in queue)
            {
                Assert.AreEqual(expected[j], element);
                j++;
                if ((j + 1) % 3 == 0)
                {
                    queue.Dequeue();
                }
            }
        }

        [Test]
        public void TestSamePrioritiesDictionaryQueue()
        {
            PriorityQueue<int, string> queue = new DictionaryQueue<int, string>();
            int[] keys = { 1, 3, 5, 3, 4, 3, 2, 4 };
            string[] vals = { "1", "3a", "5", "3b", "4a", "3c", "2", "4b" };
            for (int i = 0; i < keys.Length; i++)
            {
                queue.Enqueue(vals[i], keys[i]);
            }
            Assert.AreEqual(queue.Dequeue(), "5");
            string first4 = queue.Dequeue();
            Assert.IsTrue(first4.StartsWith("4"));
            string second4 = queue.Dequeue();
            Assert.IsTrue(second4.StartsWith("4"));
            Assert.AreNotEqual(first4, second4);
            string first3 = queue.Dequeue();
            Assert.IsTrue(first3.StartsWith("3"));
            string second3 = queue.Dequeue();
            Assert.IsTrue(second3.StartsWith("3"));
            string third3 = queue.Dequeue();
            Assert.IsTrue(third3.StartsWith("3"));
            Assert.AreEqual(queue.Dequeue(), "2");
            Assert.AreEqual(queue.Dequeue(), "1");
        }

        [Test]
        public void TestRemoveWithPriorityDictionaryQueue()
        {
            PriorityQueue<int, int> queue = new DictionaryQueue<int, int>();
            int[] keys =    new int[] { 4, 9, 8,  7, 3, 22, 4,  9, -20, -40, -100, 102, 4, 5 };
            int[] values =  new int[] { 4, 9, 8, 69, 3, 22, 9,  9, -20, -40,    3, 102, 4, 5 };
            for (int i = 0; i < keys.Length; i++)
            {
                queue.Enqueue(values[i], keys[i]);
            }
            queue.Remove(22, 22);
            queue.Remove(4, 4);
            queue.Remove(9, 4);
            queue.Remove(69, 7);
            queue.Remove(3, -100); //<- Doesn't exist
            queue.Remove(102, 102);
            int[] expected = new int[] { 9, 9, 8, 5, 4, 3, -20, -40 };
            foreach (int el in expected)
            {
                Assert.AreEqual(el, queue.Dequeue());
            }
        }

        [Test]
        public void TestRemoveWithoutPriorityDictionaryQueue()
        {
            PriorityQueue<int, int> queue = new DictionaryQueue<int, int>();
            int[] keys =   new int[] { 4, 9, 8, 7, 3, 22, 4, 9, -20, -40, -100, 102, 4, 5 };
            int[] values = new int[] { 4, 9, 8, 7, 3, 22, 4, 9, -20, -40, -100, 102, 0, 5 };
            for (int i = 0; i < keys.Length; i++)
            {
                queue.Enqueue(values[i], keys[i]);
            }
            queue.Remove(22);
            queue.Remove(0);
            queue.Remove(9);
            queue.Remove(9);
            queue.Remove(6); //<- Doesn't exist
            queue.Remove(102);
            int[] expected = new int[] {8, 7, 5, 4, 4, 3, -20, -40, -100 };
            foreach (int el in expected)
            {
                Assert.AreEqual(el, queue.Dequeue());
            }
        }

        [Test]
        public void TestExampleListQueue()
        {
            PriorityQueue<int, int> queue = new ListPriorityQueue<int, int>();
            int[] elements = new int[] { 4, 9, 8, 7, 3, 22, 4, 9, -20, -40, -100, 102, 4, 5 };
            for (int i = 0; i < elements.Length; i++)
            {
                queue.Enqueue(elements[i], elements[i]);
            }
            Assert.AreEqual(queue.Count, elements.Length);
            int[] expected = new int[] { 102, 22, 9, 9, 8, 7, 5, 4, 4, 4, 3, -20, -40, -100 };
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], queue.Peek());
                Assert.AreEqual(expected[i], queue.Dequeue());
            }
            Assert.AreEqual(queue.Count, 0);
        }

        [Test]
        public void TestEmptyListQueue()
        {
            PriorityQueue<int, string> queue = new ListPriorityQueue<int, string>();
            queue.Enqueue("1", 1);
            queue.Enqueue("2", 2);
            Assert.AreEqual(queue.Dequeue(), "2");
            Assert.AreEqual(queue.Dequeue(), "1");
            Assert.AreEqual(queue.Dequeue(), null);
            Assert.AreEqual(queue.Dequeue(), null);
            queue.Enqueue("3", 3);
            Assert.AreEqual(queue.Dequeue(), "3");
        }

        [Test]
        public void TestSparseListQueue()
        {
            PriorityQueue<int, string> queue = new ListPriorityQueue<int, string>();
            Assert.AreEqual(queue.Dequeue(), null);
            queue.Enqueue("1", 1);
            Assert.AreEqual(queue.Dequeue(), "1");
            queue.Enqueue("2", 2);
            Assert.AreEqual(queue.Dequeue(), "2");
            Assert.AreEqual(queue.Dequeue(), null);
            queue.Enqueue("3", 3);
            queue.Enqueue("4", 4);
            Assert.AreEqual(queue.Dequeue(), "4");
            Assert.AreEqual(queue.Dequeue(), "3");
        }

        [Test]
        public void TestEnumeratorListQueue()
        {
            PriorityQueue<int, int> queue = new ListPriorityQueue<int, int>();
            int[] elements = new int[] { 4, 9, 8, 7, 3, 22, 4, 9, -20, -40, -100, 102, 4, 5 };
            for (int i = 0; i < elements.Length; i++)
            {
                queue.Enqueue(elements[i], elements[i]);
            }
            int[] expected = new int[] { 102, 22, 9, 9, 8, 7, 5, 4, 4, 4, 3, -20, -40, -100 };
            int j = 0;
            foreach (int element in queue)
            {
                Assert.AreEqual(expected[j], element);
                j++;
            }
        }

        [Test]
        public void TestEnumeratorWhileChangingListQueue()
        {
            PriorityQueue<int, int> queue = new ListPriorityQueue<int, int>();
            int[] elements = new int[] { 4, 9, 8, 7, 3, 22, 4, 9, -20, -40, -100, 102, 4, 5 };
            for (int i = 0; i < elements.Length; i++)
            {
                queue.Enqueue(elements[i], elements[i]);
            }
            int[] expected = new int[] { 102, 22, 9, 9, 8, 7, 5, 4, 4, 4, 3, -20, -40, -100 };
            int j = 0;
            foreach (int element in queue)
            {
                Assert.AreEqual(expected[j], element);
                j++;
                if ((j + 1) % 3 == 0)
                {
                    queue.Dequeue();
                }
            }
        }

        [Test]
        public void TestSamePrioritiesListQueue()
        {
            PriorityQueue<int, string> queue = new ListPriorityQueue<int, string>();
            int[] keys = { 1, 3, 5, 3, 4, 3, 2, 4 };
            string[] vals = { "1", "3a", "5", "3b", "4a", "3c", "2", "4b" };
            for (int i = 0; i < keys.Length; i++)
            {
                queue.Enqueue(vals[i], keys[i]);
            }
            Assert.AreEqual(queue.Dequeue(), "5");
            string first4 = queue.Dequeue();
            Assert.IsTrue(first4.StartsWith("4"));
            string second4 = queue.Dequeue();
            Assert.IsTrue(second4.StartsWith("4"));
            Assert.AreNotEqual(first4, second4);
            string first3 = queue.Dequeue();
            Assert.IsTrue(first3.StartsWith("3"));
            string second3 = queue.Dequeue();
            Assert.IsTrue(second3.StartsWith("3"));
            string third3 = queue.Dequeue();
            Assert.IsTrue(third3.StartsWith("3"));
            Assert.AreEqual(queue.Dequeue(), "2");
            Assert.AreEqual(queue.Dequeue(), "1");
        }

        [Test]
        public void TestRemoveWithoutPriorityListQueue()
        {
            PriorityQueue<int, int> queue = new ListPriorityQueue<int, int>();
            int[] keys = new int[] { 4, 9, 8, 7, 3, 22, 4, 9, -20, -40, -100, 102, 4, 5 };
            int[] values = new int[] { 4, 9, 8, 7, 3, 22, 4, 9, -20, -40, -100, 102, 0, 5 };
            for (int i = 0; i < keys.Length; i++)
            {
                queue.Enqueue(values[i], keys[i]);
            }
            queue.Remove(22);
            queue.Remove(0);
            queue.Remove(9);
            queue.Remove(9);
            queue.Remove(6); //<- Doesn't exist
            queue.Remove(102);
            int[] expected = new int[] { 8, 7, 5, 4, 4, 3, -20, -40, -100 };
            foreach (int el in expected)
            {
                Assert.AreEqual(el, queue.Dequeue());
            }
        }

        [Test]
        public void TestRemoveWithPriorityListQueue()
        {
            PriorityQueue<int, int> queue = new ListPriorityQueue<int, int>();
            int[] keys = new int[] { 4, 9, 8, 7, 3, 22, 4, 9, -20, -40, -100, 102, 4, 5 };
            int[] values = new int[] { 4, 9, 8, 69, 3, 22, 9, 9, -20, -40, 3, 102, 4, 5 };
            for (int i = 0; i < keys.Length; i++)
            {
                queue.Enqueue(values[i], keys[i]);
            }
            queue.Remove(22, 22);
            queue.Remove(4, 4);
            queue.Remove(9, 4);
            queue.Remove(69, 7);
            queue.Remove(3, -100); //<- Doesn't exist
            queue.Remove(102, 102);
            int[] expected = new int[] { 9, 9, 8, 5, 4, 3, -20, -40 };
            foreach (int el in expected)
            {
                Assert.AreEqual(el, queue.Dequeue());
            }
        }

    }
}