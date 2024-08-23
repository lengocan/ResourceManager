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
            console.log(data);

            const selectedTeam = $('#teamSelect').val();

            
            let filteredData = data.filter(item =>
                selectedTeam == '0' || item.team == selectedTeam
            );

            if (filteredData.length > 0) {
                filteredData.map((item, index) => {
                    $('#listEmployee').append(`
                        <tr>
                            <td>${index + 1}</td>
                            <td>${item.email}</td>
                            <td>${item.fullName}</td>
                            <td>${item.phoneNumber}</td>
                            <td class="align-middle text-center">${item.team == "1" ? "TPHCM" : item.team == "2" ? "Ha Noi" : "Da Nang"}</td>
                            <td class="text-center">
                                <button class="btn btn-primary" onclick="EditTask(${item.id})">Edit</button>
                                <button class="btn btn-danger" onclick="DeleteTask(${item.id})">Delete</button>                      
                            </td>
                        </tr>`);
                });
            } else {
                $('#listEmployee').append(`
                    <tr>
                        <td colspan="6" class="text-center">No data</td>
                    </tr>`);
            }
        },
        error: function (error) {
            console.log(error);
        }
    });
}


$('#teamSelect').on('change', function () {
    loadTable();
});
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
            password: password,
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
            $('#addInfoUser').modal('hide');
            loadTable();
            toastr.success("Success add user")
        },
        error: function () {
            alert('Them that bai')
        }
    });

});