<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="Klient.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Katalog</title>
    <link rel="stylesheet" href="Style.css"/>
</head>
<body>
    <div class="header">КАТАЛОГ ГАЛЛЕРЕИ</div>
    <form id="Main" runat="server">
        <div id="Menu">
            <asp:Button ID="roomsView" runat="server" Text="Комнаты" OnClick="roomsView_Click"/>
            <asp:Button ID="exponatsView" runat="server" Text="Экспонаты" OnClick="exponatsView_Click"/>
            <asp:Button ID="migratView" runat="server" Text="Пермещения" OnClick="migratView_Click"/>
            <div>
                <asp:TextBox ID="NameBox" runat="server"></asp:TextBox>
                <asp:DropDownList ID="RoomsBox" runat="server"></asp:DropDownList>
                <asp:DropDownList ID="ExponatsBox" runat="server"></asp:DropDownList>
                                <div>Редактировать</div>
                <asp:Button ID="ButtonAdd" OnClick="ButtonAdd_Click" runat="server" Text="Добавить"/>
                <asp:Button ID="ButtonDel" OnClick="ButtonDel_Click" runat="server" Text="Удалить" />
            </div>
        </div>
        <asp:GridView ID="GridView1" runat="server" CellSpacing="5">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="checker" runat="server"/>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </form>
</body>
</html>
