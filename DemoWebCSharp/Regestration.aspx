<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Regestration.aspx.cs" Inherits="DemoWebCSharp.Regestration" %>
    

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Registration Form</title>

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />

    <style>
        body { background: linear-gradient(to right, #82a7fff2, #8fcfeb); }
        .card { border-radius: 25px; }
    </style>
</head>

<body>
<form id="form1" runat="server">

<div class="container mt-5">
<div class="row justify-content-center">
<div class="col-md-8">
<div class="card shadow p-4">

<h3 class="text-center mb-4 text-primary">Registration Form</h3>

<asp:ValidationSummary runat="server" CssClass="text-danger mb-3" />

<!-- First Name -->
<div class="mb-3">
<label>First Name</label>
<asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" />
<asp:RequiredFieldValidator runat="server"
    ControlToValidate="txtFirstName"
    ErrorMessage="First Name required"
    CssClass="text-danger" Display="Dynamic" />
<asp:RegularExpressionValidator runat="server"
    ControlToValidate="txtFirstName"
    ValidationExpression="^[a-zA-Z]+$"
    ErrorMessage="Only letters allowed"
    CssClass="text-danger" Display="Dynamic" />
</div>

<!-- Last Name -->
<div class="mb-3">
<label>Last Name</label>
<asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" />
<asp:RequiredFieldValidator runat="server"
    ControlToValidate="txtLastName"
    ErrorMessage="Last Name required"
    CssClass="text-danger" Display="Dynamic" />
<asp:RegularExpressionValidator runat="server"
    ControlToValidate="txtLastName"
    ValidationExpression="^[a-zA-Z]+$"
    ErrorMessage="Only letters allowed"
    CssClass="text-danger" Display="Dynamic" />
</div>

<!-- Contact -->
<div class="mb-3">
    <label>Contact No</label>

    <asp:TextBox ID="txtContact" 
        runat="server" 
        CssClass="form-control" 
        MaxLength="10"
        onkeypress="return isNumber(event);"
        onpaste="return false;" />

    <asp:RequiredFieldValidator runat="server"
        ControlToValidate="txtContact"
        ErrorMessage="Contact required"
        CssClass="text-danger"
        Display="Dynamic" />

    <asp:RegularExpressionValidator runat="server"
        ControlToValidate="txtContact"
        ValidationExpression="^[6-9][0-9]{9}$"
        ErrorMessage="Enter valid 10 digit number"
        CssClass="text-danger"
        Display="Dynamic" />
</div>


<!-- Email -->
<div class="mb-3">
<label>Email</label>
<asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" />
<asp:RequiredFieldValidator runat="server"
    ControlToValidate="txtEmail"
    ErrorMessage="Email required"
    CssClass="text-danger" Display="Dynamic" />
<asp:RegularExpressionValidator runat="server"
    ControlToValidate="txtEmail"
    ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$"
    ErrorMessage="Invalid email format"
    CssClass="text-danger" Display="Dynamic" />
</div>

<!-- DOB -->
<div class="mb-3">
    <label>Date of Birth</label>
    <asp:TextBox ID="txtDOB" 
        runat="server" 
        TextMode="Date" 
        CssClass="form-control"
        onkeydown="return false;" />
        
    <asp:RequiredFieldValidator runat="server"
        ControlToValidate="txtDOB"
        ErrorMessage="DOB required"
        CssClass="text-danger"
        Display="Dynamic" />
</div>


<!-- Gender -->
<div class="mb-3">
<label>Gender</label><br />
<asp:RadioButton ID="rbMale" runat="server" GroupName="Gender" Text="Male" />
<asp:RadioButton ID="rbFemale" runat="server" GroupName="Gender" Text="Female" CssClass="ms-3" />
<asp:CustomValidator runat="server"
    ErrorMessage="Select Gender"
    CssClass="text-danger"
    OnServerValidate="ValidateGender"
    Display="Dynamic" />
</div>

<!-- Password -->
<div class="mb-3">
<label>Password</label>
<asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" />
<asp:RequiredFieldValidator runat="server"
    ControlToValidate="txtPassword"
    ErrorMessage="Password required"
    CssClass="text-danger" Display="Dynamic" />
<asp:RegularExpressionValidator runat="server"
    ControlToValidate="txtPassword"
    ValidationExpression="^(?=.*[A-Z])(?=.*[\W_]).{8,}$"
    ErrorMessage="Min 8 chars, 1 uppercase & 1 special char"
    CssClass="text-danger" Display="Dynamic" />
</div>

<!-- Confirm Password -->
<div class="mb-3">
<label>Confirm Password</label>
<asp:TextBox ID="txtConfirm" runat="server" TextMode="Password" CssClass="form-control" />
<asp:RequiredFieldValidator runat="server"
    ControlToValidate="txtConfirm"
    ErrorMessage="Confirm password required"
    CssClass="text-danger" Display="Dynamic" />
<asp:CompareValidator runat="server"
    ControlToValidate="txtConfirm"
    ControlToCompare="txtPassword"
    ErrorMessage="Passwords do not match"
    CssClass="text-danger" Display="Dynamic" />
</div>

<!-- Vaccinated -->
<div class="mb-3">
<label>Vaccinated</label><br />
<asp:RadioButtonList ID="rblVaccinated" runat="server" CssClass="d-inline" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblVaccinated_SelectedIndexChanged">
    <asp:ListItem>Yes</asp:ListItem>
    <asp:ListItem>No</asp:ListItem>
</asp:RadioButtonList>
</div>

<!-- File Upload -->
<asp:Panel ID="pnlUpload" runat="server" CssClass="mb-3" Visible="false">
    <label>Upload Certificate</label>
    <asp:FileUpload ID="fileCertificate" runat="server" CssClass="form-control" />
</asp:Panel>

<!-- Submit -->
<div class="text-center">
<asp:Button ID="btnSubmit" runat="server"
    Text="Register"
    CssClass="btn btn-primary"
    OnClick="btnSubmit_Click" />
</div>

<asp:Label ID="lblMessage" runat="server" CssClass="text-success mt-3"></asp:Label>

</div>
</div>
</div>

<script runat="server">
protected void btnSubmit_Click(object sender, EventArgs e)
{
    if (Page.IsValid)
        lblMessage.Text = "Registration Successful!";
}

protected void ValidateGender(object source, ServerValidateEventArgs args)
{
    args.IsValid = rbMale.Checked || rbFemale.Checked;
}
</script>



</form>
</body>
</html>
