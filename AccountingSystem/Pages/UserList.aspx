<%@ Page Title="" Language="C#" MasterPageFile="~/NavbarAndSidebar.Master" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="AccountingSystem.Pages.UserList" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="layoutSidenav">
        <div id="layoutSidenav_content" class="m-5">
            <main>
                <form action="/UserList" method="Post">
                    <h2>會員管理</h2>
                    <a href="#">管理者後台</a> <span>>會員管理</span>
                    <div>
                        <a href="UserDetail" class="m-3 btn btn-primary">Add</a>
                        <div id="btnDel" class="m-3 btn btn-danger">Del</div>
                    </div>

                    <div class="card mb-4">
                        <div class="card-body">
                            <table id="datatablesSimple">
                                <thead>
                                    <tr>
                                        <th></th>
                                        <th>帳號</th>
                                        <th>姓名</th>
                                        <th>Email</th>
                                        <th>等級</th>
                                        <th>建立時間</th>
                                        <th>Act</th>
                                    </tr>
                                </thead>
                                <tbody>

                                    <asp:Repeater ID="UserListRepeater" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <input type="checkbox" name="userSid" value="<%# Eval("User_Sid") %>"></td>
                                                <td><%# Eval("Account") %></td>
                                                <td><%# Eval("User_Name") %></td>
                                                <td><%# Eval("Email") %></td>
                                                <td><%# (Eval("Account_Level").ToString() == "1" ? "管理者" : "一般會員") %></td>
                                                <td><%# Eval("Create_Time") %></td>
                                                <td><a>Edit</a></td>
                                            </tr>
                                        </ItemTemplate>

                                    </asp:Repeater>

                                </tbody>
                            </table>
                        </div>
                    </div>
                    <span style="color: red;" id="errMsg"></span>
                </form>
            </main>
        </div>

    </div>

    <script>

        $("#btnDel").click(function () {

            var cbxVehicle = new Array();
            $('input:checkbox:checked[name="userSid"]').each(function (i) { cbxVehicle[i] = this.value; });

            console.log(cbxVehicle)

            if (cbxVehicle.length == 0) {
                $("#errMsg").text("請至少選擇一位使用者帳號")
                return
            }

            var yes = confirm('確定刪除以選取使用者帳號?');

            if (!yes)
                return

            $.ajax({
                method: "POST",
                url: "../HttpHandlers/UserListHandler.ashx",
                data: { Delete_Sid: cbxVehicle }
            })
                .done(function (data) {

                    if (data == "success") {
                        window.location.href = 'UserList.aspx'
                    }
                });
        });

    </script>
</asp:Content>
