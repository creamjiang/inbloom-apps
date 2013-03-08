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
 * GroupListItemWidget
 *
 * Represents an individual group inside the group list. 
 */
student_grouping.groupListItemWidget = function (groupModel) {
    var me = this;
    this.pubSub = PubSub;

    this.groupModel = groupModel;
    this.dirty = false;

    this.containerId = '';
    this.groupContainerClass = '.group-container';
    this.groupTitleClass = '.group-title';
    this.groupModifiedTimestampClass = '.group-modified-timestamp';
    this.groupDescriptionClass = '.group-description';
    this.groupToggleInfoClass = '.group-toggle-info';

    this.groupDownloadLinkClass = '.group-download-link';
    this.groupAttachmentLinkClass = '.group-attachment-link';
    this.groupAttachmentIconClass = '.group-attachment-icon';
    this.groupAttachmentPrintClass = '.group-print-icon';
    this.groupDeleteIconClass = '.group-delete-icon';
    this.tooltipElems = [this.groupDownloadLinkClass, this.groupAttachmentIconClass, this.groupAttachmentPrintClass, this.groupDeleteIconClass];
    this.groupTemplate = '<div class="group-list-item">' +
									'<div class="group-checkbox"><input type="checkbox" class="group-checkbox"/></div>' +
									'<div class="group-container">' +
										'<div>' +
											'<span class="group-title"></span>' +
                                            '<a class="group-download-link" href="#"><img src="/Content/img/download-icon.png" class="group-icon"></img></a>' +
											'<a class="group-attachment-link" href="#"><img src="/Content/img/attachment-icon.png" class="group-icon group-attachment-icon"></img></a>' +
											'<img src="/Content/img/printer-icon.png" class="group-icon group-print-icon"></img>' +
											'<img src="/Content/img/trash-icon.png" class="group-icon group-delete-icon"></img>' +
											'<i class="group-modified-timestamp">Last modified: </i>' +
										'</div>' +
										'<div>' +
											'<p class="group-description"></p>' +
											'<a href="#" class="group-toggle-info"></a>' +
										'</div>'
                                '</div>' +
                            '</div>';

    /**************************
     * SETUP METHODS
     **************************/
    this.init = function () {
        me.containerId = "#" + me.groupModel.getId();
        
        // set up the tooltips
        var tooltipElems = this.tooltipElems;
        _.each(tooltipElems, function (e) {
            var tooltip = tooltipText[e];
            var elem = $(me.containerId).find(e);
            utils.uiUtils.showTooltip(elem, tooltip.message, tooltip.placement, 'hover');
        });

        me.setupEventHandlers();
        me.setupSubscriptions();
        me.showFileAttachment();
    }

    /**
     * Sets up the event handlers for user interaction with the widget
     */
    this.setupEventHandlers = function () {
        $(me.containerId).click(function (event) {
            var processing = student_grouping.groupDetailsWidgetComponent.processing;
            if (!processing) {
                me.groupSelected();
            }
        });

        $(me.containerId).find(me.groupAttachmentLinkClass).click(function (event) {
            me.downloadAttachment();
        });

        $(me.containerId).find(me.groupDeleteIconClass).click(function (event) {
            me.deleteGroup();
        });

        $(me.containerId).find(me.groupAttachmentPrintClass).click(function (event) {
            me.printGroup();
        });

        $(me.containerId).find(me.groupDownloadLinkClass).click(function (event) {
            me.downloadGroup();
        });
    }

    /**
     * Sets up listeners for pubsub events
     */
    this.setupSubscriptions = function () {
        me.pubSub.subscribe('group-saved', me.handleGroupSaved);
        me.pubSub.subscribe('remove-student', function (studentId) {
            me.removeStudent(studentId);
        });
    }

    /**************************
     * METHODS
     **************************/
    /**
	 * Return the HTML content for this object 
	 */
    this.generateTemplate = function () {
        var groupData = me.groupModel.groupData;
        var template = $(me.groupTemplate);

        $(template).attr('id', groupData.id);
        $(template).find(me.groupTitleClass).html(groupData.cohortIdentifier);

        if (me.groupModel.getCustom() !== null) {
            var lastModifiedDateStr = me.groupModel.getLastModTimeString();
            $(template).find(me.groupModifiedTimestampClass).html('Last modified: ' + lastModifiedDateStr);
        }
        $(template).find(me.groupDescriptionClass).html(groupData.cohortDescription);

        return template;
    }


    /**
     * TODO implement better solution for cutting off long file names
	 * Show the attached file
	 */
    this.showFileAttachment = function () {
        var hasLessonPlan = me.groupModel.hasAttachedFile();
        if (hasLessonPlan) {
            $(me.containerId).find(me.groupAttachmentLinkClass).show();
        } else {
            $(me.containerId).find(me.groupAttachmentLinkClass).hide();
        }
    }

    /**
     * Delete this group 
     */
    this.deleteGroup = function () {
        var groupName = me.groupModel.groupData.cohortIdentifier;
        var confirmation = confirm('Are you sure you want to delete the group: ' + groupName);
        if (confirmation) {
            // make sure we are not deleting newly created, unsaved groups
            if (me.groupModel.isNewGroup()) {
                me.deleteGroupSuccessHandler(null);
            } else {
                me.groupModel.delete(me.deleteGroupSuccessHandler, me.deleteGroupErrorHandler);                
            }
            me.toggleGroupContainerProcessingState(true);
        }
    }


    /**
     * Callback handler for successful delete
     */
    this.deleteGroupSuccessHandler = function () {
        me.pubSub.publish('group-deleted', me.groupModel.getId());

        // Let user know the delete was successful
        utils.uiUtils.showTooltip(
            $(me.containerId).find(me.groupDeleteIconClass),
            'Group has been successfully deleted.',
            'top',
            'manual',
            2000);
        setTimeout(function () {
            me.remove();
        }, 2000);
    }

    /**
     * Removes this group's DOM element
     */
    this.remove = function () {
        $(me.containerId).remove();
    }

    /**
     * Callback handler for unsuccessful delete
     */
    this.deleteGroupErrorHandler = function () {
        // Let user know the delete was not successful
        utils.uiUtils.showTooltip(
            $(me.containerId).find(me.groupDeleteIconClass),
            'Group could not be deleted. Please try again later or contact your system administrator.',
            'top',
            'manual',
            3000);
        me.toggleGroupContainerProcessingState(false);
    }

    /**
     * If doing an ajax request, fade out the background and display spinner
     */
    this.toggleGroupContainerProcessingState = function (processing) {
        if (processing) {
            $(me.containerId).css('opacity', 0.5);
            $(me.containerId).spin();
        } else {
            $(me.containerId).css('opacity', 1);
            $(me.containerId).spin(false);
        }
        me.processing = processing;
    }

    /**
     * Hide/show the group
     */
    this.toggleVisible = function (visible) {
        if (visible) {
            $(me.containerId).show();
        } else {
            $(me.containerId).hide();
        }
    }

    /**
     * Returns true if the group is selected
     */
    this.isSelected = function () {
        var checkbox = $(me.containerId).find('input.group-checkbox');
        return $(checkbox).is(':checked');
    }

    /**
     * Update the group's name
     */
    this.setName = function (newName) {
        $(me.containerId).find(me.groupTitleClass).html(newName);
    }

    /**
     * Update the group's description
     */
    this.setDescription = function (newGroupDescription) {
        $(me.containerId).find(me.groupDescriptionClass).html(newGroupDescription);
    }

    /**
     * Handle this group's selected event
     */
    this.groupSelected = function () {
        me.pubSub.publish('show-group-details', me.groupModel);

        // apply selected styling
        me.applySelectedStyle();
    }

    /**
     *
     */
    this.applySelectedStyle = function () {
        $(me.groupContainerClass).css('background-color', 'white');
        $(me.containerId).find(me.groupContainerClass).css('background-color', '#F2F2F2');
    }

    /**
     * Get the group widget's absolute top position
     */
    this.getOffsetTop = function () {
        var offset = $(me.containerId).offset();
        return offset.top;
    }

    /**
     *
     */
    this.printGroup = function () {
        var div = me.generatePrintableHtml();
        utils.printUtils.print($(div).html());
    }

    /**
     * Download the contents of this group to a text file
     */
    this.downloadGroup = function () {
        window.open('DownloadGroup?id=' + me.groupModel.getId());
    }

    /**
     * Generate HTML content to print out
     */
    this.generatePrintableHtml = function () {
        var div = $("<div>");
        $(div).append("<h2>" + me.groupModel.groupData.cohortIdentifier + "</h2>");
        $(div).append("<p>Description:<i>" +
             me.groupModel.groupData.cohortDescription !== null ? me.groupModel.groupData.cohortDescription : '' + "</i></p>");

        var students = me.groupModel.getOriginalStudents();
        if (students.length > 0) {
            var studentList = $("<ul style='list-style:none'>");
            var allStudents = student_grouping.groupDetailsWidgetComponent.allStudents;
            _.each(students, function (studentId) {
                var student = _.find(allStudents, function (studentModel) {
                    return studentModel.getId() === studentId;
                });
                $(studentList).append("<li>" + student.getName() + "</li>");
            });
            $(div).append(studentList);
        } else {
            $(div).append("<div><i>[no students]</i></div>");
        }
        return div;
    }

    /**
     * Update changes
     */
    me.handleGroupSaved = function (groupModel, result) {
        if (me.groupModel.getId() === groupModel.getId()) {
            if (result.customActionResult.isSuccess) {
                me.showFileAttachment();
                $(me.containerId).find(me.groupModifiedTimestampClass).html("Last modified: " + groupModel.getLastModTimeString());
            }

            if (result.objectActionResult.isSuccess) {
                me.setName(groupModel.groupData.cohortIdentifier);
                me.setDescription(groupModel.groupData.cohortDescription);
                $(me.containerId).find(me.groupModifiedTimestampClass).html("Last modified: " + groupModel.getLastModTimeString());
            }
        }
    }

    /**
     * Returns true if this group's name contains the given characters
     * @param groupName
     */
    me.groupNameContains = function (groupName) {
        return me.groupModel.groupData.cohortIdentifier
                .toLowerCase()
                .indexOf(groupName.toLowerCase()) !== -1;
    }

    /**
     * Download the lesson plan attached to this group
     */
    me.downloadAttachment = function () {
        if (me.groupModel.hasAttachedFile()) {
            window.open('DownloadAttachment?id=' + me.groupModel.getId());
        }
    }
}