function cmContactsController($scope, $http, umbRequestHelper, notificationsService, cmContactsResources, cmListsResources) {
    var vm = this;
    vm.loading = true;
    vm.refresh = true;

    vm.canContactAdd = false;
    vm.canContactEdit = true;
    vm.canContactDelete = true;

    vm.canListAdd = false;
    vm.canListEdit = true;
    vm.canListDelete = false;

    vm.view = '';
    vm.changeView = changeView;
    vm.deleteContact = deleteContact;
    vm.deleteList = deleteList;
    vm.updateContacts = updateContacts;
    vm.updateLists = updateLists;

    vm.nextPage = nextPage;
    vm.prevPage = prevPage;

    vm.select = {
        Status: "",
    };
    vm.pagination = {
        pageSize: 20,
        page: 1,
        maxPageNumber: -1,
        showPrev: false,
        showNext: true,
    };
    vm.orderBy = "+Created";

    function init() {
        vm.loading = false;
        vm.view = 'dashboard';
    }

    function nextPage() {
        if (vm.pagination.maxPageNumber === -1 || vm.pagination.page < vm.pagination.maxPageNumber) {
            vm.pagination.page = vm.pagination.page + 1;
            // Run current get function with updated page number
            vm.refresh = true;
            switch (vm.view) {
                case 'contacts':
                    cmContactsResources.getAllContacts(vm.pagination.pageSize, vm.pagination.page, vm.select.SearchTerm, vm.select.Status, vm.orderBy).then(function (results) {
                        vm.contacts = results;
                        // if empty results
                        if (!vm.contacts.length) {
                            //   Don't update the page
                            vm.pagination.page = vm.pagination.page - 1;
                            //   Don't update the pagination object
                            updateContacts(false);
                            //   Set Max Page to this page number
                            vm.pagination.maxPageNumber = vm.pagination.page;
                        }
                        // if we don't fill a page, know we are on the last
                        if (vm.contacts.length < vm.pagination.pageSize) {
                            vm.pagination.maxPageNumber = vm.pagination.page;
                        }
                        vm.refresh = false;
                        showPagination();
                    });
                    break;
                case 'lists':
                    cmListsResources.getAllLists(vm.pagination.pageSize, vm.pagination.page, vm.select.SearchTerm, vm.select.Status, vm.orderBy).then(function (results) {
                        vm.lists = results;
                        // if empty results
                        if (!vm.lists.length) {
                            //   Don't update the page
                            vm.pagination.page = vm.pagination.page - 1;
                            //   Don't update the pagination object
                            updateLists(false);
                            //   Set Max Page to this page number
                            vm.pagination.maxPageNumber = vm.pagination.page;
                        }
                        // if we don't fill a page, know we are on the last
                        if (vm.lists.length < vm.pagination.pageSize) {
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
                case 'contacts':
                    cmContactsResources.getAllContacts(vm.pagination.pageSize, vm.pagination.page, vm.select.SearchTerm, vm.select.Status, vm.orderBy).then(function (results) {
                        vm.contacts = results;
                        vm.refresh = false;
                        showPagination();
                    });
                    break;
                case 'lists':
                    cmListsResources.getAllLists(vm.pagination.pageSize, vm.pagination.page, vm.select.SearchTerm, vm.orderBy).then(function (results) {
                        vm.lists = results;
                        vm.refresh = false;
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

    function changeView(newView, arg0) {
        vm.loading = true;
        vm.contact = null;
        vm.customData = null;
        vm.list = null;
        vm.contacts = null;
        vm.lists = null;

        switch (newView) {
            case 'contacts':
                if (vm.view === 'dashboard') {
                    vm.select = {
                        SearchTerm: "",
                        Status: "",
                    };
                    vm.orderBy = '+Created';
                    vm.pagination.page = 1;
                    vm.pagination.maxPageNumber = -1;
                }
                cmContactsResources.getAllContacts(vm.pagination.pageSize, vm.pagination.page, vm.select.SearchTerm, vm.select.Status, vm.orderBy).then(function (results) {
                    vm.contacts = results;
                    vm.view = newView;
                    // if we don't fill a page, know we are on the last
                    if (vm.contacts.length < vm.pagination.pageSize) {
                        vm.pagination.maxPageNumber = vm.pagination.page;
                    }
                    showPagination();
                    vm.loading = false;
                    vm.refresh = false;
                });
                break;
            case 'lists':
                if (vm.view === 'dashboard') {
                    vm.select = {
                        SearchTerm: "",
                    };
                    vm.orderBy = '+Created';
                    vm.pagination.page = 1;
                    vm.pagination.maxPageNumber = -1;
                }
                cmListsResources.getAllLists(vm.pagination.pageSize, vm.pagination.page, vm.select.SearchTerm, vm.orderBy).then(function (results) {
                    vm.lists = results;
                    vm.view = newView;
                    // if we don't fill a page, know we are on the last
                    if (vm.lists.length < vm.pagination.pageSize) {
                        vm.pagination.maxPageNumber = vm.pagination.page;
                    }
                    showPagination();
                    vm.loading = false;
                    vm.refresh = false;
                });
                break;
            case 'contact-view':
                cmContactsResources.getContact(arg0).then(function (results) {
                    vm.contact = results;
                    vm.customData = results.CustomData;
                    delete vm.contact.CustomData;
                    vm.view = newView;
                    vm.loading = false;
                });
                break;
            case 'list-view':
                cmListsResources.getList(arg0).then(function (results) {
                    vm.list = results;
                    vm.view = newView;
                    vm.loading = false;
                    cmContactsResources.getContactInList(arg0).then(function (res) {
                        vm.contacts = res;
                    });
                });
                break;
            default:
                vm.view = newView;
                vm.loading = false;
                break;
        }
    }

    function deleteContact(arg0, arg1) {
        cmContactsResources.deleteContact(arg0).then(function (unsub) {
            if (vm.view === 'contacts') {
                cmContactsResources.getAllContacts(vm.pagination.pageSize, vm.pagination.page, vm.select.SearchTerm, vm.select.Status, vm.orderBy).then(function (results) {
                    vm.contacts = results;
                    notificationsService.success('Contact Unsubscribed', 'The Contact has been unsubscribed.');
                });
            }
            if (vm.view === 'contact') {
                cmContactsResources.getContact(arg1).then(function (results) {
                    vm.contact = results;
                    notificationsService.success('Contact Unsubscribed', 'The Contact has been unsubscribed.');
                });
            }
        });
    }

    function deleteList(arg0) {
        cmListsResources.deleteList(arg0).then(function (unsub) {
            if (vm.view === 'lists') {
                cmListsResources.getAllLists().then(function (results) {
                    vm.lists = results;
                    notificationsService.success('List Deleted', 'The List has been deleted.');
                });
            }
            if (vm.view === 'list-view') {
                cmListsResources.getAllLists().then(function (results) {
                    vm.view = "lists";
                    vm.lists = results;
                    notificationsService.success('List Deleted', 'The List has been deleted.');
                });
            }
        });
    }

    function updateContacts(refresh) {
        if (refresh) {
            vm.refresh = true;
            vm.pagination.page = 1;
        }
        cmContactsResources.getAllContacts(vm.pagination.pageSize, vm.pagination.page, vm.select.SearchTerm, vm.select.Status, vm.orderBy).then(function (results) {
            vm.contacts = results;
            if (refresh) {
                vm.refresh = false;
                showPagination();
            }
        });
    }

    function updateLists(refresh) {
        if (refresh) {
            vm.refresh = true;
        }
        cmListsResources.getAllLists(vm.pagination.pageSize, vm.pagination.page, vm.select.SearchTerm, vm.orderBy).then(function (results) {
            vm.lists = results;
            if (refresh) {
                vm.refresh = false;
            }
        });
    }

    init();
}

angular.module("umbraco").controller("Umbraco.Dashboard.cmContactsController", cmContactsController);