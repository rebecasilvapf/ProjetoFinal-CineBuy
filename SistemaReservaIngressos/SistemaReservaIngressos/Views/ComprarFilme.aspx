<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ComprarFilme.aspx.cs" Inherits="SistemaReservaIngressos.Views.ComprarFilme" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="/Content/styles.css" rel="stylesheet" type="text/css" />
    <div class="container-filme">
        <div class="imagem-filme">
            <asp:Image ID="imageUrl" runat="server" Height="272px" Width="208px" />
        </div>
        <div class="info-filme">
            <h1>
                <asp:Label ID="lblTitulo" runat="server" Text="Título do Filme"></asp:Label>
            </h1>
            <div class="info-span">
                <span class="classificacao">
                    <asp:Label ID="lblClassificacao" runat="server"></asp:Label></span>
                <span>
                    <asp:Label ID="traco" runat="server" Text=" | "></asp:Label></span>
                <span>
                    <asp:Label ID="lblDuracao" runat="server"></asp:Label></span>
                <span>
                    <asp:Label ID="traco2" runat="server" Text=" | "></asp:Label></span>
                <span>
                    <asp:Label ID="lblGenero" runat="server"></asp:Label></span>
                <span><asp:Label ID="lbltraco3" runat="server" Text=" | "></asp:Label></span>
                <span><asp:Label ID="lblPreco" runat="server"></asp:Label></span>      
                 <span><asp:Label ID="traco4" runat="server" Text=" | "></asp:Label></span>
                 <span><asp:Label ID="lblSala" runat="server"></asp:Label></span> 
            </div>
                <asp:Label ID="lblSinopse" runat="server" Text="Sinopse" CssClass="sinopse-filme"></asp:Label>
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
            <asp:Label ID="lblHorario" CssClass="horario-label" runat="server" Text="Data e Horário: " ></asp:Label>
            <asp:DropDownList ID="DropDownListHorarios" runat="server" CssClass="dropdown-horario" AutoPostBack="True" OnSelectedIndexChanged="ddlHorarios_SelectedIndexChanged"></asp:DropDownList>         
        </div>      
        <asp:Panel ID="panelAssentos" runat="server" CssClass="reserva-assentos" Visible="false">
        <div class="reserva-assentos">
            <h4>
                <asp:Label ID="lblAssento" runat="server" Text="Selecionar Assento" CssClass="title-assentos"></asp:Label></h4>   
            <div class="assentos">
                <div class="checkbox-container">
                    <asp:CheckBox ID="a1" runat="server" Text="A1" />
                </div>
                <div class="checkbox-container">
                    <asp:CheckBox ID="a2" runat="server" Text="A2" />
                </div>
                <div class="checkbox-container">
                    <asp:CheckBox ID="a3" runat="server" Text="A3" />
                </div>
                <div class="checkbox-container">
                    <asp:CheckBox ID="a4" runat="server" Text="A4" />
                </div>
                <div class="checkbox-container">
                    <asp:CheckBox ID="a5" runat="server" Text="A5" />
                </div>
                <div class="checkbox-container">
                    <asp:CheckBox ID="a6" runat="server" Text="A6" />
                </div>
            </div>
            <div class="assentos">
                <div class="checkbox-container">
                    <asp:CheckBox ID="b1" runat="server" Text="B1" />
                </div>
                <div class="checkbox-container">
                    <asp:CheckBox ID="b2" runat="server" Text="B2" />
                </div>
                <div class="checkbox-container">
                    <asp:CheckBox ID="b3" runat="server" Text="B3" />
                </div>
                <div class="checkbox-container">
                    <asp:CheckBox ID="b4" runat="server" Text="B4" />
                </div>
                <div class="checkbox-container">
                    <asp:CheckBox ID="b5" runat="server" Text="B5" />
                </div>
                <div class="checkbox-container">
                    <asp:CheckBox ID="b6" runat="server" Text="B6" />
                </div>
            </div>
            <div class="assentos">
                <div class="checkbox-container">
                    <asp:CheckBox ID="c1" runat="server" Text="C1" />
                </div>
                <div class="checkbox-container">
                    <asp:CheckBox ID="c2" runat="server" Text="C2" />
                </div>
                <div class="checkbox-container">
                    <asp:CheckBox ID="c3" runat="server" Text="C3" />
                </div>
                <div class="checkbox-container">
                    <asp:CheckBox ID="c4" runat="server" Text="C4" />
                </div>
                <div class="checkbox-container">
                    <asp:CheckBox ID="c5" runat="server" Text="C5" />
                </div>
                <div class="checkbox-container">
                    <asp:CheckBox ID="c6" runat="server" Text="C6" />
                </div>
            </div>
            </div>
           </asp:Panel>
            <div class="container-btn">
                <asp:Button ID="btnFinalizarCompra" CssClass="btn-finalizar-compra" runat="server" Text="Finalizar Reserva" OnClick="BtnFinalizarCompra_Click" />
            </div>         
         <asp:Panel ID="mostrarErros" runat="server" CssClass="container-mostrar-erros" Visible="true">
        <div class="mostrar-erros">
              <asp:Label ID="lblDetalhesReserva" runat="server" Text="" CssClass="mensagem-erro"></asp:Label>
        </div>
     </asp:Panel>
    </div>
</asp:Content>
