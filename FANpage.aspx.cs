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
    public partial class FANpage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string connstr = WebConfigurationManager.ConnectionStrings["m2"].ToString();
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();
            String sql = "Select * from availableMatcheswizTickets";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            Grid2.DataSource = reader;
            Grid2.DataBind();
            conn.Close();
        }

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Response.Write("St");
            GridViewRow grid = Grid2.SelectedRow;
            string connstr = WebConfigurationManager.ConnectionStrings["m2"].ToString();
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();

            String fid =Session["Fan_NID"].ToString();

            String Hc = grid.Cells[1].Text;
            String Gc = grid.Cells[2].Text;
            String St = grid.Cells[3].Text;
            DateTime st2 = Convert.ToDateTime(St);
            




            SqlCommand purchareproc = new SqlCommand("purchaseTicket", conn);
            purchareproc.CommandType = CommandType.StoredProcedure;
            purchareproc.Parameters.Add(new SqlParameter("@fnationalid", fid));
            purchareproc.Parameters.Add(new SqlParameter("@clubh", Hc));
            purchareproc.Parameters.Add(new SqlParameter("@clubg", Gc));
            purchareproc.Parameters.Add(new SqlParameter("@timest", st2));


            purchareproc.ExecuteNonQuery();
            Response.Write("Ticket Purchased");
            String sql = "Select * from availableMatcheswizTickets";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            Grid2.DataSource = reader;
            Grid2.DataBind();

            //  Response.Write(fid + " 111");

            //Grid2.DataBind();
            conn.Close();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string connstr = WebConfigurationManager.ConnectionStrings["m2"].ToString();
            SqlConnection conn = new SqlConnection(connstr);

            String date = datestart.Text;

            SqlCommand chechkt = new SqlCommand("checkST", conn);
            chechkt.CommandType = CommandType.StoredProcedure;
            chechkt.Parameters.Add(new SqlParameter("@st", date));
            SqlParameter status3 = chechkt.Parameters.Add("@stat", SqlDbType.Int);
            status3.Direction = ParameterDirection.Output;

            conn.Open();
            chechkt.ExecuteNonQuery();
            conn.Close();
            if (date.Length == 0)
                Response.Write(" Please Type Date ");
            else if (status3.Value.ToString() == "0")
            {
                Response.Write("There Is No Match At This Time");
            }
            else
            {


                conn.Open();

                String sql = "Select * from dbo.availableMatchesToAttend3(@time)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@time", date);

                SqlDataReader reader = cmd.ExecuteReader();

                GridView1.DataSource = reader;
                GridView1.DataBind();
                conn.Close();
            }
        }
        protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        

        protected void GridView1_SelectedIndexChanged1(object sender, EventArgs e)
        {
            //Response.Write("ss");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("login.aspx");
        }
    }
}