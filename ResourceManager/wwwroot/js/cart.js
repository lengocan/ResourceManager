


//import toast = require("../User/src/js/bootstrap/dist/toast.js");

// Write your JavaScript code.


$(document).ready(() => {
    console.log("hello");
    renderCart()
    
});

function renderCart() {
    $('#listProjectReceived').empty();

    $.ajax({
        url: "/Cart/getAllCart",
        type: 'GET',
        success: function (data) {
            console.log(data)
            data.map((item, index) => {
                // Generate attachment links only if attachments exist
                let attachments = item.attachments
                    ? item.attachments.map(att =>
                        `<a href="${att.filePath}" download="${att.fileName}">${att.fileName}</a>`
                    ).join(', ')
                    : '';

                $('#listProjectReceived').append(`
                <tr>
                    <td>${index + 1}</td>
                    <td class="align-middle text-center"><a href="/Project/DetailProject/${item.projectId}">${item.projectName}</a></td>
                    <td>${item.userName}</td>
                    
                    <td>${item.dueDay}</td>
                    <td>${item.timeSend}</td>
                    <td>${attachments}</td> <!-- Attachments column -->
                    <td class="text-center">
                        <input type="checkbox" ${item.isAccept ? 'checked' : ''} onclick="toggleAccept('${item.id}', this)">
                    </td>
                    <td class="text-center">
                        <button class="btn btn-danger" onclick="deleteCart('${item.id}')">Delete</button>                      
                    </td>
                </tr>`);
            });
            /*data.map((item, index) => {
                $('#listProjectReceived').append(`
                <tr>
                    <td>${index + 1}</td>
                    
                    <td>${item.projectName}</td>
                    <td>${item.userName}</td>
                    <td>${item.createDay}</td>
                    <td>${item.dueDay}</td>
                    <td>${item.timeSend}</td>
                    
                    
                    <td class="text-center">
                        <input type="checkbox" ${item.isAccept ? 'checked' : ''} onclick="toggleAccept('${item.id}', this)">
                    </td>
                   <td class="text-center">
                        
                        <button class="btn btn-danger" onclick="deleteCart('${item.id}')">Delete</button>                      
                    </td>
                    </tr>`)
            })*/
        }
    })
}
function toggleAccept(id, checkbox) {
    // Only proceed if the checkbox is being checked (not unchecked)
        var isAccept = checkbox.checked;
        $.ajax({
            url: '/Cart/ToggleAccept/' + id, // Adjust the URL if necessary
            type: 'PUT',
            data: {
                id: id,
                isAccept: isAccept}, // Send the ID as form data
            success: function (response) {
                toastr.success("Success accept Project");
            },
            error: function (xhr, status, error) {
                console.error("An error occurred:", error);
               
            }
        });
    
}
function deleteCart(id) {
    if (confirm('Are you sure you want to delete this item?')) {
        $.ajax({
            url: '/Cart/DeleteCart/' +id,
            type: 'DELETE',
            data: { id: id },
            success: function (response) {
                toastr.success("Success delete Cart");
                renderCart(); 
            },
            error: function (xhr, status, error) {
                console.error("An error occurred:", error);
               
            }
        });
    }
}