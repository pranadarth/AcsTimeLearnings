using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVDSupervisorApp.Datas
{
    static class Config
    {
        //Enter Details Accordingly
        internal const string SubscriptionId = "71528651-1c07-473c-aa26-66e89596b0e8";
        internal static readonly string[] omittedHostPools = { "IFF-AVDDR-HP-STAFF" };
        internal const string ResourceGroup = "IFF-RG-AVD";
        internal const string ClientId = "a8d896d7-f68d-4f43-8209-4626de6587bd";
        internal const string TenantId = "773a4b43-6dd9-46ef-946f-8f2ac213a43e";
        internal const string TeamLeadersGroupId = "59caaf43-aa03-4950-af79-d15b920bd9ca";
        internal const string EmailDomain = "@iffresearch.com";

        internal const string ApiVersion = "2022-02-10-preview";
        internal const string loginUrl = "https://login.microsoftonline.com/common/oauth2/nativeclient";
    }
}
