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

/**
 * SectionListWidget
 *
 * Contains a list of section widgets.
 */
student_grouping.sectionListWidget = function () {
    var me = this;
    this.pubSub = PubSub;

    this.sectionModels = [];
    this.sectionWidgets = [];
    this.sectionListBox = '.group-section-div .box';
    this.sectionListAntiscrollInner = '.group-section-div .antiscroll-inner';

    this.groupSectionClass = '.group-section';

    this.groupSectionList = '.group-section-list';
    this.editMultipleGroupsBtn = "#edit-multiple-groups-btn";

    this.newSectionId = 1;
    /**************************
     * SETUP METHODS
     **************************/
    /**
     * Initialize this widget
     */
    this.init = function (groupModels) {
        var sections = [];

        // sort the list of groups by lastModified timestamp
        var groups = _.sortBy(groupModels, function (groupModel) {
            var date = groupModel.getLastModTime();
            return parseInt(date.getTime());
        });
        groups.reverse();

        _.each(groups, function (groupModel) {
            me.addGroup(groupModel, false);
        });

        var scrollbar = $(".group-section-div .box-wrap").antiscroll();
        $('.group-section-div .antiscroll-inner').scroll(function () {
            me.pubSub.publish('group-list-scrolled');
        });

        me.setupSubscriptions();
    }

    /**
     * Sets up listeners for pubsub events
     */
    this.setupSubscriptions = function () {

        me.pubSub.subscribe('edit-multiple-groups', me.editMultipleGroups);

        me.pubSub.subscribe('add-new-group', me.addGroup);

        me.pubSub.subscribe('remove-section', me.removeSection);

        me.pubSub.subscribe('window-resized', me.handleWindowResize);
    }

    /**************************
     * METHODS
     **************************/
    /**
     * Add the group to the appropriate section 
     */
    this.addGroup = function (groupModel, prepend) {
        var lastModifiedDate = groupModel.getLastModTime();
        var key = lastModifiedDate.toFormat('DDDD, MMM DD, YYYY');
        var sectionModel = me.sectionModels[key];
        if (sectionModel === undefined || sectionModel === null) {
            
            sectionModel = new student_grouping.sectionModel(
                me.newSectionId++,
                "Last modified " + key,
                lastModifiedDate
            );
            me.sectionModels[key] = sectionModel;

            // TODO check timestamp to prepend or append
            var sectionWidget = new student_grouping.sectionWidget(sectionModel);
            if (prepend) {
                $(me.groupSectionList).prepend(sectionWidget.generateTemplate());
            } else {
                $(me.groupSectionList).append(sectionWidget.generateTemplate());
            }
            
            sectionWidget.init();

            me.sectionWidgets[key] = sectionWidget;
        }
   
        var sectionWidget = me.sectionWidgets[key];
        sectionWidget.addGroup(groupModel);
    }

    /**
     *
     */
    this.editMultipleGroups = function () {
        var selGroups = [];
        for (var sectionId in me.sectionWidgets) {
            var sectionWidget = me.sectionWidgets[sectionId];
            selGroups.push.apply(selGroups, sectionWidget.getSelectedGroups());
        }

        // make sure groups have been selected
        if (selGroups.length === 0) {
            utils.uiUtils.showTooltip(
                        $(me.editMultipleGroupsBtn),
                        'Please select some groups for editing.',
                        'right',
                        'manual',
                        3000);
            return;
        }

        var selGroupIds = _.map(selGroups, function (groupWidget) {
            return groupWidget.groupModel.getId();
        });

        var selGroupIdsStr = selGroupIds.join(",");
        window.location = 'MultipleGroupsEdit?selGroups=' + selGroupIdsStr;
    }

    /**
     * Remove the given section
     */
    this.removeSection = function (sectionId) {
        delete me.sectionModels[sectionId];
        delete me.sectionWidgets[sectionId];
    }

    /**
     * Resize the student list when the window resizes
     */
    this.handleWindowResize = function () {
        // get the width of the box container
        var width = $(me.sectionListBox).width();

        // Firefox and IE aren't entirely compatible with antiscroll 
        // so we need to add some extra width to hide the scrollbar
        if ($.browser.mozilla || $.browser.msie) {
            width += 20;
        }

        $(me.sectionListAntiscrollInner).width(width - 3);
    }
}