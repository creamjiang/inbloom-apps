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
 * StudentInGroupWidget
 *
 * Represents a student that has been assigned to a group. 
 * Contains a list of attributes that can be toggled visible/not visible.
 */
student_grouping.studentInGroupWidget = function (groupId, studentModel) {
    var me = this;
    this.pubSub = PubSub;

    this.groupId = groupId;
    this.containerId = '';
    this.studentModel = studentModel;

    /**
     * DOM Element Selectors
     */
    this.droppedElemClass = '.dropped-elem';
    this.studentAttributesClass = '.student-attributes';
    this.fullProfileLinkClass = '.student-full-profile a';
    this.studentIconClass = '.student-icon';
    this.expandedClass = 'expanded'; // not using as selector so don't need the .
    this.collapsedClass = 'collapsed'; // not using as selector so don't need the .
    this.delBtnClass = '.del-button';

    /**
	 * HTML template to be rendered to screen 
	 */
    this.droppedElemTemplate = "<div data-studentId='' class='dropped-elem'>" +
									"<img class='del-button' src='/Content/img/student-close-icon.png'></img>" +
									'<div class="student-icon-div"><img class="student-icon"></div>' +
									"<div class='student-name'></div>" +
									"<div class='student-attributes'></div>" +
                                    "<div class='student-full-profile'><a href='#'>full profile</a></div>" +
								"</div>";

    /**************************
     * SETUP METHODS
     **************************/
    /**
     * Initialize this widget
     * @param groupId - id of group this student belongs to
     * @param collapsed
     * @param selectedAttributes
     * @param draggable
     */
    this.init = function (collapsed, selectedAttributes, draggable) {
        me.containerId = "#gr-" + me.groupId + "-dr-" + me.studentModel.getId();
        var attributesDiv = $(me.containerId).find(me.studentAttributesClass);

        var state = collapsed ? me.collapsedClass : me.expandedClass;
        $(me.containerId).addClass(state);

        me.toggleAttributeVisibility(selectedAttributes);

        if (collapsed) {
            $(attributesDiv).hide();
        }

        if (draggable) {
            // make it draggable to another group
            $(me.containerId).draggable({
                drag: function (event, ui) {

                    // TODO fix after creating groupListWidget
                    // TODO refactor shortcut to groupList component
                    student_grouping.groupListWidgetComponent.currGrp = null;
                },
                revert: "invalid",
                "helper": "clone",
                "opacity": 0.7
            });
        }

        me.setupEventHandlers();
    }

    /**
     * Sets up the event handlers for user interaction with the widget
     */
    this.setupEventHandlers = function () {
        var closeBtn = $(me.containerId).find(me.delBtnClass);
        $(closeBtn).click(function (event) {
            me.pubSub.publish('remove-student-from-group', me.studentModel.getId(), me.groupId);
            me.remove();
        });

        var fullProfileLink = $(me.containerId).find(me.fullProfileLinkClass);
        $(fullProfileLink).click(function (event) {
            window.open("https://databrowser.sandbox.inbloom.org/entities/students/" + me.studentModel.getId());
        });
    }

    /**************************
     * METHODS
     **************************/
    /**
	 * Fill html template with group data and return the template
	 */
    this.generateTemplate = function () {
        var elemDiv = $(me.droppedElemTemplate);
        $(elemDiv).attr('id', 'gr-' + me.groupId + '-dr-' + me.studentModel.getId());
        $(elemDiv).attr('data-studentId', me.studentModel.getId());

        // CHANGE HERE TO INCLUDE STUDENT PROFILE PICTURES
        var studentIcon = utils.uiUtils.getStudentIcon(studentModel.getGender());
        $(elemDiv).find(me.studentIconClass).attr('src', studentIcon);

        $(elemDiv).find('.student-name').html(me.studentModel.getName());
       
        return elemDiv;
    }

    /** 
     * TODO refactor to make lookups generic
     * Show the given attributes on the students in this group
     */
    this.toggleAttributeVisibility = function (selectedAttributes) {

        // empty the attributes div 
        var attributesDiv = $(me.containerId).find(me.studentAttributesClass);
        $(attributesDiv).empty();

        // repopulate the div with the selected attributes
        var student = me.studentModel;
        _.each(selectedAttributes, function (attribute) {
            var name = attribute.attributeName;
            var value = student.getProp(attribute.attributeId);
            if (value === null || value === undefined || value === '' || value.length === 0) {
                value = '[no data]';
            } else if (value instanceof Array) {
                
            }

            // perform lookup for sections
            if (attribute.attributeId === 'sections') {
                var sectionNames = [];
                var studentSections = value;
                var sections = student_grouping.sections;
                _.each(studentSections, function (studentSection) {
                    // find the corresponding section using the id
                    var matchingSection = _.find(sections, function (section) {
                        return section.id === studentSection;
                    });
                    if (matchingSection !== undefined) {
                        sectionNames.push(matchingSection.courseTitle);
                    }
                });
                value = sectionNames;
            }

            $(attributesDiv).append("<div><strong>" + name + "</strong> " + value + "</div>");
        });
    }

    /**
     * Expands or collapses the student attributes based on the param
     * @param expand - whether to expand or collapse the student attributes
     */
    this.toggleExpandedCollapsedState = function (expand) {
        var classToRemove = expand ? this.collapsedClass : this.expandedClass;
        var classToAdd = expand ? this.expandedClass : this.collapsedClass;

        $(me.containerId).removeClass(classToRemove);
        $(me.containerId).addClass(classToAdd);

        // show/hide student attributes
        !expand ? $(me.containerId).find(me.studentAttributesClass).hide()
			: $(me.containerId).find(me.studentAttributesClass).show();
    }

    /**
     * Removes this widget from the screen
     */
    this.remove = function () {
        // remove this widget from screen
        $(me.containerId).remove();
    }
}