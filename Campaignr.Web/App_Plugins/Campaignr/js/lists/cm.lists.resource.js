function cmListsResources($q, $http, umbRequestHelper) {
    return {
        getAllLists: function (pageSize, page, searchTerm, sortBy) {
            return umbRequestHelper.resourcePromise(
                $http.get(`/umbraco/Campaignr/Api/GetLists?pageSize=${pageSize}&page=${page}&searchTerm=${searchTerm}&sortBy=${sortBy}`),
                "Failed to get Lists"
            );
        },
        getList: function (id) {
            return umbRequestHelper.resourcePromise(
                $http.get(`/umbraco/Campaignr/Api/GetList?listId=${id}`),
                "Failed to get List"
            );
        },
        updateList: function (fd) {
            return umbRequestHelper.resourcePromise(
                $http.get("/umbraco/api/cm/lists/update", fd, {
                    withCredentials: true,
                }),
                "Failed to update List"
            );
        },
        deleteList: function (id) {
            return umbRequestHelper.resourcePromise(
                $http.get(`/umbraco/Campaignr/Api/DeleteList?listId=${id}`),
                "Failed to delete List"
            );
        },
    };
}
angular.module("umbraco.resources").factory("cmListsResources", cmListsResources);