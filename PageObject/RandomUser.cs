using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTCBDD.APIObjects;
using TTCBDD.ComponentHelper;

namespace TTCBDD.PageObject
{
    public class RandomUser
    {
        private static readonly string url = "https://randomuser.me/api/";
        public string gender { get; set; }
        public Name name { get; set; }
        public string fullName { get => name.first + " " + name.last; }
        public Location location { get; set; }
        public string email { get; set; }
        public Dob dob { get; set; }
        public static RandomUser Random()
        {
            var content = new RestCall<RandomUser>(Method.GET, url, "")
                .Traverse("results")
                .Traverse(0)
                .Data();
            return content;
        }

        public static IEnumerable<RandomUser> RandomUsers(int num)
        {
            return new RestCall<List<RandomUser>>(Method.GET, url, "")
                .Where($"results == {num}")
                .Traverse("results")
                .Data();
        }

        public static string RandomName()
        {
            var user = Random();
            return user.name.first + " " + user.name.last;
        }

        public static Location RandomLocation()
        {
            return Random().location;
        }
    }

    public class Dob
    {
        public DateTime date { get; set; }
        public int age { get; set; }
    }

    public class Name
    {
        public string title { get; set; }
        public string first { get; set; }
        public string last { get; set; }
    }
    public class Location
    {
        public Street street { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string postcode { get; set; }
        public Coords coordinates { get; set; }
        public Timezone timezone { get; set; }
    }
    public class Street
    {
        public int number { get; set; }
        public string name { get; set; }
    }
    public class Coords
    {
        public string latitude { get; set; }
        public string longitude { get; set; }
    }
    
    public class Timezone
    {
        public string offset { get; set; }
        public string description { get; set; }
    }
}
