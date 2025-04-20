<%@ Page Language="C#"  MasterPageFile="Site.Master" AutoEventWireup="true" CodeBehind="FormReagendarCita.aspx.cs" Inherits="Huellitas.Forms.FormReagendarCita" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" type="text/css" href="<%: ResolveClientUrl("/Content/FormReagendarCita.css") %>" />
    <div class="form-reagendar-cita">
        <div class="reagendar-cita-box">
            <h2><asp:Label ID="lblNombreMascota" runat="server" Text="Luna"></asp:Label></h2>

            <label>Servicio:</label>
            <asp:DropDownList ID="ddlServicios" runat="server" CssClass="select-servicio"></asp:DropDownList>

            <div class="input-fecha-hora">
                <div>
                    <label>Fecha:</label>
                    <asp:TextBox ID="txtFecha" runat="server" TextMode="Date" CssClass="input-field"></asp:TextBox>
                </div>
                <div>
                    <label>Hora:</label>
                    <asp:TextBox ID="txtHora" runat="server" TextMode="Time" CssClass="input-field"></asp:TextBox>
                </div>
            </div>

            <asp:Button ID="btnReagendar" runat="server" Text="Reagendar" CssClass="btn-reagendar" OnClick="btnReagendar_Click" />
        </div>
    </div>
</asp:Content>
