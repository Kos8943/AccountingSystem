<%@ Page Title="" Language="C#" MasterPageFile="~/NavbarAndSidebar.Master" AutoEventWireup="true" CodeBehind="AccountingDetail.aspx.cs" Inherits="AccountingSystem.Pages.AccountingDetail" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="layoutSidenav">
        <div id="layoutSidenav_content" class="m-5">
            <main>
                <h2>會員管理</h2>
                <div class="mb-5">
                    <a href="#">管理者後台</a> <span>>會員管理</span>
                </div>

                <form runat="server">
                    <div class="container-fluid">

                        <div class="row mb-4">
                            <div class="col-1 p-0 pt-2">收 / 支</div>
                            <div class="col-3 p-0">
                                <select name="priceType" id="priceType" class="dataTable-selector">
                                    <option value="0">in</option>
                                    <option value="1">out</option>
                                </select>
                            </div>
                        </div>

                        <div class="row mb-4">
                            <div class="col-1 p-0 pt-2">分類</div>
                            <div class="col-3 p-0">

                                <asp:DropDownList runat="server" name="category" ID="CategoryDropDownList" class="dataTable-selector">

                                </asp:DropDownList>
                                <%--<select name="category" id="category">
                                    <option value="0">分類1</option>
                                    <option value="1">分類2</option>
                                </select>--%>
                            </div>
                        </div>

                        <div class="row mb-4">
                            <div class="col-1 p-0 pt-2">
                                <label for="price">金額</label>
                            </div>
                            <div class="col-3 p-0 d-flex">
                                <input class="form-control inputStyle" id="price"
                                    name="price" type="number">
                                <span class="col-4 p-0 mt-2 spanMsg" id="priceSpan"></span>
                            </div>
                        </div>

                        <div class="row mb-4">
                            <div class="col-1 p-0 pt-2">
                                <label for="tittle">標題</label>
                            </div>
                            <div class="col-3 p-0 d-flex">
                                <input class="form-control inputStyle" id="tittle"
                                    name="tittle">
                                <span class="col-4 p-0 mt-2 spanMsg" id="tittleSpan"></span>
                            </div>
                        </div>

                        <div class="row mb-4">
                            <div class="divArea col-1 p-0 pt-2">
                                <label for="description">備註</label>
                            </div>
                            <div class="col-2 p-0 d-flex">
                                <textarea class="form-control inputWidth" id="description"
                                    name="description"></textarea>
                            </div>
                            <div class="mt-2">
                                <span class="errMsg"></span>
                            </div>
                        </div>

                        <div class="row mb-4">
                            <div class="col-1 p-0">
                                <div id="btnSubmit" class="m-3 btn btn-primary">Save</div>
                            </div>
                        </div>
                    </div>
                    <span id="actionMsg"></span>
                </form>
            </main>
        </div>

    </div>

    <script>

        const url = new URL(window.location.href)//取得當下網址
        const accountSid = url.searchParams.get('AccountSid')//取得QueryString的AccountSid值

        //當網頁讀取完後的事件
        window.onload = function () {

            //accountSid為null判斷為新增模式並停止函式,否則維修改模式
            if (accountSid == null)
                return

            //修改模式則發送ajax至AccountingDetailHandler
            //並將回傳的結果顯示在畫面上
            $.ajax({
                method: "GET",
                url: "../HttpHandlers/AccountingDetailHandler.ashx",
                data: { Action: "GetAccounting", AccountSid: accountSid }
            })
                .done(function (data) {

                    $("#priceType").val(data['Price_Type']).change()
                    $("#<%= CategoryDropDownList.ClientID %>").val(data['CategoryList_CategorySid']).change()
                    $("#price").val(data['Price'])
                    $("#tittle").val(data['Tittle'])
                    $("#description").val(data['Description'])
                });
        }

        //btnSubmit按鈕的點擊事件
        $("#btnSubmit").click(function () {

            //宣告布林值,用來判斷是否可以發送ajax
            let conSubmit = true;

            //取得記帳的各項輸入值
            let priceType = $("#priceType").val()
            let categorySid = $("#<%= CategoryDropDownList.ClientID %>").val()
            let price = $("#price").val()
            let tittle = $("#tittle").val()
            let description = $("#description").val()

            //檢查price是否為空值,是的話回傳錯誤訊息,並將conSubmit設為否
            if (price == "") {
                $("#priceSpan").text("請輸入金額")
                conSubmit = false
            }

            //檢查tittle是否為空值,是的話回傳錯誤訊息,並將conSubmit設為否
            if (tittle == "") {
                $("#tittleSpan").text("請輸入標題")
                conSubmit = false
            }

            //conSubmit為true發送ajax
            if (conSubmit) {

                //accountSid為null判斷為新增模式,否為修改模式
                if (accountSid == null) {

                    $.ajax({
                        method: "POST",
                        url: "../HttpHandlers/AccountingDetailHandler.ashx",
                        data: {
                            Action: "InsertAccounting",
                            CategorySid: categorySid,
                            PriceType: priceType,
                            Price: price,
                            Tittle: tittle,
                            Description: description

                        }
                    })
                        .done(function (data) {

                            alert("新增成功")
                            window.location.href = "AccountingDetail.aspx?AccountSid=" + data
                        });
                } else {

                    $.ajax({
                        method: "POST",
                        url: "../HttpHandlers/AccountingDetailHandler.ashx",
                        data: {
                            Action: "UpdateAccounting",
                            AccountSid: accountSid,
                            CategorySid: categorySid,
                            PriceType: priceType,
                            Price: price,
                            Tittle: tittle,
                            Description: description
                        }
                    })
                        .done(function (data) {
                            if (data == "success") {
                                alert("修改成功")
                                window.location.href = "AccountingDetail.aspx?AccountSid=" + accountSid
                            }
                        });
                }
            }
        });

        //檢查price欄位keypress事件的輸入值是否為數字
        document.querySelector("#price").addEventListener("keypress", function (evt) {
            if (evt.which != 8 && evt.which != 0 && evt.which < 48 || evt.which > 57) {
                evt.preventDefault();
            }
        });
    </script>
</asp:Content>
