using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Milestone3
{
    public partial class CrR : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Rcr_Click(object sender, EventArgs e)
        {
            string connstr = WebConfigurationManager.ConnectionStrings["m2"].ToString();
            SqlConnection conn = new SqlConnection(connstr);
            String N = Ncr.Text;
            String CN = CNcr.Text;
            String UN = Ucr.Text;
            String PCR = Pcr.Text;

            SqlCommand addRepresentativeproc = new SqlCommand("addRepresentative", conn);
            addRepresentativeproc.CommandType = CommandType.StoredProcedure;
            addRepresentativeproc.Parameters.Add(new SqlParameter("@crname", N));
            addRepresentativeproc.Parameters.Add(new SqlParameter("@cname", CN));
            addRepresentativeproc.Parameters.Add(new SqlParameter("@suname", UN));
            addRepresentativeproc.Parameters.Add(new SqlParameter("@password", PCR));



            SqlCommand checkuser = new SqlCommand("Allusers", conn);
            checkuser.CommandType = CommandType.StoredProcedure;
            checkuser.Parameters.Add(new SqlParameter("@username", UN));
            SqlParameter status = checkuser.Parameters.Add("@stat", SqlDbType.Int);
            status.Direction = ParameterDirection.Output;

            SqlCommand checkclub = new SqlCommand("checkClubname", conn);
            checkclub.CommandType = CommandType.StoredProcedure;
            checkclub.Parameters.Add(new SqlParameter("@cname", CN));
            SqlParameter status2 = checkclub.Parameters.Add("@stat", SqlDbType.Int);
            status2.Direction = ParameterDirection.Output;


            conn.Open();
            checkuser.ExecuteNonQuery();
            checkclub.ExecuteNonQuery();
            conn.Close();
            conn.Open();
            if (status.Value.ToString() == "1")
            {
                Response.Write("This username already exist");
            }
            else if (N.Length == 0 || CN.Length == 0 || UN.Length == 0 || PCR.Length == 0) {
                Response.Write("Please Fill All Requirements");
            }
            else if (status2.Value.ToString() == "0")
            {
                Response.Write("Club name doesn't exist");
            }
           
            else
            {
                addRepresentativeproc.ExecuteNonQuery();
                Response.Write("Registeration Done");
            }
            conn.Close();

        }

        protected void Rcr_Click2(object sender, EventArgs e)
        {
            Response.Redirect("login.aspx");
        }
    }
}