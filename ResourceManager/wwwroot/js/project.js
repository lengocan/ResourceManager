// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.


//import toast = require("../User/src/js/bootstrap/dist/toast.js");

// Write your JavaScript code.


$(document).ready(() => {
    console.log("hello");
    renderTableProject();
    
});


function createProject() {
    var id = $("#idProject").val();
    var projectName = $('#projectName').val();
    var status = $("#status").val();
    var branch = $('#branch').val();
    var createDay = $('#createDay').val();
    var dueday = $('#dueday').val();
    var priority = $('#priority').val();
    var instruction = $('#instruction').val();


    if (id === '') {
        $.ajax({
            url: '/Project/addProject',
            type: 'POST',
            data: {
                id: id,
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
                    console.log(data)
                    toastr.success("Success add project");
                    $("#ModalAddProjectPartial").modal('hide');
                    renderTableProject();
                }
            }
        })
    }
    /*else {

    }*/
    
}
function renderTableProject() {
    $('#listproject').empty();
    
    $.ajax({
        url: '/Project/getAllProject',
        type: 'GET',
        success: function (data) {
            console.log("Danh sach du lieu: ", data);
            if (data.length > 0) {
                data.forEach((item, index) => {
                    
                    $('#listproject').append(`
								<tr>									
                                    <td class="align-middle text-center">${item.projectNumber}</td>    
                                    <td class="align-middle text-center"><a href="/Project/DetailProject/${item.projectId}">${item.projectName}</a></td>
									
									
									<td class="align-middle text-center">${item.branch == "1" ? "TPHCM" : item.branch == "2" ? "Ha Noi" : "Da Nang"}</td>
                                     <td class="align-middle text-center ${item.status == "1" ? "text-primary" : item.status == "2" ? "text-warning" : "text-success"}">${item.status == "1" ? "Queue" : item.status == "2" ? "In Process" : "Complete"}</td>
									
                                     <td class="align-middle text-center ${item.priority == "1" ? "text-primary" : "text-danger"}">${item.priority == "1" ? "Regular" : "High"}</td>
									<td class="align-middle text-center">${item.createDay}</td>
									<td class="align-middle text-center">${item.dueDay}</td>
									<td class="align-middle text-center">${item.turntime}</td>
									<td class="text-center">
										<button type="button" class="btn btn-primary" onclick="EditProject('${item.projectId}')">Edit</button>
										<button type="button" class="btn btn-danger" onclick="DeleteProject('${item.projectId}')">Delete</button>
									</td>
								</tr>
							`);
                });
            }
            else {
                $('#listproject').append(`
							<tr>
								<td class="text-center" colspan="8">No data</td>
							</tr>
						`);
            }

        },
        error: function (error) {
            console.log(error);
        }
    });
}

function DeleteProject(id) {
    console.log(id)
    $.ajax({
        url: '/Project/DeleteProject/' + id,
        type: 'DELETE',
        success: function (data) {
            if (data) {
                toastr.success("Success delete project");
                renderTableProject();
            }

            else {
                toastr.error("Cannot delete project");
            }
        },
        error: function (error) {
            console.log(error);
        }
    });
}



