// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

$(document).on('click', '.sub', function (e) {
    e.preventDefault();

    var $this = $(this);
    var form = $this.parents('.modal').find('form');
    var dataToSend = form.serialize();
    var url = form.attr('action');

    $.post(url, dataToSend, function (data) {
        if (!data.includes('modal-content')) {
            $('#modDialog').modal('hide');
            location.reload(true);
            return;
        }
        //$('#modDialog').modal('hide');
        $('#modDialog').html(data);
        $('#modDialog').modal('show');
    });
});

$(function () {
    $.ajaxSetup({ cache: false });
    $(document).on('click', ".openmodal", function (e) {

        var url = this.href;

        e.preventDefault();

        $.get(url, function (data) {
            //$('#modDialog').modal('hide');
            $('#modDialog').html(data);
            $('#modDialog').modal('show');
        });
    });
});
$(document).on('change', 'input#flexCheckDefault', function () {
    if ($(this).is(':checked')) {
        $('select#Book_AuthorId').prop('disabled', true);
    } else {
        $('select#Book_AuthorId').prop('disabled', false);
    }
});
$(document).ready(function () {
    $('[data-toggle="popover"]').popover();
});

$(document).on('click', '.add_to_card', function () {
    if ($('.quantity').val() > 0) {
        var price = $('.price').data('price');
        var count = $('.quantity').val();
        var amount = parseFloat(price.replace(',', '.')) * parseFloat(count.replace(',', '.'));
        confirm(`Purchase amount ${amount.toString().replace('.', ',')} ₽`);
    }
});


$(function () {
    $(document).on('click', "#reset-inputs", function () {
        if ($('#WorkingTimeFrom').length) {
            $('#WorkingTimeFrom').val(null);
        }
        if ($('#WorkingTimeTo').length) {
            $('#WorkingTimeTo').val(null);
        }
        if ($('#Name').length) {
            $('#Name').val(null);
        }
        if ($('#Adress').length) {
            $('#Adress').val(null);
        }
        if ($('#SearchString').length) {
            $('#SearchString').val(null);
        }
        if ($('#PriceTo').length) {
            $('#PriceTo').val(null);
        }
        if ($('#AuthorId').length) {
            $('#AuthorId').val(0);
        }
        if ($('#FirstName').length) {
            $('#FirstName').val(null);
        }
        if ($('#LastName').length) {
            $('#LastName').val(null);
        }
        if ($('#City').length) {
            $('#City').val(null);
        }
    });
});