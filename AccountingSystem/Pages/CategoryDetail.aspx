<%@ Page Title="" Language="C#" MasterPageFile="~/NavbarAndSidebar.Master" AutoEventWireup="true" CodeBehind="CategoryDetail.aspx.cs" Inherits="AccountingSystem.Pages.CategoryDetail" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .divArea {
            margin: 0 20px 0 0;
            width: 50px;
            height: 30px;
        }

        .errMsg {
            color: red;
        }

        .inputWidth {
            width: 500px;
        }
    </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="layoutSidenav">
        <div id="layoutSidenav_content" class="m-5">
            <main>

                <form>
                    <h2>流水帳管理</h2>
                    <a href="#">流水帳紀錄</a> <span>>流水帳分類管理</span>

                    <div class="d-flex my-4">
                        <div class="divArea py-2">
                            <label for="inputTittle">標題</label>
                        </div>
                        <div>
                            <input style="display: none;" class="form-control" id="inputSid"
                                name="categorySid" />
                            <input
                                class="form-control inputWidth" id="inputTittle" name="categoryName" />

                        </div>
                        <div class="mt-2">
                            <span class="errMsg" id="categoryNameMsg"></span>
                        </div>
                    </div>

                    <div class="d-flex mb-4">
                        <div class="divArea">
                            <label for="inputMark">備註</label>
                        </div>
                        <div>
                            <textarea class="form-control inputWidth" id="inputMark"
                                name="categoryDescription"></textarea>
                        </div>
                        <div class="mt-2">
                            <span class="errMsg"></span>
                        </div>
                    </div>

                    <div class="mt-5">
                        <div class="btn btn-primary" id="btnSubmit">Save</div>
                        <span></span>
                    </div>


                </form>
            </main>
        </div>

    </div>

    <script>

        const url = new URL(window.location.href)
        const categorySid = url.searchParams.get('CategorySid')

        window.onload = function () {

            if (categorySid == null)
                return

            $.ajax({
                method: "GET",
                url: "../HttpHandlers/CategoryDetailHandler.ashx",
                data: { Action: "GetCategory", CategorySid: categorySid }
            })
                .done(function (data) {

                    $("#inputTittle").val(data['Category_Name'])
                    $("#inputMark").val(data['Description'])
                });
        }

        $("#btnSubmit").click(function () {

            let conSubmit = true;

            let categoryName = $("#inputTittle").val().trim()
            let description = $("#inputMark").val()

            if (categoryName == "") {
                $("#categoryNameMsg").text("請輸入標題")
                conSubmit = false
            } else if (checkCategoryRepetition(categoryName) == false) {
                $("#categoryNameMsg").text("標題重複")
                conSubmit = false
            }

            if (conSubmit) {

                if (categorySid == null) {

                    $.ajax({
                        method: "POST",
                        url: "../HttpHandlers/CategoryDetailHandler.ashx",
                        data: {
                            Action: "InsertCategory",
                            CategoryName: categoryName,
                            Description: description
                        }
                    })
                        .done(function (data) {

                                alert("新增成功")
                                window.location.href = "CategoryDetail.aspx?CategorySid=" + data                           
                        });
                } else {

                    $.ajax({
                        method: "POST",
                        url: "../HttpHandlers/CategoryDetailHandler.ashx",
                        data: {
                            Action: "UpdateCategory",
                            CategoryName: categoryName,
                            Description: description,
                            CategorySid: categorySid
                        }
                    })
                        .done(function (data) {
                            if (data == "success") {
                                alert("修改成功")
                                window.location.href = "CategoryDetail.aspx?CategorySid=" + categorySid
                            }
                        });
                }
            }
        });

        function checkCategoryRepetition(categoryName) {

            var instance;

            $.ajax({
                type: "POST",
                cache: false,
                data: { Action: "CheckCategoryRepetition", CategoryName: categoryName },
                async: false,
                url: "../HttpHandlers/CategoryDetailHandler.ashx",
                success: function (res) {

                    if (res == "success")
                        instance = true;
                    else
                        instance = false;
                }
            });
            return instance;
        }

        //function checkCategoryRepetition(categoryName) {

        //    var result;
        //    $.ajax({
        //        method: "POST",
        //        url: "../HttpHandlers/CategoryDetailHandler.ashx",
        //        data: { Action: "CheckCategoryRepetition", categoryName: categoryName }
        //    })
        //        .done(function (data) {

        //            console.log(data)
        //            if (data == "success") {
        //                result = true
        //            } else {
        //                result = false
        //            }

        //            return data
        //        });

           
        //}
    
    </script>
</asp:Content>
