using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

namespace Milestone3
{
    public partial class SMpage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //stadn.Text = Session["SM_Stadiumname"].ToString();
            string connstr = WebConfigurationManager.ConnectionStrings["m2"].ToString();
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();

            String sql = "Select * from unhandledR(@smname)";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@smname", Session["SM_Username"].ToString());

            SqlDataReader reader = cmd.ExecuteReader();
            GridView2.DataSource = reader;
            GridView2.DataBind();
            conn.Close();

            conn.Open();
            String sql2 = "Select * from unhandledR(@smname2)";
            SqlCommand cmd2 = new SqlCommand(sql2, conn);
            cmd2.Parameters.AddWithValue("@smname2", Session["SM_Username"].ToString());

            SqlDataReader reader2 = cmd2.ExecuteReader();

            GridView3.DataSource = reader2;
            GridView3.DataBind();

           
            conn.Close();
        }

        protected void ViewAllInfo_Click(object sender, EventArgs e)
        {
            String connStr = WebConfigurationManager.ConnectionStrings["m2"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            String user = Session["SM_username"].ToString();

            SqlCommand stadiuminfoproc = new SqlCommand("stadiuminfo", conn);
            stadiuminfoproc.CommandType = CommandType.StoredProcedure;

            stadiuminfoproc.Parameters.Add(new SqlParameter("@username", SqlDbType.VarChar)).Value = user;
           // sm.Text = user;

            SqlParameter name = stadiuminfoproc.Parameters.Add("@name", SqlDbType.VarChar, 20);
            SqlParameter location = stadiuminfoproc.Parameters.Add("@location", SqlDbType.VarChar, 20);
            SqlParameter capacity = stadiuminfoproc.Parameters.Add("@capacity", SqlDbType.VarChar, 20);
            SqlParameter status = stadiuminfoproc.Parameters.Add("@status", SqlDbType.Bit);

            name.Direction = ParameterDirection.Output;
            location.Direction = ParameterDirection.Output;
            capacity.Direction = ParameterDirection.Output;
            status.Direction = ParameterDirection.Output;



            conn.Open();
            stadiuminfoproc.ExecuteNonQuery();

            Label1.Text = "Name: " + " " + (string)name.Value;
            Label2.Text = "Location: " + " " + (string)location.Value;
            Label3.Text = "Capacity: " + " " + (string)capacity.Value;
            if (status.Value.ToString() == "0")
            {
                Label4.Text = "Status: " + " " + "Unavailable";
            }
            else
                Label4.Text = "Status: " + " " + "Available";

            //smu.Text = sm.Text;
            //smu0.Text = sm.Text;





            conn.Close();
        }

        protected void VAR_Click(object sender, EventArgs e)
        {
            string connstr = WebConfigurationManager.ConnectionStrings["m2"].ToString();
            SqlConnection conn = new SqlConnection(connstr);

            String user = Session["SM_username"].ToString();

            conn.Open();
           
            String sql = "Select * from dbo.Requests(@u) ";

          

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@u", user);
            SqlDataReader reader = cmd.ExecuteReader();

            GridView1.DataSource = reader;
            GridView1.DataBind();

            conn.Close();
        }

       /* protected void AcceptR_Click(object sender, EventArgs e)
        {
            string connstr = WebConfigurationManager.ConnectionStrings["m2"].ToString();
            SqlConnection conn = new SqlConnection(connstr);

            String user = Session["SM_username"].ToString();

           // String smuser = sm.Text;
            String Host_Name = HN.Text;
            String Guest_Name = GN.Text;
            String Start_Time = ST.Text;
            SqlCommand acceptRequest2proc = new SqlCommand("acceptRequest2", conn);
            acceptRequest2proc.CommandType = CommandType.StoredProcedure;
            acceptRequest2proc.Parameters.Add(new SqlParameter("@username", user));
            acceptRequest2proc.Parameters.Add(new SqlParameter("@hostingclubname", Host_Name));
            acceptRequest2proc.Parameters.Add(new SqlParameter("@guestname", Guest_Name));
            acceptRequest2proc.Parameters.Add(new SqlParameter("@starttime", Start_Time));


            SqlCommand checkclub = new SqlCommand("checkClubname", conn);
            checkclub.CommandType = CommandType.StoredProcedure;
            checkclub.Parameters.Add(new SqlParameter("@cname", Host_Name));
            SqlParameter status = checkclub.Parameters.Add("@stat", SqlDbType.Int);
            status.Direction = ParameterDirection.Output;

            Boolean f1 = false;
            Boolean f2 = false;
            Boolean f3 = false;
            conn.Open();
            checkclub.ExecuteNonQuery();

            if (status.Value.ToString() == "0")
            {
                Response.Write("Host Club Doesn't Exist");
            }
            else
                f1 = true;


            SqlCommand checkclub2 = new SqlCommand("checkClubname", conn);
            checkclub2.CommandType = CommandType.StoredProcedure;
            checkclub2.Parameters.Add(new SqlParameter("@cname", Guest_Name));
            SqlParameter status2 = checkclub2.Parameters.Add("@stat", SqlDbType.Int);
            status2.Direction = ParameterDirection.Output;
            checkclub2.ExecuteNonQuery();

            if (status2.Value.ToString() == "0")
            {
                Response.Write("Guest Club Doesn't Exist");
            }
            else
                f2 = true;

            SqlCommand chechkt = new SqlCommand("checkST", conn);
            chechkt.CommandType = CommandType.StoredProcedure;
            chechkt.Parameters.Add(new SqlParameter("@st", Start_Time));
            SqlParameter status3 = chechkt.Parameters.Add("@stat", SqlDbType.Int);
            status3.Direction = ParameterDirection.Output;
            chechkt.ExecuteNonQuery();

            if (status3.Value.ToString() == "0")
            {
                Response.Write("There Is No Match At This Time");
            }
            else
                f3 = true;


            if (f1 == true && f2 == true && f3==true)
            {
                acceptRequest2proc.ExecuteNonQuery();
               
            }

            conn.Close();   
        }

        protected void RejectR_Click(object sender, EventArgs e)
        {
            string connstr = WebConfigurationManager.ConnectionStrings["m2"].ToString();
            SqlConnection conn = new SqlConnection(connstr);

            String user = Session["user"].ToString();

            String smuser = smu0.Text;
            String Host_Name = HN0.Text;
            String Guest_Name = GN0.Text;
            String Start_Time = ST0.Text;
            SqlCommand rejectRequest2proc = new SqlCommand("rejectRequest2", conn);
            rejectRequest2proc.CommandType = CommandType.StoredProcedure;
            rejectRequest2proc.Parameters.Add(new SqlParameter("@username", smuser));
            rejectRequest2proc.Parameters.Add(new SqlParameter("@hostingclubname", Host_Name));
            rejectRequest2proc.Parameters.Add(new SqlParameter("@guestname", Guest_Name));
            rejectRequest2proc.Parameters.Add(new SqlParameter("@starttime", Start_Time));


            SqlCommand checkclub = new SqlCommand("checkClubname", conn);
            checkclub.CommandType = CommandType.StoredProcedure;
            checkclub.Parameters.Add(new SqlParameter("@cname", Host_Name));
            SqlParameter status = checkclub.Parameters.Add("@stat", SqlDbType.Int);
            status.Direction = ParameterDirection.Output;

            Boolean f1 = false;
            Boolean f2 = false;
            Boolean f3 = false;
            conn.Open();
            checkclub.ExecuteNonQuery();

            if (status.Value.ToString() == "0")
            {
                Response.Write("Host Club Doesn't Exist");
            }
            else
                f1 = true;


            SqlCommand checkclub2 = new SqlCommand("checkClubname", conn);
            checkclub2.CommandType = CommandType.StoredProcedure;
            checkclub2.Parameters.Add(new SqlParameter("@cname", Guest_Name));
            SqlParameter status2 = checkclub2.Parameters.Add("@stat", SqlDbType.Int);
            status2.Direction = ParameterDirection.Output;
            checkclub2.ExecuteNonQuery();

            if (status2.Value.ToString() == "0")
            {
                Response.Write("Guest Club Doesn't Exist");
            }
            else
                f2 = true;

            SqlCommand chechkt = new SqlCommand("checkST", conn);
            chechkt.CommandType = CommandType.StoredProcedure;
            chechkt.Parameters.Add(new SqlParameter("@st", Start_Time));
            SqlParameter status3 = chechkt.Parameters.Add("@stat", SqlDbType.Int);
            status3.Direction = ParameterDirection.Output;
            chechkt.ExecuteNonQuery();

            if (status3.Value.ToString() == "0")
            {
                Response.Write("There Is No Match At This Time");
            }
            else
                f3 = true;


            if (f1 == true && f2 == true && f3 == true)
            {
                rejectRequest2proc.ExecuteNonQuery();

            }

            conn.Close();
        }*/

        protected void GridView2_SelectedIndexChanged1(object sender, EventArgs e)
        {
            GridViewRow gr = GridView2.SelectedRow;
            string connstr = WebConfigurationManager.ConnectionStrings["m2"].ToString();
            SqlConnection conn = new SqlConnection(connstr);

            conn.Open();

            String user = Session["SM_Username"].ToString();
            String Host_Name = gr.Cells[1].Text;
            String Guest_Name = gr.Cells[2].Text;
            String st = gr.Cells[3].Text;
           

            DateTime Start_Time = Convert.ToDateTime(st);


            SqlCommand acceptRequest2proc = new SqlCommand("acceptRequest2", conn);
            acceptRequest2proc.CommandType = CommandType.StoredProcedure;
            acceptRequest2proc.Parameters.Add(new SqlParameter("@username", user));
            acceptRequest2proc.Parameters.Add(new SqlParameter("@hostingclubname", Host_Name));
            acceptRequest2proc.Parameters.Add(new SqlParameter("@guestname", Guest_Name));
            acceptRequest2proc.Parameters.Add(new SqlParameter("@starttime", Start_Time));




            acceptRequest2proc.ExecuteNonQuery();

            conn.Close();

            conn.Open();
            String sql2 = "Select * from unhandledR(@smname2)";
            SqlCommand cmd2 = new SqlCommand(sql2, conn);
            cmd2.Parameters.AddWithValue("@smname2", Session["SM_Username"].ToString());

            SqlDataReader reader2 = cmd2.ExecuteReader();

            GridView2.DataSource = reader2;
            GridView2.DataBind();


            conn.Close();


            conn.Open();
            String sql = "Select * from unhandledR(@smname2)";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@smname2", Session["SM_Username"].ToString());

            SqlDataReader reader = cmd.ExecuteReader();

            GridView3.DataSource = reader;
            GridView3.DataBind();


            conn.Close();




        }

        protected void GridView3_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow gr = GridView3.SelectedRow;
            string connstr = WebConfigurationManager.ConnectionStrings["m2"].ToString();
            SqlConnection conn = new SqlConnection(connstr);

            conn.Open();

            String user = Session["SM_Username"].ToString();
            String Host_Name = gr.Cells[1].Text;
            String Guest_Name = gr.Cells[2].Text;
            String st = gr.Cells[3].Text;


            DateTime Start_Time = Convert.ToDateTime(st);


            SqlCommand rejectRequest2proc = new SqlCommand("rejectRequest2", conn);
            rejectRequest2proc.CommandType = CommandType.StoredProcedure;
            rejectRequest2proc.Parameters.Add(new SqlParameter("@username", user));
            rejectRequest2proc.Parameters.Add(new SqlParameter("@hostingclubname", Host_Name));
            rejectRequest2proc.Parameters.Add(new SqlParameter("@guestname", Guest_Name));
            rejectRequest2proc.Parameters.Add(new SqlParameter("@starttime", Start_Time));




            rejectRequest2proc.ExecuteNonQuery();
            conn.Close();

            conn.Open();
            String sql2 = "Select * from unhandledR(@smname2)";
            SqlCommand cmd2 = new SqlCommand(sql2, conn);
            cmd2.Parameters.AddWithValue("@smname2", Session["SM_Username"].ToString());

            SqlDataReader reader2 = cmd2.ExecuteReader();

            GridView3.DataSource = reader2;
            GridView3.DataBind();


            conn.Close();

            conn.Open();

            String sql = "Select * from unhandledR(@smname)";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@smname", Session["SM_Username"].ToString());

            SqlDataReader reader = cmd.ExecuteReader();
            GridView2.DataSource = reader;
            GridView2.DataBind();
            conn.Close();




        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("login.aspx");
        }
    }
}