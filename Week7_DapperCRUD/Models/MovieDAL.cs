using Dapper;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Week7_DapperCRUD.Models
{
    public class MovieDAL
    {
        //This will contain all of our code for talking to the database.
        //It should be reuseable throughout the application.

        public List<Movie> GetMovies()
        {
            using (var connect = new MySqlConnection(Secret.Connection))
            {
                var sql = "select * from movies";
                connect.Open();
                List<Movie> movies = connect.Query<Movie>(sql).ToList();
                connect.Close();
                return movies;
            }
        }

        //Read single - take in an id and return the matching row
        public Movie GetMovies(int id)
        {
            using (var connect = new MySqlConnection(Secret.Connection))
            {
                var sql = "select * from movies where id=" + id;
                connect.Open();

                //Query always returns a list regardless of how many movies we want. 
                //Even if our query is meant to return 1 movie, we still need to pull it out of a list of count 1
                Movie m = connect.Query<Movie>(sql).First();
                connect.Close();
                return m;
            }
        }

        public void DeleteMovie(int id)
        {
            using (var connect = new MySqlConnection(Secret.Connection))
            {
                var sql = "delete from Movies where id=" + id;
                connect.Open();
                connect.Query<Movie>(sql);
                connect.Close();
            }
        }

        public void UpdateMovie(Movie m)
        {
            using (var connect = new MySqlConnection(Secret.Connection))
            {
                string sql = "update movies " +
                    $"set title= '{m.Title}', genre='{m.Genre}', `year`={m.Year}, runtime={m.Runtime} " +
                    $"where id={m.Id}";
                //Now we run the query we wrote above:
                connect.Open();
                connect.Query<Movie>(sql);
                connect.Close();
            }
        }

        public void CreateMovie(Movie m)
        {
            using (var connect = new MySqlConnection(Secret.Connection))
            {
                string sql = "insert into movies " +
                    $"values(0, '{m.Title}', '{m.Genre}', {m.Year}, {m.Runtime} )";
                //Now we run the query we wrote above:
                connect.Open();
                connect.Query<Movie>(sql);
                connect.Close();
            }
        }

        public List<Movie> SearchByTitle(string title)
        {
            using (var connect = new MySqlConnection(Secret.Connection))
            {
                var sql = $"select * from movies where title like '{title}%'";
                connect.Open();

                //Query always returns a list regardless of how many movies we want. 
                //Even if our query is meant to return 1 movie, we still need to pull it out of a list of count 1
                List<Movie> movies = connect.Query<Movie>(sql).ToList();
                connect.Close();
                return movies;
            }
        }

    }
}
