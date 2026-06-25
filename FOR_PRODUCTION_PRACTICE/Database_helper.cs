using System;
using System.Data.SqlClient;
using System.Data;
using FOR_PRODUCTION_PRACTICE.Models;


public class DatabaseHelper
{
	public DatabaseHelper
	{
		private string Connect = @"Data Source=KSP1-763\BAZA;Initial Catalog=For_production_practice;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False;Command Timeout=30";

	public bool Validate(string login, string password)
	{
		try
		{
			using (SqlConnection conn=new SqlConnection(Connect))
			{
				conn.Open();
				string query = "SELECT COUNT(*) FROM Autorization Where login_user = @login AND password_user=@password";
				using(SqlCommand cmd=new SqlCommand(query, conn))
				{
					cmd.Parameters.AddWithValue(
				}

            }
				
		}
	}


    }
}
