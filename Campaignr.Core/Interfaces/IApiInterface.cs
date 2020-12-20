using Campaignr.Core.Models;
using System.Collections.Generic;

namespace Campaignr.Core.Interfaces
{
    public interface IApiInterface
    {
        #region Campaigns
        string GetAddCampaignLink();
        string GetInternalCampaignLink(string id);

        List<Campaign> GetCampaigns(int pageSize, int page, string searchTerm = "", string status = "", string sortBy = "");

        Campaign GetCampaign(int campaignId);

        List<CampaignActivity> GetCampaignActivities(int campaignId);
        #endregion

        #region Contacts
        List<Contact> GetCampaignContacts(int campaignId, int pageSize, int page);

        List<Contact> GetContacts(int pageSize = 10, int page = 1, string searchTerm = "", string sortBy = "", string status = "");

        List<Contact> GetContacts(int listId);

        Contact GetContact(int contactId);

        Contact GetContact(string email);

        Response AddContact(string email);

        Response SubscribeContact(int listId, string email);

        Response SubscribeContacts(int listId, string[] emails);

        Response UnsubscribeContact(string email);
        #endregion

        #region Lists
        List<List> GetLists(int pageSize, int page, string searchTerm = "", string sortBy = "");

        List GetList(int listId);

        Response DeleteList(int listId);

        Response UpdateList(int listId, string name);
        #endregion

        #region Account

        int GetTotalSubscribers();

        #endregion
    }
}