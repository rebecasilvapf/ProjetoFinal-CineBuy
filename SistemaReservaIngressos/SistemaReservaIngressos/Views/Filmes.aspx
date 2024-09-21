<%@ Page Title="Filmes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Filmes.aspx.cs" Inherits="SistemaReservaIngressos.Filmes" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="/Content/styles.css" rel="stylesheet" type="text/css" />
    <main aria-labelledby="title">
        <div class="filme-title">
            <h2 id="title-filmes" Cssclass="tt-filmes">Filmes</h2>
        </div>
         <asp:Panel runat="server" ID="filmesContainer" CssClass="container-filmes">  
         </asp:Panel>
    </main>
</asp:Content>
