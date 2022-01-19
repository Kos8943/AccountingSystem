<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="AccountingSystem.Pages.Index" %>

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
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="d-flex justify-content-center tittleArea">
            <div>
                <div class="d-flex mb-5">
                    <h1 class="tittle">流水帳管理系統</h1>
                    <a class="loginText" href="Login.aspx">登入系統</a>
                </div>

                <div class="">
                    <span>初次記帳</span> &emsp; <span id="firstAccounting">2021/01/01 00:00:00</span>
                    <hr />
                </div>

                <div class="">
                    <span>最後記帳</span> &emsp; <span id="lastAccounting">2021/01/01 00:00:00</span>
                    <hr />
                </div>

                <div class="">
                    <span>記帳數量</span> &emsp; <span id="totalAccounting">共22筆</span>
                    <hr />
                </div>

                <div class="">
                    <span>會員數</span> &emsp; <span id="totalUser">共4人</span>
                    <hr />
                </div>
            </div>

        </div>
    </form>

    <script>

        //畫面讀取完後發送ajax至IndexHandler,並將回傳值顯示於畫面上
        window.onload = function () {

            $.ajax({
                method: "GET",
                url: "../HttpHandlers/IndexHandler.ashx"
            })
                .done(function (data) {

                    $("#firstAccounting").text(data["FirstDate"])
                    $("#lastAccounting").text(data["LastDate"])
                    $("#totalAccounting").text(data["TotalAccounting"] + "筆")
                    $("#totalUser").text(data["TotalUser"] + "人")
                });
        }

    </script>
</body>
</html>
