

$(document).ready(() => {
    console.log("hello");
    renderTablePermission();
    
});

function CreateUpdatePermission() {
    var userid = $('#selectaccount').val();
    var roleid = $('input[type="checkbox"]:checked').val();

    console.log(userid);
    console.log(roleid);


    $.ajax({
        url: '/Assignment/CreatePermission',
        type: 'POST',
        data: {
            userId: userid,
            roleId: roleid
        },
        success: function (data) {
            console.log(data);
            
                
                toastr.success('Phân quyền thành công');

                $('#modalCreateUpdatePermission').modal('hide');

                renderTablePermission();
            
        },
        error: function (error) {
            toastr.error('Quyền đã tồn tại');
            $('#modalCreateUpdatePermission').modal('hide');
        }
    });
    
}

function renderTablePermission() {
    $('#listpermission').empty();
    $.ajax({
        url: '/Assignment/GetAllPermissions',
        type: 'GET',
        success: function (data) {
            console.log("Danh sach du lieu: ", data);
            if (data.length > 0) {
                data.forEach((item, index) => {
                    var parentName = data.filter(x => x.categoryId === item.parentId)[0].categoryName;
                    $('#listpermission').append(`
								<tr>
									<td class="text-center"><input type="checkbox"/></td>
                                    <td>${item.fullName}</td>
									<td>${item.email}</td>
									<td class="text-center">${item.roleName}</td>
                                    <td class="text-center">Active</td>
									<td class="text-center">
										<button type="button" class="btn btn-primary" onclick="EditPermission('${item.id}')">Sửa</button>
										<button type="button" class="btn btn-danger" onclick="DeletePermission('${item.id}')">Xóa</button>
									</td>
								</tr>
							`);
                });
            }

            else {
                $('#listpermission').append(`
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

function renderSelectAccount() {
    $.ajax({
        url: '/Account/GetAllEmployee',
        type: 'GET',
        success: function (data) {
            if (data.length > 0) {
                $('#selectaccount').empty();
                $('#selectaccount').append(`
                            <option value="0">Chọn tài khoản</option>
                        `);
                data.forEach((item, index) => {
                    $('#selectaccount').append(`
                            <option value="${item.id}">${item.userName}</option>
                        `);
                });
            } else {
                $('#selectaccount').append(`
                            <option value="-1">Không có dữ liệu</option>
                        `);
            }
        },
        error: function (error) {
            console.log(error);
        }
    });

}
function renderTableRoles() {
    $.ajax({
        url: '/Assignment/GetAllRoles',
        type: 'GET',
        success: function (data) {
            console.log(data)
            if (data.length > 0) {
                $('#listassignrole').empty();
                data.forEach((item, index) => {
                    $('#listassignrole').append(`<tr>
                                                    <td class="text-center">
                                                        <input type="checkbox" value=${item.id}>
                                                    </td>
                                                    <td>${item.name}</td>
                                                </tr>`);
                });
            }
            else {
                $('#listassignrole').append(`<tr>
                                                <td class="text-center" colspan="2">Không có dữ liệu</td>
                                            </tr>`);
            }
        },
        error: function (error) {
            console.log(error);
        }
    });
}

$('#modalCreateUpdatePermission').on('show.bs.modal', function (e) {
    renderSelectAccount();
    renderTableRoles();
});

//Copy from GitHub
function EditPermission(id) {
    $.ajax({
        url: '/food/' + id,
        type: 'GET',
        success: function (data) {
            if (data) {
                $('#idFood').val(data.id);
                $('#tensanpham').val(data.tenSanPham);
                $('#giaca').val(data.gia);
                $('#danhmuc').val(data.productTypeId);
                $('#mota').val(data.moTa);
                $('#activefood').prop('checked', data.kichHoat);
                $('#backDropModal').modal('show');
            }
            else {
                alert('Không tìm thấy sản phẩm');
            }
        },
        error: function (error) {
            console.log(error);
        }
    });
}
function DeletePermission(id) {
    $.ajax({
        url: '/food/delete/' + id,
        type: 'DELETE',
        success: function (data) {
            if (data) {
                renderTableCategories();
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