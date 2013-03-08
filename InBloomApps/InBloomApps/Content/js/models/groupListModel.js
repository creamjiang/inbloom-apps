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
 * List of group models
 * @param groupModels
 */
student_grouping.groupListModel = function () {
    var me = this;

    this.groupModels = [];

    /**************************
     * METHODS
     **************************/
    /**
     * Add the given group to the list
     */
    this.addGroup = function (groupModel) {
        me.groupModels.push(groupModel);
    }

    /**
     * Adds a list of groups
     */
    this.addGroups = function (groupModels) {
        _.each(groupModels, function (groupModel) {
            me.addGroup(groupModel);
        });
    }

    /**
     * Return the groupModel with the given groupId
     */
    this.getGroupById = function (groupId) {
        var groupModel = _.find(me.groupModels, function (gm) {
            return gm.getId() === groupId;
        });
        return groupModel;
    }

    /**
     * Return the groupModel with the given name
     */
    this.getGroupByName = function (groupName) {
        var groupModel = _.find(me.groupModels, function (gm) {
            return gm.groupData.cohortIdentifier === groupName;
        });
        return groupModel;
    }
}