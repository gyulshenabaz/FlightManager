$('#dataTable').DataTable({
    "lengthMenu": [
        [10, 25, 50],
        [10, 25, 50]
    ],
    columnDefs: [{
        targets: 'no-sort',
        orderable: false
    }],
    "language": {
        "lengthMenu": "Покажи _MENU_ записа на страница",
        "zeroRecords": "Не е намерен такъв запис",
        "info": "Страница _PAGE_ от _PAGES_",
        "search": "Търси",
        "paginate": {
            "first": "Първа",
            "last": "Последна",
            "next": "Следваща",
            "previous": "Предишна"
        },
        "info": "_START_ - _END_ от _TOTAL_ записа",
        "infoEmpty": "0 записа",
        "infoFiltered": "(Филтриран от общо _MAX_ записа)",
    }
});