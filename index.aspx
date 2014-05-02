<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WebApplication1.Graphs" %>

<!DOCTYPE html>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1 align="center">GRAPHS</h1>
            <asp:BulletedList ID="lblError" runat="server" Visible="false" Style="color: red"></asp:BulletedList>
            <br />
                        <asp:Label ID="lblSelected" runat="server" Text="SelectedStations"></asp:Label><br />
                        <asp:ListBox ID="listboxSelected" runat="server" SelectionMode="Multiple" Height="75px" Width="115px" Rows="10"></asp:ListBox>
            <br />
            <br />
                        <asp:Label ID="lblSelectedError" runat="server" ForeColor="Red"></asp:Label>
                        <br />
                        <asp:ImageButton ID="btn_Add" runat="server" ImageUrl="~/Images/addtoselection.png" OnClick="btn_Add_Click" />
                        &nbsp&nbsp
                        <asp:ImageButton ID="btn_Remove" runat="server" ImageUrl="~/Images/removefromselection.png" OnClick="btn_Remove_Click" />

                        <asp:Label ID="lblStartDate" runat="server" Text="Start Date" style="z-index: 1; left: 255px; top: 122px; position: absolute"></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="txtStartDate" runat="server" style="z-index: 1; left: 255px; top: 153px; position: absolute"></asp:TextBox>
                        <cc1:CalendarExtender ID="calStartDate" runat="server" TargetControlID="txtStartDate"></cc1:CalendarExtender>
                        <br />
                        <asp:TextBox ID="txtEndDate" runat="server" style="position: absolute; top: 216px; left: 254px; z-index: 1; margin-top: 0px"></asp:TextBox>
                        <cc1:CalendarExtender ID="calEndDate" runat="server" TargetControlID="txtEndDate"></cc1:CalendarExtender>
        
                        <asp:Label ID="lblAvailable" runat="server" Text="AvailableStations" style="z-index: 1; left: 12px; top: 280px; position: absolute"></asp:Label>
                        <br />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lblEndDate" runat="server" Text="End Date" style="position: absolute; text-align: left; z-index: 1; left: 255px; top: 186px"></asp:Label>
        
                        <asp:Label ID="lblParameters" runat="server" Text="Parameters" style="z-index: 1; left: 256px; top: 273px; position: absolute"></asp:Label>
                        <asp:CheckBoxList ID="CheckBoxList2" runat="server" style="position: relative; top: -23px; left: 244px; height: 25px; margin-top: 0px;">
                            <asp:ListItem>Temperature</asp:ListItem>
                            <asp:ListItem>PH</asp:ListItem>
                            <asp:ListItem Value="SpecificConductivity">Specific Conductivity</asp:ListItem>
                            <asp:ListItem>Turbidity</asp:ListItem>
                            <asp:ListItem Value="DisolvedOxygen">Disolved Oxygen</asp:ListItem>
            </asp:CheckBoxList>
                        <br />
            
            <br />
            <br />
            <asp:Table ID="tbl_Summary" runat="server" BorderStyle="Dashed" GridLines="Both" Height="20px" HorizontalAlign="Justify" Width="485px">
                <asp:TableHeaderRow>
                    <asp:TableHeaderCell HorizontalAlign="Center" Wrap="False"> Parameter </asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" Wrap="False"> Station ID </asp:TableHeaderCell>
                    <asp:TableHeaderCell Wrap="False" HorizontalAlign="Center"> Minimum </asp:TableHeaderCell>
                    <asp:TableHeaderCell Wrap="False" HorizontalAlign="Center"> Date of Minimum </asp:TableHeaderCell>
                    <asp:TableHeaderCell Wrap="False" HorizontalAlign="Center"> Maximum </asp:TableHeaderCell>
                    <asp:TableHeaderCell Wrap="False" HorizontalAlign="Center"> Date of Maximum </asp:TableHeaderCell>
                    <asp:TableHeaderCell Wrap="False" HorizontalAlign="Center"> Mean </asp:TableHeaderCell>
                    <asp:TableHeaderCell Wrap="False" HorizontalAlign="Center"> Median </asp:TableHeaderCell>
                    <asp:TableHeaderCell Wrap="False" HorizontalAlign="Center"> Standard Deviation </asp:TableHeaderCell>
                    <asp:TableHeaderCell Wrap="False" HorizontalAlign="Center"> Q1 </asp:TableHeaderCell>
                    <asp:TableHeaderCell Wrap="False" HorizontalAlign="Center"> Q2 </asp:TableHeaderCell>
                    <asp:TableHeaderCell Wrap="False" HorizontalAlign="Center"> Q3 </asp:TableHeaderCell>
                    <asp:TableHeaderCell Wrap="False" HorizontalAlign="Center"> Q4 </asp:TableHeaderCell>
                </asp:TableHeaderRow>
            </asp:Table>
                        <asp:ListBox ID="listboxAvailable" runat="server" SelectionMode="Multiple" style="z-index: 1; left: 11px; top: 316px; position: absolute; height: 130px; width: 137px; margin-top: 1px;" Rows="10"></asp:ListBox>
     
         
                        <br />
                        <br />
            <asp:Panel ID="Panel1" runat="server">
            </asp:Panel>
                        <br />
                        <br />
         
                        <asp:ImageButton ID="btn_shwGraph" runat="server" ImageUrl="~/Images/graph.png" OnClick="ShowGraph_Click" style="z-index: 1; left: 247px; top: 450px; position: absolute" />
        </div>
            <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
            </cc1:ToolkitScriptManager>
    </form>
</body>
</html>
