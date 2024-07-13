// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.


//import toast = require("../User/src/js/bootstrap/dist/toast.js");

// Write your JavaScript code.


$(document).ready(() => {
    console.log("hello");
    
});

$('#infoUser').on('shown.bs.modal', function () {
    console.log("open");
    fillInfo();
})



$('#saveInfo').on('click', function () {
    var fullName = $('.modalfullname').val();
    var phone = $('.modalnumber').val();
    var dob = $('.modalbirthday').val();
    var dayJoin = $('.modaljoinday').val();
    var address = $('.modaladdress').val();
    var team = $('.modalteam').val();

    $.ajax({
        url: '/Account/addInfoUser',
        type: 'PATCH',
        data: {
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
            toast.success("Update thanh cong")
        },
        error: function () {
            alert('Them that bai')
        }
    });
    
});
function fillInfo() {
    $.ajax({
        url: '/Account/GetCurrentAccount',
        type: 'GET',
        success: function (data) {
            console.log("danh sach du lieu", data);
            $('.modalfullname').val(data.fullName);
            $('.modalemail').val(data.email);
            $('.modalnumber').val(data.phoneNumber);
            $('.modalbirthday').val(data.dob);
            $('.modaljoinday').val(data.dayJoin);
            $('.modaladdress').val(data.address);
            $('.modalemail').val(data.email);
            $('.modalteam').val(data.team);
        }

    })
}