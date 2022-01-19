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
                        <a href="UserDetail.aspx" class="m-3 btn btn-primary">Add</a>
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
                                                <td><a href="UserDetail.aspx?UserSid=<%# Eval("User_Sid") %>">Edit</a></td>
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
        //刪除按鈕的點擊事件
        $("#btnDel").click(function () {

            //將有勾選的checkbox的值用陣列cbxVehicle來承接
            var cbxVehicle = new Array();
            $('input:checkbox:checked[name="userSid"]').each(function (i) { cbxVehicle[i] = this.value; });

            //cbxVehicle長度為0,提示錯誤訊息並終止函式
            if (cbxVehicle.length == 0) {
                $("#errMsg").text("請至少選擇一位使用者帳號")
                return
            }

            //確定使用者是否刪除資料
            var yes = confirm('確定刪除以選取使用者帳號?');

            //yes為false中止函式
            if (!yes)
                return

            //發送ajax至UserListHandler,成功後重新載入畫面
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
