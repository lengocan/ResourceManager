// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.


//import toast = require("../User/src/js/bootstrap/dist/toast.js");

// Write your JavaScript code.


$(document).ready(() => {
    $.ajax({
        url: "/Account/GetUserRole",
        type: "GET",
        success: function (data) {
            
            if (data.role == "DM") {
                renderNoticeDM();
                renderUserName();
                renderRole();
            } else {
                renderNoticeForUser();
                renderUserName();
                renderRole();
            }
            
        },
        error: function (xhr, status, error) {
            console.error('Error fetching user role:', error);
        }
    });


    
});

$('#infoUser').on('shown.bs.modal', function () {
    
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

function renderNoticeDM() {
    $.ajax({
        url: "/Notice/GetNoticesDM",
        type: "GET",
        success: function (data) {
            console.log("notice admin",data)
            var dropdown = $('#notificationDropdown');
            var notificationText = $('#notificationText');
            dropdown.empty();

            if (data.length === 0) {
                dropdown.append('<a href="#" class="dropdown-item text-center">No new notifications</a>');
                notificationText.text('Notifications');
            } else {
                data.forEach(function (notice) {
                    var noticeItem = `
                        <a href="#" class="dropdown-item" style="color: #333; background-color: #f8f9fa; border-bottom: 1px solid #dee2e6;">
                            <h6 class="fw-normal mb-0" style="font-size: 14px; font-weight: 500; color: #007bff;">${notice.content}</h6>
                            <small style="font-size: 12px; color: #6c757d;">Sent by ${notice.userSentName} at ${notice.timeCreate}</small>
                        </a>
                    `;
                    dropdown.append(noticeItem);
                });
                dropdown.append('<a href="#" class="dropdown-item text-center">See all notifications</a>');
                notificationText.text('Notifications (' + data.length + ')');
            }
        },
        error: function (xhr, status, error) {
            console.error('Error fetching notifications:', error);
        }
    });
}
function renderNoticeForUser() {
    $.ajax({
        url: "/Notice/GetUserNotices",  // Ensure the URL matches your controller route
        type: "GET",
        success: function (data) {
            console.log("notice user", data);
            var dropdown = $('#notificationDropdown');
            var notificationText = $('#notificationText');
            dropdown.empty();

            if (data.length === 0) {
                dropdown.append('<a href="#" class="dropdown-item text-center">No new notifications</a>');
                notificationText.text('Notifications');
            } else {
                data.forEach(function (notice) {
                    var noticeItem = `
                        <a href="#" class="dropdown-item" style="color: #333; background-color: #f8f9fa; border-bottom: 1px solid #dee2e6;">
                            <h6 class="fw-normal mb-0" style="font-size: 14px; font-weight: 500; color: #007bff;">${notice.content}</h6>
                            <small style="font-size: 12px; color: #6c757d;">Received at ${notice.timeCreate}</small>
                        </a>
                    `;
                    dropdown.append(noticeItem);
                });
                dropdown.append('<a href="#" class="dropdown-item text-center">See all notifications</a>');
                notificationText.text('Notifications (' + data.length + ')');
            }
        },
        error: function (xhr, status, error) {
            console.error('Error fetching notifications:', error);
        }
    });
}
function renderUserName() {
    $.ajax({
        url: '/Account/GetCurrentAccount',
        type: 'GET',
        success: function (data) {
            console.log("userinfo",data)
            $('#userNameInterfaceLeft').text(data.fullName);
            $('#userNameInterfaceRight').text(data.fullName);
        }
    })
}
function renderRole() {
    $.ajax({
        url: '/Assignment/GetUserRole',
        type: 'GET',
        success: function (data) {
            console.log("userrole", data)
            $('#userRoles').text(data.roles);
        }
    })
}