using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using SqlDapperApp.Models;

namespace SqlDapperApp
{
    internal class Program
    {
        private static readonly string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=U:\Backend\Lektion-5\SqlDapperApp\Data\sqldapper_db.mdf;Integrated Security=True;Connect Timeout=30";
        static void Main(string[] args)
        {
           //InsertOne(new CreateUserModel { FirstName = "Arthur", LastName = "Mustonen", Email = "arthur.mustonen@gmail.com" });
           SelectAll();
           // SelectOne_ByEmail("arthur.mustonen@gmail.com");
           // SelectOne_ByID(2);
            //SelectId_ByEmail("oliver.mustonen@gmail.com");

            //UpdateEmail(2, "arthur.mustonen@gmail.com");
            //UpdateEmail(new UpdateUserEmailModel { SearchId = 2, Email = "tommy.mattin-lassei@domain.com" });
        }

        #region Methods
        static void InsertOne(CreateUserModel model)
        {
            using (IDbConnection conn = new SqlConnection(_connectionString))
            {

                conn.Execute("INSERT INTO Users (FirstName, LastName, Email) VALUES (@FirstName, @LastName, @Email)", model);
                Console.WriteLine("1 User has ben added to the Database");
            }
        }

        static void SelectAll()
        {
            using (IDbConnection conn = new SqlConnection(_connectionString))
            {
                var results = conn.Query<User>("SELECT * FROM Users").ToList();
                foreach (var user in results)
                {
                    Console.WriteLine($"UserId: {user.Id}\n{user.FullName}\n{user.Email}\n");
                }
            }
        }

        static void SelectOne_ByEmail(string email)
        {
            using (IDbConnection conn = new SqlConnection(_connectionString))
            {
                var result = conn.QueryFirstOrDefault<User>("SELECT * FROM Users WHERE Email = @Email", new { Email = email});

                Console.WriteLine($"UserId: {result.Id}\n{result.FullName}\n{result.Email}\n");
            }
        }

        static void SelectOne_ByID(int id)
        {
            using (IDbConnection conn = new SqlConnection(_connectionString))
            {
                var result = conn.QueryFirstOrDefault<User>("SELECT * FROM Users WHERE Id = @Id", new { Id = id });

                Console.WriteLine($"UserId: {result.Id}\n{result.FullName}\n{result.Email}\n");
            }
        }

        static void SelectId_ByEmail(string email)
        {
            using (IDbConnection conn = new SqlConnection(_connectionString))
            {
                var result = conn.QueryFirstOrDefault<int>("SELECT Id FROM Users WHERE Email = @Email", new { Email = email });

                Console.WriteLine($"UserId: {result}\n");
            }
        }

        static void UpdateEmail(int id, string email)
        {
            using (IDbConnection conn = new SqlConnection(_connectionString))
            {
                conn.Execute("UPDATE Users SET Email = @Email WHERE Id =@SearchId", new { SearchId = id, Email = email });

                Console.WriteLine("1 User has been added to the Database");
            }
        }

        static void UpdateEmail(UpdateUserEmailModel model)
        {
            using (IDbConnection conn = new SqlConnection(_connectionString))
            {
                conn.Execute("UPDATE Users SET Email = @Email WHERE Id = @SearchId", model);
                Console.WriteLine("1 User has been added to the Database");
            }
        }


        #endregion
    }
}
