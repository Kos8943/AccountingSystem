<%@ Page Title="" Language="C#" MasterPageFile="~/NavbarAndSidebar.Master" AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" Inherits="AccountingSystem.Pages.UserProfile" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
    	.profileArea {
    		width: 800px;
    	}

    	.profileTittle {
    		width: 75px;
    		text-align: center;
    		border: 1px solid black;
    		padding: 10px;
    	}

    	.profileInput {
    		border: 1px solid black;
    		border-left: none;
    		width: 300px;
    		text-align: center;
    		padding: 6px 5px 4px 5px;
    	}

    	.eroMsg {
    		color: red;
    	}
    </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="layoutSidenav">
		<div id="layoutSidenav_content" class="m-5">
			<main>
				<h2>個人資訊</h2>
				<a href="#">後台管理</a>
				<span>>個人資訊</span>
				
				<form class="my-5">
				
					<div class="profileArea">
						<div class="d-flex">
							<div class="my-auto profileTittle">帳號</div>
							<div class="profileInput"><input id="userAccount" value="12345" disabled></div>
						</div>
					</div>
					
					<div class="profileArea">
						<div class="d-flex">
							<div class="my-auto profileTittle">姓名</div>
							<div class="profileInput">
								<input value="12345" name="UserName" id="UserName">
															
							</div>
							<span class="pt-2 eroMsg" id="userNameMsg"></span>
						</div>						
					</div>
					
					<div class="profileArea">
						<div class="d-flex">
							<div class="my-auto profileTittle">Email</div>
							<div class="profileInput"><input id="UserEmail" value="12345" type="email" name="UserEmail">
							</div>
							<span class="pt-2 eroMsg" id="userEmailMsg"></span>
						</div>
						
					</div>
					
					<div class="mt-5">
						<div id="btnSubmit" class="btn btn-primary" type="submit">Save</div>
					<span class=""></span>
					</div>
					
				</form>
			</main>
		</div>
                
	</div>

	<script>

		//當網頁讀取完後,發送ajax至UserProfileHandler,並將回傳值顯示在畫面
        window.onload = function () {

            $.ajax({
                method: "GET",
                url: "../HttpHandlers/UserProfileHandler.ashx",
                data: { Action: "GetCurrentUserInfo"}
            })
				.done(function (data) {
                    $("#userAccount").val(data['Account'])
                    $("#UserName").val(data['User_Name'])
                    $("#UserEmail").val(data['Email'])

                });
		}

		//btnSubmit按鈕的點擊事件
        $("#btnSubmit").click(function () {

			//宣告布林值,用來判斷是否可以發送ajax
			let canSubmit = true

			let userName = $("#UserName").val().trim()//取得使用者名稱欄位的值
            let userEmail = $("#UserEmail").val().trim()//取得信箱欄位的值

			//檢查姓名欄位是否為空值,是的話提示錯誤訊息並將canSubmit改為false
			if (userName == "") {
				$("#userNameMsg").text("請輸入姓名")
				canSubmit = false
			}

			//檢查信箱欄位是否為空值及是否符合格式,是的話提示錯誤訊息並將canSubmit改為false
			if (userEmail == "") {
				$("#userEmailMsg").text("請輸入信箱")
				canSubmit = false
            } else if (isEmail(userEmail) == false) {
                $("#userEmailMsg").text("信箱格式錯誤")
                canSubmit = false
			}

			//conSubmit為true發送ajax至UserProfileHandler
			if (canSubmit) {

                $.ajax({
                    method: "POST",
                    url: "../HttpHandlers/UserProfileHandler.ashx",
                    data: {
                        Action: "UpdateUserProfile",
                        UserName: userName,
                        UserEmail: userEmail
                    }
                })
					.done(function (data) {

						if (data = "success") {
                            alert("修改成功")
                            window.location.href = "UserProfile.aspx"
                        }
                        
                    });
            }
		});

		//檢查信箱格式是否正確
        function isEmail(email) {
            var EmailRegex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
            return EmailRegex.test(email);
        }
    </script>
</asp:Content>
