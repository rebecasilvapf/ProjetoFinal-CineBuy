<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ConfirmacaoReserva.aspx.cs" Inherits="SistemaReservaIngressos.Views.DetalhesReserva" Async="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <link href="/Content/styles.css" rel="stylesheet" type="text/css" />
     <asp:Panel ID="panelDetalhesReserva" runat="server" CssClass="container-detalhes-reserva">
     <div class="detalhes-titulo">
         <asp:Label ID="lblDetalhesTitulo" runat="server" Text="Reserva Confirmada!" CssClass="detalhes-reserva-titulo"></asp:Label>
     </div>
     <div class="detalhes-reserva">
         <div class ="reserva">
         <asp:Label ID="lblConcluido" runat="server"></asp:Label><br />
         </div>
         <div class ="reserva">
         <asp:Label ID="lblFilmeReserva" runat="server"></asp:Label><br />
         </div>
         <div class ="reserva">
         <asp:Label ID="lblNomeReserva" runat="server"></asp:Label><br />
         </div>
         <div class ="reserva">
         <asp:Label ID="lblPrecoReserva" runat="server"></asp:Label><br />
         </div>
         <div class ="reserva">
         <asp:Label ID="lblAssentoReserva" runat="server"></asp:Label><br />
         </div>
         <div class ="reserva">
         <asp:Label ID="lblHorarioDiaReserva" runat="server"></asp:Label>
         </div>
         <div class ="reserva">
         <asp:Label ID="lblSalaReserva" runat="server"></asp:Label>
         </div>
         <asp:Button ID="btnMenuInicial" runat="server" Text="Menu Inicial" CssClass="btn-menu-inicial" OnClick="btnMenuInicial_Click"/><asp:Button ID="btnCancelar" runat="server" Text="Cancelar Reserva" OnClick="btnCancelar_Click" CssClass="btn-cancelar-reserva" />
     </div>           
 </asp:Panel>
     <asp:Label ID="lblMensagem" runat="server" CssClass="mensagem-erro-cancelar"></asp:Label>
</asp:Content>
 
