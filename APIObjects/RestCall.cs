using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Serialization;
using TTCBDD.ComponentHelper;
using TTCBDD.CustomException;

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
        private List<object> propertyPath = new List<object>();

        public RestCall(Method method, string url, string resource, DataFormat dataFormat = DataFormat.Json)
        {
            this.method = method;
            this.dataFormat = dataFormat;
            client = new RestClient(url);
            request = new RestRequest(resource, method, dataFormat);
            client.AddHandler("text/html", () => new JsonNetSerializer());
            client.UseSerializer(new JsonNetSerializer());
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
        public RestCall<T> AddUrlParameter(string name, int segment)
        {
            request.AddUrlSegment(name, segment.ToString());
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

        public RestCall<T> Select(string property)
        {
            propertyPath.Add(property);
            return this;
        }
        public RestCall<T> Select(int index)
        {
            propertyPath.Add(index);
            return this;
        }

        public RestCall<T> Where(string field, string value)
        {
            request.AddQueryParameter(field, value);
            return this;
        }
        public RestCall<T> Where(string constraint)
        {
            var strings = constraint.Split(' ');
            (string property, string _relation, string value) = strings;
            switch (_relation)
            {
                case "==":
                    property += "";
                    break;
                case "<=":
                    property += "_lte";
                    break;
                case ">=":
                    property += "_gte";
                    break;
                case "like":
                    property += "_like";
                    break;
                default:
                    throw new Exception("Invalid relation");
            }
            request.AddQueryParameter(property, value);
            return this;
        }

        public IRestResponse<T> Execute()
        {
            response = client.Execute<T>(request);
            //If deserializing top level property return immediately
            if (propertyPath.Count() == 0)
                return response;
            //Parse content as a JObject which has properties accessed by strings like a dictionary
            JToken content = JToken.Parse(response.Content);
            //Traverse the object structure using each element in propertyPath
            //It interprets an integer segment as an array accessor and a string as an object accessor
            foreach (var segment in propertyPath)
            {
                if (segment is int index)
                {
                    content = content.ToObject<JArray>()[index];
                }
                else if (segment is string property)
                {
                    content = content.ToObject<JObject>()[property];
                }
            };
            //converts the desired field to the given type
            response.Data = content.ToObject<T>();
            return response;

        }
        public IRestResponse<T> Execute(Action<IRestResponse<T>> action)
        {
            Execute();
            action(response);
            return response;
        }
        public bool Check(Func<IRestResponse<T>, bool> success)
        {
            if (response == null) Execute();
            return success(response);
        }
        public T Data()
        {
            Execute();
            if (response.Data == null) throw new NoDataParsedException($"The request for {response.ResponseUri} has returned data that has not been deserialized into {typeof(T)}");
            return response.Data;
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