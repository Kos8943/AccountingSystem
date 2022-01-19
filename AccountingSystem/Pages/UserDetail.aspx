<%@ Page Title="" Language="C#" MasterPageFile="~/NavbarAndSidebar.Master" AutoEventWireup="true" CodeBehind="UserDetail.aspx.cs" Inherits="AccountingSystem.Pages.UserDetail" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .inputStyle {
            width: 200px;
        }

        .spanMsg {
            color: red;
        }
    </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="layoutSidenav">
        <div id="layoutSidenav_content" class="m-5">
            <main>
                <h2>會員管理</h2>
                <div class="mb-5">
                    <a href="#">管理者後台</a> <span>>會員管理</span>
                </div>

                <form>
                    <div class="container-fluid">
                        <div class="row mb-4">
                            <div class="col-1 p-0 pt-2">
                                <label for="userAccount">帳號</label>
                            </div>
                            <div class="col-3 p-0 d-flex">
                                <input id="userSid" name="userSid" style="display: none;">
                                <span class="pt-2" id="spanAccount"></span>
                                <input id="userAccount" name="account" class="form-control inputStyle">
                                <span class="col-4 p-0 mt-2 spanMsg" id="userAccountSpan"></span>
                            </div>

                        </div>

                        <div class="row mb-4">
                            <div class="col-1 p-0 pt-2">
                                <label for="userName">姓名</label>
                            </div>
                            <div class="col-3 p-0 d-flex">
                                <input class="form-control inputStyle" id="userName"
                                    name="userName">
                                <span class="col-4 p-0 mt-2 spanMsg" id="userNameSpan"></span>
                            </div>
                        </div>

                        <div class="row mb-4">
                            <div class="col-1 p-0 pt-2">
                                <label for="userEmail">Email</label>
                            </div>
                            <div class="col-3 p-0 d-flex">
                                <input id="userEmail" type="email"
                                    class="form-control inputStyle" name="email">
                                <span class="col-4 p-0 mt-2 spanMsg" id="emailSpan"></span>
                            </div>
                        </div>

                        <div class="row mb-4">
                            <div class="col-1 p-0">等級</div>
                            <div class="col-3 p-0">
                                <select name="userAccountLevel" id="userAccountLevel">
                                    <option value="0">一般會員</option>
                                    <option value="1">管理員</option>
                                </select>
                            </div>
                        </div>

                        <div class="row mb-4">
                            <div class="col-1 p-0">建立時間</div>
                            <div class="col-3 p-0" id="createTime">-</div>
                        </div>

                        <div class="row mb-4">
                            <div class="col-1 p-0">修改時間</div>
                            <div class="col-3 p-0" id="modifyTime">-</div>
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
        const userSid = url.searchParams.get('UserSid')//取得QueryString的UserSid值

        //userSid為空值則判斷為新增模式
        if (isNaN(userSid)) {
            window.location.href = "UserDetail.aspx"
        }

        //畫面讀取完後的事件
        window.onload = function () {

            //userSid為空值中止函式
            if (userSid == null)
                return

            //修改模式則發送ajax至UserDetailHandler,並將回傳值顯示於畫面上
            $.ajax({
                method: "GET",
                url: "../HttpHandlers/UserDetailHandler.ashx",
                data: { Action: "GetUserInfo", UserSid: userSid }
            })
                .done(function (data) {

                    $("#spanAccount").text(data['Account'])
                    $("#userAccount").css('display', 'none');
                    $("#userName").val(data['User_Name'])
                    $("#userEmail").val(data['Email'])
                    $("#userAccountLevel").val(data['Account_Level']).change()
                    $("#createTime").text(data['Create_Time'])
                    $("#modifyTime").text(data['Modify_Time'])

                });
        }

        //btnSubmit按鈕的點擊事件
        $("#btnSubmit").click(function () {

            //宣告布林值,用來判斷是否可以發送ajax
            let conSubmit = true;

            let userName = $("#userName").val().trim()//取得姓名欄位的值
            let email = $("#userEmail").val().trim()//取得信箱欄位的值
            let accountLevel = $("#userAccountLevel").val()//取得帳號等級下拉選單的值
            
            //檢查姓名是否為空值,是的話提示錯誤訊息,並將conSubmit改為false
            if (userName == "") {
                $("#userNameSpan").text("請輸入姓名")
                conSubmit = false
            }

            //檢查信箱欄位是否為空值及是否符合格式,是的話提示錯誤訊息並將canSubmit改為false
            if (email == "") {
                $("#emailSpan").text("請輸入信箱")
                conSubmit = false
            } else if (!isEmail(email)) {
                $("#emailSpan").text("信箱格式錯誤")
                conSubmit = false
            }

            //conSubmit為true發送ajax
            if (conSubmit) {

                //userSid為null判斷為新增模式,否為修改模式
                if (userSid == null) {

                    let account = $("#userAccount").val().trim()//取得帳號欄位的值

                    //檢查帳號是否為空值以及是否重複,是的話提示錯誤訊息,並中止函式
                    if (account == "") {

                        $("#userAccountSpan").text("請輸入帳號")
                        return

                    } else if (checkAccountRepetition(account) == false) {
                        $("#userAccountSpan").text("帳號重複")
                        return
                    }

                    $.ajax({
                        method: "POST",
                        url: "../HttpHandlers/UserDetailHandler.ashx",
                        data: {
                            Action: "Insert",
                            Account: account,
                            UserName: userName,
                            Email: email,
                            AccountLevel: accountLevel
                        }
                    })
                        .done(function (data) {

                            if (data == "success") {
                                $("#spanAccount").text(account)
                                $("#userAccount").css('display', 'none')
                                $("#spanAccount").css('display', 'block')
                                $("#actionMsg").text("新增成功")
                            }
                        });
                } else {

                    let spanAccount = $("#spanAccount").text().trim()

                    $.ajax({
                        method: "POST",
                        url: "../HttpHandlers/UserDetailHandler.ashx",
                        data: { 
                            Action: "Update",
                            UserSid: userSid,
                            Account: spanAccount,
                            UserName: userName,
                            Email: email,
                            AccountLevel: accountLevel
                        }
                    })
                        .done(function (data) {
                            if (data == "success") {
                                $("#actionMsg").text("修改成功")
                            }
                        });
                }
            }
        });


        function checkAccountRepetition(account) {

            var instance;

            $.ajax({
                type: "POST",
                cache: false,
                data: { Action: "CheckAccountRepetition", Account: account },
                async: false,
                url: "../HttpHandlers/UserDetailHandler.ashx",
                success: function (res) {

                    if (res == "success")
                        instance = true;
                    else
                        instance = false;
                }
            });
            return instance;
        }

        function isEmail(email) {
            var EmailRegex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
            return EmailRegex.test(email);
        }

        //function checkAccountRepetition(account) {

        //    $.ajax({
        //        method: "POST",
        //        url: "../HttpHandlers/UserDetailHandler.ashx",
        //        data: { Action: "CheckAccountRepetition", Account: account }
        //    })
        //        .done(function (data) {

        //            console.log(data)
        //            if (data == "success") {
        //                return true
        //            } else {
        //                return false
        //            }
        //        });
        //}


        
    </script>
</asp:Content>
