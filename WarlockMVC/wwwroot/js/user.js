var dataTable;

$(document).ready(function () {
    loadDataTable();
})

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url:  '/admin/user/getall' },
        "columns": [
            { data: 'name', "width": "10%" },
            { data: 'email', "width": "20%" },
            { data: 'phoneNumber', "width": "10%" },
            { data: 'faction.name', "width": "10%" },
            { data: 'role', "width": "5%" },
            {
                data: { id: 'id', lockoutEnd: "lockoutEnd" },
                "render": function (data) {
                    var today = new Date().getTime();
                    var lockout = new Date(data.lockoutEnd).getTime();

                    if (lockout > today) {
                        return `
                        <div class="text-center">
                            <a onclick=ToggleLock('${data.id}') class="btn btn-danger text-white" style="cursor:pointer; width:120px;">
                                <i class="bi bi-lock-fill"></i> Locked
                            </a>
                            <a href="/admin/user/RoleManagement?userId=${data.id}" class="btn btn-danger text-white" style="cursor:pointer; width:150px;">
                                <i class="bi bi-pencil-square"></i> Permission
                            </a>
                        </div>`
                    } else {
                        return `
                        <div class="text-center">
                            <a onclick=ToggleLock('${data.id}') class="btn btn-success text-white" style="cursor:pointer; width:120px;">
                                <i class="bi bi-unlock-fill"></i> Unlocked
                            </a>
                            <a href="/admin/user/RoleManagement?userId=${data.id}" class="btn btn-danger text-white" style="cursor:pointer; width:150px;">
                                <i class="bi bi-pencil-square"></i> Permission
                            </a>
                        </div>`
                    }
                },
                "width": "30%"
             }
        ]
    });
}

function ToggleLock(id) {
    $.ajax({
        type: "POST",
        url: '/admin/user/togglelock',
        data: JSON.stringify(id),
        contentType: 'application/json',
        success: function (data) {
            if (data.success) {
                toastr.success(data.message);
                dataTable.ajax.reload();
            } else {
                toastr.error(data.message);
            }
        }
    });
}