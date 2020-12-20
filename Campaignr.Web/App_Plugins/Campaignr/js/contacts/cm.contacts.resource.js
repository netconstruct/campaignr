function cmContactsResources($q, $http, umbRequestHelper) {
    return {
        getAllContacts: function (pageSize, page, searchTerm, status, sortBy) {
            return umbRequestHelper.resourcePromise(
                $http.get(`/umbraco/Campaignr/Api/GetContacts?pageSize=${pageSize}&page=${page}&searchTerm=${searchTerm}&sortBy=${sortBy}&status=${status}`),
                "Failed to get Contacts"
            );
        },
        getContact: function (id) {
            return umbRequestHelper.resourcePromise(
                $http.get(`/umbraco/Campaignr/Api/GetContact?contactId=${id}`),
                "Failed to get Contact"
            );
        },
        updateContact: function (fd) {
            return umbRequestHelper.resourcePromise(
                $http.get("/umbraco/Campaignr/Api/UpdateContact", fd, {
                    withCredentials: true,
                }),
                "Failed to update Contact"
            );
        },
        deleteContact: function (email) {
            return umbRequestHelper.resourcePromise(
                $http.get(`/umbraco/Campaignr/Api/UnsubscribeContact?email=${email}`),
                "Failed to delete Contact"
            );
        },
        getContactInList: function (id) {
            return umbRequestHelper.resourcePromise(
                $http.get(`/umbraco/Campaignr/Api/GetContacts?listId=${id}`),
                "Failed to get Contact"
            );
        },
    };
}
angular.module("umbraco.resources").factory("cmContactsResources", cmContactsResources);