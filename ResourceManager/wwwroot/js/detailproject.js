// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.


//import toast = require("../User/src/js/bootstrap/dist/toast.js");

// Write your JavaScript code.


////Detail project
$(document).ready(() => {
    console.log("hello");
    var pathArray = window.location.pathname.split('/');
    var projectId = pathArray[pathArray.length - 1];
    console.log(projectId)
    renderPageProject(projectId);
    
});

function UpdateProjectDetail() {
    var pathArray = window.location.pathname.split('/');
    var projectId = pathArray[pathArray.length - 1];
    console.log("id:", projectId)
    var projectName = $('#projectNameEdit').val();
    var status = $("#statusEdit").val();
    var branch = $('#branchEdit').val();
    var createDay = $('#createDayEdit').val();
    var dueday = $('#duedayEdit').val();
    var priority = $('#priorityEdit').val();
    var instruction = $('#instructionEdit').val();


        $.ajax({
            url: '/Project/UpdateProject/' + projectId,
            type: 'PUT',
            data: {
               
                projectName: projectName,
                createDay: createDay,
                dueday: dueday,
                priority: priority,
                instruction: instruction,
                branch: branch,
                status: status
            },
            success: function (data) {
                if (data) {
                    console.log("update ne",data)
                    toastr.success("Success update project");
                    $("#ModalEditProjectPartial").modal('hide');
                    renderPageProject(projectId);
                }
            }
        })
    
    /*else {

    }*/
}
////Detail project
function renderPageProject(projectId) {
    $.ajax({
        url: '/Project/getProjectByID/'+ projectId,
        type: 'GET',
        success: function (data) {
            console.log("Danh sach du lieu 8: ", data); 
            if (data) {
                $('#projectName').text(data.projectName)
                $('#projectNumber').text(data.projectNumber)
                
                $('#status').text(data.status == "1" ? "Queue" : data.status == "2" ? "In Process" : "Complete")
                $('#branch').text(data.branch == "1" ? "TPHCM" : data.branch == "2" ? "Ha Noi" : "Da Nang")
                $('#budget').text(data.budget)
                $('#createdDay').text(data.createDay)
                $('#dueDay').text(data.dueDay)
                $('#turnTime').text(data.turntime)
                $('#priority').text(data.priority == "1" ? "Regular" : "High")
                $('#instructionDisplay').text(data.instruction)
            }
            
            

        },
        error: function (error) {
            console.log(error);
        }
    });
}
////edit Detail project
$('#ModalEditProjectPartial').on('show.bs.modal', function () {
    // Get the button that triggered the modal
    var pathArray = window.location.pathname.split('/');
    var projectId = pathArray[pathArray.length - 1];
    console.log(projectId)
    // Call the function to load the project data
    renderEditProject(projectId)
});

////edit Detail project
function renderEditProject(projectId) {

    $.ajax({
        url: '/Project/getProjectByID/' + projectId,
        type: 'GET',
        success: function (data) {
            console.log("Danh sach du lieu 9: ", data);
            if (data) {

                $('#idProject').val(data.projectId)
                $('#projectNameEdit').val(data.projectName)
                

                $('#statusEdit').val(data.status)
                $('#branchEdit').val(data.branch)
                
                $('#createDayEdit').val(data.createDay)
                $('#duedayEdit').val(data.dueDay)
                
                $('#priorityEdit').val(data.priority)
                $('#instructionEdit').val(data.instruction)

            }
        },
        error: function (error) {
            console.log(error);
        }
    }); 
}

///Assign project to user

$('#ModalAssignProjectPartial').on('show.bs.modal', function () {
    // Get the button that triggered the modal

    renderSelectAccountAssign()
});
function addAssgin() {
    var pathArray = window.location.pathname.split('/');
    var projectId = pathArray[pathArray.length - 1];
    var userId = $('#selectaccountAssign').val();
    $.ajax({
        url: '/Project/Assign/' + projectId + '/' + userId,
        type: 'POST',
        data: {
            projectId: projectId,
            userId:userId
        },

        success: function (data) {
            console.log("assing",data)
            toastr.success('Complete assign');
            $('#ModalAssignProjectPartial').modal('hide')
        },
        error: function () {
            console.log("assing", data)
            toastr.error('Failed to assign');
        }
    })
}
function renderSelectAccountAssign() {
    $.ajax({
        url: '/Account/GetAllEmployee',
        type: 'GET',
        success: function (data) {
            if (data.length > 0) {
                $('#selectaccountAssign').empty();
                $('#selectaccountAssign').append(`
                            <option value="0">Select account</option>
                        `);
                data.forEach((item, index) => {
                    $('#selectaccountAssign').append(`
                            <option value="${item.id}">${item.fullName}</option>
                        `);
                });
            } else {
                $('#selectaccountAssign').append(`
                            <option value="-1">Không có dữ liệu</option>
                        `);
            }
        },
        error: function (error) {
            console.log(error);
        }
    });

}

function renderAssignInfo() {

}

