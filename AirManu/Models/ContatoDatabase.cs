using MySqlConnector;

namespace AirManu.Models
{
    public class ContatoDatabase
    {
        // String de conexão
        private const string DataString = "Database=AirManu;Server=localhost;Port=8889;User Id=root;Password=root";

        // Metodo para inserção de contatos no db
        public void Inserir(Contato contato)
        {
            // Cria uma conexão e abre ela
            MySqlConnection connection = new MySqlConnection(DataString);
            connection.Open();

            // Cria um novo comando
            const string sqlCommand = "INSERT INTO Contato(Nome,Email,Tele,Descr) VALUES (@Nome, @Email, @Tele, @Desc)";
            MySqlCommand command = new MySqlCommand(sqlCommand, connection);

            // Completa o comando com os dados do contato (para evitar SQL Injection)
            command.Parameters.AddWithValue("@Nome", contato.Nome);
            command.Parameters.AddWithValue("@Email", contato.Email);
            command.Parameters.AddWithValue("@Tele", contato.Tele);
            command.Parameters.AddWithValue("@Desc", contato.Desc);

            // Executa o comando e fecha a conexão
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}