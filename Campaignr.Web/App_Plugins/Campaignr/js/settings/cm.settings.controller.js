function cmSettingsController($scope, $http, umbRequestHelper, notificationsService, cmSettingsResources) {
    var vm = this;
    vm.loading = true;
    vm.saveSettings = saveSettings;

    function init() {

        cmSettingsResources.getSettings().then(function (results) {

            vm.apiNames = results.Apis;

            vm.name = results.Name;
            vm.username = results.Username;
            vm.password = results.Password;

            vm.loading = false;
        });
    }

    function saveSettings() {

        cmSettingsResources.updateSettings(vm).then(function (results) {
            vm.saveSuccessful = results;
            if (vm.saveSuccessful) {
                notificationsService.success('Settings Updated', 'The Integration settings have been updated.');
            }
            else {
                notificationsService.success('Error', 'Settings could not be saved.');
            }
            return true;
        });

        return false;
    }

    init();
}

angular.module("umbraco").controller("Umbraco.Dashboard.cmSettingsController", cmSettingsController);