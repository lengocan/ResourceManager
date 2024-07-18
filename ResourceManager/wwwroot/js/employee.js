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
            data.map((item, index) => {
                $('#listEmployee').append(`
                <tr>
                    <td>${index + 1}</td>
                    <td>${item.email}</td>
                    <td >${item.fullName}</td>
                    <td>${item.phoneNumber}</td>
                    <td>${item.isActive}</td>
                    <td class="text-center">
                        <button class="btn btn-primary" onclick="EditTask(${item.id})">Edit</button>
                        <button class="btn btn-danger" onclick="DeleteTask(${item.id})">Delete</button>                      
                    </td>
                    </tr>`)
            })
        }
    })
}

$('#btnAddInfo').on('click', function () {
    
    var fullName = $('.addmodalfullname').val();
    var phone = $('.addmodalnumber').val();
    var dob = $('.addmodalbirthday').val();
    var dayJoin = $('.addmodaljoinday').val();
    var address = $('.addmodaladdress').val();
    var team = $('.addmodalteam').val();
    var email = $('.addmodalemail').val();
    var password = $('.addmodalpassword').val();
    

    $.ajax({
        url: '/Account/addUser',
        type: 'POST',
        data: {
            email: email,
            password:password,
            fullName: fullName,
            dob: dob,
            address: address,
            dayJoin: dayJoin,
            team: team,
            isActive: true,
            phoneNumber: phone
        },
        success: function (data) {
            console.log(data)
            $('#infoUser').modal('hide');
            toast.success("Success add user")
        },
        error: function () {
            alert('Them that bai')
        }
    });

});