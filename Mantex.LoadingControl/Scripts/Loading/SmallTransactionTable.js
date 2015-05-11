(function ($) {
    //use strict;

    $(document).ready(function () {

        $('.transaction-table').on('click', ':radio', function () {
            var id = $(this).closest('tr').data('id');
            $('#BatchStatusContainer').load('/Loading/BatchStatus/', { id: id });
            $('#ObservationsContainer').load('/Loading/Observations/' + id); //Forces a Http GET by NOT specifying a request object
        });
        
    });

}(jQuery));