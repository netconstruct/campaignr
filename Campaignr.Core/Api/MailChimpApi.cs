using Campaignr.Core.Attributes;
using Campaignr.Core.Interfaces;
using Campaignr.Core.Models;
using System;
using System.Collections.Generic;

namespace Campaignr.Core.Api
{
    [FriendlyName("Mail Chimp")]
    public class MailChimpApi : IApiInterface
    {
        public Response AddContact(string email)
        {
            throw new NotImplementedException();
        }

        public Response DeleteList(int listId)
        {
            throw new NotImplementedException();
        }

        public string GetAddCampaignLink()
        {
            throw new NotImplementedException();
        }

        public Campaign GetCampaign(int campaignId)
        {
            throw new NotImplementedException();
        }

        public List<CampaignActivity> GetCampaignActivities(int campaignId)
        {
            throw new NotImplementedException();
        }

        public List<Contact> GetCampaignContacts(int campaignId, int pageSize, int page)
        {
            throw new NotImplementedException();
        }

        public List<Campaign> GetCampaigns(int pageSize, int page, string searchTerm = "", string status = "", string sortBy = "")
        {
            throw new NotImplementedException();
        }

        public Contact GetContact(int contactId)
        {
            throw new NotImplementedException();
        }

        public Contact GetContact(string email)
        {
            throw new NotImplementedException();
        }

        public List<Contact> GetContacts(int pageSize = 10, int page = 1, string searchTerm = "", string sortBy = "", string status = "")
        {
            throw new NotImplementedException();
        }

        public List<Contact> GetContacts(int listId)
        {
            throw new NotImplementedException();
        }

        public string GetInternalCampaignLink(string id)
        {
            throw new NotImplementedException();
        }

        public List GetList(int listId)
        {
            throw new NotImplementedException();
        }

        public List<List> GetLists(int pageSize, int page, string searchTerm = "", string sortBy = "")
        {
            throw new NotImplementedException();
        }

        public int GetTotalSubscribers()
        {
            throw new NotImplementedException();
        }

        public Response SubscribeContact(int listId, string email)
        {
            throw new NotImplementedException();
        }

        public Response SubscribeContacts(int listId, string[] emails)
        {
            throw new NotImplementedException();
        }

        public Response UnsubscribeContact(string email)
        {
            throw new NotImplementedException();
        }

        public Response UpdateList(int listId, string name)
        {
            throw new NotImplementedException();
        }
    }
}