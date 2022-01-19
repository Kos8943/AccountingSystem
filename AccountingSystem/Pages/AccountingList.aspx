<%@ Page Title="" Language="C#" MasterPageFile="~/NavbarAndSidebar.Master" AutoEventWireup="true" CodeBehind="AccountingList.aspx.cs" Inherits="AccountingSystem.Pages.AccountingList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .priceTypeColor {
            color:red;
        }

    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="layoutSidenav">
        <div id="layoutSidenav_content" class="m-5">
            <main>
                <form>
                    <h2>流水帳管理</h2>
                    <a href="#">流水帳紀錄</a> <span>>流水帳管理</span>
                    <div>
                        <a href="AccountingDetail.aspx" class="m-3 btn btn-primary">Add</a>
                        <div class="m-3 btn btn-danger" id="btnDel">Del</div>

                        <%--<span class="m-3">小計490元</span>--%>
                        <asp:Label CssClass="m-3" ID="subTotal" runat="server" Text="小計0元"></asp:Label>
                    </div>

                    <div class="card mb-4">
                        <div class="card-body">
                            <table id="datatablesSimple">
                                <thead>
                                    <tr>
                                        <th></th>
                                        <th>建立日期</th>
                                        <th>分類</th>
                                        <th>收 / 支</th>
                                        <th>金額</th>
                                        <th>標題</th>
                                        <th>Act</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="AccountingListRepeater" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <input type="checkbox" name="Delete_Sid" value="<%# Eval("Account_Sid") %>">
                                                </td>
                                                <td><%# Eval("Create_Time") %></td>
                                                <td><%# Eval("Category_Name") %></td>
                                                <td><%# (Eval("Price_Type").ToString() == "0") ? "收入" : "支出" %></td>
                                                <td class="<%# (Eval("Price_Type").ToString() == "0") ? "" : "priceTypeColor" %>"><%# Eval("Price") %></td>
                                                <td><%# Eval("Tittle") %></td>
                                                <td><a href="AccountingDetail.aspx?AccountSid=<%# Eval("Account_Sid") %>">Edit</a></td>
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
                $('input:checkbox:checked[name="Delete_Sid"]').each(function (i) { cbxVehicle[i] = this.value; });

                //cbxVehicle長度為0,提示錯誤訊息並終止函式
                if (cbxVehicle.length == 0) {
                    $("#errMsg").text("請至少選擇一筆紀錄")
                    return
                }

                //確定使用者是否刪除資料
                var yes = confirm('確定刪除以選取資料?');

                //yes為false中止函式
                if (!yes)
                    return

                //發送ajax至AccountingListHandler,成功後重新載入畫面
                $.ajax({
                    method: "POST",
                    url: "../HttpHandlers/AccountingListHandler.ashx",
                    data: { Delete_Sid: cbxVehicle }
                })
                    .done(function (data) {

                        if (data == "success") {
                            window.location.href = 'AccountingList.aspx'
                        } 
                    });
            });
        
    </script>
</asp:Content>
