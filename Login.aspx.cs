using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Milestone3
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void login (object sender, EventArgs e)
        {
            string connstr = WebConfigurationManager.ConnectionStrings["m2"].ToString();
            SqlConnection conn = new SqlConnection(connstr);

            String user = username.Text;
            String pass = password.Text;

            SqlCommand loginproc = new SqlCommand("login", conn);
            loginproc.CommandType = CommandType.StoredProcedure;
            loginproc.Parameters.Add(new SqlParameter("@username", SqlDbType.VarChar)).Value =user  ;
            loginproc.Parameters.Add(new SqlParameter("@password", SqlDbType.VarChar)).Value = pass;
            SqlParameter status = loginproc.Parameters.Add("@status", SqlDbType.Int);
            SqlParameter type = loginproc.Parameters.Add("@type", SqlDbType.VarChar,30);
            status.Direction = ParameterDirection.Output;
            type.Direction = ParameterDirection.Output;

            conn.Open();
            loginproc.ExecuteNonQuery();
            
            if (status.Value.ToString() == "1")
            {

                if (type.Value.ToString() == "Fan")
                {
                    if (CheckFan(getNationalId(user)))
                    {
                        Session["Fan_NID"] = getNationalId(user);
                        Response.Redirect("FANpage.aspx");
                    }
                    else
                    {
                        Response.Write("This Fan isn't Allowed to Log in");
                    }

                }
                else if (type.Value.ToString() == "ClubRepresentative")
                {

                    Session["CR_Clubname"] = getClubname(user);
                    Session["CR_username"] = user;
                   // Response.Write(Session["CR_Clubname"]);
                    Response.Redirect("CRpage.aspx");

                }

                else if (type.Value.ToString() == "StadiumManager")
                {
                    Session["SM_Stadiumname"] = getStadiumName(user);
                    Session["SM_username"] = user;
                    Response.Redirect("SMpage.aspx");

                }

                else if (type.Value.ToString() == "SportsAssociationManager")
                {
                    Response.Redirect("SAMpage.aspx");

                }
                else if (type.Value.ToString() == "Systemadmin")
                    Response.Redirect("SApage.aspx");
            }
            
            else
            {

                Response.Write("Invalid Username or Password");
            }
            conn.Close();

        }

        protected void Fan_Click(object sender, EventArgs e)
        {
            Response.Redirect("FanR.aspx");
        }

        protected void SportsAssociationManager_Click(object sender, EventArgs e)
        {
            Response.Redirect("SamR.aspx");
        }

        protected void StadiumManager_Click(object sender, EventArgs e)
        {
            Response.Redirect("SmR.aspx");
        }

        protected void ClubRepresentative_Click(object sender, EventArgs e)
        {
            Response.Redirect("CrR.aspx");
        }
        protected String getNationalId (String username)
        {


            string connstr = WebConfigurationManager.ConnectionStrings["m2"].ToString();
            SqlConnection conn = new SqlConnection(connstr);
            SqlCommand myId = new SqlCommand("MyFanID", conn);
            myId.CommandType = CommandType.StoredProcedure;
            myId.Parameters.Add(new SqlParameter("@username", username));
            SqlParameter id = myId.Parameters.Add("@id", SqlDbType.VarChar, 20);
            id.Direction = ParameterDirection.Output;

            conn.Open();
            myId.ExecuteNonQuery();
            String idd = id.Value.ToString();
            return idd;

        }

        protected String getStadiumName(String username)
        {


            string connstr = WebConfigurationManager.ConnectionStrings["m2"].ToString();
            SqlConnection conn = new SqlConnection(connstr);
            SqlCommand mySN = new SqlCommand("MyStadiumName", conn);
            mySN.CommandType = CommandType.StoredProcedure;
            mySN.Parameters.Add(new SqlParameter("@username", username));
            SqlParameter sn = mySN.Parameters.Add("@sname", SqlDbType.VarChar, 20);
            sn.Direction = ParameterDirection.Output;

            conn.Open();
            mySN.ExecuteNonQuery();
            String snn = sn.Value.ToString();
            return snn;

        }
        protected String getClubname(String username)
        {

            string connstr = WebConfigurationManager.ConnectionStrings["m2"].ToString();
            SqlConnection conn = new SqlConnection(connstr);
            SqlCommand myCn = new SqlCommand("MyClubname", conn);
            myCn.CommandType = CommandType.StoredProcedure;
            myCn.Parameters.Add(new SqlParameter("@username", username));
            SqlParameter cn = myCn.Parameters.Add("@clubname", SqlDbType.VarChar, 20);
            cn.Direction = ParameterDirection.Output;

            conn.Open();
            myCn.ExecuteNonQuery();
            String cnn = cn.Value.ToString();
            return cnn;

        }
        protected Boolean CheckFan (String nid)
        {
            string connstr = WebConfigurationManager.ConnectionStrings["m2"].ToString();
            SqlConnection conn = new SqlConnection(connstr);

            SqlCommand checkfan = new SqlCommand("checkfan", conn);
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