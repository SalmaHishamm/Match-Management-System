using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Milestone3
{
    public partial class FanR : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*string connstr = WebConfigurationManager.ConnectionStrings["m2"].ToString();
            SqlConnection conn = new SqlConnection(connstr);
            SqlCommand FanR = new SqlCommand("addFan", conn);
            FanR.CommandType = CommandType.StoredProcedure;
            conn.Open();
            SqlDataReader */

        }

        protected void register_Click(object sender, EventArgs e)
        {
            string connstr = WebConfigurationManager.ConnectionStrings["m2"].ToString();
            SqlConnection conn = new SqlConnection(connstr);
            String Nid = NidF.Text;
            String N = NF.Text;
            String UN = UNF.Text;
            String P = PF.Text;
            String BD = BDF.Text;
            String Add = Address.Text;
            String PN = PhoneNumber.Text;


            SqlCommand addFanproc = new SqlCommand("addFan", conn);
            addFanproc.CommandType = CommandType.StoredProcedure;
            addFanproc.Parameters.Add(new SqlParameter("@name", N));
            addFanproc.Parameters.Add(new SqlParameter("@username", UN));
            addFanproc.Parameters.Add(new SqlParameter("@pass", P));
            addFanproc.Parameters.Add(new SqlParameter("@nationalid", Nid));
            addFanproc.Parameters.Add(new SqlParameter("@birthdate", BD));
            addFanproc.Parameters.Add(new SqlParameter("@address", Add));
            addFanproc.Parameters.Add(new SqlParameter("@phone", PN));


            SqlCommand checkuser = new SqlCommand("Allusers", conn);
            checkuser.CommandType = CommandType.StoredProcedure;
            checkuser.Parameters.Add(new SqlParameter("@username", UN));
            SqlParameter status = checkuser.Parameters.Add("@stat", SqlDbType.Int);
            status.Direction = ParameterDirection.Output;




            conn.Open();
            checkuser.ExecuteNonQuery();

            if (N.Length == 0 || Nid.Length == 0 || UN.Length == 0 || P.Length == 0 || BD.Length == 0 || Add.Length == 0 || PN.Length == 0)
                Response.Write("Please Fill All Requirements");
            else if (CheckFan2(Nid)==true )
                Response.Write("This Fan already exist");

            else if (status.Value.ToString() == "1")
            {
                Response.Write("This username already exist");
            }
            

            else
            {
                addFanproc.ExecuteNonQuery();
                Response.Write("Registeration Done");

            }

            conn.Close();


        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("login.aspx");
        }
   
    protected Boolean CheckFan2(String nid)
    {
        string connstr = WebConfigurationManager.ConnectionStrings["m2"].ToString();
        SqlConnection conn = new SqlConnection(connstr);

        SqlCommand checkfan = new SqlCommand("checkfan2", conn);
        checkfan.CommandType = CommandType.StoredProcedure;
        checkfan.Parameters.Add(new SqlParameter("@national", nid));
        SqlParameter stat = checkfan.Parameters.Add("@stat", SqlDbType.Int);
        stat.Direction = ParameterDirection.Output;

        conn.Open();
        checkfan.ExecuteNonQuery();
        if (stat.Value.ToString() == "0")
            return false;
        return true;

    }
    }
}