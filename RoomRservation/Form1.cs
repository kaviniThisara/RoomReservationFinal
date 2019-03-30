using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RoomRservation
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }


        private void btnHome_Click(object sender, EventArgs e)
        {
            pnlHome.BringToFront();
        }

        private void btnAvailability_Click(object sender, EventArgs e)
        {
            pnlAvailbility.BringToFront();
        }


        private void frmMain_Load(object sender, EventArgs e)
        {
            pnlHome.BringToFront();
        }


        private void btnBooking_Click(object sender, EventArgs e)
        {
            pnlForm.BringToFront();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

        }
  

      
                                        //method call

      

        private void clearAddCustomerForm()
        {
            txtboxContactID.Clear();
            txtboxFirstName.Clear();
            txtboxLastName.Clear();
            txtboxAddress.Clear();
            txtboxContactNumber.Clear();
            cmbRoomType.SelectedIndex = 0;
            cmbRoom.SelectedIndex = 0;
      
        }

     

        private void btnSubmit_Click_1(object sender, EventArgs e)
        {

           
            ContactClass c = new ContactClass();
            //c.ContactID = Int32.Parse(txtboxContactID.Text);
            c.FirstName = txtboxFirstName.Text;
            c.LastName = txtboxLastName.Text;
            c.ContactNo = txtboxContactNumber.Text;
            c.Address = txtboxAddress.Text;
            String gender = "";

            if (radioMale.Checked)
            {
                gender = "Male";
            }
            else if (radioFemale.Checked)
            {
                gender = "Female";

            }

            c.Gender = gender;
            c.DateOfBirth = dob.Value.ToShortDateString();
            
            c.RoomType = cmbRoomType.Text;
            // c.Room = cmbRoom.Text;
            c.Room = Int32.Parse(cmbRoom.Text);
         





            if (!Validation.validateName(c.FirstName) || c.FirstName.Equals(String.Empty))
            {
                MessageBox.Show("Please enter a valid first Name");
            }
            else if (!Validation.validateName(c.LastName) || c.LastName.Equals(String.Empty))
            {
                MessageBox.Show("Please enter a valid Last Name");
            }
            else if (!Validation.validatePhoneNo(c.ContactNo) || c.ContactNo.Equals(String.Empty))
            {
                MessageBox.Show("Please enter a valid Phone number");
            }
             else if (c.RoomType == "")
            {
                MessageBox.Show("Please enter a valid Room Type");
            }
            else if (!radioMale.Checked && !radioFemale.Checked ){
                MessageBox.Show("You forgot to select the gender!");
            }
            else
            {

                c.InsertCustomer();
                getRoomPrice();


            }

            //clearAddCustomerForm();
        }

        private void getRoomPrice()
        {
            String q = "select Price from rooms where Type = '" + cmbRoomType.Text + "'";
            using (DBConect db = new DBConect())
            {
                MySqlCommand cmd = new MySqlCommand(q, db.con);
                MySqlDataReader r = cmd.ExecuteReader();
                if (r.HasRows)
                {
                    while(r.Read()){
                        lblPrice1.Text = (Double.Parse(r[0].ToString()) * Int32.Parse(cmbRoom.Text)).ToString() ;
                    }
                }
            }
        }

        private void btnSearch_Click_1(object sender, EventArgs e)
        {

            ContactClass c = new ContactClass();
            ContactClass d = new ContactClass();
            d = c.Search("ContactID = '" + txtboxContactID.Text + "'");

            txtboxFirstName.Text = d.FirstName;
            txtboxLastName.Text = d.LastName;
            txtboxContactNumber.Text = d.ContactNo;
            txtboxAddress.Text = d.Address;

            if (d.Gender.Equals("Male"))
            {
                radioMale.Checked = true;
            }
            else
            {
                radioFemale.Checked = false;
            }

              dob.Value = Convert.ToDateTime(d.DateOfBirth);
            // new DateTime(int year, int month, int date);
          //  c.DateOfBirth = dob.Value.ToString("yyyy-MM-dd");
           
           // txtAge.Text = d.age;
          
            cmbRoomType.Text = d.RoomType;
            cmbRoom.Text = d.Room.ToString();
           
           

        }

        private void btnViewAll_Click_1(object sender, EventArgs e)
        {

            dgvAllCustomers.DataSource = loadAllCustomers();

        }
        public DataTable loadAllCustomers()
        {
            DataTable dt = new DataTable();

            using (DBConect db = new DBConect())
            {
                String q = "select ContactID,FirstName,LastName from Customers";
                MySqlCommand cmd = new MySqlCommand(q, db.con);
                MySqlDataReader r = cmd.ExecuteReader();
                dt.Load(r);
            }
            return dt;
        }

        private void btnUpdate_Click_1(object sender, EventArgs e)
        {
            ContactClass c = new ContactClass();
            c.ContactID = Int32.Parse(txtboxContactID.Text);
            c.FirstName = txtboxFirstName.Text;
            c.LastName = txtboxLastName.Text;
            c.ContactNo = txtboxContactNumber.Text;
            c.Address = txtboxAddress.Text;
            String gender = "";

            if (radioMale.Checked)
            {
                gender = "Male";
            }
            else if (radioFemale.Checked)
            {
                gender = "Female";

            }

            c.Gender = gender;
            c.DateOfBirth = dob.Value.ToString("yyyy-MM-dd");
          
            c.RoomType = cmbRoomType.Text;
            //  c.Room = cmbRoom.Text;
            c.Room = Int32.Parse(cmbRoom.Text);
           


            //validation




            if (!Validation.validateName(c.FirstName) || c.FirstName.Equals(String.Empty))
            {
                MessageBox.Show("Please enter a valid first Name");
            }
            else if (!Validation.validateName(c.LastName) || c.LastName.Equals(String.Empty))
            {
                MessageBox.Show("Please enter a valid Last Name");
            }
            else if (!Validation.validatePhoneNo(c.ContactNo) || c.ContactNo.Equals(String.Empty))
            {
                MessageBox.Show("Please enter a valid Phone number");
            }
            else if (c.RoomType == "")
            {
                MessageBox.Show("Please enter a valid Room Type");
            }
            else if (!radioMale.Checked && !radioFemale.Checked)
            {
                MessageBox.Show("You forgot to select the gender!");
            }
            else
            {
                c.UpdateCustomer();

                clearAddCustomerForm();
            }
        }
        
        private void btnTotal_Click(object sender, EventArgs e)
        {
            if (Validation.validateDiscountText(txtDiscount.Text)){
                double discount;

                if (!txtDiscount.Text.Equals(""))
                {
                    discount = Double.Parse(txtDiscount.Text);
                }
                else
                {
                    discount = 1;
                }
                double price = Double.Parse(lblPrice1.Text) * Int32.Parse(cmbRoom.Text);
                double discountPrice = Double.Parse(lblPrice1.Text) * Int32.Parse(cmbRoom.Text) * discount / 100;
                double totalPrice = Double.Parse(lblPrice1.Text )* Int32.Parse(cmbRoom.Text) - discountPrice;
                lblTotal1.Text = totalPrice.ToString();
            }else
            {
                MessageBox.Show("Disount should be a number between 0 and 100", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

      

        private void chkDiscount_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDiscount.Checked)
            {
                lblDiscount.Enabled = true;
                txtDiscount.Enabled = true;
            }
            else
            {
                lblDiscount.Enabled = false;
                txtDiscount.Enabled = false;
            }
        }

       

        private void btnClear_Click(object sender, EventArgs e)
        {
            clearAddCustomerForm();
        }
        
        private void btnCheckForRoomType_Click(object sender, EventArgs e)
        {
            dgvAvailability.DataSource = getAvailabilityForRoomType(cmbAvailableType.Text);
        }

        private DataTable getAvailabilityForRoomType(String type)
        {
            String q;
            if (type.Equals(""))
            {
                q = "select r.RoomID as 'Room ID',r.Type as 'Room Type',c.FirstName as 'First Name', b.CheckIn as 'Check In', b.CheckOut as 'Check Out' from rooms r,room_booking b,customers c where r.RoomID = b.RoomID AND c.ContactID = b.CustomerID";
            }else
            {
                q = "select r.RoomID as 'Room ID',r.Type as 'Room Type',c.FirstName as 'First Name', b.CheckIn as 'Check In', b.CheckOut as 'Check Out' from rooms r,room_booking b,customers c where r.RoomID = b.RoomID AND c.ContactID = b.CustomerID AND r.Type = '" + type + "'";
            }

            
            DataTable dt = new DataTable();

            using (DBConect db = new DBConect())
            {
                MySqlCommand cmd = new MySqlCommand(q, db.con);
                MySqlDataReader r = cmd.ExecuteReader();

                dt.Load(r);
                return dt;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
             ContactClass c = new ContactClass();
            c.ContactID = Int32.Parse(txtboxContactID.Text);
           
            c.deleteCustomer();
            clearAddCustomerForm();
            dgvAllCustomers.DataSource = loadAllCustomers();
        }

       
    }
       
}
