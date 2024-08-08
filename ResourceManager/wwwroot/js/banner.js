$(document).ready(() => {
    console.log("hello");
    renderBanner()
});


function renderBanner() {
    $('#listBanner').empty();

    $.ajax({
        url: "https://localhost:7198/api/Banner",
        type: 'GET',
        success: function (data) {
            console.log(data)
            data.map((item, index) => {
                $('#listBanner').append(`
                <tr>
                    <td>${index + 1}</td>
                    <td class="align-middle text-center">${item.color == "1" ? "Blue" : item.color == "2" ? "Red" : "Green"}</td>
                    <td class="align-middle text-center">${item.effect == "1" ? "Blur" : item.effect == "2" ? "Light" : "Dark"}</td>
                    <td>${item.content}</td>
                    
                    <td class="text-center">
                        <button class="btn btn-primary" onclick="EditBanner('${item.id}')">Edit</button>
                        <button class="btn btn-danger" onclick="DeleteBanner('${item.id}')">Delete</button>                      
                    </td>
                    <td class="text-center">
                        <input type="checkbox" ${item.isUse ? 'checked' : ''} onclick="toggleUse('${item.id}', this)">
                    </td>
                    </tr>`)
            })
        }
    })
}

function addBanner() {
    var color = $("#color").val();
    var effect = $("#effect").val();
    var content = $("#content").val();

    $.ajax({
        url: 'https://localhost:7198/api/Banner',
        type: 'POST',
        processData: false,
        contentType: 'application/json',
        data: JSON.stringify( {
            color: color,
            effect: effect,
            content: content
        }),
        success: function (data) {
            console.log(data)
            toastr.success("Success add banner");
            $("#modalCreateUpdateBanner").modal('hide');
            renderBanner();
        },
        error: function (error) {
            console.log(error)
        }
    })
}

function DeleteBanner(id) {
    console.log(id)
    $.ajax({
        url: 'https://localhost:7198/api/Banner/' + id,
        type: 'DELETE',
        success: function () {

            toastr.success("Success delete banner");
            renderBanner();

        },
        error: function (error) {
            console.log(error);
        }
    });
}
function toggleUse(id, checkbox) {
    var isUse = checkbox.checked;

    $.ajax({
        url: 'https://localhost:7198/api/Banner/' + id,
        type: 'PUT',
        processData: false,
        contentType: 'application/json',
        data: JSON.stringify({
            id: id,
            isUse: isUse
        }),
        success: function () {
            toastr.success("Banner updated successfully.");
            renderBanner();
        },
        error: function (error) {
            console.log(error);
            toastr.error("Failed to update banner.");
        }
    });
}