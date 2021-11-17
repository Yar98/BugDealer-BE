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
using System.Text.RegularExpressions;

namespace Bug.Core.Common
{
    public static class Bts
    {
        public const int DefaultStatusTag = 1;
        public const int DefaultRelationTag = 2;
        public const int DefaultActionTag = 3;
        public const int CustomLabelTag = 4;

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
            Regex r = new Regex(@"\[(\s*\{\}\,)*\s*\{\}\s*\]");
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
                return r.Replace(strWriter.ToString(), "[]");
            }
        }
    }
}
