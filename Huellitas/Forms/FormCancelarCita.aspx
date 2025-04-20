<%@ Page Language="C#" MasterPageFile="Site.Master" AutoEventWireup="true" CodeBehind="FormCancelarCita.aspx.cs" Inherits="Huellitas.Forms.FormCancelarCita" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" type="text/css" href="<%: ResolveClientUrl("/Content/FormCancelarCita.css") %>" />
    <div class="contenedor-cancelacion">
        <div class="caja-cancelacion">
         
            <h2 class="nombre-mascota"><asp:Literal ID="nombreMascota" runat="server" /></h2>
            <p><strong>Servicio:</strong> <asp:Literal ID="servicio" runat="server" /></p>
            <p><strong>Fecha:</strong> <asp:Literal ID="fecha" runat="server" /></p>
            <p><strong>Hora:</strong> <asp:Literal ID="hora" runat="server" /></p>
            <p class="pregunta-cancelar">¿Seguro que desea cancelar la cita?</p>
            <asp:Button ID="btnCancelarCita" runat="server" Text="Cancelar" CssClass="btn-cancelar" OnClick="btnCancelarCita_Click" />
            <a href="FormGestionCitas.aspx" class="boton-accion no-cancelar">No Cancelar</a>

        </div>
    </div>
</asp:Content>