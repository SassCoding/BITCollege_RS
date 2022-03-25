<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CourseRegistration.aspx.cs" Inherits="BITCollegeSite.CourseRegistration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        <br />
        <asp:Label ID="lblStudentName" runat="server"></asp:Label>
    </p>
    <p>
        <asp:Label ID="lblCourseSelector" runat="server" Text="Course Selector:"></asp:Label>
        <asp:DropDownList ID="ddlCourses" runat="server" AutoPostBack="True">
        </asp:DropDownList>
    </p>
    <p>
        <asp:Label ID="lblRegistrationNotes" runat="server" Text="Registration Notes:"></asp:Label>
        <asp:TextBox ID="txtboxNotes" runat="server"></asp:TextBox>
    </p>
    <p>
        <asp:LinkButton ID="lnkbtnRegister" runat="server" OnClick="lnkbtnRegister_Click">Register</asp:LinkButton>
        <asp:LinkButton ID="lnkbtnReturn" runat="server" OnClick="lnkbtnReturn_Click">Return to Registration Listing</asp:LinkButton>
    </p>
    <p>
        <asp:Label ID="lblError" runat="server" Visible="False"></asp:Label>
    </p>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
</asp:Content>
