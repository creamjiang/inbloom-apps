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
 * StudentFilterWidget
 *
 * Allows the user to apply filters to the list of students in 
 * order to narrow down their search for a student(s).
 */
student_grouping.studentFilterWidget = function () {
    var me = this;
    this.pubSub = PubSub;

    this.filterAttributeElem = '#filter-attribute';
    this.filterOperatorElem = '#filter-operator';
    this.filterValueTxtElem = '#filter-value';
    this.filterValueSelElem = '#filter-values';
    this.filterAddBtnElem = '#filter-add-btn';

    this.filterDefaultOperatorClass = '.filter-operator-default-option';

    this.studentFiltersModel = null;

    this.selectedFiltersElem = '#selected-filters';
    this.selectedFilterCloseBtn = ".select2-search-choice-close";

    /**************************
     * HTML Templates
     **************************/
    this.selectedFilterTemplate = "<li class='select2-search-choice' data-selectedFilter=''>" +
									'<a href="#" onclick="return false;" class="select2-search-choice-close" style="display:inline" tabindex="-1"></a>' +
									"<div class='filter-text' style='display:inline'></div>"
    "</li>";

    /**************************
     * SETUP METHODS
     **************************/
    this.init = function (filters) {

        me.studentFiltersModel = new student_grouping.studentFiltersModel();

        // set up the filters
        _.each(filters, function (filter) {
            me.studentFiltersModel.addFilter(filter);
            $(me.filterAttributeElem).append(me.createOption(filter.attributeId, filter.attributeName));
        });

        $(me.filterAttributeElem).select2({ width: 'element' });
        $(me.filterOperatorElem).select2({ width: 'element' });
        $(me.filterOperatorElem).select2('disable');

        me.setupEventHandlers();
        me.setupSubscriptions();
    }

    /**
     * Sets up the event handlers for user interaction with the widget
     */
    this.setupEventHandlers = function () {
        $(me.filterAttributeElem).change(function (event) {
            me.attributeSelected(event);
        });

        $(me.filterOperatorElem).change(function (event) {
            me.attributeOperatorSelected(event);
        });

        $(me.filterAddBtnElem).click(function (event) {
            me.addSelectedFilter();
        });
    }

    /**
     * Sets up listeners for pubsub events
     */ 
    this.setupSubscriptions = function () {
        
        me.pubSub.subscribe('add-manual-filter', function (filter) {

            // reset filter, if already applied
            me.removeSelectedFilter(filter.attributeId);

            // add to list
            me.studentFiltersModel.addSelectedFilter(filter);
            me.pubSub.publish('filter-student-list');
        });
    }

    /**************************
     * METHODS
     **************************/
    /**
     * Populate the operators and available values  
     */
    this.attributeSelected = function (event) {
        var attributeId = $(me.filterAttributeElem).val();
        var filter = me.studentFiltersModel.getFilter(attributeId);

        // re-populate the operators dropdown
        $(me.filterOperatorElem).select2('destroy');
        $(me.filterOperatorElem).find(':not(' + me.filterDefaultOperatorClass + ')').remove();
        _.each(filter.operators, function (op) {
            $(me.filterOperatorElem).append(me.createOption(op, op));
        });
        $(me.filterOperatorElem).select2({ width: 'element' });

        $(".select2-container").not(".span11").width("100%");

    }

    /**
     *  
     */
    this.attributeOperatorSelected = function (event) {
        var attributeId = $(me.filterAttributeElem).val();
        var filter = me.studentFiltersModel.getFilter(attributeId);

        var operator = $(me.filterOperatorElem).val();

        // if operator is contains then attach multiple to the values dropdown
        if (operator === 'contains') {
            $(me.filterValueSelElem).attr('multiple', 'multiple');
        } else {
            $(me.filterValueSelElem).removeAttr('multiple');
        }

        $(me.filterValueSelElem).select2('destroy');
        if (filter.values === null || filter.values.length === 0) {
            $(me.filterValueTxtElem).show();
            $(me.filterValueSelElem).hide();
        } else {
            $(me.filterValueSelElem).empty();
            // add the available values to the dropdown
            _.each(filter.values, function (val) {

                var option = null;
                if ($.isPlainObject(val)) {
                    option = me.createOption(val.id, val.title);
                } else {
                    option = me.createOption(val, val);
                }

                $(me.filterValueSelElem).append(option);
            });
            $(me.filterValueSelElem).select2({
                width: 'element',
                closeOnSelect: false
            });
            $(".select2-container").not('.span11').width("100%");
            $(me.filterValueTxtElem).hide();
        }
    }

    /**
     * Add the selected filter to the list of filters 
     */
    this.addSelectedFilter = function () {
        var selectedAttrName = $(me.filterAttributeElem).find('option:selected').text();
        var selectedAttrId = $(me.filterAttributeElem).val();
        var selectedOperator = $(me.filterOperatorElem).val();

        // get value from either textbox or dropdown
        var value = $(me.filterValueTxtElem).val();
        var selectedText = value;
        var dropdownVal = $(me.filterValueSelElem).val() !== null ? $(me.filterValueSelElem).val() : [];

        // make sure attribute, operator and value are selected before applying the filter
        if (selectedAttrId === '') {
            return;
        } else if (selectedOperator === '') {
            return;
        } else if (value === '' && dropdownVal === '') {
            return;
        }

        if (value === '') {
            // check whether we are checking for a single value or multiple values
            if ($(me.filterValueSelElem).attr('multiple') === 'multiple') {
                _.each(dropdownVal, function (val) {
                    value += (val + ";");
                    var selText = $(me.filterValueSelElem).find('option[value="' + val + '"]').html();
                    selectedText += (selText + "; ");
                });
            } else {
                value = dropdownVal;
                selectedText = dropdownVal;
            }
        }

        // get the selected filters     	
        var filter = {
            attributeName: selectedAttrName,
            attributeId: selectedAttrId,
            operator: selectedOperator,
            value: value,
            values: dropdownVal
        }

        // remove filter from list if previously added    	
        me.studentFiltersModel.removeSelectedFilter(selectedAttrId);
        $("li[data-selectedFilter='" + selectedAttrId + "']").remove();

        // add to list of selected filters
        me.studentFiltersModel.addSelectedFilter(filter);

        // render the selected filter on screen
        var selectedFilterHtml = $(me.selectedFilterTemplate);
        $(selectedFilterHtml).find('.filter-text').html(filter.attributeName + ' '
    		+ filter.operator + ' ' + '"' + selectedText + '"');
        $(selectedFilterHtml).attr('data-selectedFilter', filter.attributeId);

        $(me.selectedFiltersElem).append(selectedFilterHtml);

        // reset the selected attribute, operator and value
        $(me.filterAttributeElem).select2('val', '');
        $(me.filterOperatorElem).select2('val', '');
        $(me.filterValueTxtElem).val('');
        $(me.filterValueSelElem).select2('val', '');

        // bind event handler for removing this filter
        $(selectedFilterHtml).find(me.selectedFilterCloseBtn).click(function (event) {
            me.removeSelectedFilter(selectedAttrId);
        });

        // notify others to filter
        me.pubSub.publish('filter-student-list');
    },

    /**
     * Remove the selected filter with the given attribute id
     * @param attributeId
     */
    this.removeSelectedFilter = function (attributeId) {
        $("li[data-selectedFilter='" + attributeId + "']").remove();
        me.studentFiltersModel.removeSelectedFilter(attributeId);

        // notify others to filter after removing
        me.pubSub.publish('filter-student-list');
    }

    /**
     * Filter the given list of students using the selected filters
     */
    this.applyFilters = function (studentModels) {
        return me.studentFiltersModel.applyFilters(studentModels);
    }

    /**
     * Creates an HMTL option element 
     */
    this.createOption = function (val, text) {
        return "<option value='" + val + "'>" +
    			text + "</option>";
    }

    /**
     *  
     */
    this.resetFilters = function () {

        $(this.filterAttributeElem).select2('val', '');

        $(this.filterOperatorElem).prepend(this.createOption('', ''));
        $(this.filterOperatorElem).select2('val', '');
        $(this.filterOperatorElem).select2('disable');

        $(this.filterValueSelElem).select2('destroy');
        $(this.filterValueSelElem).empty().hide();

        $(this.filterValueTxtElem).val('').show();
    }
}