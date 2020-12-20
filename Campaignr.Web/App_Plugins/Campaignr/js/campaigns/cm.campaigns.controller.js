function cmCampaignsController($scope, $http, umbRequestHelper, notificationsService, cmCampaignsResources, cmDashboardResources) {
    var vm = this;
    vm.loading = true;
    vm.link = '';

    vm.canAdd = true;
    vm.canEdit = true;
    vm.canDelete = true;

    vm.updateCampaigns = updateCampaigns;
    vm.nextPage = nextPage;
    vm.prevPage = prevPage;

    vm.nextPageContacts = nextPageContacts;
    vm.prevPageContacts = prevPageContacts;

    vm.view = '';
    vm.changeView = changeView;

    vm.select = {
        Name: "",
        Status: "",
    };
    vm.pagination = {
        pageSize: 10,
        page: 1,
        maxPageNumber: -1,
        showPrev: false,
        showNext: true,
    };
    vm.paginationContacts = {
        pageSize: 10,
        page: 1,
        maxPageNumber: -1,
        showPrev: false,
        showNext: true,
    };
    vm.orderBy = "+Created";

    function init() {

        cmDashboardResources.getAddCampaignLink().then(function (res) {
            vm.link = res;
        });

        cmCampaignsResources.getAllCampaigns(vm.pagination.pageSize, vm.pagination.page, vm.select.Name, vm.select.Status, vm.orderBy).then(function (results) {
            vm.campaigns = results;
            vm.view = 'list';
            vm.loading = false;
        });
    }

    function nextPage() {
        if (vm.pagination.maxPageNumber === -1 || vm.pagination.page < vm.pagination.maxPageNumber) {
            vm.pagination.page = vm.pagination.page + 1;
            // Run current get function with updated page number
            vm.refresh = true;
            switch (vm.view) {
                case 'list':
                    cmCampaignsResources.getAllCampaigns(vm.pagination.pageSize, vm.pagination.page, vm.select.Name, vm.select.Status, vm.orderBy).then(function (results) {
                        vm.campaigns = results;
                        // if empty results
                        if (!vm.campaigns.length) {
                            //   Don't update the page
                            vm.pagination.page = vm.pagination.page - 1;
                            //   Don't update the pagination object
                            updateCampaigns(false);
                            //   Set Max Page to this page number
                            vm.pagination.maxPageNumber = vm.pagination.page;
                        }
                        // if we don't fill a page, know we are on the last
                        if (vm.campaigns.length < vm.pagination.pageSize) {
                            vm.pagination.maxPageNumber = vm.pagination.page;
                        }
                        vm.refresh = false;
                        showPagination();
                    });
                    break;
            }
        }
    }
    function prevPage() {
        if (vm.pagination.page > 1) {
            vm.pagination.page = vm.pagination.page - 1;
            // Run current get function with updated page number
            vm.refresh = true;
            switch (vm.view) {
                case 'list':
                    cmCampaignsResources.getAllCampaigns(vm.pagination.pageSize, vm.pagination.page, vm.select.Name, vm.select.Status, vm.orderBy).then(function (results) {
                        vm.campaigns = results;
                        vm.loading = false;
                        showPagination();
                    });
                    break;
            }
        }
    }
    function showPagination() {
        vm.pagination.showNext = vm.pagination.page !== vm.pagination.maxPageNumber;
        vm.pagination.showPrev = vm.pagination.page > 1;
    }

    function nextPageContacts() {
        if (vm.paginationContacts.maxPageNumber === -1 || vm.paginationContacts.page < vm.paginationContacts.maxPageNumber) {
            vm.paginationContacts.page = vm.paginationContacts.page + 1;
            // Run current get function with updated page number
            vm.refresh = true;
            cmCampaignsResources.getCampaignContacts(vm.campaign.Id, vm.paginationContacts.pageSize, vm.paginationContacts.page).then(function (results) {
                vm.contacts = results;
                // if empty results
                if (!vm.contacts.length) {
                    //   Don't update the page
                    vm.paginationContacts.page = vm.paginationContacts.page - 1;
                    //   Don't update the pagination object
                    updateContacts(false);
                    //   Set Max Page to this page number
                    vm.paginationContacts.maxPageNumber = vm.paginationContacts.page;
                }
                // if we don't fill a page, know we are on the last
                if (vm.contacts.length < vm.paginationContacts.pageSize) {
                    vm.paginationContacts.maxPageNumber = vm.paginationContacts.page;
                }
                vm.refresh = false;
                showPaginationContacts();
            });
        }
    }
    function prevPageContacts() {
        if (vm.paginationContacts.page > 1) {
            vm.paginationContacts.page = vm.paginationContacts.page - 1;
            // Run current get function with updated page number
            vm.refresh = true;
            cmCampaignsResources.getCampaignContacts(vm.campaign.Id, vm.paginationContacts.pageSize, vm.paginationContacts.page).then(function (results) {
                vm.contacts = results;
                vm.loading = false;
                showPaginationContacts();
            });
        }
    }
    function showPaginationContacts() {
        vm.paginationContacts.showNext = vm.paginationContacts.page !== vm.paginationContacts.maxPageNumber;
        vm.paginationContacts.showPrev = vm.paginationContacts.page > 1;
    }

    function changeView(newView, arg0) {
        vm.loading = true;
        switch (newView) {
            case 'view-campaign':
                vm.campaigns = null;
                cmCampaignsResources.getCampaign(arg0).then(function (results) {
                    vm.campaign = results;
                    vm.view = newView;
                    vm.loading = false;
                    cmCampaignsResources.getCampaignContacts(arg0, vm.paginationContacts.pageSize, vm.paginationContacts.page).then(function (res) {
                        vm.contacts = res;
                        vm.paginationContacts.maxPageNumber = (results.Contacts / vm.paginationContacts.pageSize).toFixed(0);
                        showPaginationContacts();
                    });
                    cmCampaignsResources.getCampaignLink(arg0).then(function (result) {
                        vm.campaignLink = result
                    });
                });
                break;
            case 'list':
                vm.campaign = null;
                vm.contacts = null;
                vm.paginationContacts = {
                    pageSize: 10,
                    page: 1,
                    maxPageNumber: -1,
                    showPrev: false,
                    showNext: true,
                };
                cmCampaignsResources.getAllCampaigns(vm.pagination.pageSize, vm.pagination.page, vm.select.Name, vm.select.Status, vm.orderBy).then(function (results) {
                    vm.campaigns = results;
                    vm.view = newView;
                    vm.loading = false;
                });
                break;
        }
    }

    function updateCampaigns(refresh) {
        if (refresh) {
            vm.refresh = true;
        }
        cmCampaignsResources.getAllCampaigns(vm.pagination.pageSize, vm.pagination.page, vm.select.Name, vm.select.Status, vm.orderBy).then(function (results) {
            vm.campaigns = results;
            if (refresh) {
                vm.refresh = false;
            }
        });
    }
    function updateContacts(refresh) {
        if (refresh) {
            vm.refresh = true;
        }
        cmCampaignsResources.getCampaignContacts(vm.campaign.Id, vm.paginationContacts.pageSize, vm.paginationContacts.page).then(function (results) {
            vm.contacts = results;
            if (refresh) {
                vm.refresh = false;
            }
        });
    }

    init();
}

angular.module("umbraco").controller("Umbraco.Dashboard.cmCampaignsController", cmCampaignsController);