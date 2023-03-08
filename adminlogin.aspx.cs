using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ElibraryManagement
{
    public partial class adminlogin : System.Web.UI.Page
    {

        SqlConnection con = new SqlConnection();


        protected void Page_Load(object sender, EventArgs e)
        {
        //UnobtrusiveValidationMode


        //--->> web.config
#pragma warning disable CS0164 // This label has not been referenced
        ValidationSettings: UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
#pragma warning restore CS0164 // This label has not been referenced




        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {

                // Response.Write("<script language = javascript> alert(' Login Successful ')</script>");

                con.ConnectionString = ConfigurationManager.ConnectionStrings["first"].ToString();
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "select * from admin_login_tbl where username = '" + TextBox1.Text.Trim() + "' AND password = '" + TextBox2.Text.Trim() + "'";

                //"' AND full_name = '" + ("swasata ghosh") +



                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {

                        Response.Write("<script language = javascript> alert(' " + dr.GetValue(0).ToString() + " ')</script>");
                        Session["username"] = dr.GetValue(0).ToString();
                        Session["fullname"] = dr.GetValue(2).ToString();
                        Session["role"] = "admin";
                        //Session["ststus"] = dr.GetValue(10).ToString();

                    }
                    Response.Redirect("homepage.aspx");
                }
                else
                {
                    Response.Write("<script language = javascript> alert(' Invalid Credentials')</script>");

                }



            }

            catch (Exception ex)
            {
                Response.Write("<script language = javascript> alert(' " + ex.Message + " ')</script>");

            }
        }
    }
}