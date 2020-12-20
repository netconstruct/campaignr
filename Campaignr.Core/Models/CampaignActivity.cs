using System;

namespace Campaignr.Core.Models
{
    public class CampaignActivity
    {
        public int Id { get; set; }

        public int CampaignId { get; set; }

        public int ContactId { get; set; }

        public DateTime CreateDate { get; set; }

        public string Ip { get; set; }

        public string Country { get; set; }

        public string Region { get; set; }

        public string City { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public string ClientOperatingSystem { get; set; }

        public string DeviceType { get; set; }

        public string ClientName { get; set; }

        public string ClientType { get; set; }

        public string UserAgent { get; set; }

        public string Url { get; set; }

        public string Event { get; set; }

        public string Description { get; set; }
    }
}