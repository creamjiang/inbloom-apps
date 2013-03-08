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
 * GroupDetailsWidget
 * Allows the user to edit a group's details such as name, description, students 
 * assigned to this group, attached lesson plan, and associated student elements.
 */
student_grouping.groupDetailsWidget = function () {
    var me = this;
    this.pubSub = PubSub;

    this.allStudents = []; // studentModels 
    this.attributes = [];
    this.groupModel = null;
    this.studentWidgets = [];
    this.processing = false;
    this.dirty = false;
    this.attachmentFailed = false;

    this.groupDetails = '.group-details';
    this.groupNameClass = '.group-details .group-name';
    this.groupNameTxtClass = '.group-details .group-name-txt';
    this.groupNameTxtAreaClass = '.group-name-txtarea';
    this.groupDescriptionClass = '.group-details .group-description';
    this.groupDescriptionTxtAreaClass = '.group-details .group-description-txtarea';
    this.groupEditImgClass = '.edit-group-icon';
    this.groupSaveImgClass = '.save-group-icon';
    this.memberListClass = '.member-list';

    this.studentSearchElem = "#student-search-txt";
    this.addStudentBtnElem = "#add-student-btn";
    this.groupAddStudentClass = ".group-add-student";

    this.showMoreDataBtnElem = "#show-more-data-btn";
    this.showMoreDataModalElem = "#show-more-data-modal";
    this.modalAttributesDiv = "#modal-attributes-list";
    this.attributesSaveBtnClass = ".attributes-save-btn";
    this.attributeCheckbox = ".attribute-checkbox";

    this.lessonPlanUploadDiv = ".group-lesson-plan-upload";
    this.lessonPlanAttachmentDiv = ".group-lesson-plan-bottom";
    this.attachmentFileInput = ".real-upload-txt";
    this.attachmentFileTxt = ".fake-upload-txt";
    this.fileUploadElem = "#file_upload";
    this.addAttachmentBtn = ".add-attachment-btn";
    this.lessonPlanFileName = '.lesson-plan-file-name';
    this.lessonPlanRemoveIcon = '.lesson-plan-remove-icon';

    this.tooltipElems = [this.groupEditImgClass, this.groupSaveImgClass, this.lessonPlanRemoveIcon];
    this.scrollbar = null;

    /**************************
     * METHODS
     **************************/
    this.init = function (allStudents, attributes) {
        me.allStudents = allStudents;

        // setup the dropdown containing all the selectable students
        _.each(allStudents, function (studentModel) {
            $(me.studentSearchElem).append('<option value="' + studentModel.getId() + '">' + studentModel.getName() + '</option>');
        });
        $(me.studentSearchElem).select2({ width: 'element' });
        
        // add the student attributes to the attributes modal
        me.attributes = attributes;
        _.each(attributes, function (attribute) {
            var checkbox = $('<li class="attribute-list-item"><input class="attribute-checkbox" type="checkbox" value="' + attribute.attributeId
    			+ '" data-displayName="' + attribute.attributeName + '"/>'
    			+ attribute.attributeName + "</li>");
            $(me.modalAttributesDiv).append(checkbox);
        });
       
        // set up the tooltips
        var tooltipElems = this.tooltipElems;
        _.each(tooltipElems, function (e) {
            var tooltip = tooltipText[e];
            var elem = $(me.groupDetails).find(e);
            utils.uiUtils.showTooltip(elem, tooltip.message, tooltip.placement, 'hover');
        });

        me.setupEventHandlers();
        me.setupSubscriptions();

        // setup file upload box
        utils.fileUtils.setupFileUpload(me.fileUploadElem, function (data) {
            me.groupModel.attachmentData = data;
            $(me.lessonPlanFileName).html(data.files[0].name);
            $(me.lessonPlanAttachmentDiv).show();
            $(me.lessonPlanUploadDiv).hide();
            me.toggleDirty(true);

            // hack needed for IE, otherwise the controls are frozen
            if ($.browser.msie) {
                $(".num-groups-create-txt").focus();
            }
        });
    }

    /**
     * Sets up the event handlers for user interaction with the widget
     */
    this.setupEventHandlers = function () {
        $(me.showMoreDataBtnElem).click(function (event) {
            me.showMoreDataPopup();
        });

        $(me.showMoreDataModalElem).find(me.attributesSaveBtnClass).click(function (event) {
            me.toggleAttributesVisible();
            $(me.showMoreDataModalElem).modal('hide');
        });

        $(me.attachmentFileInput).change(function (event) {
            var file = $(me.attachmentFileInput).prop('files')[0];
            $(me.attachmentFileTxt).val(file.name);
            me.toggleDirty(true);
        });

        $(me.groupEditImgClass).click(function (event) {
            me.editGroup();
        });

        $(me.lessonPlanFileName).click(function (event) {
            if (me.groupModel.hasAttachedFile()) {
                window.open('DownloadAttachment?id=' + me.groupModel.getId());
            }
        });

        $(me.lessonPlanRemoveIcon).click(function (event) {
            me.removeAttachment();
        });

        $(me.addStudentBtnElem).click(function (event) {
            me.addSelectedStudent();
        });

        $(me.groupSaveImgClass).click(function (event) {
            me.saveGroupChanges();
        });

        $(me.groupNameTxtClass).click(function (event) {
            me.makeGroupNameEditable();
        });

        $(me.groupDescriptionClass).click(function (event) {
            me.makeGroupDescriptionEditable();
        });
    }

    /**
     * Sets up listeners for pubsub events
     */
    this.setupSubscriptions = function () {
        me.pubSub.subscribe('show-group-details', me.viewGroupDetails);
        me.pubSub.subscribe('remove-student-from-group', me.removeStudent);
        me.pubSub.subscribe('group-deleted', me.hideContent);
        me.pubSub.subscribe('group-list-scrolled', me.moveArrow);
        me.pubSub.subscribe('move-arrow', me.moveArrow);
    }

    /**************************
    * METHODS
    **************************/
    /**
     *  
     */
    this.viewGroupDetails = function (groupModel) {

        // group-details pane is hidden by default, 
        // we only show when a user clicks on a group for details
        if ($(me.groupDetails).css('display') === 'none') {
            $(me.groupDetails).show();
        }

        // prompt user to save any changes he/she made
        var dirty = me.dirty;
        if (dirty) {
            var confirmation = confirm("You have unsaved changes. If you continue these changes will be lost. Continue?");
            if (!confirmation) {
                return;
            } else {
                // reset to original values
                me.groupModel.init();
            }
        }

        groupModel.students = []; // reset the list of students
        groupModel.init();
        me.groupModel = groupModel;
        me.moveArrow();

        // fill the group details section with the group data
        var groupData = groupModel.groupData;
        $(me.groupNameTxtClass).html(groupData.cohortIdentifier);

        var groupDescription = groupData.cohortDescription;
        if (groupDescription === null || utils.uiUtils.textIsEmpty(groupDescription)) {
            groupDescription = "<i>[add description here]</i>";
        };

        $(me.groupDescriptionClass).html(groupDescription);

        $(me.memberListClass).empty();
        var students = groupModel.getOriginalStudents();
        _.each(students, function (studentId) {
            me.addStudent(studentId);
        });

        me.toggleLessonPlan();

        // only show save btn if there has been changes
        me.toggleDirty(false);
        me.attachmentFailed = false;

        // setup the antiscroll scrollbar
        if (me.scrollbar === null) {
            me.scrollbar = $('.group-details .box-wrap').antiscroll();
        }
    }

    /**
     * Return boolean indicating whether the given student was added
     */
    this.addStudent = function (studentId) {

        if (me.groupModel.hasStudent(studentId)) {
            utils.uiUtils.showTooltip(
                   $(me.groupAddStudentClass),
                   'Student is already in this group.',
                   'top',
                   'manual',
                   3000);
            return false;
        }

        var studentModel = _.find(me.allStudents, function (studentModel) {
            return studentModel.getId() === studentId;
        });
        var groupId = me.groupModel.getId();
        var studentWidget = new student_grouping.studentInGroupWidget(groupId, studentModel, false);
        $(me.memberListClass).append(studentWidget.generateTemplate());
        studentWidget.init();

        var selectedStudentAttributes = me.groupModel.selectedAttributes;
        if (selectedStudentAttributes.length > 0) {
            studentWidget.toggleAttributeVisibility(selectedStudentAttributes);
        }
        
        me.studentWidgets[studentId] = studentWidget;
        me.groupModel.addStudent(studentId);

        return true;
    }

    /**
     * 
     */
    this.toggleAttributesVisible = function () {
        var selectedAttributes = [];
        $(me.attributeCheckbox).each(function (index, elem) {
            var selected = $(elem).is(":checked");
            if (selected) {
                var val = $(elem).val();
                var attribute = _.find(me.attributes, function (attr) {
                    return attr.attributeId === val;
                });
                selectedAttributes.push(attribute);
            }
        });
        me.groupModel.selectedAttributes = selectedAttributes;
        me.toggleDirty(true);

        var students = me.studentWidgets;
        for (var studentId in students) {
            var studentWidget = students[studentId];
            studentWidget.toggleAttributeVisibility(selectedAttributes);
        }
    }

    /**
     * Show the popup for user to select which student attributes to show 
     */
    this.showMoreDataPopup = function () {
        // toggle attribute checkboxes
        $(me.attributeCheckbox).attr('checked', false);

        var selectedAttributes = me.groupModel.selectedAttributes;
        _.each(selectedAttributes, function (attr) {
            $(me.attributeCheckbox + "[value='" + attr.attributeId + "']").attr('checked', true);
        });

        $(me.showMoreDataModalElem).modal('show');
    }

    /**
    * Remove the attachment from the current group 
    */
    this.removeAttachment = function () {        
        me.groupModel.removeAttachedFile();
        me.toggleDirty(true);
        me.toggleLessonPlan();
    }

    /**
     * Show the attached file if there is one, otherwise hide it
     */
    this.toggleLessonPlan = function () {
        var hasLessonPlan = me.groupModel.hasAttachedFile();
        if (hasLessonPlan) {
            var file = me.groupModel.attachedFile;
            $(me.lessonPlanFileName).html(file.name);
            $(me.lessonPlanAttachmentDiv).show();
            $(me.lessonPlanUploadDiv).hide();
        }
        else {
            // hide the file upload div
            $(me.lessonPlanUploadDiv).show();
            $(me.lessonPlanAttachmentDiv).hide();

            // reset the attachment file input
            $(me.attachmentFileInput).val('');
            $(me.attachmentFileTxt).val('');
        }
    }

    /**
    * Adds the selected student to this group
    */
    this.addSelectedStudent = function () {
        var studentId = $(me.studentSearchElem).val();

        var added = me.addStudent(studentId);
        if (added) {
            me.toggleDirty(added);
        }
        // reset the search box
        $(me.studentSearchElem).select2('val', '');
    }

    /**
     * Remove the given student from the list of students
     */
    this.removeStudent = function (studentId) {
        me.groupModel.removeStudent(studentId);
        me.toggleDirty(true);
    }

    /**
     * Save the changes made by user
     */
    this.saveGroupChanges = function () {

        // hack to save name/description if they are being edited
        setTimeout(function () {

            me.toggleGroupContainerProcessingState(true);
            me.groupModel.saveGroupChanges(me.saveGroupChangesSuccessHandler, me.saveGroupChangesErrorHandler);

        }, 100);
    }

    /**
     * Callback handler for successful group save
     */
    this.saveGroupChangesSuccessHandler = function (result) {

        // let others know this group has finished saving
        if (result.completedSuccessfully || result.objectActionResult.isSuccess || result.customActionResult.isSuccess) {
            me.pubSub.publish('group-saved', me.groupModel, result);
        }

        // upload unsaved attachment
        if (me.groupModel.hasUnsavedAttachment()) {
            me.groupModel.uploadAttachment(
                // success
                function (results) {
                    var isSuccess = results[0].isSuccess;
                    result.attachmentFailed = !isSuccess;
                    me.saveGroupChangesSuccessHandler(result);
                },
                // error
                function () {
                    result.attachmentFailed = true;
                    me.saveGroupChangesErrorHandler(result);
                });
            return;
        }

        var msg = 'Group has been successfully saved.';
        if (result.attachmentFailed) {
            msg += ' However the lesson plan could not be attached.';
        }

        if (result.completedSuccessfully) {
            // Let user know the save was successful
            utils.uiUtils.showTooltip(
                $(me.groupSaveImgClass),
                msg,
                'right',
                'manual',
                3000);
            me.toggleDirty(false);
        } else {
            me.saveGroupChangesErrorHandler(result);
        }
        me.toggleGroupContainerProcessingState(false);
    }

    /**
     * Callback handler for unsuccessful group save
     */
    this.saveGroupChangesErrorHandler = function (result) {

        var msg = 'Group could not be saved. Please try again later or contact your system administrator if this problem persists.'; 
        
        var groupSavedSuccessfully = result.objectActionResult.isSuccess;
        if (groupSavedSuccessfully) {

            // upload unsaved attachment
            if (me.groupModel.hasUnsavedAttachment()) {
                me.groupModel.uploadAttachment(
                    // success
                    function (results) {
                        var isSuccess = results[0].isSuccess;
                        me.saveGroupChangesSuccessHandler(result);
                    },
                    // error
                    function () {
                        result.attachmentFailed = true;
                        me.saveGroupChangesErrorHandler(result);
                    });
                return;
            }

            msg = 'Group was sucessfully saved.'
            
            if (result.failToCreateAssociations.length > 0 || result.failToDeleteAssociations.length > 0) {
                msg += 'However some students could not be assigned to the group or removed from the group.';
            }

            if (result.attachmentFailed) {
                msg += ' The attachment could not be uploaded';
            }
        } else if (result.objectActionResult.status === 403) {
            msg = 'You do not have the sufficient priviledges to makes changes to the cohort.';
        }

        // Let user know the save was not successful
        utils.uiUtils.showTooltip(
            $(me.groupSaveImgClass),
            msg,
            'right',
            'manual',
            4000);

        me.toggleGroupContainerProcessingState(false);
    }    

    /**
     * If doing an ajax request, fade out the background and display spinner
     */
    this.toggleGroupContainerProcessingState = function (processing) {
        if (processing) {
            $(me.groupDetails).css('opacity', 0.5);
            $(me.groupDetails).spin();
        } else {
            $(me.groupDetails).css('opacity', 1);
            $(me.groupDetails).spin(false);
        }
        me.processing = processing;
    }

    /**
     * Go to multiple groups edit page to edit this group
     */
    this.editGroup = function () {
        // warn user about unsaved changes, if any
        if (me.dirty) {
            var confirm = window.confirm("You have unsaved changes. If you continue these changes will be lost. Continue?");
            if (!confirm) {
                return;
            }
        }
        var groupId = me.groupModel.getId();
        window.location = 'MultipleGroupsEdit?selGroups=' + groupId;
    }

    /**
     * Make the group name label editable, turns it into a textbox
     */
    this.makeGroupNameEditable = function () {
        var groupName = $(me.groupNameTxtClass).html();
        $(me.groupNameTxtClass).hide();
        $(me.groupNameTxtAreaClass)
            .val(groupName)
            .show()
            .focus();

        $(me.groupNameTxtAreaClass).unbind('blur');
        $(me.groupNameTxtAreaClass).blur(function (event) {
            setTimeout(function () {
                me.saveGroupName();
            }, 50);
        });
    }

    /**
     * Save the new group name
     */
    this.saveGroupName = function () {
        var newGroupName = $(me.groupNameTxtAreaClass).val();

        // trim whitespace and remove line breaks
        newGroupName = utils.stringUtils.trim(newGroupName);

        $(me.groupNameTxtClass).show();
        $(me.groupNameTxtAreaClass).hide();

        var currName = $(me.groupNameTxtClass).html();
        if (newGroupName !== currName) {

            // TODO check if groupName exists

            // if no input then set default name
            if (!/\S/.test(newGroupName)) {
                newGroupName = 'New Group';
            }

            me.groupModel.groupData.cohortIdentifier = newGroupName;
            $(me.groupNameTxtClass).html(newGroupName);
            me.toggleDirty(true);
        }

    }

    /**
	 * Make the group description text editable, turns it into a textarea
	 */
    this.makeGroupDescriptionEditable = function () {
        var groupDescription = $(me.groupDescriptionClass).text();
        if (groupDescription === '[add description here]') {
            groupDescription = "";
        }

        $(me.groupDescriptionClass).hide();
        $(me.groupDescriptionTxtAreaClass)
            .val(groupDescription)
            .show()
            .focus();

        $(me.groupDescriptionTxtAreaClass).unbind('focusout');
        $(me.groupDescriptionTxtAreaClass).focusout(function (event) {
            me.saveGroupDescription();
        });
    }

    /**
     * Save the group description
     */
    this.saveGroupDescription = function () {
        var newGroupDescription = $(me.groupDescriptionTxtAreaClass).val();

        // make sure that there is a description
        if (!utils.uiUtils.textIsEmpty(newGroupDescription)) {
            newGroupDescription = utils.stringUtils.trim(newGroupDescription);

            var currDescription = $(me.groupDescriptionClass).html();
            if (currDescription !== newGroupDescription) {
                me.groupModel.groupData.cohortDescription = newGroupDescription;
                $(me.groupDescriptionClass).html(newGroupDescription);
                me.toggleDirty(true);
            }
        }

        $(me.groupDescriptionClass).show();
        $(me.groupDescriptionTxtAreaClass).hide();
    }

    /**
     *
     */
    this.toggleDirty = function (dirty) {
        me.dirty = dirty;
        if (dirty) {
            $(me.groupSaveImgClass).show();
        } else {
            $(me.groupSaveImgClass).hide();
        }
        me.pubSub.publish('toggle-dirty', dirty);
    }

    /**
     * Hide the group details panel
     */
    this.hideContent = function () {
        $(me.groupDetails).hide();
        $('.group-indicator-arrow').css('top', 0); // this will hide the arrow
    }

    /**
     * Move the arrow to point at the selected group
     */
    this.moveArrow = function () {
        if (me.groupModel !== undefined && me.groupModel !== null) {
            var groupDiv = $("#" + me.groupModel.getId());
            var groupDivHeight = groupDiv.height();
            // need to offset the top a little bit to align the arrow with center of div
            var selectedGroupTop = groupDiv.position().top + groupDivHeight / 2.5;

            var arrowVisible = $('.group-indicator-arrow').css('display') !== 'none';
            if (!arrowVisible) {
                $('.group-indicator-arrow').show();
            }
            $('.group-indicator-arrow').css('top', selectedGroupTop);
        }
    }
}