


//import toast = require("../User/src/js/bootstrap/dist/toast.js");

// Write your JavaScript code.


$(document).ready(() => {
    console.log("hello");
    renderTableRoles();
    
});

function CreateUpdateRoles() {
    var id = $('#idRoles').val();
    var quyen = $('#roleName').val();
    
    

    if (id === '') {
        $.ajax({
            url: '/Assignment/CreateRole',
            type: 'POST',
            data: {
                name: quyen              
            },
            success: function (data) {
                console.log("QUYEN DC TAO LA",data);
                if (data) {
                    toastr.success('Add success!');
                    renderTableRoles();
                    $('#modalCreateUpdateRoles').modal('hide');
                }
            },
            error: function (error) {
                toastr.error('Quyền đã tồn tại');
                $('#modalCreateUpdateRoles').modal('hide');
            }
        });
    }
}

function renderTableRoles() {
    $('#listrole').empty();
    $.ajax({
        url: '/Assignment/GetAllRoles',
        type: 'GET',
        success: function (data) {
            console.log("Danh sach du lieu: ", data);
            if (data.length > 0) {
                data.forEach((item, index) => {
                    var parentName = data.filter(x => x.categoryId === item.parentId)[0].categoryName;
                    $('#listrole').append(`
								<tr>
									<td class="text-center"><input type="checkbox"/></td>
                                    <td>${item.name}</td>                                  
									
									<td class="text-center">
										
										<button type="button" class="btn btn-danger" onclick="DeleteRole('${item.id}')">Delete</button>
									</td>
								</tr>
							`);
                });
            }
            else {
                $('#listrole').append(`
							<tr>
								<td class="text-center" colspan="5">Không có dữ liệu</td>
							</tr>
						`);
            }
            
        },
        error: function (error) {
            console.log(error);
        }
    });
}
function DeleteRole(id) {
    $.ajax({
        url: '/Assignment/DeleteRole/' + id,
        type: 'DELETE',
        success: function (data) {
            if (data) {
                renderTableRoles();
            }
            else {
                alert('Xóa sản phẩm thất bại');
            }
        },
        error: function (error) {
            console.log(error);
        }
    });
}