using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlTypes;

namespace Milestone3
{
    public partial class SApage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Ncr_TextChanged(object sender, EventArgs e)
        {

        }

        protected void AddClub_Click(object sender, EventArgs e)
        {
            string connstr = WebConfigurationManager.ConnectionStrings["m2"].ToString();
            SqlConnection conn = new SqlConnection(connstr);
            String Club_Name = addCname.Text;
            String Club_Location = addClocation.Text;

            SqlCommand AddClub = new SqlCommand("AddClub", conn);
            AddClub.CommandType = CommandType.StoredProcedure;
            AddClub.Parameters.Add(new SqlParameter("@namec", Club_Name));
            AddClub.Parameters.Add(new SqlParameter("@location", Club_Location));

            SqlCommand checkclub = new SqlCommand("checkClubname", conn);
            checkclub.CommandType = CommandType.StoredProcedure;
            checkclub.Parameters.Add(new SqlParameter("@cname", Club_Name));
            SqlParameter status = checkclub.Parameters.Add("@stat", SqlDbType.Int);
            status.Direction = ParameterDirection.Output;


            conn.Open();
            
            checkclub.ExecuteNonQuery();
            //conn.Close();
            if (status.Value.ToString() == "1")
            {
                Response.Write("Club Already Exist");
            }
            else if (Club_Name.Length == 0)
            {
                Response.Write("Enter A Club Name");
            }
            else if (Club_Location.Length == 0)
            {
                Response.Write("Enter A Club Location");
            }
            else 
                {
                    AddClub.ExecuteNonQuery();
                    addCname.Text = " ";
                    addClocation.Text = " ";
                    Response.Write("Club is Added Successfully");
                }

                

            
            conn.Close();

        }

        protected void DeleteClub_Click(object sender, EventArgs e)
        {
            string connstr = WebConfigurationManager.ConnectionStrings["m2"].ToString();
            SqlConnection conn = new SqlConnection(connstr);

            String Club_Name = DeleteCname.Text;
           

            SqlCommand DeleteClub = new SqlCommand("DeleteClub", conn);
            DeleteClub.CommandType = CommandType.StoredProcedure;
            DeleteClub.Parameters.Add(new SqlParameter("@name", Club_Name));

            SqlCommand checkclub = new SqlCommand("checkClubname", conn);
            checkclub.CommandType = CommandType.StoredProcedure;
            checkclub.Parameters.Add(new SqlParameter("@cname", Club_Name));
            SqlParameter status = checkclub.Parameters.Add("@stat", SqlDbType.Int);
            status.Direction = ParameterDirection.Output;


            conn.Open();

            checkclub.ExecuteNonQuery();
           
            if (status.Value.ToString() == "0")
            {
                Response.Write("Club Doesn't Exist");
            }
            else
            {
                DeleteClub.ExecuteNonQuery();
                Response.Write("Club is Deleted Successfully");
                DeleteCname.Text = " ";

            }
            conn.Close();
        }

        protected void AddStadium_Click(object sender, EventArgs e)
        {
            string connstr = WebConfigurationManager.ConnectionStrings["m2"].ToString();
            SqlConnection conn = new SqlConnection(connstr);

            String stadium_Name = AddSname.Text;
            String stadium_Location = AddSlocation.Text;
            String stadium_cap = AddScapacity.Text;

            SqlCommand AddStad = new SqlCommand("AddStadium", conn);
            AddStad.CommandType = CommandType.StoredProcedure;
            AddStad.Parameters.Add(new SqlParameter("@name", stadium_Name));
            AddStad.Parameters.Add(new SqlParameter("@location", stadium_Location));
            AddStad.Parameters.Add(new SqlParameter("@cap", stadium_cap));


            SqlCommand checkstad = new SqlCommand("checkStadiumname", conn);
            checkstad.CommandType = CommandType.StoredProcedure;
            checkstad.Parameters.Add(new SqlParameter("@sname", stadium_Name));
            SqlParameter status = checkstad.Parameters.Add("@stat", SqlDbType.Int);
            status.Direction = ParameterDirection.Output;


            conn.Open();

            checkstad.ExecuteNonQuery();
            
            if (status.Value.ToString() == "1")
            {
                Response.Write("Stadium Already Exist");
            }
            else if (stadium_Name.Length==0)
            {
                Response.Write("Type A Stadium Name");
            }
            else if (stadium_Location.Length==0)
            {
                Response.Write("Type A Stadium Location");
            }
            else if (stadium_cap.Length==0)
            {
                Response.Write("Type A Stadium Capacity");
            }
            else
            {
                AddStad.ExecuteNonQuery();
                Response.Write("Stadium is Added Successfully");
                AddSname.Text = " ";
                AddSlocation.Text = " ";
                AddScapacity.Text = " ";


            }
            conn.Close();
        }

        protected void DeleteStad_Click(object sender, EventArgs e)
        {
            string connstr = WebConfigurationManager.ConnectionStrings["m2"].ToString();
            SqlConnection conn = new SqlConnection(connstr);

            String stadium_Name = deleteSname.Text;
           

            SqlCommand DeleteStad = new SqlCommand("DeleteStadium2", conn);
            DeleteStad.CommandType = CommandType.StoredProcedure;
            DeleteStad.Parameters.Add(new SqlParameter("@name", stadium_Name));
           

            SqlCommand checkstad = new SqlCommand("checkStadiumname", conn);
            checkstad.CommandType = CommandType.StoredProcedure;
            checkstad.Parameters.Add(new SqlParameter("@sname", stadium_Name));
            SqlParameter status = checkstad.Parameters.Add("@stat", SqlDbType.Int);
            status.Direction = ParameterDirection.Output;


            conn.Open();

            checkstad.ExecuteNonQuery();

            if (status.Value.ToString() == "0")
            {
                Response.Write("Stadium Doesn't Exist");
            }
            else
            {
                DeleteStad.ExecuteNonQuery();
                Response.Write("Stadium is Deleted Successfully");
                deleteSname.Text = " ";

            }
            conn.Close();
        }

        protected void BlockFan_Click(object sender, EventArgs e)
        {
            string connstr = WebConfigurationManager.ConnectionStrings["m2"].ToString();
            SqlConnection conn = new SqlConnection(connstr);

            conn.Open();
            String F_id = BlockFnid.Text;
            SqlCommand ids = new SqlCommand("select * from Fan",conn);
            SqlDataReader idsInfo = ids.ExecuteReader();
            Boolean f = false;

            while (idsInfo.Read() && f==false)
            {
                String id = idsInfo.GetString(0);
                if (id==F_id)
                {
                    f = true;
                }

            }

            conn.Close();

            conn.Open();
            SqlCommand BlockFan = new SqlCommand("BlockFan", conn);
            BlockFan.CommandType = CommandType.StoredProcedure;
            BlockFan.Parameters.Add(new SqlParameter("@national_ID", F_id));

            

            if (f==false)
            {
                Response.Write("Fan Doesn't Exist");
            }
            
            else
            {
                BlockFan.ExecuteNonQuery();
                Response.Write("Fan is Blocked Successfully");
                BlockFnid.Text = " ";

            }
            conn.Close();



        }

        protected void LogOutSA_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
    }
}