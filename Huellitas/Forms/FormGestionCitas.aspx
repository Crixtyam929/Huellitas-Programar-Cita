<%@ Page Language="C#" MasterPageFile="Site.Master" AutoEventWireup="true" CodeBehind="FormGestionCitas.aspx.cs" Inherits="Huellitas.Forms.FormGestionCitas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" type="text/css" href="<%: ResolveClientUrl("/Content/FormGestionCitas.css") %>" />
    <div class="contenedor-citas">
        <div class="header-citas">
            <i class="fa fa-calendar"></i> Buscar por fecha:
            <asp:TextBox ID="txtBuscarFecha" runat="server" CssClass="input-fecha" TextMode="Date" />
            <asp:Button ID="btnBuscarFecha" runat="server" Text="Buscar" OnClick="btnBuscarFecha_Click" CssClass="btn btn-buscar" />
            <a href="FormDefault.aspx" class="volver">&#8592;</a>
        </div>

        <asp:Repeater ID="rptCitas" runat="server">
            <ItemTemplate>
                <div class="cita-card">
                    <div class="datos-servicio">
                        <p><strong>Servicio:</strong> <%# Eval("Servicio") %></p>
                        <p><strong>Fecha:</strong> <%# Eval("Fecha", "{0:yyyy-MM-dd}") %></p>
                        <p><strong>Hora:</strong> <%# Eval("Hora", "{0:hh\\:mm}") %></p>
                        <p><strong>Teléfono:</strong> <%# Eval("Telefono") %></p>
                        <p><strong>Email:</strong> <%# Eval("Email") %></p>
                    </div>
                    <div class="nombre-mascota"><%# Eval("NombreMascota") %></div>
                    <div class="acciones">
                        <asp:Button ID="btnConfirmar" runat="server" Text="Confirmar" CssClass="btn btn-confirmar" CommandArgument='<%# Eval("Id") %>' OnClick="btnConfirmar_Click" />
                        <asp:Button ID="btnReagendar" runat="server" Text="Reagendar" CssClass="btn btn-reagendar" PostBackUrl='<%# "~/Forms/FormReagendarCita.aspx?id=" + Eval("Id") %>' />
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-cancelar" PostBackUrl='<%# "~/Forms/FormCancelarCita.aspx?id=" + Eval("Id") %>' />
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>