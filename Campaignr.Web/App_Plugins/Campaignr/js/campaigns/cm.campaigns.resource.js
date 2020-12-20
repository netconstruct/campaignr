function cmCampaignsResources($q, $http, umbRequestHelper) {
    return {
        getAllCampaigns: function (pageSize, page, searchTerm, status,  sortBy) {
            return umbRequestHelper.resourcePromise(
                $http.get(`/umbraco/Campaignr/Api/GetCampaigns?pageSize=${pageSize}&page=${page}&searchTerm=${searchTerm}&sortBy=${sortBy}&status=${status}`),
                "Failed to get campaigns"
            );
        },
        getCampaign: function (id) {
            return umbRequestHelper.resourcePromise(
                $http.get(`/umbraco/Campaignr/Api/GetCampaign?campaignId=${id}`),
                "Failed to get campaign"
            );
        },
        updateCampaigns: function (fd) {
            return umbRequestHelper.resourcePromise(
                $http.get("/umbraco/api/cm/campaigns/update", fd, {
                    withCredentials: true,
                }),
                "Failed to update campaign"
            );
        },
        deleteCampaigns: function (id) {
            return umbRequestHelper.resourcePromise(
                $http.get(`/umbraco/api/cm/campaigns/delete?id=${id}`),
                "Failed to delete campaign"
            );
        },
        getCampaignContacts: function (id, pageSize, page) {
            return umbRequestHelper.resourcePromise(
                $http.get(`/umbraco/Campaignr/Api/GetCampaignContacts?campaignId=${id}&pageSize=${pageSize}&page=${page}`),
                "Failed to get campaign contacts"
            );
        },
        getCampaignLink: function (id) {
            return umbRequestHelper.resourcePromise(
                $http.get(`/umbraco/Campaignr/Api/GetInternalCampaignLink?id=${id}`),
                "Failed to get campaign link"
            );
        }
    };
}
angular.module("umbraco.resources").factory("cmCampaignsResources", cmCampaignsResources);


angular.module("umbraco").filter("getCampaignStatus", [function () {
    return function (id) {
        switch (id) {
            case 1:
                return "Draft";
            case 2:
                return "Scheduled";
            case 3:
                return "Sent";
            case 4:
                return "Cancelled";
            case 5:
                return "Failed";
            default:
                return "In Progress";
        }
    };
}]);
angular.module("umbraco").filter("getCampaignStatusClass", [function () {
    return function (id) {
        switch (id) {
            case 1:
                return "draft";
            case 2:
                return "scheduled";
            case 3:
                return "sent";
            case 4:
                return "cancelled";
            case 5:
                return "failed";
            default:
                return "pending";
        }
    };
}]);
angular.module("umbraco").filter("getCampaignIcon", [function () {
    return function (id) {
        switch (id) {
            case 4:
                return "icon-block";
            case 5:
                return "icon-alert";
            default:
                return "icon-message";
        }
    };
}]);

 