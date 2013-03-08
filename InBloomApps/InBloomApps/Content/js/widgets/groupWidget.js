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
 * GroupWidget
 * This widget contains information about the cohort's name, description, students 
 * assigned to this group, attached lesson plan, and associated student elements.
 * Allows the user to modify this information and drag/drop students into the group.
 */
student_grouping.groupWidget = function(groupModel){
    var me = this;
    this.pubSub = PubSub;

    /**
     * Widget properties
     */
    this.dirty = false;
    this.processing = false;    
    this.groupModel = groupModel;
    this.color = null;
    this.studentWidgets = []; // studentInGroupWidget
    this.attachmentFailed = false;

    /**
     * DOM Element Selectors
     */
    this.groupsAreaClass = '.groups-area';
    this.groupContainerId = '';
    this.groupTopContainerClass = '.group-top-container';
    this.groupContainerClass = '.group-container';
    this.groupClass = '.group';
    this.groupNameClass = '.group-name';
    this.groupNameLblClass = '.group-name-lbl';
    this.groupNameTxtClass = '.group-name-txt';

    this.groupControlsClass = '.group-controls';
    this.delGroupBtnClass = '.del-group-btn';
    this.saveGroupBtnClass = '.save-group-btn';
    this.expColGroupBtnClass = '.exp-col-group-btn';
    this.collapsed = false;

    this.addDataDivClass = '.add-data-div';
    this.addDataBtnClass = '.add-data-button';
    this.groupCloseBtnClass = '.group-close-btn';

    this.groupPopoverClass = '.group-popover';
    this.studentPopoverElem = '.student-data-popover';

    this.groupInfoBtnClass = '.group-info-btn';
    this.groupInfoPopoverElem = '.group-description-popover';
    this.groupDescriptionTxtElem = '.group-description-text';
    this.groupDescriptionTxtAreaElem = '.group-description-textarea';

    this.groupDownloadImgClass = '.group-download-img';
    this.groupAttachmentImgClass = '.group-attachment-img';
    this.groupPrinterImgClass = '.group-printer-img';
    this.groupNumStudentsBadgeClass = '.group-num-students-badge';
    this.groupFileUploadClass = '.file-upload';
    this.groupFileInputButton = '.fileinput-button';
    this.groupAttachmentPopoverElem = '.group-attachment-popover';
    this.groupAttachmentPopoverFileInput = '.real-upload-txt';
    this.groupAttachmentPopoverFileTxt = '.fake-upload-txt';
    this.groupAttachmentPopoverDoneBtnElem = '.attachment-done-btn';
    this.groupAttachmentDivClass = '.group-file-attachment';
    this.groupAttachmentNameClass = '.file-attachment-name';
    this.groupAttachmentDelImgClass = '.del-attachment-img';

    // elems with tooltips
    this.tooltipElems = [this.groupDownloadImgClass, this.groupFileInputButton,
        this.groupPrinterImgClass, this.groupNumStudentsBadgeClass, this.groupInfoBtnClass, this.groupCloseBtnClass];

    this.groupUnsavedChangesModalElem = '#group-unsaved-changes-modal';
    this.groupUnsavedChangesGroupName = '#group-unsaved-changes-group-name';
    this.groupUnsavedChangesConfirmBtnElem = '#group-unsaved-changes-confirm-btn';

    /**
	 * HTML templates to be rendered to screen 
	 */
    this.groupContainerTemplate = '<div class="group-top-container">' +
                                    '<div class="group-container disable-select">' +
									    '<div class="group-controls">' +
										    '<button class="btn btn-link del-group-btn">delete</button>' +
										    '<button class="btn btn-link save-group-btn">save</button>' +
										    '<button class="btn btn-link exp-col-group-btn">collapse</button>' +
									    '</div>' +
									    '<div class="group-name">' +
										    '<div class="group-name-lbl"></div>' +
										    '<textarea maxlength="20" class="group-name-txt" style="display:none; overflow:hidden; resize:none; width:10em; height:1em; background-color:transparent; text-align:center; color:white; border-color:transparent"/></div>' +
									    '<img class="hide-button group-close-btn" src="/Content/img/group-close-icon.png"></img>' +
									    '<img class="hide-button group-info-btn" src="/Content/img/group-info-icon.png"></img>' +
                                        '<div class="box-wrap antiscroll-wrap group-wrap">' +
                                            '<div class="box">' +
                                                '<div class="antiscroll-inner">' +
                                                    '<div class="box-inner">' +
                                                        '<div class="group"></div>' +
                                                    '</div>' +
                                                '</div>' +
                                            '</div>' +
                                        '</div>' +
									    '<div style="text-align:center; position:relative;">' +
                                            '<img class="group-download-img" src="/Content/img/download-icon.png"/>' +
                                            '<form class="file-upload">' +
	                                            '<div class="fileupload-buttonbar">' +
		                                            '<div class="progressbar fileupload-progressbar"></div>' +
		                                            '<span class="fileinput-button">' +
                                                        '<img class="group-attachment-img" src="/Content/img/attachment-icon.png"/>' +
                                                        '<input class="file-input-field" type="file" name="files[]">' +
		                                            '</span>' +
	                                            '</div>' +
                                            '</form>' +                                            
                                            '<img class="group-printer-img" src="/Content/img/printer-icon.png"/>' +
										    '<span class="badge group-num-students-badge"></span>' +
										    '<button class="add-data-button btn btn-link">show data</button>' +
									    '</div>' +
									     '<div class="group-file-attachment">' +
										     '<a class="file-attachment-name"/>' +
										     '<img class="del-attachment-img" src="/Content/img/trash-icon.png"/>' +
									     '</div>' +
								     '</div>' +
                                     '<div class="group-description-popover group-popover" data-groupContainerId="-1" style="display:none">' +
			                                     '<strong class="group-description-lbl">Description:</strong>' +
			                                     '<div class="group-description-text">' +
				                                     '&nbsp;' +
                                                 '</div>' +
                                                 '<textarea maxlength="1024" class="group-description-textarea"></textarea>' +
                                     '</div>' +
                                     '<div class="student-data-popover group-popover" data-groupContainerId="-1" style="display: none">' +
			                            '<div class="student-data-popover-txt">select what you want to show</div>' +
                                        '<div class="box-wrap antiscroll-wrap student-data-wrap">' +
                                            '<div class="box">' +
                                                '<div class="antiscroll-inner">' +
                                                    '<div class="box-inner">' +
                                                        '<ul class="student-elements-list">' +
			                                            '</ul>' +
                                                    '</div>' +
                                                '</div>' +
                                            '</div>' +
                                        '</div>' +
		                             '</div>' +
			                       '</div>';

    

    /**************************
     * SETUP METHODS
     **************************/
    this.init = function (dataElements) {
        me.groupContainerId = $("#gc" + me.groupModel.getId());

        // add the pre-defined data elements to the popover
        _.each(dataElements, function (dataElement) {
            var dataElem = $("<li><input class='cbox-student-attribute' type='checkbox'"
                + "value='" + dataElement.attributeId + "' data-displayName='"
                + dataElement.attributeName + "'/>" + dataElement.attributeName
                + "</li>");
            $(me.groupContainerId).find(".student-data-popover .student-elements-list")
                .append(dataElem);
        });        

        // render students assigned to this group
        var studentIds = me.groupModel.getOriginalStudents();
        _.each(studentIds, function (studentId) {

            // TODO refactor dependency on studentsList
            var studentModel = student_grouping.studentListWidgetComponent.getStudentById(studentId);
            if (studentModel !== undefined) {
                me.assignStudentToGroup(studentModel);
            }
        });

        // only use antiscroll if its chrome
        if ($.browser.webkit) {
            $(me.groupContainerId).find('.group-wrap').antiscroll();
        } else {
            $(me.groupContainerId).find('.antiscroll-inner').css('overflow', 'auto');
        }

        // set up the tooltips
        var tooltipElems = this.tooltipElems;
        _.each(tooltipElems, function (e) {
            var tooltip = tooltipText[e];
            var elem = $(me.groupContainerId).find(e);
            utils.uiUtils.showTooltip(elem, tooltip.message, tooltip.placement, 'hover');
        });

        me.showFileAttachment();
        me.setupEventHandlers();
        me.setupSubscriptions();
        me.updateNumStudentsBadge();
    }

    /**
     * Sets up the event handlers for user interaction with the widget
     */
    this.setupEventHandlers = function () {
        var groupContainer = me.groupContainerId;

        $(groupContainer).find(me.addDataBtnClass).click(function (event) {
            if (!me.processing) {
                me.showStudentDataPopup();
            }
        });

        $(groupContainer).find(me.groupInfoBtnClass).click(function (event) {
            if (!me.processing) {
                me.showMoreInfoPopup();
            }
        });

        $(groupContainer).find(me.groupDownloadImgClass).click(function (event) {
            if (!me.processing) {
                me.downloadGroup();
            }
        });

        $(groupContainer).find(me.groupPrinterImgClass).click(function (event) {
            if (!me.processing) {
                me.printGroup();
            }
        });

        $(groupContainer).find(me.groupAttachmentDelImgClass).click(function (event) {
            if (!me.processing) {
                me.deleteAttachment();
            }
        });

        $(groupContainer).find(me.expColGroupBtnClass).click(function (event) {
            me.toggleStudentState();
        });

        $(groupContainer).find(me.groupCloseBtnClass).click(function (event) {
            if (!me.processing) {
                me.closeGroup();
            }
        });

        $(groupContainer).find(me.delGroupBtnClass).click(function (event) {
            if (!me.processing) {
                me.deleteGroup();
            }
        });

        $(groupContainer).find(me.groupNameLblClass).click(function (event) {
            if (!me.processing) {
                me.makeGroupNameEditable();
            }
        });

        $(groupContainer).find(me.groupAttachmentNameClass).click(function (event) {
            if (me.groupModel.hasAttachedFile()) {
                window.open('DownloadAttachment?id=' + me.groupModel.getId());
            }
        });

        $(groupContainer).find(me.saveGroupBtnClass).click(function (event) {
            if (!me.processing && (me.dirty || me.groupModel.isNewGroup())) {
                me.saveGroupChanges();
            } else if (!me.dirty) {
                // Let user know the created was successful
                utils.uiUtils.showTooltip(
                    $(me.groupContainerId).find(me.groupNameLblClass),
                    'This group does not have unsaved changes.',
                    'top',
                    'manual',
                    3000);
            }
        });

        var fileUploadElem = $(me.groupContainerId).find(me.groupFileUploadClass);
        utils.fileUtils.setupFileUpload(fileUploadElem, function (data) {
            me.attachSelectedFile(data);
        });
    }
    
    /**
     * Sets up listeners for pubsub events
     */
    this.setupSubscriptions = function () {
        // remove the given student from this group
        me.pubSub.subscribe('remove-student-from-group', me.removeStudent);

        // disable all group actions while save all is going on
        me.pubSub.subscribe('processing', function () {
            me.processing = true;
        });

        // enable all group actions after save is completed
        me.pubSub.subscribe('processing-complete', function () {
            me.processing = false;
        });
    }

    /**************************
     * METHODS
     **************************/
    /**
	 * Add the given student html elem to the given group
	 * @param {Object} studentModel
	 */
    this.assignStudentToGroup = function (studentModel) {
        var studentId = studentModel.getId();
        var groupId = me.groupModel.getId();

        // check if student is in group already
        var studentIsInGroup = me.hasStudent(studentId);
        if (!studentIsInGroup) {
            var studentWidget = new student_grouping.studentInGroupWidget(groupId, studentModel);            
            $("#" + groupId).append(studentWidget.generateTemplate());
            studentWidget.init(me.collapsed, me.groupModel.selectedAttributes, true);

            // add to dict to keep track of widget
            me.studentWidgets[studentId] = studentWidget;
           
            // add student to list of students
            me.groupModel.addStudent(studentId);
            me.pubSub.publish('student-added-to-group', studentId, me);
            // TODO make sure to add this event to student

            // make selected student attributes visible (only if group is not collapsed)
            if (!me.collapsed) {
                studentWidget.toggleAttributeVisibility(me.groupModel.selectedAttributes);
            }

            // update student count label
            me.updateNumStudentsBadge();

            // refresh antiscroll to show scrollbar
            if ($.browser.webkit) {
                $(me.groupContainerId).find('.group-wrap').antiscroll('refresh');
            }
        }
    }

    /**
	 * Removes the selected student from its group  
	 * @param {String} studentId
     * @param {String} groupId 
	 */
    this.removeStudent = function (studentId, groupId) {
        // check if given student is in this group
        var studentIsInGroup = me.hasStudent(studentId);
        if (studentIsInGroup && me.groupModel.getId().toString() === groupId.toString()) {
            // remove from model
            me.groupModel.removeStudent(studentId);

            // remove widget from memory and screen
            var studentWidget = me.studentWidgets[studentId];
            studentWidget.remove();
            delete me.studentWidgets[studentId]; // delete works with associative arrays (dictionaries) only

            me.markDirty();
            me.updateNumStudentsBadge();

            // tell student it has been removed from this group
            me.pubSub.publish('student-removed-from-group', studentId, me.groupModel);
            // TODO add event handler on studentWidget
        }          
    }
    

    /**
     * Returns true if the group contains the given student
     */
    this.hasStudent = function (studentId) {
        return me.groupModel.hasStudent(studentId);
    }

    /**
	 * Fill html template with group data
	 */
    this.generateTemplate = function (color) {
        var groupData = me.groupModel.groupData;
        var template = $(me.groupContainerTemplate);
        $(template).attr('id', 'gc' + groupData.id);

        var groupContainer = $(template).find(me.groupContainerClass);
        $(groupContainer).css('background-color', color.background);
        me.color = color;

        var groupNameLbl = $(groupContainer).find(me.groupNameLblClass);
        $(groupNameLbl).html(groupData.cohortIdentifier);

        // IE9 styling compat fix
        if ($.browser.msie) {
            $(groupNameLbl).css('line-height', '2.0em');
        }

        var groupNameDiv = $(groupContainer).find(me.groupNameClass);
        $(groupNameDiv).css('background-color', color.title);

        var groupDiv = $(groupContainer).find(me.groupClass);
        $(groupDiv).attr('id', groupData.id);

        return template;
    }

    /**
	 * Popup the menu for selecting the student data attributes to display
	 */
    this.showStudentDataPopup = function () {

        // hide the other popovers
        $(me.groupInfoPopoverElem).hide();
        $(me.groupAttachmentPopoverElem).hide();

        var groupContainerId = "gc" + me.groupModel.getId();
        var groupContainer = $("#" + groupContainerId);

        var popover = $(me.groupContainerId).find(me.studentPopoverElem);
        var popoverGroupContainerId = $(popover).attr('data-groupContainerId');

        // check if popover is already open 
        var notOpen = $(popover).css('display') === 'none';
        if (notOpen || groupContainerId !== popoverGroupContainerId) {

            // place the popover relative to the group container
            var position_size = me.getPositionAndSize();

            $(popover).attr('data-groupContainerId', groupContainerId);
            $(popover).css('height', position_size.height);
            $(popover).css('width', position_size.width);
            $(popover).css('display', '');

            // attach event handler to hide this if user clicks outside of it
            me.handleOutsideClick(me.addDataBtnClass, me.studentPopoverElem);

        } else {
            // close it
            $(popover).css('display', 'none');
        }

        var selectedAttributes = me.groupModel.selectedAttributes;
        var attributeCheckBoxes = me.studentPopoverElem + " .cbox-student-attribute";
        $(me.groupContainerId).find(attributeCheckBoxes).attr('checked', false);
        _.each(selectedAttributes, function (attribute) {
            $(me.groupContainerId).find(attributeCheckBoxes + "[value='" + attribute.attributeId + "']").attr('checked', true);
        });

        $(me.groupContainerId).find(attributeCheckBoxes).unbind('click');
        $(me.groupContainerId).find(attributeCheckBoxes).click(function (event) {
            var cbox = event.currentTarget;
            me.toggleSelectedAttributes();
            me.toggleStudentAttributeVisibility();
        });

        // make sure we only set antiscroll once
        var antiscrollSet = $(popover).attr('data-antiscroll') === 'set';
        if (!antiscrollSet) {
            $(me.groupContainerId).find('.student-data-wrap').antiscroll();
            $(popover).attr('data-antiscroll', 'set');
        }
    }

    /**
	 *  Repopulate the list of selected attributes with the user-selected attributes
	 */
    this.toggleSelectedAttributes = function () {
        me.groupModel.selectedAttributes = [];
        $(me.groupContainerId).find(me.studentPopoverElem + " .cbox-student-attribute").each(function (index, elem) {
            var selected = $(elem).is(":checked");
            if (selected) {
                var val = $(elem).val();
                var displayName = $(elem).attr('data-displayName');
                me.groupModel.selectedAttributes.push({ attributeId: val, attributeName: displayName });
            }
        });

        me.markDirty();
    }

    /**
     * Show selected attributes on this group's students
     */
    this.toggleStudentAttributeVisibility = function () {
        var selectedAttributes = me.groupModel.selectedAttributes;
        var studentWidgets = me.studentWidgets;
        for (var studentId in studentWidgets) {
            var studentWidget = studentWidgets[studentId];
            studentWidget.toggleAttributeVisibility(selectedAttributes);
        }
    }

    /**
	 * Popup the menu to show the group description 
	 */
    this.showMoreInfoPopup = function () {

        // hide the other popovers
        $(me.studentPopoverElem).hide();
        $(me.groupAttachmentPopoverElem).hide();

        var groupContainerId = "gc" + me.groupModel.getId();
        var groupContainer = $("#" + groupContainerId);

        var popover = $(me.groupContainerId).find(me.groupInfoPopoverElem);
        var popoverGroupContainerId = $(popover).attr('data-groupContainerId');

        // check if popover is already open 
        var notOpen = $(popover).css('display') === 'none';
        if (notOpen || groupContainerId !== popoverGroupContainerId) {

            var description = me.groupModel.groupData.cohortDescription;
            if (description !== null && description !== '') {
                $(popover).find(me.groupDescriptionTxtElem).html(description);
            } else {
                $(popover).find(me.groupDescriptionTxtElem).html('Type group description here');
            }

            // place the popover relative to the group container
            var position_size = me.getPositionAndSize();

            $(popover).attr('data-groupContainerId', groupContainerId);
            $(popover).css('height', position_size.height);
            $(popover).css('width', position_size.width);
            $(popover).css('display', '');

            // if user clicks on text, make it editable					
            $(me.groupContainerId).find(me.groupDescriptionTxtElem).click(function (event) {
                me.makeGroupDescriptionEditable();
            });

            // attach event handler to hide this if user clicks outside of it
            me.handleOutsideClick(me.groupInfoBtnClass, me.groupInfoPopoverElem);

        } else {
            // close it
            $(popover).css('display', 'none');
        }
    }
    
    /**
     * Attach the selected file to the group
     * @param data - arg passed by the fileupload plugin for async upload to server
     */
    this.attachSelectedFile = function (data) {
        me.groupModel.attachmentData = data;
        var fileName = data.files[0].name;
        $(me.groupContainerId).find(me.groupAttachmentNameClass).html(fileName);
        $(me.groupContainerId).find(me.groupAttachmentDivClass).show();
        $(me.groupContainerId).find(me.groupAttachmentPopoverElem).hide();
        me.markDirty();

        if ($.browser.msie) {
            // hack needed for IE
            $("#filter-value").focus();
        }
    }


    /**
	 * Remove the attachment from the group 
	 */
    this.deleteAttachment = function (event) {
        me.groupModel.removeAttachedFile();
        $(me.groupContainerId).find(me.groupAttachmentNameClass).html('');
        $(me.groupContainerId).find(me.groupAttachmentDivClass).hide();

        me.markDirty();
    }

    /**     
	 * Show the attached file
	 */
    this.showFileAttachment = function () {
        if (me.groupModel.hasAttachedFile()) {
            var fileName = me.groupModel.attachedFile.name;
            $(me.groupContainerId).find(me.groupAttachmentNameClass).html(fileName);
            $(me.groupContainerId).find(me.groupAttachmentDivClass).show();
        } else {
            $(me.groupContainerId).find(me.groupAttachmentDivClass).hide();
        }
    }

    /**
	 * Toggle student expanded/collapsed state 
	 */
    this.toggleStudentState = function () {
        
        me.collapsed = !me.collapsed;
        
        var studentWidgets = me.studentWidgets;
        for (var studentId in studentWidgets) {
            var studentWidget = studentWidgets[studentId];
            studentWidget.toggleExpandedCollapsedState(!me.collapsed)
        }
        
        var btnTxt = me.collapsed ? 'expand' : 'collapse';
        $(me.groupContainerId).find(me.expColGroupBtnClass).html(btnTxt);
    }

    /**
	 * Close the group / remove from the workspace
	 */
    this.closeGroup = function () {

        if (me.dirty) {
            var groupData = me.groupModel.groupData;
            $(me.groupUnsavedChangesGroupName).html(groupData.cohortIdentifier);

            $(me.groupUnsavedChangesConfirmBtnElem).unbind('click');
            $(me.groupUnsavedChangesConfirmBtnElem).click(function () {
                me.dirty = false;
                me.closeGroup();
            });

            $(me.groupUnsavedChangesModalElem).modal('show');
        } else {
            $(me.groupContainerId).remove();
            me.groupModel.close();
            me.pubSub.publish('group-removed', me.groupModel.getId());
            // TODO Need to tell groupList to remove the widget from memory
        }
    }

    /**
	 * Make the group name label editable, turns it into a textbox
	 */
    this.makeGroupNameEditable = function () {
        var groupName = $(me.groupContainerId).find(me.groupNameLblClass).html();
        if (groupName === 'Type group description here') {
            groupName = '';
        }
        $(me.groupContainerId).find(me.groupNameLblClass).hide();
        $(me.groupContainerId).find(me.groupNameTxtClass)
			.val(groupName)
			.css('display', '')
			.focus();

        $(me.groupContainerId).find(me.groupNameTxtClass).unbind('blur');
        $(me.groupContainerId).find(me.groupNameTxtClass).blur(function (event) {
            me.saveGroupName();
        });
        $(me.groupContainerId).find(me.groupNameTxtClass).unbind('blur keyup');
        $(me.groupContainerId).find(me.groupNameTxtClass).bind('blur keyup', function (e) {
            if (e.type === 'keyup' && e.keyCode !== 10 && e.keyCode !== 13) return;
            me.saveGroupName();
        });
    }

    /**
	 * Save the new group name 
	 */
    this.saveGroupName = function () {
        var currGroupName = me.groupModel.groupData.cohortIdentifier;       
        var newGroupName = $(me.groupContainerId).find(me.groupNameTxtClass).val();
        newGroupName = utils.stringUtils.trim(newGroupName);

        $(me.groupContainerId).find(me.groupNameLblClass).show();
        $(me.groupContainerId).find(me.groupNameTxtClass).hide();

        if (currGroupName !== newGroupName) {

            // check if group name already exists
            var groupNameExists = student_grouping.groupListWidgetComponent.groupNameExists(newGroupName);
            if (groupNameExists) {
                // Let user know the name already exists
                utils.uiUtils.showTooltip(
                    $(me.groupContainerId).find(me.groupNameLblClass),
                    'Group name "' + newGroupName + '" already exists. Please use a different name.',
                    'top',
                    'manual',
                    3000);
                return;
            }

            // if no input, then set default name
            if (!/\S/.test(newGroupName)) {
                newGroupName = 'New Group';
            }
            me.groupModel.groupData.cohortIdentifier = newGroupName;
            $(me.groupContainerId).find(me.groupNameLblClass).html(newGroupName);

            me.markDirty();
        }
    }

    /**
	 * Make the group description text editable, turns it into a textarea
	 */
    this.makeGroupDescriptionEditable = function () {
        var groupDescription = me.groupModel.groupData.cohortDescription;
        $(me.groupContainerId).find(me.groupDescriptionTxtElem).hide();

        var height = $(me.groupContainerId).find(me.groupInfoPopoverElem).css('height').replace('px', '');
        $(me.groupContainerId).find(me.groupDescriptionTxtAreaElem).css('height', parseInt(height) - 40);
        $(me.groupContainerId).find(me.groupDescriptionTxtAreaElem)
			.val(groupDescription)
			.show()
			.focus();

        $(me.groupContainerId).find(me.groupDescriptionTxtAreaElem).unbind('blur');
        $(me.groupContainerId).find(me.groupDescriptionTxtAreaElem).blur(function (event) {
            me.saveGroupDescription();
        });
    }

    /**
	 * TODO check if new description is different from old one 
     * Save the new group description
	 */
    this.saveGroupDescription = function () {

        var newGroupDescription = $(me.groupContainerId).find(me.groupDescriptionTxtAreaElem).val();
        newGroupDescription = utils.stringUtils.trim(newGroupDescription);
        me.groupModel.groupData.cohortDescription = newGroupDescription;

        $(me.groupContainerId).find(me.groupDescriptionTxtElem).html(newGroupDescription);
        $(me.groupContainerId).find(me.groupDescriptionTxtAreaElem).hide();
        $(me.groupContainerId).find(me.groupDescriptionTxtElem).show();

        me.markDirty();
    }

    /**
	 * Returns the position and size of this group's container element 
	 */
    this.getPositionAndSize = function () {
        var position = $(me.groupContainerId).offset();
        var width = $(me.groupContainerId).find(me.groupContainerClass).width();
        var height = $(me.groupContainerId).find(me.groupContainerClass).height();

        // if has attachment, subtract the height of the attachment
        var attachmentDiv = $(me.groupContainerId).find(me.groupAttachmentDivClass)
        var attachmentVisible = $(attachmentDiv).css('display') !== 'none';
        if (attachmentVisible) {
            var attachmentHeight = $(attachmentDiv).outerHeight();
            height -= attachmentHeight;
        }

        var position_size = {
            left: position.left,
            top: position.top,
            width: width,
            height: height
        }
        return position_size;
    }

    /**
	 * Mark this group as dirty so that changes are saved back to server 
	 */
    this.markDirty = function () {
        me.dirty = true;
    }

    /**
     * Handle outside click event to hide popover
     */
    this.handleOutsideClick = function (triggetBtn, container) {
        $(me.groupsAreaClass).unbind('click');
        $(me.groupsAreaClass).click(function (e) {
            if ((!$(container).is(e.target) && $(container).has(e.target).length === 0)
                    && (!$(triggetBtn).is(e.target) && $(triggetBtn).has(e.target).length === 0)) {
                $(container).hide();
                $(me.groupsAreaClass).unbind('click');
            }
        });
    }

    /**
     * Handle the save button click event
     */
    this.saveGroupChanges = function () {

        // Determine whether the model data is valid
        var validation = me.groupModel.validateModel();
        if (!validation.isValid) {
            // let the user know 
            me.showMessageAboveTitle(validation.message);
            return;
        }

        // determine the callback handlers based on whether its a new group or not
        var successHandler = null;
        var errorHandler = null;

        // if group is new we save the group first, then the attachment
        if (me.groupModel.isNewGroup()) {
            successHandler = me.createGroupSuccessHandler;
            errorHandler = me.createGroupErrorHandler;
        } else {
            successHandler = me.updateGroupSuccessHandler;
            errorHandler = me.updateGroupErrorHandler;
        }

        me.groupModel.saveGroupChanges(successHandler, errorHandler);
        me.toggleGroupContainerProcessingState(true);
        me.pubSub.publish('processing');
    }

    /**
     * Handle successful saving of group
     */
    this.createGroupSuccessHandler = function (result) {
        me.updateId(result.objectActionResult.objectId);

        var msg = 'Group has been successfully created.';

        // upload unsaved lesson plan if there is one
        if (me.groupModel.hasUnsavedAttachment()) {
            me.saveAttachment(msg);
        } else {
            // Let user know the created was successful
            utils.uiUtils.showTooltip(
                $(me.groupContainerId).find(me.groupNameLblClass),
                msg,
                'top',
                'manual',
                3000);
            me.saveComplete();
        }

        me.dirty = false;
        // request to be added to list of existing groups
        me.pubSub.publish('add-to-existing-groups', me.groupModel);
    }

    /**
     * Handle error with saving a group
     */
    this.createGroupErrorHandler = function (result) {
        var msg = "Group could not be created. Please try again later or contact your system administrator.";

        var groupCreatedSuccessfully = result.objectActionResult.isSuccess;
        if (groupCreatedSuccessfully) {
            me.updateId(result.objectActionResult.objectId);
            msg = "Group was created successfully.";
            if (result.failToCreateAssociations.length > 0){
                msg += " However some students could not be assigned to the group.";
            }

            // upload unsaved lesson plan if there is one
            if (me.groupModel.hasUnsavedAttachment()) {
                me.saveAttachment(msg);
                return;
            }
        }

        me.saveComplete();
        // Let user know the create was not successful
        utils.uiUtils.showTooltip(
            $(me.groupContainerId).find(me.groupNameLblClass),
            msg,
            'top',
            'manual',
            3000);
    }
    
    /**
     * Handles saving the attached lesson plan back to the server
     */
    this.saveAttachment = function (msg) {
        // upload the attachment after the group has been created and assigned an id
        me.groupModel.uploadAttachment(
            // success
            function () {
                // Let user know the lesson plan was attached
                utils.uiUtils.showTooltip(
                    $(me.groupContainerId).find(me.groupNameLblClass),
                    msg,
                    'top',
                    'manual',
                    3000);
                me.saveComplete();
            },
            // error
            function () {
                msg += ' However the lesson plan could not be attached.';

                // Let user know the lesson plan was not attached
                utils.uiUtils.showTooltip(
                    $(me.groupContainerId).find(me.groupNameLblClass),
                    msg,
                    'top',
                    'manual',
                    3000);

                // mark dirty again if lesson plan could not be uploaded
                me.markDirty();
                me.saveComplete();
            });
    }

    /**
     *
     */
    this.updateGroupSuccessHandler = function (result) {

        var msg = 'Group has been successfully saved.';
        if (me.groupModel.hasUnsavedAttachment()) {
            me.saveAttachment(msg);
        } else {
            // Let user know the save was successful
            utils.uiUtils.showTooltip(
                $(me.groupContainerId).find(me.groupNameLblClass),
                msg,
                'top',
                'manual',
                3000);

            me.saveComplete();
        }

        me.dirty = false;
    }

    /**
     *
     */
    this.updateGroupErrorHandler = function (result) {
        var msg = 'Group could not be updated. Please try again later or contact your system administrator.';

        var groupUpdatedSuccessfully = result.objectActionResult.isSuccess;
        var failToCreateAssociations = result.failToCreateAssociations;
        var failToDeleteAssociations = result.failToDeleteAssociations;
        if (groupUpdatedSuccessfully) {
            msg = "Group was updated successfully.";
            if ((failToCreateAssociations !== null && failToCreateAssociations.length > 0) ||
            (failToDeleteAssociations !== null && failToDeleteAssociations.length > 0)) {
                msg += " However some students could not be assigned to or deleted from the group.";
            } 

            if (me.groupModel.hasUnsavedAttachment()){
                me.saveAttachment(msg);
                return;
            }
        }

        me.saveComplete();
        // Let user know the update was not successful
        utils.uiUtils.showTooltip(
            $(me.groupContainerId).find(me.groupNameLblClass),
            msg,
            'top',
            'manual',
            3000);
    }

    this.saveComplete = function () {
        me.pubSub.publish('processing-complete');
        me.toggleGroupContainerProcessingState(false);
    }

    /**
     * Delete this group permanently
     */
    this.deleteGroup = function () {
        var groupName = me.groupModel.groupData.cohortIdentifier;
        var confirmation = confirm('Are you sure you want to delete the group: ' + groupName);
        if (confirmation) {
            // make sure we are not deleting newly created, unsaved groups
            if (me.groupModel.isNewGroup()) {
                // just delete the group locally since it was not saved to the server
                me.deleteGroupSuccessHandler(null);
            } else {
                me.groupModel.delete(me.deleteGroupSuccessHandler, me.deleteGroupErrorHandler);               
            }

            me.toggleGroupContainerProcessingState(true);
        }
    }

    /**
     *
     */
    this.deleteGroupSuccessHandler = function (result) {
        me.pubSub.publish('group-deleted', me.groupModel.getId());

        // Let user know the delete was successful
        utils.uiUtils.showTooltip(
            $(me.groupContainerId).find(me.groupNameLblClass),
            'Group has been successfully deleted.',
            'top',
            'manual',
            2000);
        setTimeout(function () {
            me.toggleGroupContainerProcessingState(false);
            $(me.groupContainerId).remove();
        }, 2000);
    }

    /**
     *
     */
    this.deleteGroupErrorHandler = function (result) {
        me.toggleGroupContainerProcessingState(false);
        // Let user know the delete was not successful
        utils.uiUtils.showTooltip(
            $(me.groupContainerId).find(me.groupNameLblClass),
            'Group could not be deleted. Please try again later or contact your system administrator.',
            'top',
            'manual',
            3000);
    }

    /**
     * If doing an ajax request, fade out the background and display spinner
     */
    this.toggleGroupContainerProcessingState = function (processing) {
        if (processing) {
            $(me.groupContainerId).css('opacity', 0.5);
            $(me.groupContainerId).spin();
        } else {
            $(me.groupContainerId).css('opacity', 1);
            $(me.groupContainerId).spin(false);
        }
        me.processing = processing;
    }

    /**
     * Update the label that shows the number of students in group
     */
    this.updateNumStudentsBadge = function () {
        var numStudents = me.groupModel.students.length;
        $(me.groupContainerId).find(me.groupNumStudentsBadgeClass).css('background-color', me.color.title);
        $(me.groupContainerId).find(me.groupNumStudentsBadgeClass).html(numStudents);
    }

    /**
     * Remove all the students from this group
     */
    this.removeAllStudents = function () {
        me.groupModel.students = [];
        
        var studentWidgets = me.studentWidgets;
        for (var studentId in studentWidgets) {
            var studentWidget = studentWidgets[studentId];
            studentWidget.remove();
            studentWidget = null;
            delete studentWidgets[studentId];

            // tell student it has been removed from this group
            me.pubSub.publish('student-removed-from-group', studentId, me.groupModel);
        }
        me.updateNumStudentsBadge();
    }

    /**
     * Update this group's id
     */
    this.updateId = function (id) {
        var origGroupContainerId = $(me.groupContainerId);
        var originalId = $(origGroupContainerId).find(me.groupClass).attr('id');
        me.groupContainerId = "#gc" + id;
        $(origGroupContainerId).find(me.groupClass).attr('id', id);
        $(origGroupContainerId).attr('id', "gc" + id);

        me.pubSub.publish('update-group-id', originalId, id);
    }

    /**
     * Print out the contents of this group
     */
    this.printGroup = function () {
        var div = me.generatePrintableHtml();
        utils.printUtils.print($(div).html());
    }

    /**
     * Generate HTML using the group's content for printing
     */
    this.generatePrintableHtml = function () {
        var groupData = me.groupModel.groupData;
        var div = $("<div style='page-break-after:always'>");
        $(div).append("<h2>Name: " + groupData.cohortIdentifier + "</h2>");
        $(div).append("<p>Description:<i>" +
            groupData.cohortDescription !== null ? groupData.cohortDescription : '' + "</i></p>");

        var students = me.groupModel.students;
        if (students.length > 0) {
            $(div).append("<p>Students</p>");
            var studentList = $("<ul style='list-style:none'>");
            var studentWidgets = me.studentWidgets;
            _.each(students, function (studentId) {
                var studentWidget = studentWidgets[studentId];
                var studentModel = studentWidget.studentModel;
                $(studentList).append("<li>" + studentModel.getName() + "</li>");
            });
            $(div).append(studentList);
        } else {
            $(div).append("<div><i>[no students]</i></div>");
        }
        return div;
    }

    /**
     * Download the contents of this group to a text file
     */
    this.downloadGroup = function () {
        if (me.groupModel.isNewGroup()) {
            var downloadBtn = $(me.groupContainerId).find(me.groupDownloadImgClass);
            utils.uiUtils.showTooltip(downloadBtn,
                "Group must be saved before it can be downloaded",
                "top",
                "manual",
                3000);
            return;
        }
        window.open('DownloadGroup?id=' + me.groupModel.getId());
    }

    /**
     * Return the number of students in this group
     */
    this.getNumberOfStudents = function () {
        return Object.keys(me.studentWidgets).length;
    }

    /**
     * Displays the given message above the title
     */
    this.showMessageAboveTitle = function (msg) {
            // let the user know 
            utils.uiUtils.showTooltip(
                $(me.groupContainerId).find(me.groupNameLblClass),
                msg,
                'top',
                'manual',
                3000);
            return;
    }
}