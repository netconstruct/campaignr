function cmSettingsResources($q, $http, umbRequestHelper) {
    return {
        getApiNames: function () {
            return umbRequestHelper.resourcePromise(
                $http.get("/umbraco/Campaignr/Api/GetApiNames"),
                "Failed to get Apis"
            );
        },
        getSettings: function () {
            return umbRequestHelper.resourcePromise(
                $http.get("/umbraco/Campaignr/Api/GetSettings"),
                "Failed to get settings"
            );
        },
        updateSettings: function (model) {
            return umbRequestHelper.resourcePromise(
                $http.post("/umbraco/Campaignr/Api/SaveSettings", model, {
                    withCredentials: true,
                }),
                "Failed to update APi Settings"
            );
        },
    };
}
angular.module("umbraco.resources").factory("cmSettingsResources", cmSettingsResources);