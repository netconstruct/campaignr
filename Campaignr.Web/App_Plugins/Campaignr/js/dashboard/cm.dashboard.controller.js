function cmDashboardController($scope, $http, umbRequestHelper, notificationsService, cmDashboardResources, cmSettingsResources) {
    var vm = this;
    vm.loading = true;
    vm.link = '';
    vm.invalidSetUp = false;

    vm.goTo = goTo;

    function init() {

        cmSettingsResources.getSettings().then(function (results) {
            if (!results.ValidSettings) {
                vm.invalidSetUp = true;
                return;
            }
        });

        cmDashboardResources.getAddCampaignLink().then(function (results) {
            vm.link = results;
        });

        cmDashboardResources.getAllCampaignStats().then(function (results) {
            vm.stats = results;
            vm.loading = false;
        });

    }
    init();
}

angular.module("umbraco").controller("Umbraco.Dashboard.cmDashboardController", cmDashboardController);