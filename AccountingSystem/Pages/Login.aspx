<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AccountingSystem.Pages.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="stylesheet" href="~/bootstrap-5.1.1-dist/css/bootstrap.min.css" />
    <script src="../Jquery/jquery-3.6.0.min.js"></script>
    <script src="../bootstrap-5.1.1-dist/js/bootstrap.min.js"></script>
    <style>
        .tittleArea {
            margin: 20px;
        }

        .tittle {
            margin-right: 40px;
        }

        .loginText {
            margin-top: 20px;
        }

        .loginMsg {
            color: red;
        }
    </style>
</head>
<body>
    <div class="d-flex justify-content-center tittleArea">
        <div>
            <div class="d-flex mb-5">
                <h1 class="tittle">流水帳管理系統</h1>
            </div>

        </div>
    </div>
    <form method="post">
        <div class="d-flex justify-content-center tittleArea">
            <div>
                <h2>登入</h2>
                <div class="d-flex justify-content-between">
                    <label class="mt-3 mr-3" for="inputAccount">Account</label>
                    &nbsp;
					<div class="form-floating mb-3">
                        <input class="form-control" id="inputAccount" name="account" autocomplete="off" />
                    </div>
                </div>

                <div class="d-flex justify-content-between">
                    <label class="mt-3 mr-3" for="inputPassword">PWD</label>
                    &nbsp;
					<div class="form-floating mb-3">
                        <input class="form-control" id="inputPassword" name="password" autocomplete="off" />
                    </div>
                </div>

                <div class="mb-3">
                    <div class="btn btn-primary" id="btnSubmit">Login</div>
                </div>
                <div class="loginMsg" id="loginMsg"></div>
            </div>
        </div>

    </form>

    <script>
        //btnSubmit按鈕的點擊事件
        $("#btnSubmit").click(function () {

            let account = document.getElementById("inputAccount")//取得帳號欄位的值
            let password = document.getElementById("inputPassword")//取得密碼欄位的值

            //檢查帳號及密碼欄位是否為空值,是的話提示錯誤訊息,並中止函式
            if (account.value.trim() == "" || password.value.trim() == "") {
                $("#loginMsg").text("請輸入帳號密碼")
                return
            }

            //發送ajax至LoginHandler
            $.ajax({
                method: "POST",
                url: "../HttpHandlers/LoginHandler.ashx",
                data: { account: account.value.trim(), password: password.value.trim() }
            })
                .done(function (data) {
                    console.log(data)

                    if (data == "success") {
                        window.location.href = 'AccountingList.aspx'
                    } else {
                        $("#loginMsg").text("帳號或密碼錯誤")
                    }
                });
        });
    </script>
</body>
</html>
