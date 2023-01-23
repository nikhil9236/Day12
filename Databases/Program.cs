using Microsoft.Data.SqlClient;
using System.Data;
namespace Databases
{
    internal class Program
    {
        static void Main()
        {
            //Connect();
            //Insert();
            Employee obj = new Employee { EmpNo = 7, Name = "Peter D'Silva", Basic = 12345, DeptNo = 20 };
            //InsertWithParameters(obj);
            InsertWithStoredProcedure(obj);
        }
        static void Connect()
        {
            SqlConnection cn = new SqlConnection();
            //Data Source=(localdb)\ProjectModels;Initial Catalog=JkJan23;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False

            //cn.ConnectionString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=JkJan23;User Id=root;Password=password";
            cn.ConnectionString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=JkJan23;ntegrated Security=True;";
            try
            {
                cn.Open();
                Console.WriteLine("okay");
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
            
            cn.Close();
        }
        static void Insert()
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=JkJan23;integrated Security=True;";
            try
            {
                cn.Open();
                SqlCommand cmdInsert = new SqlCommand();
                cmdInsert.Connection = cn;
                cmdInsert.CommandType = CommandType.Text;
                cmdInsert.CommandText = "insert into Employees values(4,'yogesh',12345,10)";
                
                
                cmdInsert.ExecuteNonQuery();
                Console.WriteLine("okay");
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }

            cn.Close();
        }
        static void InsertEmployee(Employee obj)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=JkJan23;Integrated Security=True;";
            try
            {
                cn.Open();
                SqlCommand cmdInsert = new SqlCommand();
                cmdInsert.Connection = cn;
                cmdInsert.CommandType = CommandType.Text;
                cmdInsert.CommandText = $"insert into Employees values ({obj.EmpNo}, '{obj.Name}', {obj.Basic}, {obj.DeptNo})";

                Console.WriteLine(cmdInsert.CommandText);
                Console.ReadLine();
                cmdInsert.ExecuteNonQuery();
                Console.WriteLine("okay");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                cn.Close();
            }
        }
        static void InsertWithParameters(Employee obj)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=JkJan23;Integrated Security=True;";
            try
            {
                cn.Open();
                SqlCommand cmdInsert = new SqlCommand();
                cmdInsert.Connection = cn;
                cmdInsert.CommandType = CommandType.Text;
                cmdInsert.CommandText = "insert into Employees values (@EmpNo,@Name,@Basic,@DeptNo)";
                //update Employees set Name=@Name,Basic=@Basic,DeptNo=@DeptNo where EmpNo=@EmpNo
                //delete from Employees where EmpNo=@EmpNo
                cmdInsert.Parameters.AddWithValue("@EmpNo", obj.EmpNo);
                cmdInsert.Parameters.AddWithValue("@Name", obj.Name);
                cmdInsert.Parameters.AddWithValue("@Basic", obj.Basic);
                cmdInsert.Parameters.AddWithValue("@DeptNo", obj.DeptNo);


                cmdInsert.ExecuteNonQuery();
                Console.WriteLine("okay");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                cn.Close();
            }
        }
        static void InsertWithStoredProcedure(Employee obj)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=JkJan23;Integrated Security=True;";
            try
            {
                cn.Open();
                SqlCommand cmdInsert = new SqlCommand();
                cmdInsert.Connection = cn;
                cmdInsert.CommandType = CommandType.StoredProcedure;
                cmdInsert.CommandText = "InsertEmployee";
                cmdInsert.Parameters.AddWithValue("@EmpNo", obj.EmpNo);
                cmdInsert.Parameters.AddWithValue("@Name", obj.Name);
                cmdInsert.Parameters.AddWithValue("@Basic", obj.Basic);
                cmdInsert.Parameters.AddWithValue("@DeptNo", obj.DeptNo);


                cmdInsert.ExecuteNonQuery();
                Console.WriteLine("okay");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                cn.Close();
            }
        }
    }

    public class Employee
    {
        public int EmpNo { get; set; }
        public string Name { get; set; }

        public decimal Basic { get; set; }
        public int DeptNo { get; set; }


    }
}
