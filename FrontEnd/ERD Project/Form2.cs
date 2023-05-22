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

//Jayden Brown
//Stored Procedure for ERD

namespace ERD_Project
{
    public partial class Form2 : Form
    {


        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void buildAndDisplay(int pTITLE_ID)
        {
            try
            {
                //Step 1: Declare local variables for data access objects
                //   DataReader is instantiated by invoking "ExecuteReader" method
                //   on Command object

                //   SQL string is not passed as an argument to the constructor of
                //   the Command object...SP name will be assigned to the 
                //   Command object's Text property
                //   Also, the connection is assigned to the "Connection" property
                //   of the Command object ... not passed as second argument
                //   in constructor
                SqlConnection objConnection = new SqlConnection("Data Source=(local);initial catalog=ERD; User ID=sa;Password=SQL2022");
                SqlCommand objCommand = new SqlCommand();
                SqlDataReader objReader;

                //Step 2:  Assign property values to Coomand object
                objCommand.Connection = objConnection;
                objCommand.CommandText = "procHRFronendOpt3";
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Parameters.AddWithValue("@x", pTITLE_ID);//Name of
                //parameter(s) must match name in SP... data types must also match

                //Step 3: Make DB connection
                objConnection.Open();

                //Step 4. Tell DBMS to execute SP and instantiate DataReader Class
                objReader = objCommand.ExecuteReader();




                //Step 5: Fetch data from result set 1 row at a time 
                //(connection open throughout).  Exit loop when reader fails.
                while (objReader.Read())
                {
                    listBox1.Items.Add(objReader[0].ToString().PadRight(50) + objReader[1].ToString().PadRight(55) + objReader[2].ToString().PadLeft(5));

                }

                //objConnection.Close(); //unnecessary because local variable loses scope at end of

            }

            catch (Exception e)
            {

                MessageBox.Show(e.ToString());
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text.Length > 0)
                {
                    int id = Convert.ToInt32(textBox1.Text);
                    buildAndDisplay(id);
                    button1.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Please Enter ID");
                    textBox1.Focus();
                }


            }
            catch
            {
                MessageBox.Show("Enter valid EMPLOYEE_ID");
                textBox1.Text = "";
                textBox1.Focus();
            }
        }
    }

}


