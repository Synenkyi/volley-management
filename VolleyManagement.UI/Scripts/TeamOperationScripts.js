﻿$(document).ready(function () {

    // register namespace
    var editScope = $("div[vmscope='team_edit']"),
        handlers = VM.addNamespace("team.handlers"),
        teamId = null,
        selectedPlayers = [],
        completerSourceFunc;

    teamId = (function () {
        var teamIdField = $("[name='Id']", editScope);

        return null;
    })();

    //Autocompleter
    completerSourceFunc = function(requestObj, responseHandler) {
        var searchString,
            result;

        if (requestObj.term && requestObj.term.length > 1) {         
            searchString = encodeURI(requestObj.term);
            result = "/Mvc/Players/GetFreePlayers?searchString=" + searchString;
            if (selectedPlayers) {
                
            }
        }

        return false;
    };

    // Register handlers
    handlers.deleteTeam = function (teamId, teamName) {
        var message = $("#DeleteConfirmationMessage").val();
        var confirmation = confirm(message + ' "' + teamName + '" ?');
        if (confirmation) {
            $.ajax({
                url: 'Teams/Delete',
                type: 'POST',
                data: { id: teamId },
                dataType: 'json',
                success: function (resultJson) {
                    alert(resultJson.Message);
                    if (resultJson.OperationSuccessful) {
                        $("#team" + teamId).remove();
                    } else {
                        window.location.pathname = "Mvc/Teams";
                    }
                }
            });
        }
    };

    handlers.changePage = function (newPage, numberOfPages) {
        ajaxPlayersRefresh(newPage);
        printPagesBar(newPage, numberOfPages);
    };

    handlers.ajaxPlayersRefresh = function (pageNumber) {

        $.get("ChoosePlayers",
            { page: pageNumber },
            function (data) { $("#currentPlayersPage").html(data); },
            "html");
    };

    handlers.openChoosingPlayersWindow = function (windowName) {
        chosingWindow = window.open("/Mvc/Players/ChoosePlayers",
            windowName,
            "width=800, height=400, top =200, left=200, status=0, location=0, resizable=1");
    };

    handlers.chooseCaptain = function () {

    };

    handlers.printPagesBar = function (currentPage, numberOfPages) {

        var innerHtml = "<nav><ul class='pagination'>";
        var moveToNextPage = "'changePage(" + (currentPage + 1) + ", " + numberOfPages + ")'";
        var moveToPrevPage = "'changePage(" + (currentPage - 1) + ", " + numberOfPages + ")'";
        var moveToFirstPage = "'changePage(1, " + numberOfPages + ")'";
        var moveToLastPage = "'changePage(" + numberOfPages + ", " + numberOfPages + ")'";

        // move previous page, first page
        if (currentPage == 1) {
            innerHtml += "<li class='disabled'><a href='#' aria-label='Previous'><span aria-hidden='true'>&laquo;</span></a></li></li>";
            innerHtml += "<li class='active'><a href='#'>1 <span class='sr-only'>(current)</span></a></li>";
        } else {
            innerHtml += "<li><a href='#' aria-label='Previous' onclick = " + moveToPrevPage + ">";
            innerHtml += "<span aria-hidden='true'>&laquo;</span></a></li></li>";

            innerHtml += "<li><a href='#' onclick = " + moveToFirstPage + ">1</a></li>";
        }

        // left "..."
        if (currentPage > 3) {
            innerHtml += "<li class='disabled'><a href='#' onclick = " + moveToPrevPage + ">...</a></li></li>";
        }

        // inner three pages

        // prev
        if (currentPage > 2) {
            innerHtml += "<li><a href='#' onclick = " + moveToPrevPage + ">" + (currentPage - 1) + "</a></li></li>";
        }

        //curr
        if (currentPage != 1 && currentPage != numberOfPages) {
            innerHtml += "<li class='active'><a href='#'>" + currentPage + " <span class='sr-only'>(current)</span></a></li>";
        }

        // next
        if (currentPage < (numberOfPages - 1)) {
            innerHtml += "<li><a href='#' onclick = " + moveToNextPage + ">" + (currentPage + 1) + "</a></li></li>";
        }

        // right "..."
        if (currentPage < (numberOfPages - 2)) {
            innerHtml += "<li class='disabled'><a href='#'>...</a></li></li>";
        }

        // move next page, last page
        if (currentPage == numberOfPages) {
            innerHtml += "<li class='active'><a href='#'>" + numberOfPages + " <span class='sr-only'>(current)</span></a></li>";
            innerHtml += "<li class='disabled'><a href='#' aria-label='Next'><span aria-hidden='true'>&raquo;</span></a></li></li>";
        } else {
            innerHtml += "<li><a href='#' onclick = " + moveToLastPage + ">" + numberOfPages + "</a></li>";
            innerHtml += "<li><a href='#' aria-label='Next' onclick = " + moveToNextPage + ">";
            innerHtml += "<span aria-hidden='true'>&raquo;</span></a></li></li>";
        }

        innerHtml += "</ul></nav>";

        var wrapper = $("#pagesBar ul");
        wrapper.empty();
        wrapper.html(innerHtml);
    };

    handlers.allPlayersAreSet = function (rows) {
        'use strict';

        for (var i = 0; i < rows.length; i++) {
            if (rows[i].value === "") {
                return false;
            }
        }

        return true;
    };



    handlers.addRowToTeamPlayersTable = function () {
        'use strict';

        var teamPlayersTable = $("#teamRoster", editScope),
                rows = $('.teamPlayer', teamPlayersTable),
                currentCount = rows.length,
                template;

        if (handlers.allPlayersAreSet(rows)) {
            template = "<tr><td><input type='text' name='Roster[" + currentCount + "].FullName' class='teamPlayer'/></td><td></td></tr>";
            $('#teamRoster > tbody:last', editScope).append(template);
            $('#teamRoster > tbody:last', editScope).autocomlete()
        }

        //var newElementId = "player_" + id;
        //$("#" + newElementId).hide();

        //var indexOfNewPlayer = window.opener.$("#teamRoster tr.rosterPlayer").length;

        //if (window.name == "ChoosingRosterWindow") {
        //    if (!(window.opener.$("#" + newElementId).get(0))) {
        //        var fullNameInput = "<input type='text' name='Roster[" + indexOfNewPlayer + "].FullName' value='" + fullName + "' readonly />";
        //        var idInput = "<input id='" + newElementId + "' type='text' name='Roster[" + indexOfNewPlayer + "].Id' value='" + id + "' hidden />";

        //        var newPlayer = "<tr class='rosterPlayer'><td>" + fullNameInput + "</td><td>" + idInput + "<td></tr>";
        //        window.opener.$("#teamRoster").children().append(newPlayer);
        //    }
        //} else {
        //    window.opener.$("#captainFullName").val(fullName);
        //    window.opener.$("#captainId").val(id);
        //    window.close();
        //}
    };

    handlers.showCreateTeamErrors = function showCreateTeamErrors() {
    };

    /// Bindings
    $(".addPlayerToTeamButton", editScope).bind('click', handlers.addRowToTeamPlayersTable);
})





