
var Xuly = {
    init: function () {
        Xuly.registerEvent();
    },
    registerEvent: function ()
    {
        $(".btnDetail").on("click", function () {
            $("#myModal").modal("show");
            var id = $(this).data('id');
            $(".btnEmail").on("click", function () {
                var email = $(".btnDetail").data("bind");
                Xuly.loadUserADDByEmail(email);             
            })
            Xuly.loadDeatail(id);
        });
       
    },

    loadDeatail: function (id)
    {
        $.ajax({
            url: '/Home/GetDetail',
            data: {
                id: id
            },
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    var data = response.data;
                    $("#tdId").html(data.id);
                    $("#tdName").html(data.name);
                    $("#tdEmail").html(data.email);
                    $("#tdNumber").html(data.contactNumber);
                          
                }
            },
            error: function (err) {
                alert("lỗi  ");
            }
        })
    },
    loadUserADDByEmail: function (email) {
        $.ajax({
            url: '/Home/GetDetailUserADD',
            data: {
                email:email
            },
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    var data = response.data;
                    $("#ADDCompany").html(data.companyName);
                    $("#ADDepartment").html(data.department);
                    $("#ADDName").html(data.displayName);
                    $("#ADDUser").html(data.userType);
                    $("#ADDJob").html(data.jobTitle);
                    $("#ADDPrincipal").html(data.principalName);
                    $("#ADDEmployee").html(data.employeeId);
                }
            },
            error: function (err) {
                alert("lỗi  ");
            }
        });
    }
}
Xuly.init();