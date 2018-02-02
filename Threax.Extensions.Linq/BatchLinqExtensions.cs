using System;
using System.Collections.Generic;
using System.Text;

namespace Threax.Extensions.Linq
{
    /// <summary>
    /// Extension method to break an IEnumerable into smaller pieces.
    /// </summary>
    /// <remarks>
    /// Taken from https://blogs.msdn.microsoft.com/pfxteam/2012/11/16/plinq-and-int32-maxvalue/
    /// </remarks>
    public static class BatchLinqExtensions
    {
        /// <summary>
        /// Break a enumerable up into smaller pieces. Will return an IEnumerable&lt;IEnumerable&lt;T/&gt;&gt;.
        /// </summary>
        /// <typeparam name="T">The type of the enumerable.</typeparam>
        /// <param name="source">The source collection.</param>
        /// <param name="batchSize">The number of items to break each batch into.</param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<T>> Batch<T>(this IEnumerable<T> source, int batchSize)
        {
            using (var enumerator = source.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    yield return YieldBatchElements(enumerator, batchSize - 1);
                }
            }
        }

        private static IEnumerable<T> YieldBatchElements<T>(IEnumerator<T> source, int batchSize)
        {
            yield return source.Current;
            for (int i = 0; i < batchSize && source.MoveNext(); i++)
            {
                yield return source.Current;
            }
        }
    }
}
