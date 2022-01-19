<%@ Page Title="" Language="C#" MasterPageFile="~/NavbarAndSidebar.Master" AutoEventWireup="true" CodeBehind="CategoryList.aspx.cs" Inherits="AccountingSystem.Pages.CategoryList" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="layoutSidenav">
        <div id="layoutSidenav_content" class="m-5">
            <main>
                <form>
                    <h2>流水帳管理</h2>
                    <a href="#">流水帳紀錄</a> <span>>流水帳分類管理</span>
                    <div>
                        <a href="CategoryDetail.aspx" class="m-3 btn btn-primary">Add</a>
                        <div id="btnDel" class="m-3 btn btn-danger">Del</div>
                    </div>

                    <div class="card mb-4">
                        <div class="card-body">
                            <table id="datatablesSimple">
                                <thead>
                                    <tr>
                                        <th></th>
                                        <th>建立日期</th>
                                        <th>分類</th>
                                        <th>流水帳數</th>
                                        <th>Act</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="CategoryRepeater" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <input type="checkbox" name="categorySid" value="<%# Eval("Category_Sid") %>" ></td>
                                                <td><%# Eval("Create_Time") %></td>
                                                <td><%# Eval("Category_Name") %></td>
                                                <td><%# Eval("Has_Accounting") %></td>
                                                <td><a href="CategoryDetail.aspx?CategorySid=<%# Eval("Category_Sid") %>">Edit</a></td>
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
            $('input:checkbox:checked[name="categorySid"]').each(function (i) { cbxVehicle[i] = this.value; });

            //cbxVehicle長度為0,提示錯誤訊息並終止函式
            if (cbxVehicle.length == 0) {
                $("#errMsg").text("請至少選擇一項分類")
                return
            }

            //確定使用者是否刪除資料
            var yes = confirm('確定刪除以選取資料?');

            //yes為false中止函式
            if (!yes)
                return

            //發送ajax至CategoryListHandler,成功後重新載入畫面
            $.ajax({
                method: "POST",
                url: "../HttpHandlers/CategoryListHandler.ashx",
                data: { Delete_Sid: cbxVehicle }
            })
                .done(function (data) {

                    console.log(data)
                    if (data["result"] == "success") {
                        window.location.href = 'CategoryList.aspx'
                    } else if (data["result"] == "Fail") {
                        $("#errMsg").text(data["message"])
                    }
                });
        });

        
    </script>
</asp:Content>
