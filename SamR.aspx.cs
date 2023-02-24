using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;

namespace Milestone3
{
    public partial class SamR : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Rsam_Click(object sender, EventArgs e)
        {
            string connstr = WebConfigurationManager.ConnectionStrings["m2"].ToString();
            SqlConnection conn = new SqlConnection(connstr);
            String N = Nsam.Text;
            String U = Usam.Text;
            String P = Psam.Text;
           

            SqlCommand addAssociationManagerproc = new SqlCommand("addAssociationManager", conn);
            addAssociationManagerproc.CommandType = CommandType.StoredProcedure;
            addAssociationManagerproc.Parameters.Add(new SqlParameter("@name", N));
            addAssociationManagerproc.Parameters.Add(new SqlParameter("@username", U));
            addAssociationManagerproc.Parameters.Add(new SqlParameter("@password", P));



            SqlCommand checkuser = new SqlCommand("Allusers", conn);
            checkuser.CommandType = CommandType.StoredProcedure;
            checkuser.Parameters.Add(new SqlParameter("@username", U));
            SqlParameter status = checkuser.Parameters.Add("@stat", SqlDbType.Int);
            status.Direction = ParameterDirection.Output;




            conn.Open();
            checkuser.ExecuteNonQuery();


            if (status.Value.ToString() == "1")
            {
                Response.Write("This username already exist");
            }
            else if (N.Length == 0 || P.Length == 0 || U.Length==0)
                Response.Write("Please Fill All Requirements");

            else
            {
                addAssociationManagerproc.ExecuteNonQuery();
                Response.Write("Registeration Done");

            }

            conn.Close();

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("login.aspx");
        }
    }
}