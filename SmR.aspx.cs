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
    public partial class SmR : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Rsm_Click(object sender, EventArgs e)
        {
            string connstr = WebConfigurationManager.ConnectionStrings["m2"].ToString();
            SqlConnection conn = new SqlConnection(connstr);
            String N = Nsm.Text;
            String sn = SN.Text;
            String USM = Usm.Text;
            String PSM = Psm.Text;

            SqlCommand addStadiumManagerproc= new SqlCommand("addStadiumManager ", conn);
            addStadiumManagerproc.CommandType = CommandType.StoredProcedure;
            addStadiumManagerproc.Parameters.Add(new SqlParameter("@name", N));
            addStadiumManagerproc.Parameters.Add(new SqlParameter("@stadiumname", sn));
            addStadiumManagerproc.Parameters.Add(new SqlParameter("@username", USM));
            addStadiumManagerproc.Parameters.Add(new SqlParameter("@password", PSM));

            SqlCommand checkuser = new SqlCommand("Allusers", conn);
            checkuser.CommandType = CommandType.StoredProcedure;
            checkuser.Parameters.Add(new SqlParameter("@username", USM));
            SqlParameter status = checkuser.Parameters.Add("@stat", SqlDbType.Int);
            status.Direction = ParameterDirection.Output;

            SqlCommand checkstad = new SqlCommand("checkStadiumname", conn);
            checkstad.CommandType = CommandType.StoredProcedure;
            checkstad.Parameters.Add(new SqlParameter("@sname", sn));
            SqlParameter status2 = checkstad.Parameters.Add("@stat", SqlDbType.Int);
            status2.Direction = ParameterDirection.Output;

            
            conn.Open();
            checkuser.ExecuteNonQuery();
            checkstad.ExecuteNonQuery();
            if (N.Length == 0 || sn.Length == 0 || USM.Length == 0 || PSM.Length == 0)
                Response.Write("Please Fill All Requirements");
            else if (status.Value.ToString() == "1")
            {
                Response.Write("This username already exist");
            }
            else if (status2.Value.ToString() == "0")
            {
                Response.Write("Stadium name doesn't exist");
            }
           
            else
            {
                addStadiumManagerproc.ExecuteNonQuery();
                Response.Write("Registeration Done");
            }

            conn.Close();


        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("login.aspx");
        }
    }
}