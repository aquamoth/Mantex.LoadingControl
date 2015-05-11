(function ($) {
    //use strict;

    $(document).ready(function () {

        $('.transaction-table').on('click', ':radio', function () {

            var id = $(this).closest('tr').data('id');
            console.log('clicked: ' + id);
            $('#BatchStatusContainer').load('/Loading/BatchStatus/', { id: id });
        });
        
    });

}(jQuery));