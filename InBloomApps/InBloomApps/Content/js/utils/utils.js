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

var utils = utils || {};

utils.arrayUtils = {
	
	shuffle : function(array) {
	    var tmp, current, top = array.length;
	
	    if(top) while(--top) {
	        current = Math.floor(Math.random() * (top + 1));
	        tmp = array[current];
	        array[current] = array[top];
	        array[top] = tmp;
	    }
	
	    return array;
	}
}

utils.uiUtils = {

    /**
     * Create a tooltip on the fly for the given element     
     */
    showTooltip: function (elem, msg, placement, trigger, timeout) {
        // create tooltip on the fly
        $(elem).tooltip('destroy');
        $(elem).tooltip({
            title: msg,
            placement: placement,
            trigger: trigger
        });

        if (trigger === 'manual') {
            $(elem).tooltip('show');
        }
        if (timeout !== undefined && timeout !== null) {
            setTimeout(function () {
                $(elem).tooltip('hide');
            }, timeout);
        }
    }, 

    getUrlParams: function () {
        var params = {};
        window.location.search.replace(/[?&]+([^=&]+)=([^&]*)/gi, function(str,key,value) {
            params[key] = value;
        });
 
        return params;
    },
    
    /**
     * Returns true if the given text is empty or blank text 
     */
    textIsEmpty: function (text) {
        return !/\S/.test(text);
    },

    /**
     * Return the path to a student icon based on the gender
     */
    getStudentIcon: function (gender) {
        var index = Math.floor(Math.random() * 3) + 1; // used to randomize the icon
        if (gender === null) {
            gender = 'male';
        }
        var iconPath = '/Content/img/student-icon-' + gender.toLowerCase() + '.png';
        return iconPath;
    }
}

utils.stringUtils = {

    /**
     * remove trailing whitespaces and line breaks
     */
    trim: function (str) {
        return str.trim().replace(/(\r\n|\n|\r)/gm, " ");
    }
}

utils.printUtils = {
    
    /**
     * prints the given div
     */
    print: function (html) {
        var w = window.open();
        w.document.write(html);
        w.document.close();
        w.focus();
        w.print();
        w.close();
    }
}

utils.fileUtils = {
    
    /**
     * Initializes the jquery-file-upload plugin on the given element
     * and calls the callback whenever a file(s) is selected
     */
    setupFileUpload: function (elem, callback) {
        $(elem).fileupload({
            dataType: 'text',
            url: 'UploadFiles',
            add: function (e, data) {
                callback(data);
            }
        });
    }
}
