using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTCBDD.PageObject
{
    public class Shareholder
    {
        public string id { get; set; }
        public string name { get; set; }
        public int stake { get; set; }
        public DateTime creationDate { get; set; }

        public Shareholder()
        {
            this.creationDate = DateTime.UtcNow;
        }

        public Shareholder(string shareholder_name,  int shareholder_stake = 1, string id = "")
        {
            this.id = id;
            this.name = shareholder_name;
            this.stake = shareholder_stake;
            this.creationDate = DateTime.UtcNow;
        }

        public bool Equals(Shareholder other)
        {
            return this.id.Equals(other.id)
                && this.name.Equals(other.name)
                && this.stake.Equals(other.stake)
                && this.creationDate.Date.CompareTo(other.creationDate.Date) == 0;
        }
        public override string ToString()
        {
            return $"ID: {this.id} Name: {this.name} Stake: {this.stake} Creation Date: {creationDate}";
        }

        public static List<Shareholder> Randoms()
        {
            var shareholders = new List<Shareholder>();
            var remainingStake = 100;
            var people = RandomUser.RandomUsers(100);
            var random = new Random();
            var i = 0;
            while (remainingStake > 0)
            {           
                var stake = random.Next(1, remainingStake + 1);
                var shareholder = new Shareholder(people.ElementAt(i++).fullName, stake);
                remainingStake -= stake;
                shareholders.Add(shareholder);
            }
            return shareholders;
        }
    }
}
