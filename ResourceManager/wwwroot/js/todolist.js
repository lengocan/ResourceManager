


//import toast = require("../User/src/js/bootstrap/dist/toast.js");

// Write your JavaScript code.


$(document).ready(() => {
    console.log("hello");
    renderProject()
});


let expandedProjectId = null;

function renderProject() {
    $('#listProjectTDL').empty();

    $.ajax({
        url: '/Project/getAllProjectAsCurrentUser',
        type: 'GET',
        success: function (data) {
            console.log("Danh sach du lieu: ", data);
            if (data.length > 0) {
                data.forEach((item, index) => {
                    $('#listProjectTDL').append(`
					<div class="mb-3 p-3 mt-3 d-flex justify-content-around">
                            <h5 class="project-label" onclick="toggleTaskList('${item.projectId}')">${item.projectName}</h5>
                            <a href="#" class="btn btn-primary shadow rounded pl" data-bs-toggle="modal" data-bs-target="#ModalAddToDoTaskPartial" onclick="setProjectId('${item.projectId}')">Add Task</a>
                        </div>
                
                `);
                    
                    $('#listProjectTDL').append(`
                        <div id="task-list-${item.projectId}" class="task-list"></div>
                    `);

                    renderTask(item.projectId);
                });
                if (expandedProjectId) {
                    $(`#task-list-${expandedProjectId}`).show();
                }
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
function toggleTaskList(projectId) {
    $(`#task-list-${projectId}`).toggle();
    expandedProjectId = $(`#task-list-${projectId}`).is(':visible') ? projectId : null;
}
function setProjectId(projectId) {
    $('#idProjectTodo').val(projectId);
}
function renderTask(projectId) {
    $(`#task-list-${projectId}`).empty();
    $.ajax({
        url: '/TodoList/getAllTask/' + projectId,
        type: 'GET',
        success: function (data) {
            console.log("Danh sach du lieu to do list: ", data);
            if (data.length > 0) {
                data.forEach((item, index) => {
                    $(`#task-list-${projectId}`).append(`


                        <div class="task-item ${item.isCompleted ? 'Completed' : ''}">
                            <input type="checkbox" class="form-check-input" ${item.isCompleted ? 'checked' : ''} onclick="toggleCompleteTask('${item.todoListId}', this)">
                            <span>${item.taskName}</span>
                            <span class="ms-auto">${item.estimateHour} hours</span>
                            <button type="button" class="btn btn-primary ms-2" onclick="editTask('${item.todoListId}')">Edit</button>
                            <button type="button" class="btn btn-danger ms-2" onclick="deleteTask('${item.todoListId}')">Delete</button>
                        </div>
                    `);
                });
            } else {
                $(`#task-list-${projectId}`).append(`
                    <div class="text-center">No tasks found</div>
                `);
            }
        }
    });
}

function AddTask() {
    var projectId = $('#idProjectTodo').val();
    var taskName = $('#taskName').val();
    var estimateHour = $('#estimateHour').val();
    console.log(projectId);
    
    $.ajax({
        url: '/ToDoList/addTask/' + projectId,
        type: 'POST',
        data: {
            taskName: taskName,
            estimateHour:estimateHour
        },
        success: function (data) {
            console.log(data)
            toastr.success("Success add task");
            $('#ModalAddToDoTaskPartial').modal('hide');
            expandedProjectId = projectId; 
            renderProject();
        }

    })
}
function deleteTask(taskId) {
    
    $.ajax({
        url: '/ToDoList/deletetask/' + taskId,
        type: 'DELETE',
        success: function (data) {
            console.log("delete task", data)

            toastr.success("Success delete task")
            renderProject();
        },
        error: function (error) {
            console.log(error)
        }

    })
}

function toggleCompleteTask(taskId, checkbox) {
    
    var isComplete = checkbox.checked;
    $.ajax({
        url: '/ToDoList/updateStatus/' + taskId,
        type: 'PATCH',
        data: {
            taskId: taskId,
            isComplete: isComplete 
        },
        success: function (data) {
            console.log(data);
            toastr.success("This taks is completed");
        },
        error: function (error) {
            console.log(error)
        }
    })
}         