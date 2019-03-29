using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RoomRservation
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        public object Sqlcommand { get; private set; }

        private void btnAdd_Click(object sender, EventArgs e)
        {
           DialogResult d = MessageBox.Show("Are you sure you want to add this user ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (d.Equals(DialogResult.Yes)){
                addUser();
            }
       

        }
        private void addUser()
        {

            String userName = txtUn.Text;
            String password = txtPw.Text;

            String q = "insert into Users(userName,password) values ('" + userName + "','" + password + "')";

            try
            {
                using (DBConect db = new DBConect())
                {

                    db.insert(q);
                    //SqlCommand cmd = new SqlCommand(q, db.con);
                    //cmd.ExecuteNonQuery();
                    //MessageBox.Show("User inserted successfully", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //clearTexts();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
                

        }

        private void clearTexts()
        {
            txtPw.Clear();
            txtUn.Clear();
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {


            String userName = txtUn.Text;
            String password = txtPw.Text;
            String pass = null ;
            String q = "select password from Users where userName = '" + userName + "'";

            using (DBConect db = new DBConect())
            {
                MySqlCommand cmd = new MySqlCommand(q, db.con);
                MySqlDataReader r = cmd.ExecuteReader();

                if (r.HasRows)
                {
                    while (r.Read())
                    {
                        pass = r["password"].ToString();
                    }

                    if (pass.Equals(password)){
                        frmMain m = new frmMain();
                        this.Hide();
                        m.Show();
                    }
                    else {
                        MessageBox.Show("Invalid Password");
                    }
                }else
                {
                    MessageBox.Show("Invalid user name");
                }
            }

        }
    }
}
