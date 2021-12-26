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
        public enum Role
        {
            Leader = 1,
            Developer,
            DeveloperManager,
            Tester,
            TesterManager
        }
        // category
        public enum Category
        {
            DefaultStatusTag = 1,
            DefaultRelationTag,
            DefaultActionTag,
            DefaultWorklogTag,
            ProjectPermission,
            IssuePermission,
            CustomLabelTag
        }
        public const int DefaultStatusTag = 1;
        public const int DefaultRelationTag = 2;
        public const int DefaultActionTag = 3;
        public const int CustomLabelTag = 4;
        public const int ProjectPermission = 5;
        public const int IssuePermission = 6;
        
        // tag
        public const int LogCreateIssueTag = 12;
        public const int LogUpdateStatusTag = 13;
        public const int LogUpdateAssigneeTag = 14;
        public const int LogUpdateReporterTag = 15;
        public const int LogUpdatePriorityTag = 16;
        public const int LogUpdateSeverityTag = 17;
        public const int LogUpdateLabelTag = 18;
        public const int LogUpdateAttachmentTag = 19;
        public const int LogUpdateRemainTag = 20;
        public const int LogUpdateOriginTag = 21;
        public const int LogUpdateDueDateTag = 22;
        public const int LogUpdateDescriptionTag = 23;
        public const int LogUpdateEnvironmentTag = 24;
        public const int LogUpdateLinkTag = 25;
        public const int LogAddWorklogRealTimeTag = 26;

        // permission
        public enum Permission
        {
            EditProject = 1,
            ManageRoles,
            ManageStatuses,
            ManageMember,
            CreateIssue,
            CloneIssue,
            DeleteOtherIssues,
            EditOtherIssues,
            DeleteOtherComments
        }

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
