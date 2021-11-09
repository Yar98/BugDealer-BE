using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bug.Core.Common
{
    public static class Bts
    {
        public static readonly int IssueTag = 1;
        public static readonly int ProjectTag = 2;
        public static readonly int AccountTag = 3;


        public const int GetDetailProject = 1;

        public static string ConvertJson(Object result)
        {
            
            var json = JsonConvert.SerializeObject(result, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            /*
            var json = System.Text.Json.JsonSerializer.Serialize(result, new JsonSerializerOptions()
            {
                WriteIndented = true,
                ReferenceHandler = ReferenceHandler.Preserve
            });
            */
            return json;
        }
    }
}
