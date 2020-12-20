using Campaignr.Core.Api.Responses;
using Campaignr.Core.Attributes;
using Campaignr.Core.Database.Tables;
using Campaignr.Core.Interfaces;
using Campaignr.Core.Models;
using Campaignr.Core.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Campaignr.Core.Api
{
    [FriendlyName("Wired Plus"), Enabled(true)]
    public class WiredPlusApi: IApiInterface
    {
        public string ApiUsername => SettingsTableStore.GetSettingValue("ApiUsername");
        public string ApiPassword => SettingsTableStore.GetSettingValue("ApiPassword");
        public string ApiUrl => "https://api.wiredplus.com";

        #region Campaigns

        public string GetAddCampaignLink()
        {
            return "https://app.wiredplus.com/app/campaigns/add";
        }

        public string GetInternalCampaignLink(string id)
        {
            return $"https://app.wiredplus.com/app/reports/campaigns/overview?id={id}";
        }

        public List<Campaign> GetCampaigns(int pageSize, int page, string searchTerm = "", string status = "", string sortBy = "")
        {
            var select = pageSize;
            var skip = (page - 1) * pageSize;
            // Handle Sort By
            var sortResult = "";
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                var type = "";
                string dir;
                if (sortBy.StartsWith("+"))
                {
                    dir = "asc";
                }
                else
                {
                    dir = "desc";
                }
                var rem = sortBy.TrimStart(new[] { '+', '-', ' ' });
                switch (rem)
                {
                    case "Created":
                        type = "create_date";
                        break;
                    case "Update":
                        type = "update_date";
                        break;
                    case "Name":
                        type = "name";
                        break;
                }
                sortResult = $"{type}_{dir}";
            }
            var statusResult = -1;
            switch (status)
            {
                case "Active":
                    statusResult = 1;
                    break;
                case "Scheduled":
                    statusResult = 2;
                    break;
                case "Sent":
                    statusResult = 3;
                    break;
                case "Cancelled":
                    statusResult = 4;
                    break;
                case "Failed":
                    statusResult = 5;
                    break;
            }
            return ApiCall<List<Campaign>>("/v1/GetCampaigns?" +
                $"select={select}" +
                $"&skip={skip}" +
                $"{(!string.IsNullOrWhiteSpace(searchTerm) ? "&name=" + searchTerm : "")}" +
                $"{(statusResult != -1 ? "&status=" + statusResult : "")}" +
                $"{(!string.IsNullOrWhiteSpace(sortResult) ? "&sort=" + sortResult : "")}");
        }

        public Campaign GetCampaign(int campaignId)
        {
            return ApiCall<Campaign>($"/v1/GetCampaignById?id={campaignId}");
        }

        public List<CampaignActivity> GetCampaignActivities(int campaignId)
        {
            return ApiCall<List<CampaignActivity>>($"/v1/GetCampaignActivities?id={campaignId}");
        }

        public List<Contact> GetCampaignContacts(int campaignId, int pageSize, int page)
        {
            var select = pageSize;
            var skip = (page - 1) * pageSize;
            return ApiCall<List<Contact>>($"/v1/GetCampaignContacts?campaign_id={campaignId}" +
                $"select={select}" +
                $"&skip={skip}");
        }

        #endregion

        #region Contacts

        public List<Contact> GetContacts(int pageSize = 10, int page = 1, string searchTerm = "", string sortBy = "", string status = "")
        {
            var select = pageSize;
            var skip = (page - 1) * pageSize;
            // Handle Sort By
            var sortResult = "";
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                var type = "";
                var dir = "";
                if (sortBy.StartsWith("+"))
                {
                    dir = "asc";
                }
                else
                {
                    dir = "desc";
                }
                var rem = sortBy.TrimStart(new[] { '+', '-', ' ' });
                switch (rem)
                {
                    case "Created":
                        type = "create_date";
                        break;
                    case "Update":
                        type = "update_date";
                        break;
                }
                sortResult = $"{type}_{dir}";
            }
            return ApiCall<List<Contact>>($"/v1/GetContacts?" +
                $"select={select}" +
                $"&skip={skip}" +
                $"{(!string.IsNullOrWhiteSpace(searchTerm) ? "&first_name=" + searchTerm : "" )}" +
                $"{(!string.IsNullOrWhiteSpace(status) ? "&status=" + status : "")}" +
                $"{(!string.IsNullOrWhiteSpace(sortResult) ? "&sort=" + sortResult : "")}");
        }

        public Contact GetContact(int contactId)
        {
            return ApiCall<Contact>($"/v1/GetContactById?id={contactId}");
        }

        public Contact GetContact(string email)
        {
            return ApiCall<Contact>($"/v1/GetContactByEmail?email={email}");
        }

        public List<Contact> GetContacts(int listId)
        {
            return ApiCall<List<Contact>>($"/v1/GetContactsInList?listid={listId}");
        }

        public Response AddContact(string email)
        {
            return ApiCall<Response>($"/v1/CreateContact?email={email}");
        }

        public Response SubscribeContact(int listId, string email)
        {
            List<string> emails = new List<string>();
            emails.Add(email);

            return SubscribeContacts(listId, emails.ToArray());
        }

        public Response SubscribeContacts(int listId, string[] emails)
        {
            string data = "";
            if (emails != null && emails.Count() > 0)
            {
                data = JsonConvert.SerializeObject(emails);
            }
            return ApiCall<Response>($"/v1/BulkAssignContactsToList?id={listId}", data: data);
        }

        public Response UnsubscribeContact(string email)
        {
            return ApiCall<Response>($"/v1/UnsubscribeContact?email={email}");
        }

        #endregion

        #region Lists
        public List<List> GetLists(int pageSize, int page, string searchTerm = "", string sortBy = "")
        {
            var select = pageSize;
            var skip = (page - 1) * pageSize;
            // Handle Sort By
            var sortResult = "";
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                var type = "";
                var dir = "";
                if (sortBy.StartsWith("+"))
                {
                    dir = "asc";
                }
                else
                {
                    dir = "desc";
                }
                var rem = sortBy.TrimStart(new[] { '+', '-', ' ' });
                switch (rem)
                {
                    case "Created":
                        type = "create_date";
                        break;
                    case "Update":
                        type = "update_date";
                        break;
                    case "Name":
                        type = "name";
                        break;
                }
                sortResult = $"{type}_{dir}";
            }
            return ApiCall<List<List>>("/v1/GetLists?" +
                $"select={select}" +
                $"&skip={skip}" +
                $"{(!string.IsNullOrWhiteSpace(searchTerm) ? "&name=" + searchTerm : "")}" +
                $"{(!string.IsNullOrWhiteSpace(sortResult) ? "&sort=" + sortResult : "")}");
        }

        public List GetList(int listId)
        {
            return ApiCall<List>($"/v1/GetListById?id={listId}");
        }

        public Response DeleteList(int listId)
        {
            return ApiCall<Response>($"/v1/DeleteList?id={listId}");
        }

        public Response UpdateList(int listId, string name)
        {
            return ApiCall<Response>($"/v1/UpdateList?id={listId}&name={name}");
        }
        #endregion

        #region Account

        public int GetTotalSubscribers()
        {
            var account = ApiCall<AccountInfo>($"/v1/GetAccountInfo");
            return account?.Contacts ?? 0;
        }

        #endregion

        private T ApiCall<T>(string url, ContractResolver contractResolver = null, string data = null)
        {
            object retVal = null;

            string fullUrl = $"{ApiUrl}{url}";

            var response = GetData(fullUrl, data);

            if (response != null && response.IsSuccess)
            {
                var settings = new JsonSerializerSettings();

                if (contractResolver == null)
                {
                    contractResolver = ContractResolver();
                }

                if (contractResolver != null)
                {
                    settings.ContractResolver = contractResolver;
                }

                retVal = JsonConvert.DeserializeObject(response.ResponseMessage, typeof(T), settings);
            }

            return (T)Convert.ChangeType(retVal, typeof(T));
        }

        private ApiResponse GetData(string url, string data)
        {
            var response = new ApiResponse();

            try
            {
                var base64authorization = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{ApiUsername}:{ApiPassword}"));

                using (var wb = new WebClient())
                {
                    wb.Headers.Add("Authorization", $"Basic {base64authorization}");
                    wb.Headers.Add("Accept", "application/json");

                    if (!string.IsNullOrWhiteSpace(data))
                    {
                        // POST
                        response.ResponseMessage = wb.UploadString(url, data);
                    }
                    else
                    {
                        // GET
                        response.ResponseMessage = wb.DownloadString(url);
                    }
                    response.IsSuccess = true;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ResponseMessage = ex.Message;
            }
            
            return response;
        }

        private ContractResolver ContractResolver()
        {
            return new ContractResolver(new Dictionary<string, string>
            {
                {"FromAddress", "from_email"},
                {"FromName", "from_name"},
                {"ReplyAddress", "reply_to"},
                {"Contacts", "no_contacts"},
                {"Delivered", "no_delivered"},
                {"Bounced", "no_bounced"},
                {"Opened", "no_opens"},
                {"UniqueOpened", "no_unique_opens"},
                {"Dropped", "no_dropped"},
                {"Clicks", "no_clicks"},
                {"UniqueClicks", "no_unique_clicks"},
                {"Unsubscribes", "no_unsubscribes"},
                {"Complaints", "no_complaints"},
                {"OptInType", "opt_in"},
                {"FirstName", "first_name"},
                {"LastName", "last_name"},
                {"OptInDate", "opt_in_date"},
                {"PostCode", "post_code"},
                {"EmailType", "email_type"},
                {"CampaignId", "campaign_id"},
                {"ContactId", "contact_id"},
                {"CreateDate", "create_date"},
                {"UpdateDate", "update_date"},
                {"ScheduledDate", "scheduled_date"},
                {"ClientOperatingSystem", "client_os"},
                {"DeviceType", "device_type"},
                {"ClientName", "client_name"},
                {"ClientType", "client_type"},
                {"UserAgent", "user_agent"},
                
            });
        }

 
    }
}