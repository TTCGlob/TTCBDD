﻿using Newtonsoft.Json.Linq;
using System;
using TTCBDD.Context;
using TTCBDD.Helpers.Rest;

namespace TTCBDD.BaseClass.RestObjects
{
    public class RestActions
    {
        [Obsolete]
        public static void PostRestMethod(string TagToVerify, string body)
        {
            body = RestHelper.CreateJsonRequest();
            PublicVar.Response = RestHelper.RestPost(PublicVar.BaseUrl, PublicVar.RequestType, TagToVerify, body);
            JObject json = JObject.Parse(PublicVar.Response);
            PublicVar.UniqueId = json.Last.Last.ToString().Replace("{", "").Replace("}", "").Trim();
        }

        public static void GetRestMethod(string TagToVerify)
        {
            PublicVar.Response = RestHelper.RestGet(PublicVar.BaseUrl, PublicVar.RequestType + PublicVar.UniqueId, TagToVerify);
        }

        [Obsolete]
        public static void PutRestMethod(string TagToVerify, string body)
        {
            body = RestHelper.CreateJsonRequest();
            PublicVar.Response = RestHelper.RestPut(PublicVar.BaseUrl, PublicVar.RequestType + PublicVar.UniqueId, TagToVerify, body);
        }

        public static void DelRestMethod(string TagToVerify)
        {
            PublicVar.Response = RestHelper.RestDel(PublicVar.BaseUrl, PublicVar.RequestType + PublicVar.UniqueId, TagToVerify);
        }
    }
}
