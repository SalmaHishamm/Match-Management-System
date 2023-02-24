using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Milestone3
{
    public partial class SAMpage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }


        protected void AMd_Click(object sender, EventArgs e)
        {

            string connstr = WebConfigurationManager.ConnectionStrings["m2"].ToString();
            SqlConnection conn = new SqlConnection(connstr);
            String Host_Club_Name = HCN.Text;
            String Guest_Club_Name = GCN.Text;
            String Start_Time = MST.Text;
            String End_Time = MET.Text;

            SqlCommand addNewMatch = new SqlCommand("addNewMatch", conn);
            addNewMatch.CommandType = CommandType.StoredProcedure;
            addNewMatch.Parameters.Add(new SqlParameter("@hostname", Host_Club_Name));
            addNewMatch.Parameters.Add(new SqlParameter("@guestname", Guest_Club_Name));
            addNewMatch.Parameters.Add(new SqlParameter("@starttime", Start_Time));
            addNewMatch.Parameters.Add(new SqlParameter("@endtime", End_Time));


            SqlCommand checkclub = new SqlCommand("checkClubname", conn);
            checkclub.CommandType = CommandType.StoredProcedure;
            checkclub.Parameters.Add(new SqlParameter("@cname", Host_Club_Name));
            SqlParameter status = checkclub.Parameters.Add("@stat", SqlDbType.Int);
            status.Direction = ParameterDirection.Output;

            SqlCommand checkmatch = new SqlCommand("checkmatch", conn);
            checkmatch.CommandType = CommandType.StoredProcedure;

            checkmatch.Parameters.Add(new SqlParameter("@hostc", Host_Club_Name));
            checkmatch.Parameters.Add(new SqlParameter("@guestc", Guest_Club_Name));
            checkmatch.Parameters.Add(new SqlParameter("@st", Start_Time));
            checkmatch.Parameters.Add(new SqlParameter("@end", End_Time));

            SqlParameter status5 = checkmatch.Parameters.Add("@stat", SqlDbType.Int);
            status5.Direction = ParameterDirection.Output;

            Boolean f1 = false;
            Boolean f2 = false;

            conn.Open();
            checkclub.ExecuteNonQuery();

            if (status.Value.ToString() == "0")
            {
                Response.Write(" Host Club Doesn't Exist ");
            }
            else if (Host_Club_Name.Length == 0)
                Response.Write(" Please Type Host Club Name ");
            else
                f1 = true;


            SqlCommand checkclub2 = new SqlCommand("checkClubname", conn);
            checkclub2.CommandType = CommandType.StoredProcedure;
            checkclub2.Parameters.Add(new SqlParameter("@cname", Guest_Club_Name));
            SqlParameter status2 = checkclub2.Parameters.Add("@stat", SqlDbType.Int);
            status2.Direction = ParameterDirection.Output;
            checkclub2.ExecuteNonQuery();

            if (status2.Value.ToString() == "0")
            {
                Response.Write(" Guest Club Doesn't Exist ");
            }
            else if (Guest_Club_Name.Length == 0)
                Response.Write(" Please Type Guest Club Name ");
            else
                f2 = true;


            if (Start_Time.Length == 0)
                Response.Write(" Type Start Time ");
            else if (End_Time.Length == 0)
                Response.Write(" Type End Time ");
            else if (f1 == true && f2 == true)
            {
                checkmatch.ExecuteNonQuery();
                if (status5.Value.ToString() == "1")
                {
                    Response.Write("There Is A Match Exist With Such Info");
                }


                else
                {
                    addNewMatch.ExecuteNonQuery();
                    Response.Write(" Match Added ");
                }
               
            }


            conn.Close();

        }

        protected void DMd_Click(object sender, EventArgs e)
        {
            string connstr = WebConfigurationManager.ConnectionStrings["m2"].ToString();
            SqlConnection conn = new SqlConnection(connstr);



            
            String Host_Club_Name = HCN0.Text;
            String Guest_Club_Name = GCN0.Text;



            String Start_Time = MST0.Text;
            String End_Time = MET0.Text;
            

            SqlCommand DelMatch = new SqlCommand("deleteMatchOnTime", conn);
            DelMatch.CommandType = CommandType.StoredProcedure;
            DelMatch.Parameters.Add(new SqlParameter("@hostname", Host_Club_Name));
            DelMatch.Parameters.Add(new SqlParameter("@guestname", Guest_Club_Name));
            DelMatch.Parameters.Add(new SqlParameter("@start", Start_Time));
            DelMatch.Parameters.Add(new SqlParameter("@end", End_Time));


            SqlCommand checkmatch = new SqlCommand("checkmatch", conn);
            checkmatch.CommandType = CommandType.StoredProcedure;

            checkmatch.Parameters.Add(new SqlParameter("@hostc", Host_Club_Name));
            checkmatch.Parameters.Add(new SqlParameter("@guestc", Guest_Club_Name));
            checkmatch.Parameters.Add(new SqlParameter("@st", Start_Time));
            checkmatch.Parameters.Add(new SqlParameter("@end", End_Time));

            SqlParameter status = checkmatch.Parameters.Add("@stat", SqlDbType.Int);
            status.Direction = ParameterDirection.Output;

          

            conn.Open();
            checkmatch.ExecuteNonQuery();

            if (status.Value.ToString() == "0")
            {
                Response.Write("No Match Exist With Such Info");
            }


            else
            {
                DelMatch.ExecuteNonQuery();
                Response.Write("Match Deleted");
            }




            conn.Close();

        }

        protected void VAPM_Click(object sender, EventArgs e)
        {
            string connstr = WebConfigurationManager.ConnectionStrings["m2"].ToString();
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();
            String sql = "Select * from Alreadyplayed";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            GridView1.DataSource = reader;
            GridView1.DataBind();
            conn.Close();
        }

        protected void VAM_Click(object sender, EventArgs e)  
        {
            string connstr = WebConfigurationManager.ConnectionStrings["m2"].ToString();
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();
            String sql = "Select * from AllUpcoming";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            GridView1.DataSource = reader;
            GridView1.DataBind();
            conn.Close();
        }

        protected void VNPM_Click(object sender, EventArgs e) 
        {
            string connstr = WebConfigurationManager.ConnectionStrings["m2"].ToString();
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();
            String sql = "Select * from clubsNeverMatched";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            GridView1.DataSource = reader;
            GridView1.DataBind();
            conn.Close();
        }

        protected void LO_Click(object sender, EventArgs e)
        {
            Response.Redirect("login.aspx");
        }
    }
}