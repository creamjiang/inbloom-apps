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
 * StudentListWidget
 *
 * Contains a list of studentWidgets and logic for adding/removing the student
 * widgets from the screen. 
 */
student_grouping.studentListWidget = function () {
    var me = this;
    this.pubSub = PubSub;

    this.studentModels = [];
    this.studentWidgets = [];

    this.studentListBox = '#studentListDiv .box';
    this.studentListAntiscrollInner = '#studentListDiv .antiscroll-inner';
    this.studentListElem = '#studentList';
    this.selectAllBtn = '#select-all-btn';
    this.clearSelectionBtn = '#clear-selection-btn';
    this.randomBtn = '#random-btn';
    this.randomNumTxt = '#random-num-txt';
    this.studentSearchBox = '#txtStudentSearchBox';
   
    this.processing = false;

    /**************************
     * SETUP METHODS
     **************************/
    /**
     * Initialize this widget
     * @params students - server-side students
     */
    this.init = function (studentModels) {

        // add all students to the list
        for (var i = 0; i < studentModels.length; i++) {
            var studentModel = studentModels[i];
            
            var studentListItemWidget = new student_grouping.studentListItemWidget(studentModel);
            $(me.studentListElem).append(studentListItemWidget.generateTemplate());
            studentListItemWidget.init();

            me.studentModels.push(studentModel);
            me.studentWidgets[studentModel.getId()] = studentListItemWidget;
        }

        this.setupEventHandlers();
        this.setupSubscriptions();

        $('#studentListDiv .box-wrap').antiscroll();

        // if firefox or IE, fix the width of the antiscroll-inner to hide the default scrollbars	
        if (!$.browser.webkit) {
            var listWidth = $('#studentListDiv').find('.antiscroll-inner').width();
            $('#studentListDiv').find('.antiscroll-inner').css('width', listWidth + 2);
        }

    }

    /**
     * Sets up the event handlers for user interaction with the widget
     */
    this.setupEventHandlers = function () {
        // add event handler for filter student list using search box
        $(me.studentSearchBox).keyup(function () {
            var filterVal = $(this).val();
            var filter = {
                attributeName: 'name',
                attributeId: 'name',
                operator: 'matches',
                value: filterVal,
                values: []
            }

            // notify filter component to add filter for 
            me.pubSub.publish('add-manual-filter', filter);
        });

        $(me.selectAllBtn).click(function (event) {
            me.selectAllStudents(true);
        });

        $(me.clearSelectionBtn).click(function (event) {
            me.selectAllStudents(false);
        });

        $(me.randomBtn).click(function (event) {
            if (!me.processing) {
                me.assignRandom();
            }
        });
    }

     /**
      * Sets up listeners for pubsub events
      */
    this.setupSubscriptions = function () {

        me.pubSub.subscribe('processing', function () {
            me.processing = true;
        });

        me.pubSub.subscribe('processing-complete', function () {
            me.processing = false;
        });

        me.pubSub.subscribe('filter-student-list', function () {
            me.filterStudentList();
        });

        me.pubSub.subscribe('student-selection-changed', function (studentId) {
            // TODO add method
        });

        me.pubSub.subscribe('window-resized', function (width, height) {
            me.handleWindowResize();
        });
    }

    /**************************
     * METHODS
     **************************/
    /**
	 * Repopulate the list of students with given list 
     * TODO method is not used, need to set up
	 */
    this.changeSelectableList = function (listStudentData, changeEventHandler) {
        var options = [];
        _.each(listStudentData, function (studentData) {
            return options.push(
				{ id: studentData.id, text: studentData.name });
        });

        $(me.studentSearchBox).select2('destroy');
        $(me.studentSearchBox).select2(
			{
			    data: options,
			    width: 'element'
			});
        $(me.studentSearchBox).on('change', changeEventHandler);
    }

    /**
	 * Applies the filters from the filter 
	 */
    this.filterStudentList = function () {
        // TODO refactor dependency on filter component
        var filteredStudents = student_grouping.studentFilterWidgetComponent.applyFilters(me.studentModels);
        _.each(me.studentModels, function (studentModel) {
            var studentId = studentModel.getId();
            var filteredStudent = _.find(filteredStudents, function (s) {
                return s.getId() === studentId;
            });
            var studentListItemWidget = me.studentWidgets[studentId];
            studentListItemWidget.toggleVisible(filteredStudent !== undefined);
        });

        // if the deselect all btn has been toggled, change it back to select all
        $(me.selectAllBtn).html('select all');
    }

    /**
	 * Select all students in the list 
	 */
    this.selectAllStudents = function (selected) {
        var studentWidgets = me.studentWidgets;
        for (var studentId in studentWidgets) {
            var studentListItemWidget = studentWidgets[studentId];
            if (studentListItemWidget.visible) {
                studentListItemWidget.toggleStudentSelection(selected);
            }
        }
    }

    /**
	 * Randomly organizes all students into groups (reassigns students that are already
     * assigned to groups)
	 */
    this.assignRandom = function () {

        var randomNum = $(me.randomNumTxt).val().trim();
        if (isNaN(randomNum) || randomNum === '' || parseInt(randomNum) <= 0) {
            utils.uiUtils.showTooltip(
                        $(me.randomNumTxt),
                        'Please enter a number greater than 0',
                        'bottom',
                        'manual',
                        3000);
            $(me.randomNumTxt).val('');
            return;
        }

        // TODO show warning only if there are students assigned to groups already
        // show warning and confirm that the user would like to perform the action
        var confirmation = confirm('Random will reorganize your students into groups randomly,'
            + ' even the students that are already assigned to groups. Would you like to continue?');
        if (confirmation) {
            me.pubSub.publish('assign-random', me.studentModels, randomNum);
            $(me.randomNumTxt).val('');
        }
    }

    /**
	 * Return the student object with the given id 
	 */
    this.getStudentById = function (studentId) {
        var matchingStudent = _.find(me.studentModels, function (studentModel) {
            return studentModel.getId() === studentId;
        });
        return matchingStudent;
    }

    /**
     * Resize the student list when the window resizes
     */
    this.handleWindowResize = function () {
        // get the width of the box container
        var width = $(me.studentListBox).width();

        // Firefox and IE aren't entirely compatible with antiscroll 
        // so we need to add some extra width to hide the scrollbar
        if ($.browser.mozilla || $.browser.msie) {
            width += 20;
        }

        $(me.studentListAntiscrollInner).width(width - 3);
    }
}