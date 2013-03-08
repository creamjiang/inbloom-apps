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
 * SectionWidget
 *
 * Groups are grouped into Sections depending on their last modified data
 */
student_grouping.sectionWidget = function (sectionModel) {
    var me = this;
    this.pubSub = PubSub;

    this.sectionModel = sectionModel;
    this.groupWidgets = [];
    this.groupSectionClass = '.group-section';

    this.containerId = '';

    this.groupListClass = '.group-list';
    this.groupSectionTitleClass = '.group-section-title';
    this.groupSectionTemplate = '<div class="group-section">' +
									'<div class="group-section-title"></div>' +
									'<div class="group-list"> </div>' +
								'</div>';
    this.groupListClass = ".group-list";
    this.groupListItemClass = ".group-list-item";

    /**************************
     * SETUP METHODS
     **************************/
    /**
     * Initialize this widget
     */
    this.init = function () {
        me.containerId = "#" + me.sectionModel.getId();
        me.setupSubscriptions();
    }

    /**
     * Sets up listeners for pubsub events
     */
    this.setupSubscriptions = function () {

        me.pubSub.subscribe('group-deleted', me.removeGroup);

        me.pubSub.subscribe('filter-group', me.filterGroup);

        me.pubSub.subscribe('group-saved', me.moveGroupToTop);
    }

    /**************************
     * METHODS
     **************************/
    /**
     * Add the given group to this seciton 
     * @param 
     */
    this.addGroup = function (groupModel) {
        var firstGroupModel = me.sectionModel.getGroupByIndex(0);
        me.sectionModel.addGroup(groupModel);

        var groupWidget = new student_grouping.groupListItemWidget(groupModel);
        var groupWidgetTemplate = groupWidget.generateTemplate();

        // check if the new group was modified at a later date than the first group in the list,
        // then insert at the beginning of the list
        if (firstGroupModel !== undefined) {
            var firstGroupDate = firstGroupModel.getLastModTime();
            var groupDate = groupModel.getLastModTime();

            if (groupDate.isAfter(firstGroupDate)) {
                $(me.containerId).find(me.groupListClass).prepend(groupWidgetTemplate);
            } else {
                $(me.containerId).find(me.groupListClass).append(groupWidgetTemplate);
            }
        } else {
            $(me.containerId).find(me.groupListClass).append(groupWidgetTemplate);
        }

        groupWidget.init();
        me.groupWidgets[groupModel.getId()] = groupWidget;
    }

    /**
     * Remove the given group from the list
     */
    this.removeGroup = function (groupId) {
        me.sectionModel.removeGroup(groupId);
        delete me.groupWidgets[groupId];

        // remove this section if there are no groups left
        if (me.sectionModel.groupModels.length === 0) {
            $(me.containerId).remove();
            me.pubSub.publish('remove-section', me.sectionModel.getId());
        }
    }

    /**
     * Return the HTML content for this object 
     */
    this.generateTemplate = function () {
        var template = $(me.groupSectionTemplate);
        $(template).attr('id', me.sectionModel.getId());

        $(template).find(me.groupSectionTitleClass).html(me.sectionModel.title);
        return template;
    }

    /**
     * Filter the list of groups by name
     */
    this.filterGroup = function (groupName) {
        var containsGroups = false;
        var groupWidgets = me.groupWidgets;
        for (var groupId in groupWidgets) {
            var groupWidget = groupWidgets[groupId];
            if (groupWidget.groupNameContains(groupName)) {
                groupWidget.toggleVisible(true);
                containsGroups = true;
            } else {
                groupWidget.toggleVisible(false);
            }
        }

        if (!containsGroups) {
            me.toggleVisible(false);
        } else {
            me.toggleVisible(true);
        }
    }

    /**
     * Hide/show section
     */
    this.toggleVisible = function (visible) {
        if (visible) {
            $(me.containerId).show();
        } else {
            $(me.containerId).hide();
        }
    }

    /**
     * Return the ids of the selected groups
     */
    this.getSelectedGroups = function () {
        var selGroups = [];
        var groupWidgets = me.groupWidgets;
        for (var groupId in groupWidgets) {
            var groupWidget = groupWidgets[groupId];
            if (groupWidget.isSelected()) {
                selGroups.push(groupWidget);
            }
        }
        return selGroups;
    }

    /**
     *
     */
    this.moveGroupToTop = function (groupModel) {
        var groupId = groupModel.getId();
        if (me.sectionModel.hasGroup(groupId)) {
            var groupModel = me.sectionModel.getGroupById(groupId);
            // determine if group should be in this section or move to a new one
            var sectionDate = me.sectionModel.date;
            var groupDate = groupModel.getLastModTime();
            var groupWidget = me.groupWidgets[groupId];

            if (sectionDate.getDate() !== groupDate.getDate()) {
                me.sectionModel.removeGroup(groupId);
                
                var groupWidget = me.groupWidgets[groupId];
                groupWidget.remove();
                me.pubSub.publish('add-new-group', groupModel, true);

                // remove this section if there are no more groups
                if (me.sectionModel.groupModels.length === 0) {
                    me.pubSub.publish('remove-section', me.sectionModel.getId());
                    $(me.containerId).remove();
                }
            } else {
                // get the list item html
                var groupLi = $(me.containerId).find(me.groupListClass).find("#" + groupId);

                // move to the top of the list in this section as this would be the most recently update group
                $(me.containerId).find(me.groupListClass).prepend(groupLi);

                groupWidget.applySelectedStyle();
            }
            
            me.pubSub.publish('move-arrow');
        }
    }
}