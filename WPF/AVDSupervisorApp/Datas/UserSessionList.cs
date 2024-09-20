using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVDSupervisorApp.Datas
{
    public class UserSessionList
    {
        public List<UserSession> Value { get; set; }
    }

    public class UserSession
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public string Type { get; set; }
        public SystemData SystemData { get; set; }
        public UserSessionProperties Properties { get; set; }
    }

    public class SystemData
    {
        public string CreatedBy { get; set; }
        public string CreatedByType { get; set; }
        public DateTime CreatedAt { get; set; }
        public string LastModifiedBy { get; set; }
        public string LastModifiedByType { get; set; }
        public DateTime LastModifiedAt { get; set; }
    }

    public class UserSessionProperties
    {
        public string ObjectId { get; set; }
        public string UserPrincipalName { get; set; }
        public string ApplicationType { get; set; }
        public string SessionState { get; set; }
        public string ActiveDirectoryUserName { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
