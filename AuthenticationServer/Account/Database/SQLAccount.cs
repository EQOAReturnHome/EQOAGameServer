using System;
using System.Data;
using System.Text;
using AuthServer.Server;
using MySql.Data.MySqlClient;
using EQOACryptoLibrary;
using System.Reflection.PortableExecutable;

namespace AuthServer.Account.Database
{
    public class SQLAccount : SQLBase
    {
        public bool AccountCreation(EQOAClient Client, Memory<byte> Password)
        {
            //See if account exists
            if (AccountExists(Client.Username, out int _))
            {
                return false;
            }

            Span<byte> t = Password.Span;

            for (int i = 0; i < Password.Length; ++i)
            {
                if (t[i] != '\0')
                    continue;

                Password = Password.Slice(i, Password.Length - i);
                break;
            }

            //Create account
            string hashedPwd = PasswordHash.HashPassword(Password);
            using var cmd2 = new MySqlCommand("CreateAccount", con);
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.Parameters.AddWithValue("usernam", Client.Username);
            cmd2.Parameters.AddWithValue("pwhas", hashedPwd);
            cmd2.Parameters.AddWithValue("actstatus", 1);
            cmd2.Parameters.AddWithValue("actlevel", 1);
            cmd2.Parameters.AddWithValue("actcreation", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd2.Parameters.AddWithValue("lastlogi", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd2.Parameters.AddWithValue("ipaddres", Client.ClientIPEndPoint.Address.ToString());
            cmd2.Parameters.AddWithValue("emai", Client.Email);
            cmd2.Parameters.AddWithValue("resul", 0);
            cmd2.Parameters.AddWithValue("subtim", 2592000);
            cmd2.Parameters.AddWithValue("partim", 2592000);
            cmd2.Parameters.AddWithValue("subfeature", 4);
            cmd2.Parameters.AddWithValue("gamefeature", 3);
            try
            {
                cmd2.ExecuteNonQuery();
            }

            catch(MySqlException e)
            {
                Console.WriteLine(e);
            }

            t.Fill(0);

            //See if account exists
            if (AccountExists(Client.Username, out int _))
            {
                Console.WriteLine("Account created");
                return true;
            }
            else
            {
                Console.WriteLine("Could not create account. Account name already exists.");
                return false;
            }
            
        }

        // Check if account name exists in the database
        public bool AccountExists(string accountName, out int accountID)
        {
            accountID = -1;
            bool exists = false;

            using var cmd = new MySqlCommand("DoesAccountExist", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Uname", accountName);

            using MySqlDataReader rdr = cmd.ExecuteReader();
            {
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        string username = rdr.GetString(0);
                        if (string.Equals(username, accountName, StringComparison.OrdinalIgnoreCase))
                        {
                            exists = true;
                            accountID = rdr.GetInt32(1);
                            break;
                        }
                    }
                }
                else
                {
                    exists = false;
                }
            }

            return exists;
        }

        public bool VerifyAccount(string Client, Memory<byte> Password, out int AccountID)
        {
            AccountID = -1;
            //See if account exists
            if (!AccountExists(Client, out int _))
                return false;

            for (int i = 0; i < Password.Length; i++)
            {
                if (Password.Span[i] != '\0')
                    continue;
                Password = Password.Slice(i, Password.Length - i);
                break;
            }

            //Query hashed password
            bool isMatch = false;
            using var cmd = new MySqlCommand("QueryHash", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Uname", Client);
            using MySqlDataReader rdr = cmd.ExecuteReader();
            {
                //Should only return 1 result technically
                while (rdr.Read())
                {
                    byte[] temp = new byte[66];
                    rdr.GetBytes(0, 0, temp, 0, 66);
                    string data = Encoding.Default.GetString(temp);
                    isMatch = PasswordHash.ValidatePassword(Password, data);
                    AccountID = rdr.GetInt32(1);
                }
            }

            Password.Span.Fill(0);
            return isMatch;
        }
    }
}
