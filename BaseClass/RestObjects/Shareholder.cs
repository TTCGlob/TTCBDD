using System;
using System.Collections.Generic;
using System.Linq;

namespace TTCBDD.BaseClass.RestObjects
{
    public class Shareholder
    {
        public string id { get; set; }
        public string name { get; set; }
        public int stake { get; set; }
        public DateTime creationDate { get; set; }

        public Shareholder()
        {
            creationDate = DateTime.UtcNow;
        }

        public Shareholder(string shareholder_name, int shareholder_stake = 1, string id = "")
        {
            this.id = id;
            name = shareholder_name;
            stake = shareholder_stake;
            creationDate = DateTime.UtcNow;
        }

        public override bool Equals(object _other)
        {
            return _other is Shareholder other
                && id.Equals(other.id)
                && name.Equals(other.name)
                && stake.Equals(other.stake)
                && creationDate.Date.CompareTo(other.creationDate.Date) == 0;
        }
        public override int GetHashCode() => base.GetHashCode();
        public override string ToString()
        {
            return $"ID: {id} Name: {name} Stake: {stake} Creation Date: {creationDate}";
        }

        public static List<Shareholder> Randoms()
        {
            var shareholders = new List<Shareholder>();
            var remainingStake = 100;
            var people = RandomUser.RandomUsers(100);
            var random = new Random();
            while (remainingStake > 0)
            {
                var stake = random.Next(1, remainingStake + 1);
                var shareholder = new Shareholder(people.First().fullName, stake);
                people = people.Skip(1);
                remainingStake -= stake;
                shareholders.Add(shareholder);
            }
            return shareholders;
        }
    }
}
