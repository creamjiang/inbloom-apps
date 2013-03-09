require(["main"],
    function (mainConfig) {
        require.config({
            baseUrl: "/Content/",
            paths: {
                // group selection specific
            }
        });


        require(["dateutils", "idletimer", "js/views/groupSelection"],
            function (dateutils, idletimer, GroupSelectionView) {
                // render the main view
                var groupSelectionView = new GroupSelectionView();
                groupSelectionView.render();
            }
        );

    }
);
