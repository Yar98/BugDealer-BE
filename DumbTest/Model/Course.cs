using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DumbTest.Model
{
    public class Course
    {
        public int Id { get; private set; }
        public Course() { }
        public List<string> myList { get; private set; }
    }
}
