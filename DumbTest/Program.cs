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
            var developer = new Developer { FirstName = "Juliaaa" };
            string favoriteTask = developer switch
            {
                { FirstName: "Julia" } => "Writing code",
                { FirstName: "Thomas" } => "Writing this blog post",
                _ => "Watching TV",
            };
            //Course course = new();
            //course.myList.Add("ss");
            Console.WriteLine(favoriteTask);
            var names = new[] { "nick0", "mike1", "john2", "david3", "damina4", "haha5", "hehe6" };
            var chunked = ChunkBy(names,3);

            Console.ReadLine();
        }
        static IEnumerable<IEnumerable<T>> ChunkBy<T>(IEnumerable<T> source, int chunkSize)
        {
            return source
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / chunkSize)
                .Select(x => x.Select(v => v.Value));
        }
        public class Person
        {
            public string FirstName { get; set; }
            public int YearOfBirth { get; set; }
        }
        public class Developer : Person
        {
            public Manager Manager { get; set; }
        }
        public class Manager : Person
        {
        }
    }
}
