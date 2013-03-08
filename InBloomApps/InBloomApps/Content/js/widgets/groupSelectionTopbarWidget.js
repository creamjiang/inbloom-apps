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

student_grouping.groupSelectionTopbarWidget = function () {
    var me = this;
    this.dirty = false;
    this.pubSub = PubSub;

    this.containerId = ".top-bar-controls";
    this.editMultipleGroupsBtn = "#edit-multiple-groups-btn";
    this.groupSearchTxtElem = "#group-search-txt";
    this.groupSearchBtnClass = ".group-search-btn";
    this.groupSearchClearBtnClass = ".group-search-clear-btn";
    this.createGroupsBtn = '#create-groups-btn';
    this.helpBtn = '#help-btn';
    this.numGroupsCreateTxt = '.num-groups-create-txt';
    this.logoutBtnClass = '#logout-btn';

    this.tooltipElems = [this.editMultipleGroupsBtn, this.helpBtn];

    /**************************
     * SETUP METHODS
     **************************/
    /**
     * Initialize this widget
     */
    this.init = function () {

        // set up the tooltips
        var tooltipElems = this.tooltipElems;
        _.each(tooltipElems, function (e) {
            var tooltip = tooltipText[e];
            var elem = $(me.containerId).find(e);
            utils.uiUtils.showTooltip(elem, tooltip.message, tooltip.placement, 'hover');
        });

        me.setupEventHandlers();
        me.setupSubscriptions();
    }

    /**
     * Sets up the event handlers for user interaction with the widget
     */
    this.setupEventHandlers = function () {

        // add event handler for filter student list using search box
        $(me.groupSearchTxtElem).keyup(function () {
            var filterVal = $(this).val();
            me.filterGroupsByName(filterVal);
        });

        $(me.groupSearchClearBtnClass).click(function (event) {
            me.clearGroupSearch();
        });

        $(me.createGroupsBtn).click(function (event) {
            me.createGroups();
        });

        $(me.helpBtn).click(function (event) {
            me.showHelp();
        });

        $(me.editMultipleGroupsBtn).click(function (event) {
            me.editMultipleGroups();
        });

        $(me.logoutBtnClass).click(function (event) {
            if (student_grouping.groupDetailsWidgetComponent.dirty) {
                var confirmDirtyLogout = confirm("You have unsaved changes. Would you still like to log out?");
                if (!confirmDirtyLogout) {
                    return;
                }
            }
            me.pubSub.publish('logout');
        });
    }

    /**
     * 
     */
    this.setupSubscriptions = function () {
        me.pubSub.subscribe('toggle-dirty', me.toggleDirty);
    }

    /**************************
     * METHODS
     **************************/
    /**
     * 
     */
    this.filterGroupsByName = function (groupName) {
        me.pubSub.publish('filter-group', groupName);
    }

    /**
     * Resets the list of filtered groups
     */
    this.clearGroupSearch = function () {
        $(me.groupSearchTxtElem).val('');
        me.pubSub.publish('filter-group', '');
    }

    /**
     * Create the specified number of groups
     */
    this.createGroups = function () {
        // warn user about unsaved changes, if any
        if (me.dirty) {
            var confirm = window.confirm("You have unsaved changes. If you continue these changes will be lost. Continue?");
            if (!confirm) {
                return;
            }
        }
        var numGroups = $(me.numGroupsCreateTxt).val();
        window.location = "MultipleGroupsEdit?create=" + numGroups;
    }

    /**
     * Handle editMultipleGroups btn click event
     */
    this.editMultipleGroups = function () {
        // warn user about unsaved changes, if any
        if (me.dirty) {
            var confirm = window.confirm("You have unsaved changes. If you continue these changes will be lost. Continue?");
            if (!confirm) {
                return;
            }
        }
        me.pubSub.publish('edit-multiple-groups');
    }
    
    /**
     * Show/hide the toolbar
     */
    this.toggleVisible = function (visible) {
        if (visible) {
            $(me.containerId).show();
        } else {
            $(me.containerId).hide();
        }
    }

    /**
     * Toggle the dirty state
     */
    this.toggleDirty = function (dirty) {
        me.dirty = dirty;
    }

    /**
     * Opens up the user guide
     */
    this.showHelp = function () {
        window.open('/Content/static/UserGuide.htm');
    }
}