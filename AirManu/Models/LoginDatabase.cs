using MySqlConnector;

namespace AirManu.Models
{
    public class LoginDatabase
    {
        // String de conexão
        private const string DataString = "Database=AirManu;Server=localhost;Port=8889;User Id=root;Password=root";

        // Método de insersão de Login no banco de dados 
        public void Inserir(Login login)
        {

            // Cria uma conexão e abre ela
            MySqlConnection connection = new MySqlConnection(DataString);
            connection.Open();

            // Cria um novo comando 
            string sqlCommand = "INSERT INTO Usuario(email,senha) VALUES (@Email, @Senha)";
            MySqlCommand command = new MySqlCommand(sqlCommand, connection);

            // Completa o comando anterior com os parametros para evitar SQL Injection
            command.Parameters.AddWithValue("@Email", login.Email);
            command.Parameters.AddWithValue("@Senha", login.Senha);

            // Executa o comando e fecha a conexão
            command.ExecuteNonQuery();
            connection.Close();
        }

        // Método de extração de Login no banco de dados
        public Login Login(Login login)
        {

            // Cria uma conexão e abre ela
            MySqlConnection connection = new MySqlConnection(DataString);
            connection.Open();

            // Cria um novo comando 
            string sqlCommand = "SELECT * FROM Usuario WHERE email = @Email AND senha = @Senha";
            MySqlCommand command = new MySqlCommand(sqlCommand, connection);

            // Completa o comando anterior com os parametros para evitar SQL Injection
            command.Parameters.AddWithValue("@Email", login.Email);
            command.Parameters.AddWithValue("@Senha", login.Senha);

            // Cria um reader
            MySqlDataReader reader = command.ExecuteReader();

            // Faz um loop nos dados recebidos pelo reader
            while (reader.Read())
            {
                // Verifica novamente se o email e a senha estão corretos
                if (reader.GetString("email") == login.Email && reader.GetString("senha") == login.Senha)
                {
                    // Cria um novo objeto de model Login e preenche ele
                    Login loginReturn = new Login();
                    loginReturn.Email = reader.GetString("email");
                    loginReturn.Senha = reader.GetString("senha");
                    loginReturn.Id = reader.GetInt32("idUsuario");

                    // Fecha a conexão e retorna o Login
                    connection.Close();
                    return loginReturn;
                }
            }
            // Fecha a conexão e retorna null
            connection.Close();
            return null;
        }
    }
}