using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ElibraryManagement
{
    public partial class adminbookinventory : System.Web.UI.Page
    {

        SqlConnection con = new SqlConnection();
        static string global_filepath;
        static int global_actual_stock, global_current_stock, global_issued_books;

        protected void Page_Load(object sender, EventArgs e)
        {
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
            

            if (!IsPostBack)
            {
                GridView1.DataBind();
                fillAuthorPublisherValues();

            }


        }


        //go button click
        protected void Button4_Click(object sender, EventArgs e)
        {

            getBookById();

        }


        //add button click
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (checkIfBookExists())
            {
                Response.Write("<script language = javascript> alert(' " + "Book Already Exists . Try Some Other BOOK ID " + " ')</script>");

            }
            else
            {
                if (
                    (FileUpload1.HasFile)  // image of the book
                    && (ListBox1.GetSelectedIndices() != null) //genres 
                    && (TextBox1.Text.Trim().Length != 0)  //book ID
                    && (TextBox2.Text.Trim().Length != 0) //book name
                    && (TextBox4.Text.Trim().Length != 0) //actual stock
                    && (TextBox3.Text.Trim().Length != 0) //publish date
                    )
                {
                    addNewBook();
                }
                else
                {
                    Response.Write("<script language = javascript> alert(' " + " Please Fill The Requirements  " + " ')</script>");

                }

            }
        }


        //update button click
        protected void Button3_Click(object sender, EventArgs e)
        {
            if (
                (((TextBox5.Text.Trim().Length!=0) || (TextBox7.Text.Trim().Length != 0)))
                && (ListBox1.GetSelectedIndices() != null))
            {
                updateBookById();
            }
         
            else
            {
                Response.Write("<script language = javascript> alert(' " + " Stockes or Genrey Cannot Be Empty .For Genrey Select It One More Time and For Stocks Enter The Id and Press The Go Button " + " ')</script>");

            }
        }


        //delete button click
        protected void Button2_Click(object sender, EventArgs e)
        {
            deleteBookById();

        }





        //user defined methods


        //delete custom function
        void deleteBookById()
        {


            //try
            {

                if (TextBox1.Text.Trim().Equals(""))
                {
                    Response.Write("<script language = javascript> alert('Book ID can not blank! Please enter ID  ')</script>");

                }
                else if (checkIfBookExists())
                {

                    con.ConnectionString = ConfigurationManager.ConnectionStrings["first"].ToString();
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "delete from book_master_tbl where book_id ='" + TextBox1.Text.Trim() + "'";
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Response.Write("<script language = javascript> alert('Book Deleted Successfully ')</script>");


                    TextBox1.Text = ""; //book-id
                    TextBox2.Text = ""; //fullname
                    DropDownList1.SelectedItem.Value = "";
                    DropDownList2.SelectedItem.Value = "";
                    DropDownList3.SelectedItem.Value = "";
                    TextBox3.Text = ""; //contact no
                    ListBox1.Items.Clear(); //genres
                    TextBox4.Text = ""; //emailid
                    TextBox9.Text = ""; //state
                    TextBox10.Text = ""; //city
                    TextBox11.Text = ""; //pin code
                    TextBox6.Text = ""; //pin code
                    TextBox5.Text = ""; //current stock
                    TextBox7.Text = ""; //issued books

                    GridView1.DataBind();

                }
                else
                {
                    Response.Write("<script language = javascript> alert('Book with thid ID doesnot Exists! Enter anothor  ')</script>");

                }
            }
            // catch (Exception ex)
            {
                //  Response.Write("<script language = javascript> alert '" + ex.Message + "' </script>");

            }

        }


        //update button custom function
        void updateBookById()
        {
            if (checkIfBookExists())
            {


                //try
                {

                    int actual_stock = Convert.ToInt32(TextBox4.Text.Trim());
                    int current_stock = Convert.ToInt32(TextBox5.Text.Trim());

                    if (global_actual_stock == actual_stock)
                    {

                    }
                    else
                    {
                        if (actual_stock < global_issued_books)
                        {
                            //Response.Write("<script>alert('Actual Stock value cannot be less than the Issued books');</script>");
                            Response.Write("<script language = javascript> alert(' " + "Actual Stock value cannot be less than the Issued books" + " ')</script>");

                            return;


                        }
                        else
                        {
                            current_stock = actual_stock - global_issued_books;
                            TextBox5.Text = "" + current_stock;
                        }
                    }

                    string genres = "";
                    foreach (int i in ListBox1.GetSelectedIndices())
                    {
                        genres = genres + ListBox1.Items[i] + ",";
                    }
                    genres = genres.Remove(genres.Length - 1);

                    string filepath = "~/books_inventory/books1";
                    string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                    if (filename == "" || filename == null)
                    {
                        filepath = global_filepath;

                    }
                    else
                    {
                        FileUpload1.SaveAs(Server.MapPath("books_inventory/" + filename));
                        filepath = "~/books_inventory/" + filename;
                    }



                    // Response.Write("<script language = javascript> alert(' Login Successful ')</script>");

                    con.ConnectionString = ConfigurationManager.ConnectionStrings["first"].ToString();
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = System.Data.CommandType.Text;

                    //cmd.CommandText = "update book_master_tbl set book_name=@book_name, genre=@genre, author_name=@author_name, publisher_name=@publisher_name, publish_date=@publish_date, language=@language, edition=@edition, book_cost=@book_cost, no_of_pages=@no_of_pages, book_description=@book_description, actual_stock=@actual_stock, current_stock=@current_stock, book_img_link=@book_img_link where book_id='" + TextBox1.Text.Trim() + "'";

                    //cmd.Parameters.AddWithValue("@book_name", TextBox2.Text.Trim());
                    //cmd.Parameters.AddWithValue("@genre", genres);
                    //cmd.Parameters.AddWithValue("@author_name", DropDownList3.SelectedItem.Value);
                    //cmd.Parameters.AddWithValue("@publisher_name", DropDownList2.SelectedItem.Value);
                    //cmd.Parameters.AddWithValue("@publish_date", TextBox3.Text.Trim());
                    //cmd.Parameters.AddWithValue("@language", DropDownList1.SelectedItem.Value);
                    //cmd.Parameters.AddWithValue("@edition", TextBox9.Text.Trim());
                    //cmd.Parameters.AddWithValue("@book_cost", TextBox10.Text.Trim());
                    //cmd.Parameters.AddWithValue("@no_of_pages", TextBox11.Text.Trim());
                    //cmd.Parameters.AddWithValue("@book_description", TextBox6.Text.Trim());
                    //cmd.Parameters.AddWithValue("@actual_stock", actual_stock.ToString());
                    //cmd.Parameters.AddWithValue("@current_stock", current_stock.ToString());
                    //cmd.Parameters.AddWithValue("@book_img_link", filepath);

                    cmd.CommandText = "update book_master_tbl set book_name='" + TextBox2.Text.Trim() + "',genre='" + genres + "',author_name='" + DropDownList3.SelectedItem.Value + "',publisher_name='" + DropDownList2.SelectedItem.Value + "',publish_date='" + TextBox3.Text.Trim() + "',language='" + DropDownList1.SelectedItem.Value + "',edition='" + TextBox9.Text.Trim() + "',book_cost='" + TextBox10.Text.Trim() + "',no_of_pages='" + TextBox11.Text.Trim() + "',book_description='" + TextBox6.Text.Trim() + "',actual_stock='" + actual_stock.ToString() + "',current_stock='" + current_stock.ToString() + "',book_img_link='" + filepath + "'  where book_id='" + TextBox1.Text + "'";



                    cmd.ExecuteNonQuery();
                    con.Close();
                    GridView1.DataBind();
                    Response.Write("<script language = javascript> alert(' " + "Book Updated Successfully" + " ')</script>");

                }

                //catch (Exception ex)
                {
                    // Response.Write("<script language = javascript> alert(' " + ex.Message + " ')</script>");

                }

            }
        }



        void fillAuthorPublisherValues()
        {
            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["first"].ToString();
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "select author_name from author_master_tbl";
                con.Close();

                DataTable dt = new DataTable();
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
                DropDownList3.DataSource = dt;
                DropDownList3.DataValueField = "author_name";
                DropDownList3.DataBind();



                con.Open();
                cmd.CommandText = "select publisher_name from publisher_master_tbl";
                con.Close();
                dt = new DataTable();
                con.Open();
                da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
                DropDownList2.DataSource = dt;
                DropDownList2.DataValueField = "publisher_name";
                DropDownList2.DataBind();



            }

            catch (Exception ex)
            {

            }


        }




        //go button function
        void getBookById()
        {
            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["first"].ToString();
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "select author_name from author_master_tbl";
                con.Close();

                DataTable dt = new DataTable();
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
                DropDownList3.DataSource = dt;
                DropDownList3.DataValueField = "author_name";
                DropDownList3.DataBind();



                con.Open();
                cmd.CommandText = "select * from book_master_tbl where book_id='" + TextBox1.Text.Trim().ToString() + "'";
                con.Close();
                dt = new DataTable();
                con.Open();
                da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();

                if (dt.Rows.Count >= 1)
                {
                    TextBox2.Text = dt.Rows[0]["book_name"].ToString();
                    TextBox3.Text = dt.Rows[0]["publish_date"].ToString();
                    TextBox9.Text = dt.Rows[0]["edition"].ToString();
                    TextBox10.Text = dt.Rows[0]["book_cost"].ToString().Trim();
                    TextBox11.Text = dt.Rows[0]["no_of_pages"].ToString().Trim();
                    TextBox4.Text = dt.Rows[0]["actual_stock"].ToString().Trim();
                    TextBox5.Text = dt.Rows[0]["current_stock"].ToString().Trim();
                    TextBox6.Text = dt.Rows[0]["book_description"].ToString();
                    TextBox7.Text = "" + (Convert.ToInt32(dt.Rows[0]["actual_stock"].ToString()) - Convert.ToInt32(dt.Rows[0]["current_stock"].ToString()));
                    DropDownList1.SelectedValue = dt.Rows[0]["language"].ToString().Trim();
                    DropDownList2.SelectedValue = dt.Rows[0]["publisher_name"].ToString().Trim();
                    DropDownList3.SelectedValue = dt.Rows[0]["author_name"].ToString().Trim();

                    ListBox1.ClearSelection();
                    string[] genre = dt.Rows[0]["genre"].ToString().Trim().Split(',');
                    for (int i = 0; i < genre.Length; i++)
                    {
                        for (int j = 0; j < ListBox1.Items.Count; j++)
                        {
                            if (ListBox1.Items[j].ToString() == genre[i])
                            {
                                ListBox1.Items[j].Selected = true;
                                //Response.Write("<script language = javascript> alert(' " +ListBox1.Items[j].Selected  + " ')</script>");

                            }
                        }
                    }

                    global_actual_stock = Convert.ToInt32(dt.Rows[0]["actual_stock"].ToString().Trim());
                    global_current_stock = Convert.ToInt32(dt.Rows[0]["current_stock"].ToString().Trim());
                    global_issued_books = global_actual_stock - global_current_stock;
                    global_filepath = dt.Rows[0]["book_img_link"].ToString();





                }

                else
                {
                    Response.Write("<script language = javascript> alert(' " + "Invalid Book ID" + " ')</script>");
                }


            }
            catch (Exception ex)
            {
                Response.Write("<script language = javascript> alert(' " + ex.Message + " ')</script>");

            }


        }


        bool checkIfBookExists()
        {
            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["first"].ToString();
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "select * from book_master_tbl where book_id = '" + TextBox1.Text.Trim() + "' OR book_name = '" + TextBox2.Text.Trim() + "' ";
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

        void addNewBook()
        {


            //try
            {

                String genres = "";

                foreach (int i in ListBox1.GetSelectedIndices())
                {
                    genres = genres + ListBox1.Items[i] + " , ";
                }
                genres = genres.Remove(genres.Length - 1);



                //adding book image link to the database

                string filepath = "~/book_inventory/books1.png";
                string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                FileUpload1.SaveAs(Server.MapPath("books_inventory/" + filename));
                filepath = "~/books_inventory/" + filename;




                con.ConnectionString = ConfigurationManager.ConnectionStrings["first"].ToString();
                con.Open();
                //SqlCommand cmd = con.CreateCommand();
                //cmd.CommandType = System.Data.CommandType.Text;
                //TextBox4.Text = TextBox5.Text;
                //cmd.CommandText = "insert into publisher_master_tbl values('" + TextBox1.Text.Trim() + "','" + TextBox2.Text.Trim() + "','" + DropDownList1.SelectedItem.Value + "','" + DropDownList2.SelectedItem.Value + "','" + DropDownList3.SelectedItem.Value + "','" + TextBox3.Text.Trim() + "','" + genres + "','" + TextBox9.Text.Trim() + "','" + TextBox10.Text.Trim() + "','" + TextBox11.Text.Trim() + "','" + TextBox4.Text.Trim() + "','" + TextBox4.Text.Trim() + "','" + TextBox6.Text.Trim() + "','" + filepath + "' )";




                SqlCommand cmd = new SqlCommand("insert into book_master_tbl(book_id,book_name,genre,author_name,publisher_name,publish_date,language,edition,book_cost,no_of_pages,book_description,actual_stock,current_stock,book_img_link) values(@book_id,@book_name,@genre,@author_name,@publisher_name,@publish_date,@language,@edition,@book_cost,@no_of_pages,@book_description,@actual_stock,@current_stock,@book_img_link)", con);

                cmd.Parameters.AddWithValue("@book_id", TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@book_name", TextBox2.Text.Trim());
                cmd.Parameters.AddWithValue("@genre", genres);
                cmd.Parameters.AddWithValue("@author_name", DropDownList3.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@publisher_name", DropDownList2.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@publish_date", TextBox3.Text.Trim());
                cmd.Parameters.AddWithValue("@language", DropDownList1.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@edition", TextBox9.Text.Trim());
                cmd.Parameters.AddWithValue("@book_cost", TextBox10.Text.Trim());
                cmd.Parameters.AddWithValue("@no_of_pages", TextBox11.Text.Trim());
                cmd.Parameters.AddWithValue("@book_description", TextBox6.Text.Trim());
                cmd.Parameters.AddWithValue("@actual_stock", TextBox4.Text.Trim());
                cmd.Parameters.AddWithValue("@current_stock", TextBox4.Text.Trim());
                cmd.Parameters.AddWithValue("@book_img_link", filepath);

                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script>alert('Book added successfully.');</script>");
                GridView1.DataBind();

            }


        }



    }



}
