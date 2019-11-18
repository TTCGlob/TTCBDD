using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serialization;

namespace TTCBDD.APIObjects
{
    public class RestCall<T> where T : new()
    {
        private readonly Method method;
        private T payload;
        private IRestClient client;
        private IRestRequest request;
        private IRestResponse<T> response;
        private DataFormat dataFormat;
        private T data;

        public RestCall(Method method, string url, string resource, DataFormat dataFormat = DataFormat.Json)
        {
            this.method = method;
            this.dataFormat = dataFormat;
            client = new RestClient(url);
            request = new RestRequest(resource, method, dataFormat);
            client.AddHandler("text/html", () => new JsonNetSerializer());
            if (dataFormat == DataFormat.Json)
            {
                AddHeader("Accept", "application/json");
            }
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
            this.payload = payload;
            request.AddBody(payload);
            if (this.dataFormat == DataFormat.Json)
                AddHeader("Content-Type", "application/json");
            return this;
        }
        public RestCall<T> Where(string field, string value)
        {
            request.AddQueryParameter(field, value);
            return this;
        }
        public RestCall<T> Where(string field, int value)
        {
            request.AddQueryParameter(field, value.ToString());
            return this;
        }

        public IRestResponse<T> Execute()
        {
            response = client.Execute<T>(request);
            return response;
        }
        public IRestResponse<T> Execute(Action<IRestResponse<T>> action)
        {
            response = client.Execute<T>(request);
            action(response);
            return response;
        }
        public bool Check(Func<IRestResponse<T>, bool> success)
        {
            if (response == null) Execute();
            return success(response);
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
