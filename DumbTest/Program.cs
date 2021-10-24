using System;
using System.Collections.Generic;
using System.Linq;
using DumbTest.Model;
namespace DumbTest
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            Func<ICollection<Project>, int, string, int> dumbTest =
                (p, m, n) => p.Select(
                    i => i.Tags.Where(
                        t => t.CategoryId == m && t.Name == n))
                .Count();
            */
            //Course course = new Course();
            Console.WriteLine("Hello World!");
            var names = new[] { "nick0", "mike1", "john2", "david3", "damina4", "haha5", "hehe6" };
            var chunked = ChunkBy(names,3);
        }
        static IEnumerable<IEnumerable<T>> ChunkBy<T>(IEnumerable<T> source, int chunkSize)
        {
            return source
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / chunkSize)
                .Select(x => x.Select(v => v.Value));
        }
    }
}
