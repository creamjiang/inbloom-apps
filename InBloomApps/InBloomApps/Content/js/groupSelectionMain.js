/*
 * Copyright 2012-2013 inBloom, Inc. and its affiliates.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

var student_grouping = student_grouping || {};

student_grouping.groupSelectionMain = function () {
    var me = this;
    this.pubSub = PubSub;

    this.timeout = 1200000;
    this.mainContent = "#group-selection";
    this.topbarControlClass = ".top-bar-controls";
    this.userInactivityModal = "#user-inactivity-modal";
    this.userInactivityOkBtn = ".user-inactivity-ok";
    this.headerClass = '.inbloom-header';
    this.footerClass = '.inbloom-footer';

    /**
     * Initialize the app
     */
    this.init = function () {
       
        student_grouping.sectionListWidgetComponent = new student_grouping.sectionListWidget();
        student_grouping.groupDetailsWidgetComponent = new student_grouping.groupDetailsWidget();
        student_grouping.groupSelectionTopbarWidgetComponent = new student_grouping.groupSelectionTopbarWidget();

        // set the size of page
        var windowHeight = $(window).height();
        var headerHeight = $(me.headerClass).outerHeight();
        var footerHeight = $(me.footerClass).outerHeight();
        var topbarControlsHeight = $(me.topbarControlClass).height();
        $(".main-content").height((windowHeight - topbarControlsHeight - headerHeight - footerHeight - 5) + 'px');

        me.setupIdleTimer();

        // grab the data from the server
        $.ajax({
            type: 'GET',
            url: 'Group',
            success: function (data) {

                if (data.sections === undefined) {
                    window.location = "/Home";
                }

                student_grouping.sections = data.sections;

                // setup the groups list
                var groups = data.cohorts;
                var groupModels = _.map(groups, function (group) {
                    var groupModel = new student_grouping.groupModel(group);
                    groupModel.init();
                    return groupModel;
                });
                student_grouping.sectionListWidgetComponent.init(groupModels);

                // setup the group details component
                var students = data.students;
                var studentModels = _.map(students, function (student) {
                    return new student_grouping.studentModel(student);
                });
                student_grouping.groupDetailsWidgetComponent.init(studentModels, data.dataElements);

                student_grouping.groupSelectionTopbarWidgetComponent.init();
                $(me.mainContent).spin(false);
                $(me.mainContent).css('opacity', 1);
            },
            error: function (errorMsg) {
                $(me.mainContent).spin(false);
                $(me.mainContent).css('opacity', 1);
                window.location = 'Error';
            }
        });

        $(me.userInactivityOkBtn).click(function (event) {
            me.logout();
        });

        // notify others when window size has changed
        $(window).resize(function () {
            me.pubSub.publish('window-resized', innerWidth, innerHeight);
        });

        me.pubSub.subscribe('logout', me.logout);
    }

    /**
     * Detect inactivity. If user has been idle for 20 minutes,
     * warn the user and then perform a logout operation
     */
    this.setupIdleTimer = function () {
        $.idleTimer(me.timeout);
        $(document).bind('idle.idleTimer', function () {
            setTimeout(function () {
                me.logout();
            }, 10000);
            $(me.userInactivityModal).modal('show');
        });
    }

    /**
         * Log the user out, kill the session
         */
    this.logout = function () {
        $.ajax({
            type: 'GET',
            url: 'Logout',
            success: function (result) {
                if (result.logout) {
                    window.location = "/Home";
                } else {
                    window.location = "Error";
                }
            },
            error: function (result) {
                window.location = "Error";
            }
        });
    }

}

// initialize module
$(function () {
    var main = new student_grouping.groupSelectionMain();
    main.init();
    $(main.mainContent).spin();
});