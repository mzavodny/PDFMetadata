<%@ Page Async="true" Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PDFMetadata.UI._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <section class="row" aria-labelledby="aspnetTitle" style="margin-top: 50px">
        <p>
            <asp:FileUpload ID="fileUpload" runat="server"></asp:FileUpload>
            <asp:Button ID="btnUploadFile" runat="server" Text="Upload" OnClick="btnUploadFile_Click" />
        </p>
        <p>
            <asp:Label ID="lblMessage" runat="server" Font-Bold="true"></asp:Label>
        </p>
    </section>

</asp:Content>
