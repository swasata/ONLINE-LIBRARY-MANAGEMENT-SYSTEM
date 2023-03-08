<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="adminbookissuing.aspx.cs" Inherits="ElibraryManagement.adminbookissuing" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



    <div class="container">
      <div class="row ">
         <div class="col-md-12 pt-5 px-5 pb-5 ">
            <div class="card bg-black  ">
               <div class="card-body">
                  <div class="row">
                     <div class="col">
                        <div class="text-center">
                           <h4 class="text-white text-decoration-underline">Book Issuing</h4>
                        </div>
                     </div>
                  </div>
                  <div class="row">
                     <div class="col">
                        <div class="text-center">
                           <img width="100px" src="imgs/books.png" />
                        </div>
                     </div>
                  </div>
                   <br />

                     <div class="text-center text-primary">
                      ..... The Member ID AND The Book ID Should Proper While Issuing Or Returning A Book From A User ..... 
                  </div>

                  <div class="text-center text-danger">
                      ..... While Issuing The Book We Have To Give Proper Dates ..... 
                       </div>
                  
                       <div class="text-center text-info">
                      ..... While Returning The Book We Can Give Any Dates ..... 
                  </div>
                  <div class="row">
                     <div class="col">
                        <hr>
                     </div>
                  </div>
                  <div class="row">
                     <div class="col-md-6">
                        <label class="text-white">Member ID</label>
                           <span  class =" text-danger">*</span>
                        <div class="form-group">
                           <asp:TextBox CssClass="form-control" ID="TextBox2" runat="server" required="required" placeholder="Member ID"></asp:TextBox>
                        </div>
                     </div>
                     <div class="col-md-6">
                        <label class="text-white">Book ID</label>
                           <span  class =" text-danger">*</span>
                        <div class="form-group">
                           <div class="input-group">
                              <asp:TextBox CssClass="form-control" ID="TextBox1" runat="server" required="required" placeholder="Book ID"></asp:TextBox>
                              <asp:Button class="btn btn-primary" ID="Button1" runat="server" Text="Go" OnClick="Button1_Click" />
                           </div>
                        </div>
                     </div>
                  </div>
                  <div class="row">
                     <div class="col-md-6">
                        <label class="text-white">Member Name</label>
                         <span class="text-danger">*</span>
                        <div class="form-group">
                           <asp:TextBox CssClass="form-control" ID="TextBox3" runat="server" placeholder="Member Name" ReadOnly="True" BackColor="#CCCCCC"></asp:TextBox>
                        </div>
                     </div>
                     <div class="col-md-6">
                        <label class="text-white">Book Name</label>
                         <span class="text-danger">*</span>
                        <div class="form-group">
                           <asp:TextBox CssClass="form-control" ID="TextBox4" runat="server"  placeholder="Book Name" ReadOnly="True" BackColor="#CCCCCC"></asp:TextBox>
                        </div>
                     </div>
                  </div>
                  <div class="row">
                     <div class="col-md-6">
                        <label class="text-white">Start Date</label>
                         <span  class =" text-danger">*</span>
                        <div class="form-group">
                           <asp:TextBox CssClass="form-control" ID="TextBox5" runat="server" required="required" placeholder="Start Date" TextMode="Date"></asp:TextBox>
                        </div>
                     </div>
                     <div class="col-md-6">
                        <label class="text-white">End Date</label>
                         <span  class =" text-danger">*</span>
                        <div class="form-group">
                           <asp:TextBox CssClass="form-control" ID="TextBox6" runat="server" required="required" placeholder="End Date" TextMode="Date"></asp:TextBox>
                        </div>
                     </div>
                  </div>
                  <div class="row">
                     <div class="col-6 pt-3">
                        <asp:Button ID="Button2" class="btn d-grid gap-2 col-6 mx-auto btn-primary" runat="server" Text="Issue" OnClick="Button2_Click" />
                     </div>
                     <div class="col-6 pt-3">
                        <asp:Button ID="Button4" class="btn d-grid gap-2 col-6 mx-auto btn-success" runat="server" Text="Return" OnClick="Button4_Click" />
                     </div>
                  </div>
               </div>
            
            <a href="homepage.aspx"><< Back to Home</a><br>
            <br>
                </div>
         </div>
          </div>



         <div class="container-fluid">
      <div class="row">
         <div class="col-md-12 px-5 pt-5 pb-5">
            <div class="card rounded-2">
               <div class="card-body">
                  <div class="row">
                     <div class="col">
                        <div class="text-center">
                           <h4 class="">Issued Book List</h4>
                        </div>
                     </div>
                  </div>
                  <div class="row">
                     <div class="col">
                        <hr>
                     </div>
                  </div>
                  <div class="row">
                      <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:elibraryDBConnectionString %>" SelectCommand="SELECT * FROM [book_issue_tbl]"></asp:SqlDataSource>

                     <div class="col">
                        <asp:GridView class="table table-striped table-bordered" ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" OnRowDataBound="GridView1_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="member_id" HeaderText="Member ID" SortExpression="member_id" />
                                <asp:BoundField DataField="member_name" HeaderText="Member Name" SortExpression="member_name" />
                                <asp:BoundField DataField="book_id" HeaderText="Book ID" SortExpression="book_id" />
                                <asp:BoundField DataField="book_name" HeaderText="Book Name" SortExpression="book_name" />
                                <asp:BoundField DataField="issu_date" HeaderText="Issue Date" SortExpression="issu_date" />
                                <asp:BoundField DataField="due_date" HeaderText="Due Date" SortExpression="due_date" />
                            </Columns>
                         </asp:GridView>
                     </div>
                  </div>
               </div>
            </div>
         </div>
      </div>
   </div>

</div>




</asp:Content>
