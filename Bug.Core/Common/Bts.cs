using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Bug.Core.Utils;
using System.IO;

namespace Bug.Core.Common
{
    public static class Bts
    {
        public const int DefaultProjectTag = 1;
        public const int DefaultLabelTag = 2;
        public const int DefaultAccountTag = 3;
        public const int DefaultStatusTag = 4;
        public const int DefaultRelationTag = 5;
        public const int CustomProjectTag = 6;
        public const int CustomLabelTag = 7;
        public const int CustomStatusTag = 8;
        //public const int DefaultProjectPermission = 9;
        //public const int DefaultIssuePermission = 10;

        public const int GetDetailProject = 1;

        public static string ConvertJson(Object result)
        {
            
            var json = JsonConvert.SerializeObject(result, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
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

        public static string ConvertJson(Object obj, int maxDepth)
        {
            using (var strWriter = new StringWriter())
            {
                using (var jsonWriter = new CustomJsonTextWriter(strWriter))
                {
                    Func<bool> include = () => jsonWriter.CurrentDepth <= maxDepth;
                    var resolver = new CustomContractResolver(include);
                    var serializer = new Newtonsoft.Json.JsonSerializer 
                    {
                        Formatting = Formatting.Indented,
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                        ContractResolver = resolver
                    };
                    serializer.Serialize(jsonWriter, obj);
                }
                return strWriter.ToString();
            }
        }
    }
}
