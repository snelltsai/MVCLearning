using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ZachT.Models
{
    public class DBManager
    {
        private readonly string ConnStr = "Data Source=LAPTOP-JBG07UFR\\SQLEXPRESS;User ID=zach;Password=..cmo123;Encrypt=False";

        public List<ClsPokemon> GetPokemons()
        {
            
            List<ClsPokemon> pokes = new List<ClsPokemon>();

            //建立連線
            SqlConnection sqlConn = new SqlConnection(ConnStr);
            SqlCommand sqlCommand = new SqlCommand("Select * from Pokemon");
            sqlCommand.Connection = sqlConn;
            sqlConn.Open();
            
            //執行命令
            SqlDataReader reader = sqlCommand.ExecuteReader();

            //確認有無資料
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ClsPokemon poke = new ClsPokemon
                    {
                        // GetOrdinal : 使用欄位名稱找索引值
                        PokeID = reader.GetInt32(reader.GetOrdinal("PokeID")),
                        PokeName = reader.GetString(reader.GetOrdinal("PokeName"))
                    };

                    pokes.Add(poke);
                }

            }
            else
            {
                // 要在Debug模式才會顯示writeline
                System.Diagnostics.Debug.WriteLine("Database is empty");
            }

            sqlConn.Close();
            return pokes;
        }

        public void NewPoke(ClsPokemon poke)
        {
            SqlConnection sqlConn = new SqlConnection(ConnStr);
            SqlCommand sqlCommand = new SqlCommand(
            @"INSERT INTO Pokemon (PokeID, PokeName) Values (@PokeID, @PokeName)"
            );

            sqlCommand.Connection = sqlConn;
            sqlCommand.Parameters.Add(new SqlParameter("@PokeID", poke.PokeID));
            sqlCommand.Parameters.Add(new SqlParameter("@PokeName", poke.PokeName));

            sqlConn.Open();
            sqlCommand.ExecuteNonQuery();
            sqlConn.Close();
        }


        public ClsPokemon GetPokeByID(int id)
        {
            ClsPokemon poke = new ClsPokemon();
            SqlConnection sqlConn = new SqlConnection(ConnStr);
            SqlCommand sqlCommand = new SqlCommand("Select * from Pokemon where PokeID=@id");
            sqlCommand.Parameters.Add(new SqlParameter("@id", id));
            sqlCommand.Connection = sqlConn;
            sqlConn.Open();

            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    poke = new ClsPokemon
                    {
                        PokeID = reader.GetInt32(reader.GetOrdinal("PokeiD")),
                        PokeName = reader.GetString(reader.GetOrdinal("PokeName"))
                    };
                }
            }
            else
            {
                poke.PokeName = "No pokemon";
            }

            sqlConn.Close();
            return poke;
        }

        public void UpdatedPoke(ClsPokemon poke)
        {
            SqlConnection sqlConn = new SqlConnection(ConnStr);
            SqlCommand sqlCommand = new SqlCommand(@"UPDATE Pokemon SET PokeName = @PokeName where PokeID = @id");
            sqlCommand.Connection = sqlConn;
            sqlCommand.Parameters.Add(new SqlParameter("@PokeName", poke.PokeName));
            sqlCommand.Parameters.Add(new SqlParameter("@id", poke.PokeID));
            sqlConn.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            sqlConn.Close();
        }

        public void DeletedPokeByID(int id)
        {
            SqlConnection sqlConn = new SqlConnection(ConnStr);
            SqlCommand sqlCommand = new SqlCommand(@"Delete Pokemon where PokeID = @id");
            sqlCommand.Connection = sqlConn;
            sqlCommand.Parameters.Add(new SqlParameter("@id", id));
            sqlConn.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            sqlConn.Close();
        }
    }
}