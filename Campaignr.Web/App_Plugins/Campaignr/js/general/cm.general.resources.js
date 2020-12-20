angular.module("umbraco").filter("getCustomDate", [function () {
    return function (dateTime) {
        var thisTime = new Date(dateTime);
        if (thisTime.getFullYear() === 1) {
            return "N/A";
        }
        return getFormattedDate(thisTime);
    };
}]);

angular.module("umbraco").filter("getCustomDateTime", [function () {
    return function (dateTime) {
        var thisTime = new Date(dateTime);
        if (thisTime.getFullYear() === 1) {
            return "N/A";
        }
        return getFormattedDateTime(thisTime);
    };
}]);


function getFormattedDate(date) {
    var year = date.getFullYear();

    var month = (1 + date.getMonth()).toString();
    month = month.length > 1 ? month : '0' + month;

    var day = date.getDate().toString();
    day = day.length > 1 ? day : '0' + day;

    return day + '/' + month + '/' + year;
}

function getFormattedDateTime(date) {
    var year = date.getFullYear();

    var month = (1 + date.getMonth()).toString();
    month = month.length > 1 ? month : '0' + month;

    var day = date.getDate().toString();
    day = day.length > 1 ? day : '0' + day;

    return day + '/' + month + '/' + year + ' ' + date.getHours() + ':' + date.getMinutes();
}

function goTo(board, name) {
    board.tabs.forEach(tab => {
        tab.active = false;
    });
    board.tabs.filter(tab => tab.alias === name)[0].active = true;
}

angular.module("umbraco").filter("formatString", [function () {
    return function (input) {
        const lowerToUpperRegex = /([a-z])([A-Z])/;
        input = input.replace(lowerToUpperRegex, '$1 $2');
        input = input.replace('_', ' ');
        return input.charAt(0).toUpperCase() + input.slice(1);
    };
}]);
