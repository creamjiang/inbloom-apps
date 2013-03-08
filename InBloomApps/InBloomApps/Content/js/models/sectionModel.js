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
 * SectionModel 
 * @param id 
 * @param title
 * @param date
 *
 * Sections are groups of cohorts with the same last modified date.
 */
student_grouping.sectionModel = function (id, title, date) {
    var me = this;
    this.pubSub = PubSub;

    this.id = id;
    this.title = title;
    this.date = date;

    this.groupModels = [];

    /**************************
     * GETTER AND SETTERS
     **************************/
    /**
     * Returns the cohort id
     */
    this.getId = function () {
        return me.id;
    }

    /**************************
    * METHODS
    **************************/

    /**
     * Add the given group to this section
     */
    this.addGroup = function (groupModel) {
        me.groupModels.push(groupModel);
    }

    /**
     * Delete the given group from this section
     */
    this.removeGroup = function (groupId) {
        me.groupModels = _.filter(me.groupModels, function (groupModel) {
            return groupModel.getId() !== groupId;
        });
    }

    /**
     * Returns the group with the given id
     */
    this.getGroupById = function (groupId) {
        return _.find(me.groupModels, function (groupModel) {
            return groupModel.getId() === groupId;
        });
    }

    /**
     * Returns true if this section contains the given group
     */
    this.hasGroup = function (groupId) {
        var groupModel = me.getGroupById(groupId);
        return groupModel !== undefined && groupModel !== null;
    }

    /**
     *
     */
    this.getGroupByIndex = function (index) {
        return me.groupModels[0];
    }
}