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
 * Client side group model. Contains the application logic for manipulating
 * the group on the client side and saving these changes back to the server
 * @param groupData - Server side group model
 */
student_grouping.groupModel = function (groupData) {
    
    var me = this;
    this.pubSub = PubSub;

    this.serverGroup = groupData;
    this.groupData = null;
    this.students = [];
    this.selectedAttributes = [];
    this.attachedFile = null;
    this.attachmentData = null;

    /**************************
     * GETTER AND SETTERS
     **************************/
    /**
     * Returns the cohort id
     */
    this.getId = function () {
        return me.groupData.id;
    }

    /**
     * Set the id of the group to the given id
     */
    this.setId = function (id) {
        me.groupData.id = id;
        me.serverGroup.cohort.id = id;
    }

    /**
     * Returns the custom object
     */
    this.getCustom = function () {
        return me.serverGroup.custom;
    }

    /**
     * Returns an array containing the students in the original list (sent from server)
     */
    this.getOriginalStudents = function () {
        return me.serverGroup.students;
    }

    /** 
     * Returns the lastModified timestamp in MM/DD/YYYY HH:MI PP formate
     */ 
    this.getLastModTimeString = function () {
        var lastModifiedDate = me.getLastModTime();
        var lastModifiedDateStr = lastModifiedDate.toFormat('MM/DD/YYYY HH:MI PP');
        return lastModifiedDateStr;
    }

    /**
     * Return the lastModified timestamp date object
     */
    this.getLastModTime = function () {
        var custom = me.getCustom();
        var lastModifiedDate = new Date(parseInt(custom.lastModifiedDate.replace('/Date(', '').replace(')/', '')));
        return lastModifiedDate;
    }

    /**************************
     * METHODS
     **************************/
    /**
     * 
     */
    this.init = function () {

        // reset student list
        me.students = [];
        me.attachmentData = null;
        // copy over group data
        me.groupData = $.extend(true, {}, me.serverGroup.cohort);

        // load the custom attributes
        var custom = me.getCustom();
        if (custom !== null && custom !== undefined) {
            var selectedAttributes = custom.dataElements;
            me.selectedAttributes = [];
            _.each(selectedAttributes, function (selectedAttribute) {
                me.selectedAttributes.push(selectedAttribute);
            });

            me.attachFile(custom.lessonPlan);
        }
    }

    /**
     * Returns true if student is already part of group
     */
    this.hasStudent = function (studentId) {
        var student = _.find(me.students, function (s) {
            return s === studentId;
        });
        return student !== undefined;
    }

    /**
     * Add given student to list of student
     */
    this.addStudent = function (studentId) {
        me.students.push(studentId);
    }

    /**
     * Removes the given student from the list
     */
    this.removeStudent = function (studentId) {
        me.students = _.filter(me.students, function (id) {
            return id !== studentId;
        });
    }

    /**
     * Attach the given file to the group
     */
    this.attachFile = function (file) {
        me.attachedFile = file;
    }

    /**
     * Remove the attached lesson plan
     */
    this.removeAttachedFile = function () {
        me.attachedFile = null;

        if (me.hasUnsavedAttachment()) {
            me.attachmentData = null;
        }
    }

    /**
     * Returns true if the group has a lesson plan attached
     */
    this.hasAttachedFile = function () {
        return me.attachedFile !== null;
    }

    /**
     * Returns true if there is a unsaved attachment (not yet uploaded to server)
     */
    this.hasUnsavedAttachment = function () {
        return me.attachmentData !== null;
    }

    /**
     * Returns the file object from the attachment data
     */
    this.getUnsavedAttachmentFile = function () {
        if (me.hasUnsavedAttachment()) {
            return me.attachmentData.files[0]
        } else {
            return null;
        }
    }

    /**
     * Returns true if this group is a new group. 
     * A new group has a negative id
     */
    this.isNewGroup = function () {
        return parseInt(me.groupData.id) < 0;
    }

    /**
     * Checks the model's data against business rules to make sure the data is valid
     */
    this.validateModel = function () {
        var validationResult = {
            isValid: true,
            message: 'Model data is valid'
        };

        // Make sure the name is unique TODO check against server instead of locally
        var groupName = me.groupData.cohortIdentifier;
        var groupNameExists = student_grouping.groupListWidgetComponent.groupNameExists(groupName);
        if (groupNameExists && me.getId() < 0) {
            validationResult.isValid = false;
            validationResult.message = 'The group name "' + groupName + '" already exists. Please use a different name.';
        }

        return validationResult;
    }

    /**
     * Add new group or update a group.
     */
    this.saveGroupChanges = function (successHandler, errorHandler) {

        var cohortActionObject = me.prepareGroupForSaving();

        // negative ids represent new groups
        var method = 'CreateGroup';
        var successHandler = successHandler;
        var errorHandler = errorHandler;
        if (me.isNewGroup()) {
            cohortActionObject.cohort.id = null;
        } else {
            method = 'UpdateGroup';
        }

        $.ajax({
            type: 'POST',
            url: method,
            contentType: 'application/json',
            data: JSON.stringify(cohortActionObject),
            success: function (result) {

                // sync new data with old data
                me.saveResultHandler(result);

                if (result.completedSuccessfully) {
                    successHandler(result);
                } else if (!result.partialCreateSuccess || !result.partialDeleteSuccess || !result.customActionResult.isSuccess) {
                    errorHandler(result);
                }                
            },
            error: function (result) {
                // should implement exception handling
            }
        });
    }

    /**
     * Creates JSON object to send back to server for saving changes
     */
    this.prepareGroupForSaving = function () {
       
        var newStudents = me.getStudentsToCreate()
        var studentsToDelete = me.getStudentsToDelete();
        
        // if negative, then it is a new group so it doesn't have an id.
        // server expects null for new groups
        var id = me.isNewGroup() ?
             null : me.groupData.id;

        var cohortActionObject = {
            cohort: {
                id: id,
                cohortDescription: me.groupData.cohortDescription,
                cohortIdentifier: me.groupData.cohortIdentifier
            },
            custom: { dataElements: me.selectedAttributes, lessonPlan: me.attachedFile },
            studentsToDelete: studentsToDelete !== null ? studentsToDelete : [],
            studentsToCreate: newStudents !== null ? newStudents : []
        }

        return cohortActionObject;
    }

    /**
     * Uploads the unsaved attachment to the server
     * @param callback - function to call after uploading to the server
     */
    this.uploadAttachment = function (successCallback, errorCallback) {
        if (me.attachmentData !== null) {

            var attachmentData = me.attachmentData;

            // pass the group id
            attachmentData.formData = { "groupId": me.getId() }

            // submit() POSTS the file to the server
            attachmentData.submit()
                .success(function (result, textStatus, jqXHR) {
                    var jsonResult = me.syncUploadAttachmentResult(result);
                    successCallback(jsonResult);
                })
                .error(function (jqXHR, textStatus, errorThrown){
                    errorCallback(errorThrown);
                });
        }
    }

    /**
     * Parses the 
     */
    this.syncUploadAttachmentResult = function (result) {
        me.attachmentData = null;

        // attached the uploaded file to this model
        var jsonResult = JSON.parse(result);
        var res = jsonResult[0];
        var lessonPlan = {
            name: res.Name,
            type: res.Type
        }
        me.serverGroup.custom.lessonPlan = lessonPlan;
        me.attachFile(lessonPlan);
        return jsonResult;
    }

    /**
     * Returns the new students. Determined by new students minus old students
     */
    this.getStudentsToCreate = function () {
        var originalStudents = me.serverGroup.students;
        var newStudents = _.filter(me.students, function (student) {
            var matchingStudent = _.find(originalStudents, function (origStudentId) {
                return origStudentId === student;
            });
            return matchingStudent === undefined;
        });

        // remove blanks
        newStudents = _.filter(newStudents, function (student) {
            return student !== "";
        });

        return newStudents;
    }

    /**
     * Returns the students to delete from this group. Determined by old 
     * students minus new students
     */
    this.getStudentsToDelete = function () {
        var originalStudents = me.serverGroup.students;
        var studentsToDelete = _.filter(originalStudents, function (origStudentId) {
            var matchingStudent = _.find(me.students, function (student) {
                return origStudentId === student;
            });
            return matchingStudent === undefined;
        });

        // remove blanks
        studentsToDelete = _.filter(studentsToDelete, function (student) {
            return student !== "";
        });

        return studentsToDelete;
    }

    /**
     * Sync this group's server side data with newly saved changes
     * @param result - result from server side
     */
    this.saveResultHandler = function (result) {        
        var originalId = me.groupData.id;
        if (result.objectActionResult.isSuccess) {
            me.setId(result.objectActionResult.objectId);
            me.serverGroup.cohort.cohortIdentifier = me.groupData.cohortIdentifier;
            me.serverGroup.cohort.cohortDescription = me.groupData.cohortDescription;           
            me.pubSub.publish('group-changed', originalId, me);
        }
        me.updateStudentList();
        
        if (result.customActionResult !== null && result.customActionResult.isSuccess) {
            if (me.attachedFile !== null) {
                var attachedFile = $.extend(true, {}, me.attachedFile);
                me.serverGroup.custom.lessonPlan = attachedFile;
            } else {
                me.serverGroup.custom.lessonPlan = null;
            }

            me.serverGroup.custom.dataElements = [];
            _.each(me.selectedAttributes, function (attr) {
                me.serverGroup.custom.dataElements.push(attr);
            });

            var currTimestamp = new Date();
            me.serverGroup.custom.lastModifiedDate = currTimestamp.getTime().toString();
        }
    }

    /**
     * Sync the group's student list with changes from server
     * @param failToCreateAssociations - failed to create new student associations
     * @param failToDeleAssociations - failed to delete student associations
     */
    this.updateStudentList = function (failToCreateAssociations, failToDeleteAssociations) {
        var failedCreates = [];
        if (failToCreateAssociations !== null) {
            failedCreates = _.pluck(failToCreateAssociations, 'objectId');
        }
        me.updateListWithNewStudents(failedCreates);

        var failedDeletes = [];
        if (failToDeleteAssociations !== null) {
            _.pluck(failToDeleteAssociations, 'objectId');
        }
        me.updateListWithDeletedStudents(failedDeletes);

    }

    /**
     * Adds the newly created students to the original list of students
     * @param failedStudentIds - ids of students that could not be added to the group in the backend
     */
    this.updateListWithNewStudents = function (failedStudentIds) {
        var studentsToCreate = me.getStudentsToCreate();

        // filter out the failed creates
        _.each(studentsToCreate, function (studentToCreate) {
            var matchingStudent = _.find(failedStudentIds, function (failedStudentId) {
                return failedStudentId === studentToCreate;
            });

            if (matchingStudent === undefined) {
                // student was successfully created in backend
                me.serverGroup.students.push(studentToCreate);
            }
        });
    }

    /**
     * Removes the deleted students from the original list of students
     * @param failedStudentIds - ids of the students that could not be deleted from grop in the backend
     */
    this.updateListWithDeletedStudents = function (failedStudentIds) {
        // filter out unsuccessful deletes
        var studentsToDelete = me.getStudentsToDelete();
        _.each(studentsToDelete, function (studentToDel) {
            var matchingStudent = _.find(failedStudentIds, function (failedStudentId) {
                return failedStudentId === studentToDel;
            });

            if (matchingStudent === undefined) {
                // student was successfully deleted in backend
                me.serverGroup.students = _.filter(me.serverGroup.students, function (originalStudent) {
                    return originalStudent !== studentToDel;
                });
            }
        });
    }

    /**
     * Reset group state to original state
     */
    this.close = function () {
        me.students = [];
    }

    /**
     * Delete this group permanently from backend
     * @param successHandler
     * @param errorHandler
     */
    this.delete = function (successHandler, errorHandler) {
        var successHandler = successHandler;
        var errorHandler = errorHandler;

        var groupId = me.getId();
        $.ajax({
            type: 'POST',
            url: 'DeleteGroup?id=' + groupId,
            contentType: 'application/json',
            success: function (result) {
                if (result.completedSuccessfully) {
                    successHandler(result);
                } else {
                    errorHandler(result);
                }
            },
            error: function (result) {
                errorHandler(result);
            }
        });
    }
}