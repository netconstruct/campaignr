using System;
using System.Runtime.Serialization;

namespace Campaignr.Core.Models
{
    public class Campaign
    {
        public int Id { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public string FromName { get; set; }

        public string FromAddress { get; set; }

        public string ReplyAddress { get; set; }

        public DateTime ScheduledDate { get; set; }

        public string Name { get; set; }

        public string Subject { get; set; }

        public int Contacts { get; set; }

        public int Delivered { get; set; }

        public int Bounced { get; set; }

        public int Dropped { get; set; }

        public int Opened { get; set; }

        public int UniqueOpened { get; set; }

        public int? Clicks { get; set; } = 0;

        public int UniqueClicks { get; set; }

        public int OpenedRate { get; set; }

        public int ClickRate { get; set; }

        public int? Unsubscribes { get; set; } = 0;

        public int? Complaints { get; set; } = 0;

        public int Status { get; set; }

        public int UniqueOpenedRate { get; set; }

        public int UniqueClickRate { get; set; }


        [OnDeserialized]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            double totalSent = Delivered;

            if (Opened > 0)
            {
                OpenedRate = (int)Math.Round((Opened / totalSent) * 100);
            }

            if (Clicks > 0)
            {
                ClickRate = (int)Math.Round((Clicks.HasValue ? Clicks.Value : 0  / totalSent) * 100);
            }

            if (UniqueOpened > 0)
            {
                UniqueOpenedRate = (int)Math.Round((UniqueOpened / totalSent) * 100);
            }

            if (UniqueClicks > 0)
            {
                UniqueClickRate = (int)Math.Round((UniqueClicks / totalSent) * 100);
            }
        }
    }
}