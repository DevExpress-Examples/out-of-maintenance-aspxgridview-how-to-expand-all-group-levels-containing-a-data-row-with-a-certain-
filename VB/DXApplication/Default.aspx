<%@ Page Language="vb" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="DXApplication.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <dx:ASPxGridView runat="server" ID="Grid" KeyFieldName="ProductID" DataSourceID="DataSource">
        <Columns>
            <dx:GridViewDataTextColumn FieldName="ProductID" ReadOnly="True" VisibleIndex="0">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="ProductName" VisibleIndex="1" >
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="SupplierID" VisibleIndex="2" GroupIndex="1">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="CategoryID" VisibleIndex="3" GroupIndex="0">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="UnitPrice" VisibleIndex="4" >
            </dx:GridViewDataTextColumn>
               <dx:GridViewDataTextColumn FieldName="QuantityPerUnit" VisibleIndex="5" >
            </dx:GridViewDataTextColumn>
        </Columns>
          <SettingsPager Mode="ShowAllRecords" />
        </dx:ASPxGridView>
        <asp:AccessDataSource runat="server" ID="DataSource" DataFile="~/App_Data/Northwind.MDB" SelectCommand="SELECT [ProductID], [ProductName], [SupplierID], [CategoryID], [UnitPrice], [QuantityPerUnit], [UnitsInStock], [UnitsOnOrder], [ReorderLevel], [Discontinued] FROM [Alphabetical list of products]"></asp:AccessDataSource>
    </div>
    </form>
</body>
</html>