/* MinPriorityQueueTests.cs
 * Author: Rod Howell
 */
using NUnit.Framework;
using System;

namespace Ksu.Cis300.SudokuSolver.Tests
{
    /// <summary>
    /// Unit tests for MinPriorityQueue.
    /// </summary>
    [TestFixture]
    public class MinPriorityQueueTests
    {
        /// <summary>
        /// Tests that a new queue has a Count of 0.
        /// </summary>
        [Test, Timeout(1000), Category("A: Empty Queue Tests")]
        public void TestEmptyCount()
        {
            Assert.That(new MinPriorityQueue(), Has.Count.EqualTo(0));
        }

        /// <summary>
        /// Tests that invoking MinimumPriority on a new queue throws an
        /// InvalidOperationException.
        /// </summary>
        [Test, Timeout(1000), Category("A: Empty Queue Tests")]
        public void TestEmptyMinPriority()
        {
            Assert.That(() => new MinPriorityQueue().MinimumPriority,
                Throws.InstanceOf<InvalidOperationException>());
        }

        /// <summary>
        /// Tests that invoking RemoveMinimumPriority on a new queue throws an
        /// InvalidOperationException.
        /// </summary>
        [Test, Timeout(1000), Category("A: Empty Queue Tests")]
        public void TestEmptyRemoveMin()
        {
            Assert.That(() => new MinPriorityQueue().RemoveMinimumPriority(),
                Throws.InstanceOf<InvalidOperationException>());
        }

        /// <summary>
        /// Tests that invoking IncreasePriority on a new queue does not throw an exception.
        /// </summary>
        [Test, Timeout(1000), Category("A: Empty Queue Tests")]
        public void TestEmptyIncreasePriority()
        {
            Location loc = new Location(1, 2);
            Assert.That(() => new MinPriorityQueue().IncreasePriority(loc), Throws.Nothing);
        }

        /// <summary>
        /// Tests that invoking DecreasePriority on a new queue does not throw an exception.
        /// </summary>
        [Test, Timeout(1000), Category("A: Empty Queue Tests")]
        public void TestEmptyDecreasePriority()
        {
            Location loc = new Location(3, 4);
            Assert.That(() => new MinPriorityQueue().DecreasePriority(loc), Throws.Nothing);
        }

        /// <summary>
        /// Tests that adding one element to an empty queue yields a Count of 1.
        /// </summary>
        [Test, Timeout(1000), Category("B: Add Count Tests")]
        public void TestAdd1Count()
        {
            MinPriorityQueue q = new MinPriorityQueue();
            q.Add(1, new Location(0, 1));
            Assert.That(q, Has.Count.EqualTo(1));
        }

        /// <summary>
        /// Tests that adding two elements to an empty queue yields a Count of 2.
        /// </summary>
        [Test, Timeout(1000), Category("B: Add Count Tests")]
        public void TestAdd2Count()
        {
            MinPriorityQueue q = new MinPriorityQueue();
            q.Add(1, new Location(0, 0));
            q.Add(3, new Location(1, 1));
            Assert.That(q, Has.Count.EqualTo(2));
        }

        /// <summary>
        /// Tests that adding a duplicate priority yields the correct Count.
        /// </summary>
        [Test, Timeout(1000), Category("B: Add Count Tests")]
        public void TestAddDuplicatePriority()
        {
            MinPriorityQueue q = new MinPriorityQueue();
            q.Add(2, new Location(0, 0));
            q.Add(2, new Location(0, 1));
            Assert.That(q, Has.Count.EqualTo(2));
        }

        /// <summary>
        /// Tests that adding 2 elements, then removing 1, results in a Count of 1.
        /// </summary>
        [Test, Timeout(1000), Category("C: Remove Count Tests")]
        public void TestRemove1Count()
        {
            MinPriorityQueue q = new MinPriorityQueue();
            q.Add(1, new Location(0, 1));
            q.Add(3, new Location(1, 1));
            q.RemoveMinimumPriority();
            Assert.That(q, Has.Count.EqualTo(1));
        }

        /// <summary>
        /// Tests that adding 2 elements, then removing 2, results in a Count of 0.
        /// </summary>
        [Test, Timeout(1000), Category("C: Remove Count Tests")]
        public void TestRemove2Count()
        {
            MinPriorityQueue q = new MinPriorityQueue();
            q.Add(1, new Location(0, 1));
            q.Add(3, new Location(1, 1));
            q.RemoveMinimumPriority();
            q.RemoveMinimumPriority();
            Assert.That(q, Has.Count.EqualTo(0));
        }

        /// <summary>
        /// Tests that after adding a single element, MinimumPriority gives its priority.
        /// </summary>
        [Test, Timeout(1000), Category("D: Add Tests")]
        public void TestAdd1()
        {
            MinPriorityQueue q = new MinPriorityQueue();
            q.Add(1, new Location(2, 2));
            Assert.That(q.MinimumPriority, Is.EqualTo(1));
        }

        /// <summary>
        /// Tests that after adding two elements in order of increasing priority,
        /// MinimumPriority returns the first priority.
        /// </summary>
        [Test, Timeout(1000), Category("D: Add Tests")]
        public void TestAdd2Increasing()
        {
            MinPriorityQueue q = new MinPriorityQueue();
            q.Add(4, new Location(2, 2));
            q.Add(5, new Location(3, 3));
            Assert.That(q.MinimumPriority, Is.EqualTo(4));
        }

        /// <summary>
        /// Tests that after adding two elements in order of decreasing priority,
        /// MinimumPriority returns the second priority.
        /// </summary>
        [Test, Timeout(1000), Category("D: Add Tests")]
        public void TestAdd2Decreasing()
        {
            MinPriorityQueue q = new MinPriorityQueue();
            q.Add(9, new Location(0, 1));
            q.Add(7, new Location(2, 3));
            Assert.That(q.MinimumPriority, Is.EqualTo(7));
        }

        /// <summary>
        /// Tests that after adding 6 elements, MinimumPriority returns the smallest priority.
        /// </summary>
        [Test, Timeout(1000), Category("D: Add Tests")]
        public void TestAdd6()
        {
            MinPriorityQueue q = new MinPriorityQueue();
            q.Add(5, new Location(1, 4));
            q.Add(3, new Location(1, 2));
            q.Add(7, new Location(1, 6));
            q.Add(9, new Location(1, 8));
            q.Add(1, new Location(1, 0));
            q.Add(11, new Location(1, 10));
            Assert.That(q.MinimumPriority, Is.EqualTo(1));
        }

        /// <summary>
        /// Tests that adding a duplicate location throws an ArgumentException.
        /// </summary>
        [Test, Timeout(1000), Category("D: Add Tests")]
        public void TestAddDuplicateLocation()
        {
            MinPriorityQueue q = new MinPriorityQueue();
            q.Add(3, new Location(1, 1));
            q.Add(4, new Location(2, 2));
            Assert.That(() => q.Add(5, new Location(1, 1)), Throws.InstanceOf<ArgumentException>());
        }

        /// <summary>
        /// Tests that removing from a single-element queue gets the correct location.
        /// </summary>
        [Test, Timeout(1000), Category("E: Remove Tests")]
        public void TestAdd1Remove1()
        {
            MinPriorityQueue q = new MinPriorityQueue();
            Location loc = new Location(2, 3);
            q.Add(5, loc);
            Location result = q.RemoveMinimumPriority();
            Assert.Multiple(() =>
            {
                Assert.That(result.Row, Is.EqualTo(loc.Row));
                Assert.That(result.Column, Is.EqualTo(loc.Column));
            });
        }

        /// <summary>
        /// Tests that removing both elements from a queue gives the right locations.
        /// </summary>
        [Test, Timeout(1000), Category("E: Remove Tests")]
        public void TestAdd2Remove2()
        {
            MinPriorityQueue q = new MinPriorityQueue();
            Location loc1 = new Location(0, 1);
            q.Add(1, loc1);
            Location loc2 = new Location(2, 3);
            q.Add(2, loc2);
            Location result1 = q.RemoveMinimumPriority();
            Location result2 = q.RemoveMinimumPriority();
            Assert.Multiple(() =>
            {
                Assert.That(result1.Row, Is.EqualTo(loc1.Row),
                    "The first removed location has the wrong row.");
                Assert.That(result1.Column, Is.EqualTo(loc1.Column),
                    "The first removed location has the wrong column.");
                Assert.That(result2.Row, Is.EqualTo(loc2.Row),
                    "The second removed location has the wrong row.");
                Assert.That(result2.Column, Is.EqualTo(loc2.Column),
                    "The second removed location has the wrong column.");
            });
        }

        /// <summary>
        /// Tests that removing all locations from a 9-element queue gives the
        /// correct sequence of locations.
        /// </summary>
        [Test, Timeout(1000), Category("E: Remove Tests")]
        public void TestAdd9Remove9()
        {
            int[] priorities = { 5, 3, 0, 6, 2, 4, 8, 1, 7 };
            int[] indices = { 2, 7, 4, 1, 5, 0, 3, 8, 6 };
            MinPriorityQueue q = new MinPriorityQueue();
            for (int i = 0; i < priorities.Length; i++)
            {
                q.Add(priorities[i], new Location(2 * i, 2 * i + 1));
            }
            Location[] results = new Location[indices.Length];
            for (int i = 0; i < results.Length; i++)
            {
                results[i] = q.RemoveMinimumPriority();
            }
            Assert.Multiple(() =>
            {
                for (int i = 0; i < results.Length; i++)
                {
                    Assert.That(results[i].Row, Is.EqualTo(2 * indices[i]),
                        $"The {i + 1}th remove has the wrong row.");
                    Assert.That(results[i].Column, Is.EqualTo(2 * indices[i] + 1),
                        $"The {i + 1}th remove has the wrong column.");
                }
            });
        }

        /// <summary>
        /// Tests that increasing the priority works in a 1-element queue.
        /// </summary>
        [Test, Timeout(1000), Category("F: IncreasePriority Tests")]
        public void TestIncrease1()
        {
            MinPriorityQueue q = new MinPriorityQueue();
            Location loc = new Location(3, 4);
            q.Add(1, loc);
            q.IncreasePriority(loc);
            int p = q.MinimumPriority;
            Location result = q.RemoveMinimumPriority();
            Assert.Multiple(() =>
            {
                Assert.That(p, Is.EqualTo(2));
                Assert.That(result.Row, Is.EqualTo(3));
                Assert.That(result.Column, Is.EqualTo(4));
            });
        }

        /// <summary>
        /// Tests that increasing the priority twice correctly reorders a 2-element queue.
        /// </summary>
        [Test, Timeout(1000), Category("F: IncreasePriority Tests")]
        public void TestIncrease2()
        {
            MinPriorityQueue q = new MinPriorityQueue();
            Location loc1 = new Location(4, 5);
            q.Add(1, loc1);
            Location loc2 = new Location(6, 7);
            q.Add(2, loc2);
            q.IncreasePriority(loc1);
            q.IncreasePriority(loc1);
            int p1 = q.MinimumPriority;
            Location result1 = q.RemoveMinimumPriority();
            int p2 = q.MinimumPriority;
            Location result2 = q.RemoveMinimumPriority();
            Assert.Multiple(() =>
            {
                Assert.That(p1, Is.EqualTo(2));
                Assert.That(result1.Row, Is.EqualTo(6));
                Assert.That(result1.Column, Is.EqualTo(7));
                Assert.That(p2, Is.EqualTo(3));
                Assert.That(result2.Row, Is.EqualTo(4));
                Assert.That(result2.Column, Is.EqualTo(5));
            });
        }

        /// <summary>
        /// Tests that decreasing the priority works in a 1-element queue.
        /// </summary>
        [Test, Timeout(1000), Category("G: DecreasePriority Tests")]
        public void TestDecrease1()
        {
            MinPriorityQueue q = new MinPriorityQueue();
            Location loc = new Location(3, 4);
            q.Add(2, loc);
            q.DecreasePriority(loc);
            int p = q.MinimumPriority;
            Location result = q.RemoveMinimumPriority();
            Assert.Multiple(() =>
            {
                Assert.That(p, Is.EqualTo(1));
                Assert.That(result.Row, Is.EqualTo(3));
                Assert.That(result.Column, Is.EqualTo(4));
            });
        }

        /// <summary>
        /// Tests that decreasing the priority twice correctly reorders a 2-element queue.
        /// </summary>
        [Test, Timeout(1000), Category("G: DecreasePriority Tests")]
        public void TestDecrease2()
        {
            MinPriorityQueue q = new MinPriorityQueue();
            Location loc1 = new Location(4, 5);
            q.Add(1, loc1);
            Location loc2 = new Location(6, 7);
            q.Add(2, loc2);
            q.DecreasePriority(loc2);
            q.DecreasePriority(loc2);
            int p1 = q.MinimumPriority;
            Location result1 = q.RemoveMinimumPriority();
            int p2 = q.MinimumPriority;
            Location result2 = q.RemoveMinimumPriority();
            Assert.Multiple(() =>
            {
                Assert.That(p1, Is.EqualTo(0));
                Assert.That(result1.Row, Is.EqualTo(6));
                Assert.That(result1.Column, Is.EqualTo(7));
                Assert.That(p2, Is.EqualTo(1));
                Assert.That(result2.Row, Is.EqualTo(4));
                Assert.That(result2.Column, Is.EqualTo(5));
            });
        }

        /// <summary>
        /// Tests that adding two elements with the same priority keeps the
        /// elements in the order they were added. This ensures that sifting
        /// up doesn't swap equal priorities.
        /// </summary>
        [Test, Timeout(1000), Category("H: Duplicate Priority Tests")]
        public void TestAdd2Duplicate()
        {
            MinPriorityQueue q = new MinPriorityQueue();
            Location loc1 = new Location(3, 4);
            q.Add(1, loc1);
            Location loc2 = new Location(5, 6);
            q.Add(1, loc2);
            Location result = q.RemoveMinimumPriority();
            Assert.Multiple(() =>
            {
                Assert.That(result.Row, Is.EqualTo(3));
                Assert.That(result.Column, Is.EqualTo(4));
            });
        }

        /// <summary>
        /// Tests that increasing the priority of the first of 7 equal-priority elements added 
        /// to the queue causes that element to be removed last.
        /// </summary>
        [Test, Timeout(1000), Category("H: Duplicate Priority Tests")]
        public void TestDuplicate7Increase()
        {
            MinPriorityQueue q = new MinPriorityQueue();
            for (int i = 1; i <= 7; i++)
            {
                q.Add(1, new Location(i, i));
            }
            Location loc = new Location(1, 1);
            q.IncreasePriority(loc);
            Location result = default;
            for (int i = 0; i < 7; i++)
            {
                result = q.RemoveMinimumPriority();
            }
            Assert.That(result.Row, Is.EqualTo(loc.Row));
        }

        /// <summary>
        /// Tests that decreasing the priority of the last of 7 equal-priority elements added
        /// to a queue causes that element to be removed first.
        /// </summary>
        [Test, Timeout(1000), Category("H: Duplicate Priority Tests")]
        public void TestDuplicate7Decrease()
        {
            MinPriorityQueue q = new MinPriorityQueue();
            for (int i = 1; i <= 7; i++)
            {
                q.Add(1, new Location(i, i));
            }
            Location loc = new Location(7, 7);
            q.DecreasePriority(loc);
            Assert.That(q.RemoveMinimumPriority().Row, Is.EqualTo(loc.Row));
        }

        /// <summary>
        /// Tests the performance of Add/RemoveMinimumPriority. A correct implementation 
        /// shouldn't time out.
        /// </summary>
        [Test, Timeout(5000), Category("I: Performance Tests")]
        public void TestPerformanceAddRemove()
        {
            MinPriorityQueue q = new MinPriorityQueue();
            Random rand = new Random();
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    q.Add(rand.Next(10000), new Location(i, j));
                }
            }
            for (int i = 0; i < 10000; i++)
            {
                q.RemoveMinimumPriority();
            }
            Assert.Pass();
        }

        /// <summary>
        /// Tests the performance of DecreasePriority/IncreasePriority. A correct implementation
        /// shouldn't time out.
        /// </summary>
        [Test, Timeout(5000), Category("I: Performance Tests")]
        public void TestPerformanceIncreaseDecrease()
        {
            MinPriorityQueue q = new MinPriorityQueue();
            for (int i = 0; i < 500; i++)
            {
                q.Add(1, new Location(i, i));
            }
            Location loc = new Location(0, 0);
            q.DecreasePriority(loc);
            for (int i = 0; i < 500; i++)
            {
                q.IncreasePriority(loc);
                q.IncreasePriority(loc);
                q.DecreasePriority(loc);
                q.DecreasePriority(loc);
            }
            Assert.Pass();
        }
    }
}
