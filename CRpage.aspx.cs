using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection.Emit;
using System.Xml.Linq;
using System.Drawing;

namespace Milestone3
{
    public partial class CRpage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            clubn.Text = Session["CR_Clubname"].ToString();
            string connstr = WebConfigurationManager.ConnectionStrings["m2"].ToString();
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();

            String sql = "Select * from SendR(@cn)";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@cn", Session["CR_Clubname"].ToString());

            SqlDataReader reader = cmd.ExecuteReader();
            GridView3.DataSource = reader;
            GridView3.DataBind();
            conn.Close();
        }

        protected void VMI_Click(object sender, EventArgs e)
        {
            String connStr = WebConfigurationManager.ConnectionStrings["m2"].ToString();

            SqlConnection conn = new SqlConnection(connStr);

            String user = Session["CR_username"].ToString();

            SqlCommand clubinfoproc = new SqlCommand("clubinfo", conn);
            clubinfoproc.CommandType = CommandType.StoredProcedure;

            clubinfoproc.Parameters.Add(new SqlParameter("@username", SqlDbType.VarChar)).Value = user;


            SqlParameter name = clubinfoproc.Parameters.Add("@name", SqlDbType.VarChar, 20);

            SqlParameter location = clubinfoproc.Parameters.Add("@location", SqlDbType.VarChar, 20);
            name.Direction = ParameterDirection.Output;
            location.Direction = ParameterDirection.Output;



            conn.Open();
            clubinfoproc.ExecuteNonQuery();

            Label1.Text = "Name: " +" "+ (string)name.Value;
            Label2.Text = "Location: " + " " + (string)location.Value;
            



            conn.Close();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string connstr = WebConfigurationManager.ConnectionStrings["m2"].ToString();
            SqlConnection conn = new SqlConnection(connstr);

            String user = Session["CR_username"].ToString();

           /* SqlCommand MyClubNameproc = new SqlCommand("MyClubName", conn);
            MyClubNameproc.CommandType = CommandType.StoredProcedure;

            MyClubNameproc.Parameters.Add(new SqlParameter("@username", SqlDbType.VarChar)).Value = user;
            SqlParameter cname = MyClubNameproc.Parameters.Add("@clubname", SqlDbType.VarChar, 20);
            cname.Direction = ParameterDirection.Output;
            

          
            MyClubNameproc.ExecuteNonQuery();
            String c = cname.Value.ToString();
           */

            
            String sql = "Select * from AllUpcoming2 where guest_club =@cn or host_club=@cn";

            conn.Open();
           

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@cn", Session["CR_Clubname"].ToString());
            //Response.Write(Session["CR_Clubname"].ToString());
            SqlDataReader reader = cmd.ExecuteReader();

            GridView1.DataSource = reader;
            GridView1.DataBind();
            
            conn.Close();
          




            }

        protected void VAS_Click1(object sender, EventArgs e)
        {
            string connstr = WebConfigurationManager.ConnectionStrings["m2"].ToString();
            SqlConnection conn = new SqlConnection(connstr);
            String t = date.Text;
            conn.Open();

            if (t.Length > 0)
            {
                String sql = "Select * from dbo.viewAvailableStadiumsOn(@time)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@time", t);
                SqlDataReader reader = cmd.ExecuteReader();

                GridView2.DataSource = reader;
                GridView2.DataBind();
            }
            else
                Response.Write("Please Type Valid Time");
            conn.Close();

        }

        protected void Send_Request_Click(object sender, EventArgs e)
        {
            string connstr = WebConfigurationManager.ConnectionStrings["m2"].ToString();
            SqlConnection conn = new SqlConnection(connstr);


            //String Club_Name = clubn.Text;
           // String Stadium_Name = stadiumn.Text;
            //String Start_Time = matchst.Text;

           /* SqlCommand addHostRequestproc = new SqlCommand("addHostRequest", conn);
            addHostRequestproc.CommandType = CommandType.StoredProcedure;
            addHostRequestproc.Parameters.Add(new SqlParameter("@cname", Session["CR_Clubname"]));
            addHostRequestproc.Parameters.Add(new SqlParameter("@sname", Stadium_Name));
            addHostRequestproc.Parameters.Add(new SqlParameter("@start_time", Start_Time));

            SqlCommand checkstad = new SqlCommand("checkStadiumname", conn);
            checkstad.CommandType = CommandType.StoredProcedure;
            checkstad.Parameters.Add(new SqlParameter("@sname", Stadium_Name));
            SqlParameter status = checkstad.Parameters.Add("@stat", SqlDbType.Int);
            status.Direction = ParameterDirection.Output;


            SqlCommand chechkt = new SqlCommand("checkST", conn);
            chechkt.CommandType = CommandType.StoredProcedure;
            chechkt.Parameters.Add(new SqlParameter("@st", Start_Time));
            SqlParameter status3 = chechkt.Parameters.Add("@stat", SqlDbType.Int);
            status3.Direction = ParameterDirection.Output;


            conn.Open();

            chechkt.ExecuteNonQuery();
            checkstad.ExecuteNonQuery();

           
            if (status.Value.ToString() == "0")
            {
                Response.Write("Stadium Doesn't Exist");
            }
            
           else if (status3.Value.ToString() == "0")
                {
                    Response.Write("There Is No Match At This Time");
                }
            else { 
                
                addHostRequestproc.ExecuteNonQuery();
                Response.Write("YOUR REQUEST HAS BEEN SENT SUCCESSFULLY");
            }
            conn.Close();
           */



        }

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridView3_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow gr = GridView3.SelectedRow;
            string connstr = WebConfigurationManager.ConnectionStrings["m2"].ToString();
            SqlConnection conn = new SqlConnection(connstr);

            conn.Open();

           
            String st = gr.Cells[3].Text;
            String Stadium_Name = TB.Text;

            DateTime st2 = Convert.ToDateTime(st);


            SqlCommand addHostRequestproc = new SqlCommand("addHostRequest", conn);
            addHostRequestproc.CommandType = CommandType.StoredProcedure;
            addHostRequestproc.Parameters.Add(new SqlParameter("@cname", Session["CR_Clubname"]));
            addHostRequestproc.Parameters.Add(new SqlParameter("@sname", Stadium_Name));
            addHostRequestproc.Parameters.Add(new SqlParameter("@start_time", st2));


            SqlCommand checkstad = new SqlCommand("checkStadiumname", conn);
            checkstad.CommandType = CommandType.StoredProcedure;
            checkstad.Parameters.Add(new SqlParameter("@sname", Stadium_Name));
            SqlParameter status = checkstad.Parameters.Add("@stat", SqlDbType.Int);
            status.Direction = ParameterDirection.Output;

            checkstad.ExecuteNonQuery();



            if (status.Value.ToString() == "0")
            {
                Response.Write("Please Type Valid Stadium Name");
            }
            else
            {

                addHostRequestproc.ExecuteNonQuery();
                Response.Write("Request Sent Successfully");


            }


        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("login.aspx");
        }
    }
}