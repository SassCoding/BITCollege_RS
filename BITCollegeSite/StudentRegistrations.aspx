<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StudentRegistrations.aspx.cs" Inherits="BITCollegeSite.StudentRegistrations" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        <br />
        <asp:Label ID="lblStudentName" runat="server"></asp:Label>
    </p>
    <p>
        <asp:GridView ID="dgvCourses" runat="server" AutoGenerateColumns="False" AutoGenerateSelectButton="True" OnSelectedIndexChanged="dgvCourses_SelectedIndexChanged">
            <Columns>
                <asp:BoundField DataField="Course.CourseNumber" HeaderText="Course" />
                <asp:BoundField DataField="Course.Title" HeaderText="Title" />
                <asp:BoundField DataField="Course.CourseType" HeaderText="Course Type" />
                <asp:BoundField DataField="Course.TuitionAmount" DataFormatString="{0:c}" HeaderText="Tuition">
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="Grade" DataFormatString="{0:p}" HeaderText="Grade">
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
    </p>
    <p>
        <asp:Label ID="lblViewDrop" runat="server" Text="Click the Select Link beside a registration (above) to View or Drop the course"></asp:Label>
    </p>
    <p>
        <asp:LinkButton ID="lnkbtnRegister" runat="server" OnClick="lnkbtnRegister_Click">Click Here to Register for a Course link.</asp:LinkButton>
    </p>
    <p>
        <asp:Label ID="lblError" runat="server" Visible="False"></asp:Label>
    </p>
    <p>
    </p>
    <p>
    </p>
</asp:Content>
