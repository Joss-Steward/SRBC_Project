<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="SRBC.aspx.cs" Inherits="WebApplication1.Index" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    
        <span>
        <h1 align="center">SUSQUEHANNA RIVER BASIN COMMISSION</h1>
        <asp:Panel ID="Panel_Err" runat="server">
            <asp:BulletedList ID="lblError" runat="server" Visible="false" Style="color: red"></asp:BulletedList>
        </asp:Panel>
            
        <span style="float: left; width: 25%;">
            <div>
            <asp:Label ID="lblSelected" runat="server" Text="Stations" Font-Size="14pt"></asp:Label>
            <br />
                <div style="border-style: solid; border-width: 1px; overflow: scroll; height: 220px; width: 250px;" >
            &nbsp;&nbsp;<asp:CheckBoxList ID="listboxAvailable" runat="server">
        </asp:CheckBoxList>
                    </div>
            <br />
                </div>
            <div>
            <asp:Label ID="lbl_Timespan" runat="server" Text="Time Span" Font-Size="14pt"></asp:Label>
            <br />
            <div style="border-style: solid; border-width: 1px;  height: 200px; width: 250px;" >
            &nbsp;&nbsp;
                    <asp:Label ID="lblStartDate" runat="server" Text="Start Date"></asp:Label>
            <br />
            <cc1:CalendarExtender ID="calStartDate" runat="server" TargetControlID="txtStartDate">
            </cc1:CalendarExtender>
            &nbsp;&nbsp;<asp:TextBox ID="txtStartDate" runat="server"></asp:TextBox>
                <cc1:CalendarExtender ID="txtStartDate_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtStartDate">
                </cc1:CalendarExtender>
            <br />
            &nbsp;&nbsp;
            <asp:Label ID="lblEndDate" runat="server" Text="End Date"></asp:Label>
            <br />
            &nbsp;&nbsp;<asp:TextBox ID="txtEndDate" runat="server"></asp:TextBox>
            <cc1:CalendarExtender ID="calEndDate" runat="server" TargetControlID="txtEndDate">
            </cc1:CalendarExtender>
                <br />
&nbsp;&nbsp;
                <asp:Label ID="Label1" runat="server" Text="Histogram Time Step"></asp:Label>
            <br />
            &nbsp;
                <asp:TextBox ID="txtbox_timestep" runat="server" Height="16px" Width="106px"></asp:TextBox>
&nbsp;
                <asp:DropDownList ID="ddl_histogramstep" runat="server">
                    <asp:ListItem>Minutes</asp:ListItem>
                    <asp:ListItem>Hours</asp:ListItem>
                    <asp:ListItem>Days</asp:ListItem>
                    <asp:ListItem>Weeks</asp:ListItem>
                    <asp:ListItem>Months</asp:ListItem>
                    <asp:ListItem>Years</asp:ListItem>
                </asp:DropDownList>
                <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Button1" runat="server" Text="GRAPH" OnClick="Button1_Click" />
            <br />
                </div>
                </div>
        </span>
        <span style="float:left; width: 60%; ">
         <div>
             <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="750px" Height="250px" OnActiveTabChanged="TabContainer1_ActiveTabChanged1" AutoPostBack="True" TabStripPlacement="TopRight" ScrollBars="Both">
                <cc1:TabPanel runat="server" HeaderText="Temperature" ID="TabPanel_temp">
                    <HeaderTemplate>
                        Temperature
                    </HeaderTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel runat="server" HeaderText="PH" ID="TabPanel_ph">
                </cc1:TabPanel>
                <cc1:TabPanel ID="TabPanel_SC" runat="server" HeaderText="Specific Conductivity">
                </cc1:TabPanel>
                <cc1:TabPanel ID="TabPanel_tur" runat="server" HeaderText="Turbidity">
                </cc1:TabPanel>
                <cc1:TabPanel ID="TabPanel_do" runat="server" HeaderText="Dissolved Oxygen">
                </cc1:TabPanel>
            </cc1:TabContainer>
         </div>
            <br />
            <br />
            <div>
                <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" Width="750px" Height="250px" BorderWidth="1">
                </asp:Panel>

            </div>
        </span>
            <span style="float:left; width: 15%;">
                
            <asp:Panel ID="Panel2" runat="server" Height="250px" ScrollBars="Vertical">
        </asp:Panel>
                
            </span>
            <br />
            </span>
          <br />
     <br />
        <span>
            <br />
            <br />
            <br />
            <br />
            <br />
            
            
        <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </cc1:ToolkitScriptManager>
            </span>
    
</asp:Content>
