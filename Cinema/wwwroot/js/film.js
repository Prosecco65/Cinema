var dataTable;

$(document).ready(function () {
    dataTable = $('#tblData')
        .DataTable({
            dom:
                "<'row'<'col-sm-12 col-md-6'l><'col-sm-12 col-md-6'<'float-md-right ml-2'B>f>>" +
                "<'row'<'col-sm-12'tr>>" +
                "<'row'<'col-sm-12 col-md-5'i><'col-sm-12 col-md-7'p>>",
            language: {
                url: "https://cdn.datatables.net/plug-ins/1.13.2/i18n/it-IT.json"
            },
            ajax: {

                url: "/Admin/Films/GetAll",
                deferRender: true,
            },
            buttons: ['csv', {
                text: '<i class="bi bi-person-badge"></i> &nbsp; Change view',
                action: function (e, dt, node) {
                    $(dt.table().node()).toggleClass('cards');
                    $('.bi', node).toggleClass(['bi-table', 'bi-person-badge']);
                    dt.draw('page');
                },
                className: 'btn-sm',
                attr: {
                    title: 'Change views',
                }
            }],
            columns: [
                {
                    orderable: false,
                    data: "copertina",
                    className: 'text-center align-bottom',
                    render: function (data, type, full, meta) {
                        if (type === 'display') { 
                            return `
                                <img src="${data}" alt="Image Url" class="cards avatar">
                            `
                        }
                        return null
                    }
                },
                { data: "titolo" },
                { data: "genere" },
                { data: "descrizione" },
                { data: "durata" },
                {
                    data: "id",
                    orderable: false,
                    className: 'align-bottom',
                    render: function (data) {
                        return `
                            <a href="/Customer/Home/Details?productId=${data}" class="btn btn-primary form-control">Details</a>
                        `
                    }
                },
            ],
            'initComplete': function (settings, json) {
                $('#tblData').DataTable().ajax.reload();
            },
            'drawCallback': function (settings) {

                var api = this.api();

                var $table = $(api.table().node());

                if ($table.hasClass('cards')) {

                    // Create an array of labels containing all table headers

                    var labels = [];

                    $('thead th', $table).each(function () {

                        labels.push($(this).text());

                    });

                    var max = 650;//valore minimo delle card 
                    // per ogni riga del body




                    //$('tbody tr', $table).each(function () {

                    // //max = Math.max($(this).height(), max);

                    //}).height(max);

                } else
                {

                    // Remove data-label attribute from each cell

                    $('tbody td', $table).each(function () {

                        $(this).removeAttr('data-label');

                    });

                    $('tbody tr', $table).each(function () {

                        $(this).height('auto');

                    });

                }

            }

        })
});