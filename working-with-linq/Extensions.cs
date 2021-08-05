using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqFaroShuffle
{
    public static class Extensions
    {
        /// <summary>
        /// Shuffles two sets of cards (top and bottom set)
        /// </summary>
        /// <param name="first">First set of cards to shuffle</param>
        /// <param name="second">Second set of cards to shuffle</param>
        /// <returns></returns>
        public static IEnumerable<T> InterleaveSequenceWith<T>(this IEnumerable<T> first, IEnumerable<T> second)
        {
            var firstIter = first.GetEnumerator();
            var secondIter = second.GetEnumerator();

            while (firstIter.MoveNext() && secondIter.MoveNext())
            {
                yield return firstIter.Current;
                yield return secondIter.Current;
            }
        }

        /// <summary>
        /// Compares each shuffled sequence and retuens true if they match, or false if not
        /// </summary>
        /// <param name="first">First set of cards to compare</param>
        /// <param name="second">Second set of cards to compare</param>
        /// <returns></returns>
        public static bool SequenceEquals<T>(this IEnumerable<T> first, IEnumerable<T> second)
        {
            var firstIter = first.GetEnumerator();
            var secondIter = second.GetEnumerator();

            while (firstIter.MoveNext() && secondIter.MoveNext())
            {
                if (!firstIter.Current.Equals(secondIter.Current))
                {
                    return false;
                }
            }

            return true;
        }


        /// <summary>
        /// Logs each query to a debug file as the program runs
        /// </summary>
        /// <param name="tag">Value of log to be written</param>
        /// <returns></returns>
        public static IEnumerable<T> LogQuery<T>(this IEnumerable<T> sequence, string tag)
        {
            using (var writer = File.AppendText("debug.log"))
            {
                writer.WriteLine($"Executing Query {tag}");
            }

            return sequence;
        }
    }
}
