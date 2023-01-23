using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;

namespace DataSetExample
{
    public partial class Form1 : Form
    {
        DataSet ds;
        public Form1()
        {
            InitializeComponent();
        }

        private void Fill_Click(object sender, EventArgs e)
        {
            //to do here
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=JkJan23;Integrated Security=True;";
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;

                ds = new DataSet();

                cmd.CommandText = "select * from Employees";
                da.Fill(ds, "Emps");

                cmd.CommandText = "select * from Departments";
                da.Fill(ds, "Deps");

                //primary key constraint
                DataColumn[] arrCols = new DataColumn[1];
                arrCols[0] = ds.Tables["Emps"].Columns["EmpNo"];
                ds.Tables["Emps"].PrimaryKey = arrCols;


                //foreign key constraint
                ds.Relations.Add(ds.Tables["Deps"].Columns["DeptNo"],
                    ds.Tables["Emps"].Columns["DeptNo"]);

                //column level constraint
                ds.Tables["Deps"].Columns["DeptName"].Unique = true;

                //dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.DataSource = ds.Tables["Emps"];
                //dataGridView1.DataSource = ds.Tables["Deps"];

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cn.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            //to do here
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=JkJan23;Integrated Security=True;";
            try
            {
                cn.Open();
                SqlCommand cmdUpdate = new SqlCommand();
                cmdUpdate.Connection = cn;
                cmdUpdate.CommandType = CommandType.Text;
                cmdUpdate.CommandText = "update Employees set Name=@Name,basic=@Basic,DeptNo=@DeptNo where EmpNo=@EmpNo";
                 
                // INSERT Record
                SqlCommand cmdInsert = new SqlCommand();
                cmdInsert.Connection = cn;
                cmdInsert.CommandType = CommandType.Text;
                cmdInsert.CommandText = "insert into Employees values(@EmpNo, @Name, @Basic, @DeptNo)";;

                //DELETE Record
                SqlCommand cmdDelete = new SqlCommand();
                cmdDelete.Connection = cn;
                cmdDelete.CommandType = CommandType.Text;
                cmdDelete.CommandText = "Delete from Employees where EmpNo = @EmpNo"; 

                //SqlParameter p;
                //p = new SqlParameter();
                //p.ParameterName = "@Name";
                //p.SourceColumn = "Name";
                // p.SourceVersion = DataRowVersion.Current;
                // below line same as above 
                cmdUpdate.Parameters.Add(new SqlParameter { ParameterName = "@Name", SourceColumn = "Name", SourceVersion = DataRowVersion.Current });
                cmdUpdate.Parameters.Add(new SqlParameter { ParameterName = "@Basic", SourceColumn = "Basic", SourceVersion = DataRowVersion.Current });
                cmdUpdate.Parameters.Add(new SqlParameter { ParameterName = "@DeptNo", SourceColumn = "DeptNo", SourceVersion = DataRowVersion.Current });
                cmdUpdate.Parameters.Add(new SqlParameter { ParameterName = "@EmpNo", SourceColumn = "EmpNo", SourceVersion = DataRowVersion.Original });

                // TO DO - INSERT
                cmdInsert.Parameters.Add(new SqlParameter { ParameterName = "@Name", SourceColumn = "Name", SourceVersion = DataRowVersion.Current });
                cmdInsert.Parameters.Add(new SqlParameter { ParameterName = "@Basic", SourceColumn = "Basic", SourceVersion = DataRowVersion.Current });
                cmdInsert.Parameters.Add(new SqlParameter { ParameterName = "@DeptNo", SourceColumn = "DeptNo", SourceVersion = DataRowVersion.Current });
                cmdInsert.Parameters.Add(new SqlParameter { ParameterName = "@EmpNo", SourceColumn = "EmpNo", SourceVersion = DataRowVersion.Current });

                //TODO - DELETE
                
                cmdDelete.Parameters.Add(new SqlParameter { ParameterName = "@EmpNo", SourceColumn = "EmpNo", SourceVersion = DataRowVersion.Current });

                //cmdUpdate.Parameters.Add(p);

                SqlDataAdapter da = new SqlDataAdapter();
                da.UpdateCommand = cmdUpdate;
                da.InsertCommand = cmdInsert;
                da.DeleteCommand = cmdDelete;

                da.Update(ds, "Emps");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cn.Close();
            }
        }
    }
}