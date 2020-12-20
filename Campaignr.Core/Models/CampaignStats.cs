using System;
using System.Collections.Generic;

namespace Campaignr.Core.Models
{
    public class CampaignStats
    {
        public int TotalSentCampaigns { get; set; }

        public int OpenRate { get; set; }

        public int ClickRate { get; set; }

        public int Subscribers { get; set; }

        public string RoundedSubscribers { get; set; }

        public List<Campaign> Campaigns { get; set; }
    }
}