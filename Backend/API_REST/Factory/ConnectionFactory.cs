using System.Data.SqlClient;

namespace API_REST.Factory
{
    public class ConnectionFactory
    {
        private SqlConnectionStringBuilder sqlConnectionStringBuilder;

        public ConnectionFactory()
        {
            sqlConnectionStringBuilder = new SqlConnectionStringBuilder("" +
                "Data Source=DESKTOP-PVM7BM6;" +
                "Initial Catalog=Libros;" +
                "Integrated Security=True;");
        }

        public SqlConnection RecuperaConexion()
        {
            try
            {
                // Crear y devolver una nueva conexión
                SqlConnection connection = new(sqlConnectionStringBuilder.ToString());
                connection.Open();
                return connection;
            }
            catch (Exception ex)
            {
                // Capturar cualquier excepción y relanzar como una Exception
                throw new Exception("Error al obtener la conexión a la base de datos.", ex);
            }
        }
    }
}
