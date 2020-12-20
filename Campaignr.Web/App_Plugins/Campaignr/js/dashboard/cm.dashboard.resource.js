function cmDashboardResources($q, $http, umbRequestHelper) {
    return {
        getAllCampaignStats: function () {
            return umbRequestHelper.resourcePromise(
                $http.get("/umbraco/Campaignr/Api/GetAllCampaignStats"),
                "Failed to get campaigns"
            );
        },
        getAddCampaignLink: function () {
            return umbRequestHelper.resourcePromise(
                $http.get("/umbraco/Campaignr/Api/GetCreateCampaignLink"),
                "Failed to get campaigns"
            );
        },
    };
}
angular.module("umbraco.resources").factory("cmDashboardResources", cmDashboardResources);