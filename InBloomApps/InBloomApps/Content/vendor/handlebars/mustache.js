/**
 * mustache.js - Minimalistic plugin for requiring 
 * Handlebars templates into RequireJS modules.
 */

define([], function () {
    var ret = {
        load: function (name, req, callback) {
            var name = req.toUrl(this.parse(name)),
              xhr = new XMLHttpRequest();

            xhr.open('GET', name, false);
            xhr.onreadystatechange = function () {
                if (xhr.readyState == 4) {
                    callback(Handlebars.compile(
                      xhr.responseText
                    ));
                }
            }
            xhr.send(null);
        },

        parse: function (name) {
            return name.indexOf('!') !== -1 ?
              name.substr(name.indexOf('!') + 1) :
              name;
        }
    };

    return ret;
});