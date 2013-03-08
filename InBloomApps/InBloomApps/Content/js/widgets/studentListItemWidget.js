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

student_grouping.studentListItemWidget = function (studentModel) {
    var me = this;
    this.pubSub = PubSub;

    this.studentModel = studentModel;
    this.containerId = '';
    this.studentLiContainerClass = '.studentListItem';
    this.studentLiSelectedClass = 'studentListItem-selected'; // not using as selector, so dont need the .
    this.visible = true;

    this.idAttributeName = 'data-studentId';
    this.listItemClass = '.studentListItem';
    this.groupIndicatorsClass = '.group-indicators';

    // student info popover
    this.studentInfoBtnClass = '.student-info-btn';
    this.studentInfoPopoverElem = '#student-info-popover';
    this.studentInfoAttributesElem = '#student-attributes';
    this.studentInfoTestScoreElem = '#student-testScore-chart';
    this.studentInfoLearningStyleElem = 'student-learningStyle-chart'; // jqplot doesn't need the # selector

    // student attributes
    this.nameClass = '.student-name';
    this.iconClass = '.student-icon';
    this.gpaClass = '.gpa';
    this.selBoxClass = '.student-selBox';

    // elems with tooltips
    this.tooltipElems = [this.selBoxClass];

    /**
	 * HTML template to be rendered to screen 
	 */
    this.listItemTemplate = '<li data-studentId="" class="studentListItem multidraggable disable-select">' +
								'<div class="group-indicators"></div>' +
								'<div class="student-icon-div"><img class="student-icon"/></div>' +
								'<div class="student-container">' +
									'<p class="student-name"></p>' +
									'GPA <span class="gpa"></span>' +
									'<input type="checkbox" class="student-selBox"/>' +
								'</div>' +
							    '<img class="hide-button student-info-btn" src="/Content/img/group-info-icon.png"></img>' +
							'</li>';

    /**************************
     * SETUP METHODS
     **************************/
    /**
     * Attach event handlers 
     */
    this.init = function () {
        
        // set up the tooltips
        var tooltipElems = this.tooltipElems;
        _.each(tooltipElems, function (e) {
            var tooltip = tooltipText[e];
            var elem = $(me.studentLiContainer).find(e);
            utils.uiUtils.showTooltip(elem, tooltip.message, tooltip.placement, 'hover');
        });

        this.setupEventHandlers();
        this.setupSubscriptions();
    };

    /**
     * Sets up the event handlers for user interaction with the widget
     */
    this.setupEventHandlers = function () {
        var studentId = me.studentModel.getId();
        me.containerId = "li[data-studentId='" + studentId + "']";
        $(me.containerId).find(me.selBoxClass)
			.change(function (event) {
			    var elem = event.currentTarget;
			    var selected = $(elem).prop('checked');
			    me.toggleStudentSelection(selected);

			    //notify others that a student has been selected/deselected
			    me.pubSub.publish('student-selection-changed', studentId);
			});

        $(me.containerId).find(me.studentInfoBtnClass).click(function (event) {
            me.showStudentInfoPopover();
        });

        $(me.containerId).click(function (event) {
            // close the student info popover if still open
            var popoverStudentId = $(me.studentInfoPopoverElem).attr('data-studentContainerId');
            if (popoverStudentId !== studentId) {
                $(me.studentInfoPopoverElem).hide();
            }

            $(me.studentLiContainerClass).removeClass(me.studentLiSelectedClass);
            $(me.containerId).addClass(me.studentLiSelectedClass);
        });
    }

    /**
     * Sets up listeners for pubsub events
     */ 
    this.setupSubscriptions = function () {
        this.pubSub.subscribe('group-removed', function (groupId) {
            me.removeGroupIndicator(groupId);
        });

        this.pubSub.subscribe('group-deleted', function (groupId) {
            me.removeGroupIndicator(groupId);
        });

        this.pubSub.subscribe('update-group-id', function (originalGroupId, newGroupId) {
            me.updateGroupIndicatorId(originalGroupId, newGroupId);
        });

        this.pubSub.subscribe('student-added-to-group', function (studentId, groupWidget) {
            if (me.studentModel.getId() === studentId) {
                me.addGroupIndicator(groupWidget.groupModel.getId(), groupWidget.color.background);
            }
        });
        /** 
         * Listen to when this student is removed from a group
         */
        this.pubSub.subscribe('student-removed-from-group', function (studentId, groupModel) {
            if (me.studentModel.getId() === studentId) {
                me.removeGroupIndicator(groupModel.getId());
            }
        });
    }

    /**************************
     * METHODS
     **************************/
    /**
	 * Select/deselect this student
	 * @param {Boolean} selected
	 */
    this.toggleStudentSelection = function (selected) {
        if (selected) {
            $(me.containerId).addClass('ui-multidraggable');
        } else {
            $(me.containerId).removeClass('ui-multidraggable');
        }
        $(me.containerId).find(me.selBoxClass).prop('checked', selected);
    },

    /**
	 * Add group indicator to this student
	 * @param {String} groupId
	 * @param {String} color 
	 */
	this.addGroupIndicator = function (groupId, color) {
	    $(me.containerId).find(me.groupIndicatorsClass)
			.append("<div data-groupId='" + groupId + "' class='circle' style='background-color: " + color + "'/>");
	}

    /**
	 * Remove the group indicator for the given group	 
	 * @param {String} groupId
	 */
    this.removeGroupIndicator = function (groupId) {
        $(me.containerId)
			.find("div[data-groupId='" + groupId + "']").remove();
    }

    /** 	 	
     * Update the group id on the matching group indicator
     */
    this.updateGroupIndicatorId = function (originalGroupId, newGroupId) {
        var groupIndicator = $(me.containerId).find("div[data-groupId='" + originalGroupId + "']");
        $(groupIndicator).attr('data-groupId', newGroupId);
    }

    /**
	 * Fill html template with student data
	 */
    this.generateTemplate = function () {
        var studentModel = me.studentModel;
        var template = $(me.listItemTemplate);
        $(template).attr('data-studentId', studentModel.getId());
        $(template).find(me.nameClass).html(studentModel.getName());
        $(template).find(me.nameClass).attr('title', studentModel.getName());

        // CHANGE HERE TO INCLUDE STUDENT PROFILE PICTURES
        var studentIcon = utils.uiUtils.getStudentIcon(studentModel.getGender());

        $(template).find(me.iconClass).attr('src', studentIcon);
        $(template).find(me.gpaClass).html(studentModel.getProp('cumulativeGradePointAverage'));

        return template;
    }

    /**
	 * Show/Hide this student
	 * @param {boolean} visible 
	 */
    this.toggleVisible = function (visible) {
        if (visible) {
            $(me.containerId).show();
        } else {
            $(me.containerId).hide();

            // unselect the student if it 
            if ($(me.containerId).hasClass('ui-multidraggable')) {
                $(me.containerId).removeClass('ui-multidraggable');
                $(me.containerId).find(me.selBoxClass).attr('checked', false);
            }
        }
        me.visible = visible;
    }

    /**
	 * TODO add description
	 */
    this.showStudentInfoPopover = function () {

        var studentContainer = $(me.containerId);
        var studentId = me.studentModel.getId();

        var popover = $(me.studentInfoPopoverElem);
        var popoverStudentContainerId = $(popover).attr('data-studentContainerId');

        // check if popover is already open
        var notOpen = $(popover).css('display') === 'none';
        if (notOpen || studentId !== popoverStudentContainerId) {

            // populate student info content
            $(me.studentInfoAttributesElem).empty();
            // TODO remove hardcoding
            var attributesToShow = [
                { attributeId: 'sections', attributeName: ' Classes ' },
                { attributeId: 'cumulativeGradePointAverage', attributeName: 'Cummulative GPA ' },
                { attributeId: 'disabilities', attributeName: 'Disabilities' }
            ];
            _.each(attributesToShow, function (attr) {
                var attributeDiv = me.populateAttributeDiv(attr);
                $(me.studentInfoAttributesElem).append(attributeDiv);
            });

            // place the popover relative to the student container
            var position = $(studentContainer).offset();
            var width = $(studentContainer).width();
            var height = $(studentContainer).height();
            var marginLeft = parseFloat($(studentContainer).css('margin-left').replace('px', ''));

            $(popover).attr('data-studentContainerId', studentId);
            $(popover).css('left', position.left + width + marginLeft);
            $(popover).css('top', position.top);
            $(popover).css('display', '');

            // draw table for assessment data
            //this.drawAssessmentTable

            // draw the required charts
            me.drawLearningStylesChart();

        } else {
            // close it
            $(popover).css('display', 'none');
        }
    }

    /**
	 * Draws the learning styles chart
	 */
    this.drawLearningStylesChart = function () {
        $("#" + me.studentInfoLearningStyleElem).empty();
        var studentModel = me.studentModel;
        var learningStylesData = [
			['Auditory', studentModel.getProp('auditoryLearning')],
			['Tactile', studentModel.getProp('tactileLearning')],
			['Visual', studentModel.getProp('visualLearning')]
        ];
        var labels = _.map(learningStylesData, function (style, key) {
            return style[0] + " " + style[1] + "%";
        });
        var chart = $.jqplot(me.studentInfoLearningStyleElem, [learningStylesData],
		{
		    seriesDefaults: {
		        renderer: $.jqplot.PieRenderer,
		        rendererOptions: {
		            dataLabels: labels,
		            showDataLabels: true
		        }
		    },
		    // green, blue, red
		    seriesColors: ['#C73C39', '#8AAF3F', '#3771B8'],
		    grid: {
		        background: 'transparent',
		        borderWidth: 0,
		        shadow: false
		    }
		});
    }

    /**
     * Draws a table with the student assessment data
     */
    this.drawAssessmentTable = function () {

    }

    /**
	 * TODO add description and refactor lookups to make generic
	 */
    this.populateAttributeDiv = function (attribute) {
        var div = $("<div>");
        $(div).append('<strong>' + attribute.attributeName + ' : </strong>');

        var studentModel = me.studentModel;
        var studentAttrVal = studentModel.getProp([attribute.attributeId]);

        // lookups
        if (attribute.attributeId === 'sections') {
            var sectionNames = [];
            var studentSections = studentAttrVal;
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
            studentAttrVal = sectionNames;
        }

        var str = ""
        if ($.isArray(studentAttrVal)) {
            _.each(studentAttrVal, function (c) {
                str += (c + ' ,');
            });
            str = str.substring(0, str.length - 2);
        } else {
            str = studentAttrVal;
        }

        $(div).append(str);
        return div;
    }

    /**
	 * TODO make this part of the model logic
     * Returns TRUE if this student is assigned to at least one group 
	 */
    this.inAGroup = function () {
        var inAGroup = $(me.containerId).find(me.groupIndicatorsClass).children().length > 0;
        return inAGroup;
    }
}