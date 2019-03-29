using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomRservation
{
    class Rooms
    {
        public String CheckIn { get; set; }
        public String CheckOut { get; set; }
        public String Roomtypess { get; set; }
        public int RoomNo { get; set; }
   

    public void InsertRooms()
    {
        //int ContactId = txtUn;
        //String FirstName = txtFn;
        //String LastName = txtLn;
        //String ContactNo = txtCn;
        //String Address = txtAd;
        //String Gender = txtGn;
        //String DateOfBirth = txtDob;
        //String RoomType = txtRt;
        //String Room = txtRo;


        String q = "insert into Rooms(CheckIn,CheckOut,Roomtypess,RoomNo) values ('" +this.CheckIn + "','" + this.CheckOut + "','" + Roomtypess + "','" + RoomNo + "')";

        try
        {
            using (DBConect db = new DBConect())
            {

                bool ok = db.insert(q);

             /*   if (ok)
                {
                    
                    MessageBox.Show("Room inserted successfully", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Room insertion failed", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }*/

                //SqlCommand cmd = new SqlCommand(q, db.con);
                //cmd.ExecuteNonQuery();
                //MessageBox.Show("User inserted successfully", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //clearTexts();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.StackTrace);
        }


    }
}
}
