using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace FirstTask_ADO.NET

{
    public class Application
    {
        SqlConnection? connection = null;
        public Application()
        {

            string connectionString = "Data Source=DESKTOP-IBRAHIM\\SQLEXPRESS;Initial Catalog=Library;Integrated Security=True;";
            connection = new SqlConnection();
            connection.ConnectionString = connectionString;


        }
        public void InsertDataToSql()
        {
            try
            {
                connection!.Open();
                string insertData = "INSERT INTO Authors (Id, FirstName, LastName) VALUES (15,'Ibrahim','Asadov')";
                using SqlCommand cmd = new SqlCommand(insertData,connection);
                cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection!.Close();
            }
        } 
        public void ReadDataToSql()
        {
            try
            {
                connection!.Open();
                string readData = "SELECT * FROM Authors";
                using SqlCommand cmd = new SqlCommand(readData, connection);
                using SqlDataReader reader = cmd.ExecuteReader();

                while(reader.Read())
                {
                    Console.WriteLine($"{reader[0]}   {reader[1]}   {reader[2]}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection!.Close();
            }

        }
        public void ReadDataFromSqlWayTwo()
        {
            try
            {
                connection!.Open();
                SqlDataReader? reader = null;
                string readData = "SELECT * FROM Authors";
                using SqlCommand cmd = new SqlCommand(readData, connection);
                reader= cmd.ExecuteReader();
                List<string> columnName = new();
                int line = 0;
                while (reader.Read())
                {
                    if (line == 0)
                    {
                        columnName.Add(reader.GetName(0));
                        columnName.Add(reader.GetName(1));
                        columnName.Add(reader.GetName(2));
                        Console.WriteLine($"{columnName[0]}   {columnName[1]}   {columnName[2]}");
                    }
                    line++;
                    Console.WriteLine($"{reader["Id"]}   {reader["FirstName"]}   {reader["LastName"]}");

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection!.Close();
            }

        }
        public void ReadDataFromSqlWayThree()
        {
            try
            {
                connection!.Open();
                SqlDataReader? reader = null;
                string readData = "SELECT * FROM Authors";
                using SqlCommand cmd = new SqlCommand(readData, connection);
                reader = cmd.ExecuteReader();
                List<string> columnName = new();
                int line = 0;
                while (reader.Read())
                {
                    if (line == 0)
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            columnName.Add(reader.GetName(i));
                            Console.Write(columnName[i] + "   ");
                        }
                        Console.WriteLine();
                    }
                    line++;

                    for (int i = 0; i <reader.FieldCount; i++)
                    {
                        Console.Write(reader[i] + "   ");
                    }
                    Console.WriteLine();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection!.Close();
            }

        }
        public void CreateProcedure()
        {
            try
            {
                connection!.Open();
                SqlCommand crProcedure = null;
                string readData = $"usp_getBooksNumber";
                using SqlCommand cmd=new SqlCommand(readData , connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AuthorId", SqlDbType.Int).Value = 4;
                SqlParameter sqlParameter = new("@BookCount", SqlDbType.Int);
                sqlParameter.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(sqlParameter);
                
                cmd.ExecuteNonQuery();

                Console.WriteLine(cmd.Parameters["@BookCount"].Value);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }




        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            Application application = new Application();
            //application.InsertDataToSql();
            //application.ReadDataToSql();
            //application.ReadDataFromSqlWayTwo();
            //application.ReadDataFromSqlWayThree();
            application.CreateProcedure();
        }
    }
}