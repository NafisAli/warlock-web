$(document).ready(function () {
    loadDataTable();
})

function loadDataTable() {
    $('#tblData').DataTable({
        "ajax": { url:  '/admin/product/getall' },
        "columns": [
            { data: 'title', "width": "20%" },
            { data: 'level', "width": "5%" },
            { data: 'tier', "width": "10%" },
            { data: 'listPrice', "width": "10%" },
            { data: 'category.name', "width": "10%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                      <a href="/admin/product/upsert?=${data}" class="btn btn-primary mx-1"><i class="bi bi-pencil-square"></i> Edit</a>
                      <a href="/admin/product/delete/${data}" class="btn btn-danger "><i class="bi bi-trash-fill"></i> Delete</a>
                    </div>`
                },
                "width": "20%"
             }
        ]
    });
}