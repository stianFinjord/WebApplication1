using System.Data.SqlClient;
using Dapper;

namespace WebApplication1
{
    public class DataManager
    {
        private static string connectionString = "Server=(localdb)\\local;Database=GETDb;Trusted_Connection=True;";
       

        public static Car GetCar()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string query = "SELECT * FROM Cars";
            var result = connection.Query<Car>(query).ToList();
            return result[0];


        }

        public static void GetAllUsers()
        {

        }

        public static void GetUser(int userId)
        {

        }

        public static void GetFriends(int loggedInUser)
        {

        }

        //public static User GetUser()
        //{
        //    return;
        //}
    }
}
