jQuery(function ($) {
    //Custom code to trigger sidebar
    $('#sidebar ul li').click(function (e) {
       // e.preventDefault(); //prevent the link from being followed
        $('#sidebar ul li').removeClass('active');
        $(this).addClass('active');
    });


    $(document).on('click', 'th input:checkbox', function () {
        var that = this;
        $(this).closest('table').find('tr > td:first-child input:checkbox')
        .each(function () {
            this.checked = that.checked;
            $(this).closest('tr').toggleClass('selected');
        });
    });


    $('[data-rel="tooltip"]').tooltip({ placement: tooltip_placement });
    function tooltip_placement(context, source) {
        var $source = $(source);
        var $parent = $source.closest('table');
        var off1 = $parent.offset();
        var w1 = $parent.width();

        var off2 = $source.offset();
        //var w2 = $source.width();

        if (parseInt(off2.left) < parseInt(off1.left) + parseInt(w1 / 2)) return 'right';
        return 'left';
    }


})