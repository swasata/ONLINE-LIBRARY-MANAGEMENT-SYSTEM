<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="adminlogin.aspx.cs" Inherits="ElibraryManagement.adminlogin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/userlogin.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">
       <div class="container pb-5 pt-5">
      <div class="row">
         <div class="col-md-6 mx-auto">
            <div class="card adminlogin-bg ">
               <div class="card-body">
                  <div class="row">
                     <div class="col">
                        <div class="text-center">
                           <img class="adminlogin-img" width="150px" src="imgs/adminuser.png"/>
                        </div>
                     </div>
                  </div>
                  <div class="row">
                     <div class="col">
                        <div class="text-center">
                           <h3 class="text-white">Admin Login</h3>
                        </div>
                     </div>
                  </div>
                  <div class="row">
                     <div class="col">
                        <hr>
                     </div>
                  </div>
                  <div class="row">
                     <div class="col">
                        <label class="text-white">Admin ID</label>
                         <span class="text-danger">*</span>
                        <div class="form-group">
                           <asp:TextBox CssClass="form-control" ID="TextBox1" runat="server" required="required" placeholder="Admin ID"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red" ErrorMessage="Please Enter Your User ID" ControlToValidate="TextBox1"></asp:RequiredFieldValidator>--%>

                        </div>
                        <label class="text-white">Password</label>
                         <span class="text-danger">*</span>
                        <div class="form-group">
                           <asp:TextBox CssClass="form-control" ID="TextBox2" runat="server" required="required" placeholder="Password" TextMode="Password"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ForeColor="red" ErrorMessage="Please Enter Password" ControlToValidate="TextBox2"></asp:RequiredFieldValidator>--%>

                        </div>
                         <br />
                        <div class="form-group">
                           <asp:Button class="btn btn-success d-grid gap-2 col-6 mx-auto pb-2" ID="Button1" runat="server" Text="Login" OnClick="Button1_Click" />
                        </div>
                     </div>
                  </div>
               </div>
          
            <a class="px-3" href="homepage.aspx"><< Back to Home</a><br><br>
                  </div>
         </div>
      </div>
   </div>



    </div>

</asp:Content>
