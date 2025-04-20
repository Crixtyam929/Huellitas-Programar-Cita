<%@ Page Language="C#" MasterPageFile="Site.Master" AutoEventWireup="true" CodeBehind="FormAgendarCita.aspx.cs" Inherits="Huellitas.Forms.FormAgendarCita" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" type="text/css" href="<%: ResolveClientUrl("/Content/FormAgendarCita.css") %>" />
    <div class="contenedor-cita">
        <div class="encabezado-cita">
            <h2>Agendar Cita</h2>
            <a href="FormDefault.aspx" class="volver">&#8592;</a>
        </div>

        <div class="formulario-cita">
            <asp:TextBox ID="txtNombreMascota" runat="server" CssClass="input-text" Placeholder="Nombre de la mascota" />

            <div class="fila">
                <asp:TextBox ID="txtFecha" runat="server" CssClass="input-text" TextMode="Date" />
                <asp:TextBox ID="txtHora" runat="server" CssClass="input-text" TextMode="Time" />
            </div>

            <div class="fila">
                <asp:TextBox ID="txtTelefono" runat="server" CssClass="input-text" Placeholder="Celular" />
                <asp:TextBox ID="txtEmail" runat="server" CssClass="input-text" Placeholder="Email" />
            </div>

            <asp:DropDownList ID="ddlTipoCita" runat="server" CssClass="input-text">
                <asp:ListItem Text="Seleccione tipo de cita" Value="" />
                <asp:ListItem Text="Consulta general" />
                <asp:ListItem Text="Vacunación" />
                <asp:ListItem Text="Control" />
            </asp:DropDownList>

            <asp:TextBox ID="txtComentarios" runat="server" CssClass="input-text-area" TextMode="MultiLine" Rows="5" Placeholder="Comentarios" />
            <asp:Label ID="lblError" runat="server" ForeColor="Red" CssClass="error-label" />
            <asp:Button ID="btnAgendar" runat="server" Text="Agendar" CssClass="boton-agendar" OnClick="btnAgendar_Click" />
        </div>
    </div>
</asp:Content>
