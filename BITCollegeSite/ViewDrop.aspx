<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewDrop.aspx.cs" Inherits="BITCollegeSite.ViewDrop" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        <br />
        <asp:DetailsView ID="dtvDropReg" runat="server" AutoGenerateRows="False" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2" ForeColor="Black" Height="50px" Width="228px" OnPageIndexChanging="dtvDropReg_PageIndexChanging">
            <EditRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
            <Fields>
                <asp:BoundField DataField="RegistrationId" HeaderText="Registration">
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Student.FullName" HeaderText="Student">
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Course.Title" HeaderText="Course">
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="RegistrationDate" HeaderText="Date">
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Grade" DataFormatString="{0:p}" HeaderText="Grade">
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Something" ShowHeader="False" />
            </Fields>
            <FooterStyle BackColor="#CCCCCC" />
            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
            <RowStyle BackColor="White" />
        </asp:DetailsView>
    </p>
    <p>
        &nbsp;</p>
    <p>
        <asp:LinkButton ID="lnkbtnDropCourse" runat="server" OnClick="lnkbtnDropCourse_Click" Visible="False">Drop Course</asp:LinkButton>
        <asp:LinkButton ID="lnkbtnReturn" runat="server" OnClick="lnkbtnReturn_Click">Return to Registration Listing</asp:LinkButton>
    </p>
    <p>
        &nbsp;</p>
    <p>
        <asp:Label ID="lblError" runat="server"></asp:Label>
    </p>
    <br />
</asp:Content>
