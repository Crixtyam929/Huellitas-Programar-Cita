<%@ Page Language="C#"  MasterPageFile="Site.Master" AutoEventWireup="true" CodeBehind="FormReagendarCita.aspx.cs" Inherits="Huellitas.Forms.FormReagendarCita" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" type="text/css" href="<%: ResolveClientUrl("/Content/FormReagendarCita.css") %>" />
    <div class="formulario-reagendar">
            <h2>Reagendar Cita para <asp:Label ID="lblNombreMascota" runat="server" Text=""></asp:Label></h2>

            <div class="form-group">
                <label for="ddlServicio">Servicio:</label>
                <asp:DropDownList ID="ddlServicio" runat="server" CssClass="form-control">
                    <asp:ListItem Value="">Seleccionar el servicio</asp:ListItem>
                    <%-- Los ítems del servicio se cargarán desde el CodeBehind --%>
                </asp:DropDownList>
            </div>

            <div class="form-group">
                <label for="txtFecha">Fecha:</label>
                <asp:TextBox ID="txtFecha" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="txtHora">Hora:</label>
                <asp:TextBox ID="txtHora" runat="server" CssClass="form-control" TextMode="Time"></asp:TextBox>
            </div>

            <asp:HiddenField ID="hdnIdCita" runat="server" />
            <asp:Button ID="btnReagendar" runat="server" Text="Reagendar" CssClass="btn-reagendar" OnClick="btnReagendar_Click" />
            <asp:Label ID="lblMensaje" runat="server" Text="" CssClass="mensaje"></asp:Label>
        </div>
</asp:Content>
