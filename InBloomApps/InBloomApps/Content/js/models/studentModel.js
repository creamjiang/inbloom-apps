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
 * Client side student model
 * @param studentData - Server side student model
 */
student_grouping.studentModel = function (studentData) {
    var me = this;
    this.pubSub = PubSub;

    this.serverStudent = studentData;

    /**************************
     * GETTER AND SETTERS
     **************************/
    /**
     * Returns the student id
     */
    this.getId = function () {
        return me.serverStudent.id;
    }

    /**
     * Returns the student name
     */
    this.getName = function () {
        return me.serverStudent.name;
    }

    /**
     * Returns the student's gender
     */
    this.getGender = function () {
        return me.serverStudent.sex;
    }

    /**
     * Generic getter for student properties, 
     * quick workaround to avoid mapping all server-side attributes
     */
    this.getProp = function (propName) {
        // TODO null and sanity check
        return me.serverStudent[propName];
    }
}