using Campaignr.Core.Database.Tables;
using Campaignr.Core.Helpers;
using Campaignr.Core.Interfaces;
using Campaignr.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Umbraco.Web.Mvc;
using Umbraco.Web.WebApi;

namespace Campaignr.Core.Controllers
{
    [PluginController("Campaignr")]
    public class ApiController : UmbracoApiController
    {
        private IApiInterface _api;

        public ApiController()
        {
            _api = ApiHelper.GetCurrentApi();
        }

        #region Dashboard

        public string GetCreateCampaignLink()
        {
            return _api?.GetAddCampaignLink() ?? null;
        }

        #endregion

        #region Campaigns

        public List<Campaign> GetCampaigns(int pageSize, int page, string searchTerm = "", string status = "", string sortBy = "")
        {
            return _api?.GetCampaigns(pageSize, page, searchTerm, status, sortBy) ?? null;
        }

        public Campaign GetCampaign(int campaignId)
        {
            return _api?.GetCampaign(campaignId) ?? null;
        }

        public List<CampaignActivity> GetCampaignActivities(int campaignId)
        {
            return _api?.GetCampaignActivities(campaignId) ?? null;
        }

        public CampaignStats GetAllCampaignStats()
        {
            CampaignStats stats = new CampaignStats();

            stats.Campaigns = _api?.GetCampaigns(500, 1, sortBy: "-Created") ?? null;
            if (stats.Campaigns != null)
            {
                double totalSent = 0;
                double totalOpened = 0;
                double totalClicked = 0;

                foreach (var campaign in stats.Campaigns)
                {
                    if (campaign != null && campaign.Status == 3)
                    {
                        stats.TotalSentCampaigns++;
                        totalSent += campaign.Delivered;
                        totalOpened += campaign.UniqueOpened;
                        totalClicked += campaign.UniqueClicks;
                    }
                }

                if (totalOpened > 0)
                {
                    stats.OpenRate = (int)Math.Round((totalOpened / totalSent) * 100);
                }

                if (totalClicked > 0)
                {
                    stats.ClickRate = (int)Math.Round((totalClicked/ totalSent) * 100);
                }

                // Get Active Campaigns
                stats.Campaigns = stats.Campaigns.Where(c => c.Status < 3).ToList();
            }

            stats.Subscribers = _api?.GetTotalSubscribers() ?? 0;
            stats.RoundedSubscribers = stats.Subscribers.ToString();

            if (stats.Subscribers > 10000)
            {
                var roundedString = NumbersHelper.RoundToNearestX(stats.Subscribers, 10000).ToString();
                stats.RoundedSubscribers = $"{roundedString.Substring(0, roundedString.Length - 3)}k";
            }

            if (stats.Subscribers > 1000000)
            {
                var roundedString = NumbersHelper.RoundToNearestX(stats.Subscribers, 1000000).ToString();
                stats.RoundedSubscribers = $"{roundedString.Substring(0, roundedString.Length - 6)}mil";
            }

            return stats;
        }

        public List<Contact> GetCampaignContacts(int campaignId, int pageSize, int page)
        {
            return _api?.GetCampaignContacts(campaignId, pageSize, page) ?? null;
        }

        public string GetInternalCampaignLink(string id)
        {
            return _api.GetInternalCampaignLink(id);
        }

        #endregion

        #region Contacts

        public List<Contact> GetContacts(int pageSize, int page, string searchTerm = "", string sortBy = "", string status = "")
        {
            return _api?.GetContacts(pageSize, page, searchTerm, sortBy, status) ?? null;
        }

        public List<Contact> GetContacts(int listId)
        {
            return _api?.GetContacts(listId) ?? null;
        }

        public Contact GetContact(int contactId)
        {
            return _api?.GetContact(contactId) ?? null;
        }

        public Contact GetContact(string email)
        {
            return _api?.GetContact(email) ?? null;
        }

        [HttpGet]
        public Response AddContact(string email)
        {
            return _api?.AddContact(email) ?? null;
        }

        [HttpGet]
        public Response SubscribeContact(int listId, string email)
        {
            return _api?.SubscribeContact(listId, email) ?? null;
        }

        [HttpGet]
        public Response UnsubscribeContact(string email)
        {
            return _api?.UnsubscribeContact(email) ?? null;
        }

        #endregion

        #region Lists

        public List<List> GetLists(int pageSize, int page, string searchTerm = "", string sortBy = "")
        {
            return _api?.GetLists(pageSize, page, searchTerm, sortBy) ?? null;
        }

        public List GetList(int listId)
        {
            return _api?.GetList(listId) ?? null;
        }

        [HttpGet]
        public Response DeleteList(int listId)
        {
            return _api?.DeleteList(listId) ?? null;
        }

        [HttpGet]
        public Response UpdateList(int listId, string name)
        {
            return _api?.UpdateList(listId, name) ?? null;
        }

        #endregion

        public List<string> GetApiNames()
        {
            return ApiHelper.GetApiNames();
        }

        public Settings GetSettings()
        {
            return ApiHelper.GetSettings();
        }

        [HttpPost]
        public bool SaveSettings([FromBody]Settings settings)
        {
            return ApiHelper.SaveSettings(settings);
        }

    }
}