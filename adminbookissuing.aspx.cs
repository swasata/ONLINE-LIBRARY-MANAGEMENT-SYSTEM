using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ElibraryManagement
{
    public partial class adminbookissuing : System.Web.UI.Page
    {

        SqlConnection con = new SqlConnection();
        protected void Page_Load(object sender, EventArgs e)
        {

        //--->> web.config
#pragma warning disable CS0164 // This label has not been referenced
        ValidationSettings: UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
#pragma warning restore CS0164 // This label has not been referenced


            try
            {
                if (Session["username"].ToString() == "" || Session["username"] == null)
                {
                    Response.Redirect("adminlogin.aspx");

                }

            }
            catch
            {
                //Response.Write("<script language = javascript> alert(' " + ex + " ')</script>");
                Response.Write("<script language = javascript> alert(' " + " " + " ')</script>");
                Response.Redirect("adminlogin.aspx");
            }

            GridView1.DataBind();
        }


        //issue Book click event
        protected void Button2_Click(object sender, EventArgs e)
        {
           
            if (checkIfBookExist() && checkIfMemberExist())
            {

                if (checkIfIssueEntryExist())
                {
                    Response.Write("<script>alert('This Member already has this book');</script>");
                }
                else
                {
                    if (TextBox3.Text.Length == 0 && TextBox4.Text.Length == 0 )
                    {
                        Response.Write("<script language = javascript> alert('book or member name cannot be empty . please click on go button')</script>");

                        //if (TextBox5.Text.Length == 0 && TextBox6.Text.Length == 0)
                        //{
                        //    Response.Write("<script language = javascript> alert('book or member name cannot be empty . please click on go button')</script>");

                        //}
                    }
                  

                    else
                    {
                        issueBook();
                        TextBox1.Text = "";
                        TextBox2.Text = "";
                        TextBox3.Text = "";
                        TextBox4.Text = "";
                        TextBox5.Text = "";
                        TextBox6.Text = "";
                    }
                    

                }

            }
            else
            {
                Response.Write("<script language = javascript> alert(' " + "Invalid Member or Book Details." + " ')</script>");

            }


        }


        //return book click event
        protected void Button4_Click(object sender, EventArgs e)
        {

            if (checkIfBookExist() && checkIfMemberExist())
            {

                if (checkIfIssueEntryExist())
                {
                    returnBook();
                    TextBox1.Text = "";
                    TextBox2.Text = "";
                    TextBox3.Text = "";
                    TextBox4.Text = "";
                    TextBox5.Text = "";
                    TextBox6.Text = "";
                }
                else
                {

                    Response.Write("<script language = javascript> alert(' " + "Invalid Entry . Doesnot exists ." + " ')</script>");
                    
                }

            }
            else
            {
                Response.Write("<script language = javascript> alert(' " + "Invalid Member or Book Details." + " ')</script>");

            }


        }



        //go button click event
        protected void Button1_Click(object sender, EventArgs e)
        {
            getNames();
        }


        //user defined function


        //return book
        void returnBook()
        {
            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["first"].ToString();
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "Delete from book_issue_tbl where book_id='" + TextBox1.Text.Trim() + "' AND member_id='" + TextBox2.Text.Trim() + "'";
                
                int result = cmd.ExecuteNonQuery();  //holdes the number of rows effected ---- if one row got deleted we get result as = 1

                //GridView1.DataBind();    //not need now
               
                // Response.Write("<script>alert('"+result+"');</script>");


                if (result > 0)
                {
                    
                    cmd = con.CreateCommand();
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "update book_master_tbl set current_stock = current_stock+1 where book_id='" + TextBox1.Text.Trim() + "'";
                    cmd.ExecuteNonQuery();
                    
                    Response.Write("<script>alert('Book Returned Successfully');</script>");
                    GridView1.DataBind();

                    con.Close();

                }
                else
                {
                    Response.Write("<script>alert('Error - Invalid details');</script>");
                }

            }

            
            catch(Exception ex)
            {
                Response.Write("<script language = javascript> alert(' " + ex.Message + " ')</script>");

            }
        }



        //check if issue entry exists
        bool checkIfIssueEntryExist()
        {
            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["first"].ToString();
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "select * from book_issue_tbl where member_id='" + TextBox2.Text.Trim() + "' AND book_id='" + TextBox1.Text.Trim() + "'";
                con.Close();

                DataTable dt = new DataTable();
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();

                if (dt.Rows.Count >= 1)
                {
                    //Response.Write("<script>alert('return true');</script>");
                    return true;
                }
                else
                {
                    //Response.Write("<script>alert('return false');</script>");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script language = javascript> alert(' " + ex.Message + " ')</script>");
                return false;
            }
        }




        //issue Book
        void issueBook()
        {
            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["first"].ToString();
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                    cmd.CommandText = "insert into book_issue_tbl values ('" + TextBox2.Text.Trim() + "','" + TextBox3.Text.Trim() + "','" + TextBox1.Text.Trim() + "','" + TextBox4.Text.Trim() + "','" + TextBox5.Text.Trim() + "','" + TextBox6.Text.Trim() + "')";
                    cmd.ExecuteNonQuery();


                    cmd = con.CreateCommand();
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "update book_master_tbl set current_stock = current_stock-1 where book_id='" + TextBox1.Text.Trim() + "'";
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Response.Write("<script language = javascript> alert('Book Issued Successfully! ')</script>");

                    GridView1.DataBind();

                
            }
            catch (Exception ex)
            {
                Response.Write("<script language = javascript> alert(' " + ex.Message + " ')</script>");

            }
        }

        //check if book exixts
        bool checkIfBookExist()
        {
            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["first"].ToString();
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "select * from book_master_tbl where book_id = '" + TextBox1.Text.Trim() + "' AND current_stock > 0 ";
                con.Close();

                DataTable dt = new DataTable();
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();

                if (dt.Rows.Count >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script language = javascript> alert(' " + ex.Message + " ')</script>");
                return false;
            }
        }







        //check if member exixts
        bool checkIfMemberExist()
        {
            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["first"].ToString();
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "select full_name from member_master_tbl where member_id = ('" + TextBox2.Text.Trim() + "' )";
                con.Close();

                DataTable dt = new DataTable();
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();

                if (dt.Rows.Count >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script language = javascript> alert(' " + ex.Message + " ')</script>");
                return false;
            }
        }


        //get bookname and member-names
        void getNames() 
        {
            try
            {

                //book----->>

                con.ConnectionString = ConfigurationManager.ConnectionStrings["first"].ToString();
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "select book_name from book_master_tbl where book_id = ('" + TextBox1.Text.Trim() + "' )";
                con.Close();


                DataTable dt = new DataTable();
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();

                if (dt.Rows.Count >= 1)
                {
                    TextBox4.Text = dt.Rows[0]["book_name"].ToString();
                }
                else
                {
                    Response.Write("<script language = javascript> alert(' " + "Invalide Book ID." + " ')</script>");

                }



                //member--->>
                dt = new DataTable();
                con.Open();
                cmd.CommandText = "select full_name from member_master_tbl where member_id = ('" + TextBox2.Text.Trim() + "' )";
                con.Close();

                con.Open();
                da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();

                if (dt.Rows.Count >= 1)
                {
                    TextBox3.Text = dt.Rows[0]["full_name"].ToString();

                }
                else
                {
                    Response.Write("<script language = javascript> alert(' " + "Invalide Member ID." + " ')</script>");

                }

            }



            catch (Exception ex)
            {
                Response.Write("<script language = javascript> alert(' " + ex.Message + " ')</script>");


            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if(e.Row.RowType == DataControlRowType.DataRow)  // fire every time for every row ----having a proper row which is having some values
                {
                    //check your condition here 
                    DateTime dt = Convert.ToDateTime(e.Row.Cells[5].Text);  //taking the 5th cell which is a due date
                    DateTime today = DateTime.Today;  //taking today's date
                    if (today > dt)
                    {
                        e.Row.BackColor = System.Drawing.Color.PaleVioletRed;
                        
                    }
                    else if (today == dt)
                    {
                        e.Row.BackColor = System.Drawing.Color.DeepSkyBlue;

                    }
                    else
                    {
                        e.Row.BackColor = System.Drawing.Color.LimeGreen;

                    }


                }

            }
            catch(Exception ex)
            {
                Response.Write("<script language = javascript> alert(' " + ex.Message + " ')</script>");

            }


        }
    }
}