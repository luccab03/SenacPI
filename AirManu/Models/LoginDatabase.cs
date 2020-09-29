using MySqlConnector;

namespace AirManu.Models
{
    public class LoginDatabase
    {
        private const string DataString = "Database=AirManu;Server=localhost;Port=8889;User Id=root;Password=root";

        public void Inserir(Login login){
            MySqlConnection connection = new MySqlConnection(DataString);
            connection.Open();
            string sqlCommand = "INSERT INTO Usuario(email,senha) VALUES (@Email, @Senha)";
            MySqlCommand command = new MySqlCommand(sqlCommand, connection);
            command.Parameters.AddWithValue("@Email", login.Email);
            command.Parameters.AddWithValue("@Senha", login.Senha);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public Login Login(Login login){
            MySqlConnection connection = new MySqlConnection(DataString);
            connection.Open();
            string sqlCommand = "SELECT * FROM Usuario WHERE email = @Email AND senha = @Senha";
            MySqlCommand command = new MySqlCommand(sqlCommand, connection);
            command.Parameters.AddWithValue("@Email", login.Email);
            command.Parameters.AddWithValue("@Senha", login.Senha);
            MySqlDataReader reader = command.ExecuteReader();
            while(reader.Read()){
                if(reader.GetString("email") == login.Email && reader.GetString("senha") == login.Senha){
                    Login loginReturn = new Login();
                    loginReturn.Email = reader.GetString("email");
                    loginReturn.Senha = reader.GetString("senha");
                    loginReturn.Id = reader.GetInt32("idUsuario");
                    connection.Close();
                    return loginReturn;
                }
            }
            connection.Close();
            return null;
        }
    }
}