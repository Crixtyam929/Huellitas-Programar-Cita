<%@ Page Language="C#" MasterPageFile="Site.Master" AutoEventWireup="true" CodeBehind="FormCancelarCita.aspx.cs" Inherits="Huellitas.Forms.FormCancelarCita" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" type="text/css" href="<%: ResolveClientUrl("/Content/FormCancelarCita.css") %>" />
    <div class="contenedor-cancelacion">
        <div class="caja-cancelacion">
         
            <h2 class="nombre-mascota">Luna</h2>
            <p><strong>Servicio:</strong> Chequeo</p>
            <p><strong>Fecha:</strong> 2024-01-20</p>
            <p><strong>Hora:</strong> 10:00</p>
            <p class="pregunta-cancelar">¿Seguro que desea cancelar la cita?</p>
            <asp:Button ID="btnCancelarCita" runat="server" Text="Cancelar" CssClass="btn-cancelar" OnClick="btnCancelarCita_Click" />
            <a href="FormGestionCitas.aspx" class="boton-accion no-cancelar">No Cancelar</a>

        </div>
    </div>
</asp:Content>