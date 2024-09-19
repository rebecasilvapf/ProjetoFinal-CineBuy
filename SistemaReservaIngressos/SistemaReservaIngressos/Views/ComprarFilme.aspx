<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ComprarFilme.aspx.cs" Inherits="SistemaReservaIngressos.Views.ComprarFilme" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="/Content/styles.css" rel="stylesheet" type="text/css" />
    <div class="container-filme">
        <div class="imagem-filme">
            <asp:Image ID="imageUrl" runat="server" Height="239px" Width="197px" />
        </div>
        <div class="info-filme">
            <h1>
                <asp:Label ID="lblTitulo" runat="server" Text="Título do Filme"></asp:Label>
            </h1>
            <div class="info-span">
                <span class="classificacao">
                    <asp:Label ID="lblClassificacao" runat="server" Text="14"></asp:Label></span>
                <span>
                    <asp:Label ID="traco" runat="server" Text=" | "></asp:Label></span>
                <span>
                    <asp:Label ID="lblDuracao" runat="server" Text="60m"></asp:Label></span>
                <span>
                    <asp:Label ID="traco2" runat="server" Text=" | "></asp:Label></span>
                <span>
                    <asp:Label ID="lblGenero" runat="server" Text=" Animação"></asp:Label></span>
                <span>
                    <asp:Label ID="lbltraco3" runat="server" Text=" | "></asp:Label></span>
                <span>
                    <asp:Label ID="lblPreco" runat="server" Text="Preço"></asp:Label></span>
            </div>
            <h3 class="sinopse">
                <asp:Label ID="lblSinopse" runat="server" Text="Sinopse"></asp:Label></h3>
        </div>
    </div>
    <div class="reserva-nome">
        <div class="nome-cliente">
            Digite seu nome:
            <asp:TextBox ID="txtNome" runat="server" CssClass="txtbox-nome"></asp:TextBox>
        </div>
    </div>
    <div class="container-reserva">
        <div class="reserva-horario">
            <asp:Label ID="lblHorario" CssClass="horario-label" runat="server" Text="Selecionar Horário: "></asp:Label>
            <asp:DropDownList ID="DropDownListHorarios" runat="server"></asp:DropDownList>         
        </div>
         <div class="reserva-sala">
             <asp:Label ID="lblSala" CssClass="sala-label" runat="server" Text="Selecionar Sala: "></asp:Label>
             <asp:DropDownList ID="DropDownListSalas" runat="server"></asp:DropDownList>
        </div>
        <div class="reserva-assentos">
            <h4>
                <asp:Label ID="lblAssento" runat="server" Text="Selecionar Assento"></asp:Label></h4>
            <div class="assentos">
                <asp:CheckBox ID="a1" runat="server" Text="A1" />
                <asp:CheckBox ID="a3" runat="server" Text="A2" />
                <asp:CheckBox ID="a4" runat="server" Text="A3" />
                <asp:CheckBox ID="a5" runat="server" Text="A4" />
                <asp:CheckBox ID="a6" runat="server" Text="A5" />
                <asp:CheckBox ID="a7" runat="server" Text="A6" />
            </div>
            <div class="assentos">
                <asp:CheckBox ID="b1" runat="server" Text="B1" />
                <asp:CheckBox ID="b2" runat="server" Text="B2" />
                <asp:CheckBox ID="b3" runat="server" Text="B3" />
                <asp:CheckBox ID="b4" runat="server" Text="B4" />
                <asp:CheckBox ID="b5" runat="server" Text="B5" />
                <asp:CheckBox ID="b6" runat="server" Text="B6" />
            </div>
            <div class="assentos">
                <asp:CheckBox ID="c1" runat="server" Text="C1" />
                <asp:CheckBox ID="c2" runat="server" Text="C2" />
                <asp:CheckBox ID="c3" runat="server" Text="C3" />
                <asp:CheckBox ID="c4" runat="server" Text="C4" />
                <asp:CheckBox ID="c5" runat="server" Text="C5" />
                <asp:CheckBox ID="c6" runat="server" Text="C6" />
            </div>
            <div class="container-btn">
                <asp:Button ID="btnFinalizarCompra" CssClass="btn-finalizar-compra" runat="server" Text="Finalizar Compra" OnClick="btnFinalizarCompra_Click" />
            </div>
        </div>
        <div class="container-detalhes-reserva">
            <div>
                <asp:Label ID="lblDetalhesTitulo" runat="server" Text="Detalhes da Reserva" CssClass="detalhes-reserva-titulo"></asp:Label>
            </div>
            <div>
                <asp:Label ID="lblDetalhesReserva" runat="server" Text="Teste123" CssClass="detalhes-reserva" Visible="false"></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>
