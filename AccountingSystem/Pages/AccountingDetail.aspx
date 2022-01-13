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

                <form>
                    <div class="container-fluid">

                        <div class="row mb-4">
                            <div class="col-1 p-0">收 / 支</div>
                            <div class="col-3 p-0">
                                <select name="userAccountLevel" id="userAccountLevel">
                                    <option value="0">in</option>
                                    <option value="1">out</option>
                                </select>
                            </div>
                        </div>

                        <div class="row mb-4">
                            <div class="col-1 p-0">等級</div>
                            <div class="col-3 p-0">
                                <select name="userAccountLevel" id="accountingCategory">
                                    <option value="0">分類1</option>
                                    <option value="1">分類2</option>
                                </select>
                            </div>
                        </div>

                        <div class="row mb-4">
                            <div class="col-1 p-0 pt-2">
                                <label for="price">金額</label>
                            </div>
                            <div class="col-3 p-0 d-flex">
                                <input class="form-control inputStyle" id="price"
                                    name="userName">
                                <span class="col-4 p-0 mt-2 spanMsg" id="priceSpan"></span>
                            </div>
                        </div>

                        <div class="row mb-4">
                            <div class="col-1 p-0 pt-2">
                                <label for="tittle">標題</label>
                            </div>
                            <div class="col-3 p-0 d-flex">
                                <input class="form-control inputStyle" id="tittle"
                                    name="userName">
                                <span class="col-4 p-0 mt-2 spanMsg" id="tittleSpan"></span>
                            </div>
                        </div>

                        <div class="row mb-4">
                            <div class="divArea col-1 p-0 pt-2">
                                <label for="inputMark">備註</label>
                            </div>
                            <div class="col-3 p-0 d-flex">
                                <textarea class="form-control inputWidth" id="inputMark"
                                    name="categoryDescription"></textarea>
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
</asp:Content>
