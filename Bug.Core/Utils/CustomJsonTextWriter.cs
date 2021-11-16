using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Core.Utils
{
    public class CustomJsonTextWriter : JsonTextWriter
    {
        public CustomJsonTextWriter(TextWriter textWriter) : base(textWriter) { }
        public int CurrentDepth { get; private set; }
        public int MaxDepth { get; set; }
        
        public override void WriteStartObject()
        {
            CurrentDepth++;
            
            base.WriteStartObject();
        }

        public override void WriteEndObject()
        {
            CurrentDepth--;
            
            base.WriteEndObject();
        }
    }
}
