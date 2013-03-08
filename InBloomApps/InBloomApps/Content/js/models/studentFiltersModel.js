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
 * StudentFiltersModel
 * @param filters
 *
 * Contains logic for filtering a list of studentModels and maintaining
 * a list of available and selected student data filters.
 */
student_grouping.studentFiltersModel = function (filters) {
    var me = this;

    this.availableFilters = [];
    this.selectedFilters = [];

    /**************************
     * METHODS
     **************************/
    /**
     * Add the given filter to the list of filters
     * @param filter - server-side filter
     */
    this.addFilter = function(filter){
        me.availableFilters[filter.attributeId] = filter;
    }

    /**
     * Return the filter for the given attribute
     * @param attributeId
     */
    this.getFilter = function (attributeId) {
        return me.availableFilters[attributeId];
    }

    /**
     * Add the filter to the list of selected filters
     * @param filter
     */
    this.addSelectedFilter = function (filter) {
        me.selectedFilters[filter.attributeId] = filter;
    }

    /**
     * Remove the filter from the list of selected filters
     * @param attributeId - id of the filter to remove
     */
    this.removeSelectedFilter = function (attributeId) {
        if (me.selectedFilters[attributeId] !== undefined) {
            delete me.selectedFilters[attributeId];
        }
    }

    /**
     * Filter the given list of students using the selected filters
     * @param listOfStudentModels
     */
    this.applyFilters = function (listOfStudentModels) {
        var filteredList = listOfStudentModels;

        var selectedFilters = me.selectedFilters;
        for (var filterId in selectedFilters) {
            var filter = selectedFilters[filterId];
            var attribute = filter.attributeId;
            var operator = filter.operator;
            var value = '';

            if (filter.values.length === 0) {
                value = filter.value;
            } else {
                value = filter.values;
            }

            // filter with selected operator and value		
            filteredList = _.filter(filteredList, function (studentModel) {
                var studentAttributeVal = studentModel.getProp([attribute]);

                // convert date to milliseconds
                if (attribute === 'birthDate') {
                    studentAttributeVal = new Date(studentAttributeVal).getTime();
                    value = new Date(value).getTime();
                }

                switch (operator) {
                    case '=': return studentAttributeVal == parseFloat(value);
                    case '<': return studentAttributeVal < parseFloat(value);
                    case '>': return studentAttributeVal > parseFloat(value);
                    case '<=': return studentAttributeVal <= parseFloat(value);
                    case '>=': return studentAttributeVal >= parseFloat(value);
                    case 'contains':
                        // if not array, then make it into one
                        if (!Array.isArray(studentAttributeVal)) {
                            var copyOfStudentAttributeVal = studentAttributeVal;
                            studentAttributeVal = [];
                            studentAttributeVal.push(copyOfStudentAttributeVal);
                        }

                        var intersection = _.any(studentAttributeVal, function (studentVal) {
                            var studentHasDisability = _.find(value, function (val) {
                                return val === studentVal;
                            });
                            return studentHasDisability !== undefined;
                        });

                        return intersection;
                    case 'equals':
                        var matchingVal = _.find(studentAttributeVal, function (studentVal) {
                            return studentVal === value;
                        });
                        return matchingVal !== undefined;
                    case 'startsWith':
                        return studentAttributeVal
                            .toLowerCase()
                            .lastIndexOf(value.toLowerCase(), 0) === 0;
                    case 'matches':
                        return studentAttributeVal
                            .toLowerCase()
                            .indexOf(value.toLowerCase()) !== -1;
                }
            });
        };

        return filteredList;
    }
}