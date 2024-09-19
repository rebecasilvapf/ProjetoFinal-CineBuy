<%@ Page Title="Filmes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Filmes.aspx.cs" Inherits="SistemaReservaIngressos.Filmes" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="/Content/styles.css" rel="stylesheet" type="text/css" />
    <main aria-labelledby="title">
        <div class="filme-title">
            <h2 id="title-filmes" cssclass="tt-filmes">Filmes</h2>
        </div>
        <div class="container-filmes">
            <div class="image-container-filmes">
                <asp:ImageButton ID="btnACaca" runat="server" ImageUrl="~/Images/ACaca.jpg" cssclass="image-button-filmes" OnClick = "btnCompraClick" Height="250px" Width="181px" CommandArgument="4" /><br>
                <asp:Label ID="lblACaca" runat="server" Text="A Caça"></asp:Label>
            </div>
            <div class="image-container-filmes">
                <asp:ImageButton ID="btnOsFantasmas" runat="server" ImageUrl="~/Images/OsFantasmasSeDivertem.jpg" cssclass="image-button-filmes" OnClick = "btnCompraClick" Height="250px" Width="181px" CommandArgument="1" /><br>
                <asp:Label ID="lblOsFantasmas" runat="server" Text="Os Fantasmas se Divertem"></asp:Label>
            </div>
            <div class="image-container-filmes">
                <asp:ImageButton ID="btnEAssimQueAcaba" runat="server" ImageUrl="~/Images/EAssimQueAcaba.jpg" cssclass="image-button-filmes" OnClick = "btnCompraClick" Height="250px" Width="181px" CommandArgument="2" /><br>
                <asp:Label ID="Label1" runat="server" Text="É Assim Que Acaba"></asp:Label>
            </div>
            <div class="image-container-filmes">
                <asp:ImageButton ID="btnDivertidamente2" runat="server" ImageUrl="~/Images/Divertidamente2.jpg" cssclass="image-button-filmes" OnClick = "btnCompraClick" Height="250px" Width="181px" CommandArgument="12" /><br>
                <asp:Label ID="Label2" runat="server" Text="Divertida mente 2"></asp:Label>
            </div>
            <div class="image-container-filmes">
                <asp:ImageButton ID="btnDeadpoolWolverine" runat="server" ImageUrl="~/Images/Deadpool&Wolverine.jpg" cssclass="image-button-filmes" OnClick = "btnCompraClick" Height="250px" Width="181px" CommandArgument="8" /><br>
                <asp:Label ID="Label3" runat="server" Text="Deadpool & Wolverine"></asp:Label>
            </div>
            <div class="image-container-filmes">
                <asp:ImageButton ID="btnMeuMalvadoFavorito4" runat="server" ImageUrl="~/Images/meuMalvadoFavorito4.jpg" cssclass="image-button-filmes" OnClick = "btnCompraClick" Height="250px" Width="181px" CommandArgument="9" /><br>
                <asp:Label ID="Label4" runat="server" Text="Meu Malvado Favorito 4"></asp:Label>
            </div>
            <div class="image-container-filmes">
                <asp:ImageButton ID="btnPetsEmAcao" runat="server" ImageUrl="~/Images/PetsEmAcao.jpg" cssclass="image-button-filmes" OnClick = "btnCompraClick" Height="250px" Width="181px" CommandArgument="3" /><br>
                <asp:Label ID="Label5" runat="server" Text="Pets Em Ação!"></asp:Label>
            </div>
            <div class="image-container-filmes">
                <asp:ImageButton ID="btnBaileDasLoucas" runat="server" ImageUrl="~/Images/BaileDasLoucas.jpg" cssclass="image-button-filmes" OnClick = "btnCompraClick" Height="250px" Width="181px" CommandArgument="11" /><br>
                <asp:Label ID="Label6" runat="server" Text="Baile Das Loucas"></asp:Label>
            </div>
            <div class="image-container-filmes">
                <asp:ImageButton ID="btnEstomago2" runat="server" ImageUrl="~/Images/Estomago2.jpg" cssclass="image-button-filmes" OnClick = "btnCompraClick" Height="250px" Width="181px" CommandArgument="5" /><br>
                <asp:Label ID="Label7" runat="server" Text="Estômago 2"></asp:Label>
            </div>
            <div class="image-container-filmes">
                <asp:ImageButton ID="btnBernadette" runat="server" ImageUrl="~/Images/Bernadette.jpg" cssclass="image-button-filmes" OnClick = "btnCompraClick" Height="250px" Width="181px" CommandArgument="7" /><br>
                <asp:Label ID="Label8" runat="server" Text="Bernadette"></asp:Label>
            </div>
            <div class="image-container-filmes">
                <asp:ImageButton ID="btnPrincesaAdormecida" runat="server" ImageUrl="~/Images/PrincesaAdormecida.jpg" cssclass="image-button-filmes" OnClick = "btnCompraClick" Height="250px" Width="181px" CommandArgument="10" /><br>
                <asp:Label ID="Label9" runat="server" Text="Princesa Adormecida"></asp:Label>
            </div>
            <div class="image-container-filmes">
                <asp:ImageButton ID="btnZuzubalandia" runat="server" ImageUrl="~/Images/Zuzubalandia.jpg" cssclass="image-button-filmes" OnClick = "btnCompraClick" Height="250px" Width="181px" CommandArgument="6" /><br>
                <asp:Label ID="Label10" runat="server" Text="Zuzubalandia O Filme"></asp:Label>
            </div>
        </div>
    </main>
</asp:Content>
