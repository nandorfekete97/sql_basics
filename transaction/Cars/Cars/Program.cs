using Npgsql;
using System.Data;
using System.Runtime.CompilerServices;

namespace Cars
{
    class Program
    {
        static void UpdateCar(string connectionString, int carId, int ownerId)
        {
            const string updateCommand = @"UPDATE car SET owner_id = @owner_id
                                        WHERE id = @id";
            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    NpgsqlTransaction transaction = connection.BeginTransaction();

                    var cmd = new NpgsqlCommand(updateCommand, connection, transaction);
                    
                    cmd.Parameters.AddWithValue("@owner_id", ownerId);
                    cmd.Parameters.AddWithValue("@id", carId);

                    cmd.ExecuteNonQuery();
                    transaction.Commit();
 
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void Main(string[] args)
        {
            string connectionString = Environment.GetEnvironmentVariable("CARS_DB_CONNECTION_STRING");

            if(connectionString == null)
            {
                Console.WriteLine("CARS_DB_CONNECTION_STRING environment variable is not set");
                return;
            }

            UpdateCar(connectionString, 1, 3);            
        }
    }
}

         