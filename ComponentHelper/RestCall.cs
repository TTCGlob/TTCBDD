using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serialization;

namespace TTCBDD.ComponentHelper
{
    public class RestCall<T> where T : new()
    {
        private Method method;
        private T payload;
        private IRestClient client;
        private IRestRequest request;
        private IRestResponse<T> response;
        private Func<IRestResponse<T>, bool> successCondition;
        public bool Success
        {
            get => successCondition(response);
        }
        private T data;

        public RestCall(Method method, string url, string resource, DataFormat dataFormat = DataFormat.Json)
        {
            this.method = method;
            client = new RestClient(url);
            request = new RestRequest(resource, method, dataFormat);
            client.AddHandler("text/html", () => new JsonNetSerializer());
        }

        public RestCall<T> AddHeader(string header, string value)
        {
            request.AddHeader(header, value);
            return this;
        }
        public RestCall<T> AddUrlParameter(string name, string segment)
        {
            request.AddUrlSegment(name, segment);
            return this;
        }

        public RestCall<T> AddPayload(T payload)
        {
            request.AddBody(payload);
            return this;
        }

        public IRestResponse<T> Execute()
        {
            response = client.Execute<T>(request);
            return response;
        }
        public RestCall<T> AddSuccessCondition(Func<IRestResponse, bool> condition)
        {
            successCondition = condition;
            return this;
        }
    }
    public class JsonNetSerializer : IRestSerializer
    {
        public string Serialize(object obj) =>
            JsonConvert.SerializeObject(obj);

        public string Serialize(Parameter parameter) =>
            JsonConvert.SerializeObject(parameter.Value);

        public T Deserialize<T>(IRestResponse response) =>
            JsonConvert.DeserializeObject<T>(response.Content);

        public string[] SupportedContentTypes { get; } =
        {
            "application/json", "text/json", "text/x-json", "text/javascript", "*+json", "text/html"
        };

        public string ContentType { get; set; } = "application/json";

        public DataFormat DataFormat { get; } = DataFormat.Json;
    }
}
