$(document).ready(() => {
    console.log("hello");
    renderPost()
});

$('#modalCreateUpdatePost').on('show.bs.modal', function (e) {
    $.ajax({
        url: "/Account/GetCurrentAccount",
        type: "GET",
        success: function (data) {
            console.log(data)
            if (data && data.fullName) {
                $("#userCreated").val(data.fullName);
            }
        }
    })
});

function renderPost() {
    $('#listPost').empty();

    $.ajax({
        url: "https://localhost:7198/api/Post",
        type: 'GET',
        success: function (data) {
            console.log(data)
            
                data.map((item, index) => {
                    $('#listPost').append(`
                <tr>
                    <td>${index + 1}</td>
                    <td>${item.createdBy}</td>
                    <td>${item.created}</td>
                    <td>${item.content}</td>
                    
                    <td class="text-center">
                        <button class="btn btn-primary" onclick="EditPost('${item.id}')">Edit</button>
                        <button class="btn btn-danger" onclick="DeletePost('${item.id}')">Delete</button>                      
                    </td>
                    
                    </tr>`)
                })
            
           
           
        }
    })
}
function getCurrentDateTime() {
    var now = new Date();
    var year = now.getFullYear();
    var month = (now.getMonth() + 1).toString().padStart(2, '0'); 
    var day = now.getDate().toString().padStart(2, '0');
    var hours = now.getHours().toString().padStart(2, '0');
    var minutes = now.getMinutes().toString().padStart(2, '0');
    var seconds = now.getSeconds().toString().padStart(2, '0');
    return `${year}-${month}-${day} ${hours}:${minutes}:${seconds}`;
}
function addPost() {
    var createdBy = $("#userCreated").val();
    var created = getCurrentDateTime();
    var content = $("#content").val();

    $.ajax({
        url: 'https://localhost:7198/api/Post',
        type: 'POST',
        processData: false,
        contentType: 'application/json',
        data: JSON.stringify({
            createdBy: createdBy,
            created: created,
            content: content
        }),
        success: function (data) {
            console.log(data)
            toastr.success("Success add post");
            $("#modalCreateUpdatePost").modal('hide');
            renderPost();
        },
        error: function (error) {
            console.log(error)
        }
    })
}

function DeletePost(id) {
    console.log(id)
    $.ajax({
        url: 'https://localhost:7198/api/Post/' + id,
        type: 'DELETE',
        success: function () {

            toastr.success("Success delete banner");
            renderPost();

        },
        error: function (error) {
            console.log(error);
        }
    });
}
