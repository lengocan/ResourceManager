// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    console.log("hello Employee");
    loadTable();
})

function loadTable() {
    $('#listEmployee').empty();
    
    $.ajax({
        url: "/Account/GetAllEmployee",
        type: 'GET',
        success: function (data) {
            console.log(data)
           /* data.map((item, index) => {
                $('#listEmployee').append(`
                <tr>
                    <td>${index + 1}</td>
                    <td>${item.email}</td>
                    <td >${item.FullName}</td>
                    <td>${item.PhoneNumber}</td>
                    <td>${item.isActive}</td>
                    <td class="text-center">
                        <button class="btn btn-primary" onclick="EditTask(${item.id})">Edit</button>
                        <button class="btn btn-danger" onclick="DeleteTask(${item.id})">Delete</button>                      
                    </td>
                    </tr>`)
            })*/
        }
    })
}