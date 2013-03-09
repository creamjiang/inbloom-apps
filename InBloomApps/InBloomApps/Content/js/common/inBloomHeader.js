/**
 * This is the inBloom header module
 */
define(['mustache!templates/common/header.hbs'],
    function (headerTemplate) {
        var HeaderView = Backbone.View.extend({
            tagName: 'div',
            render: function () {
                var html = headerTemplate();
                this.$el.append(html);
                return this.$el;
            }
        });

        return HeaderView;
    }
);