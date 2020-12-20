using System.Collections.Generic;

namespace Campaignr.Core.Models
{
    public class Settings
    {
        public string Name { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public List<string> Apis { get; set; }

        public bool ValidSettings { get; set; }
    }
}