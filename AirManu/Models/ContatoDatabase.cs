using MySqlConnector;

namespace AirManu.Models
{
    public class ContatoDatabase
    {
        private const string DataString = "Database=AirManu;Server=localhost;Port=8889;User Id=root;Password=root";

        public void Inserir(Contato contato)
        {
            MySqlConnection connection = new MySqlConnection(DataString);
            connection.Open();
            const string sqlCommand = "INSERT INTO Contato(Nome,Email,Tele,Descr) VALUES (@Nome, @Email, @Tele, @Desc)";
            MySqlCommand command = new MySqlCommand(sqlCommand, connection);
            command.Parameters.AddWithValue("@Nome", contato.Nome);
            command.Parameters.AddWithValue("@Email", contato.Email);
            command.Parameters.AddWithValue("@Tele", contato.Tele);
            command.Parameters.AddWithValue("@Desc", contato.Desc);
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}