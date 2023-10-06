using API_REST.Models;
using System.Data.SqlClient;

namespace API_REST.Dao
{
    public class AutorDao
    {
        private SqlConnection con;

        public AutorDao(SqlConnection con) { 
            this.con = con;
        }

        private void CerrarConexion()
        {
            try
            {
                con.Close();
            }
            catch (Exception e)
            {
                throw new Exception($"Error al cerrar la Base de Datos. {e}");
            }
        }

        // Read
        public List<Autor> ListarAutores()
        {
            List<Autor> lista = new();
            try
            {
                string queryString = "SELECT * FROM Autor;";
                using SqlCommand command = new(queryString, con);
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var autor = new Autor
                    {
                        IdAutor = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Nacionalidad = reader.GetString(2)
                    };
                    lista.Add(autor);
                }
            }
            catch (Exception e)
            {
                ErrorConsulta(e);
                throw new Exception($"Error al ejecutar la consulta. {e}");
            }
            finally
            {
                CerrarConexion();
            }
            return lista;
        }

        public Autor ListarAutorPorId(int id)
        {
            Autor? autor = null;
            try
            {
                string queryString = "SELECT * FROM Autor WHERE idAutor = @IdAutor;";
                using SqlCommand command = new(queryString, con);
                command.Parameters.AddWithValue("@IdAutor", id);

                using SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                autor = new Autor
                {
                    IdAutor = reader.GetInt32(0),
                    Nombre = reader.GetString(1),
                    Nacionalidad = reader.GetString(2)
                };
            }
            catch (Exception e)
            {
                ErrorConsulta(e);
                throw new Exception($"Error al ejecutar la consulta. {e}");
            }
            finally
            {
                CerrarConexion();
            }
            return autor;
        }

        public Autor ListarAutorPorNombre(string nombre)
        {
            Autor? autor = null;
            try
            {
                string queryString = "SELECT * FROM Autor WHERE Nombre = @Nombre;";
                using SqlCommand command = new(queryString, con);
                command.Parameters.AddWithValue("@Nombre", nombre);
                using SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                autor = new Autor
                {
                    IdAutor = reader.GetInt32(0),
                    Nombre = reader.GetString(1),
                    Nacionalidad = reader.GetString(2)
                };
            }
            catch (Exception e)
            {
                ErrorConsulta(e);
                throw new Exception($"Error al ejecutar la consulta. {e}");
            }
            finally
            {
                CerrarConexion();
            }
            return autor;
        }

        // Create
        public string CrearAutor(Autor autor)
        {
            int filasAfectadas = 0;
            try
            {
                string queryString = "INSERT INTO Autor (Nombre, Nacionalidad) " +
                    "VALUES (@Nombre, @Nacionalidad);";

                using SqlCommand command = new(queryString, con);
                command.Parameters.AddWithValue("@Nombre", autor.Nombre);
                command.Parameters.AddWithValue("@Nacionalidad", autor.Nacionalidad);

                filasAfectadas = command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                ErrorConsulta(e);
                throw new Exception($"Error al ejecutar la consulta. {e}");
            }
            finally
            {
                CerrarConexion();
            }
            return $"Autor creado. {FilasAfectadas(filasAfectadas)}";
        }

        // Update
        public string ActualizarAutor(Autor autor)
        {
            int filasAfectadas = 0;
            try
            {
                string queryString = "UPDATE Autor SET " +
                    "Nombre = @Nombre, " +
                    "Nacionalidad = @Nacionalidad " +
                    "WHERE IdAutor = @IdAutor;";

                using SqlCommand command = new(queryString, con);
                command.Parameters.AddWithValue("@IdAutor", autor.IdAutor);
                command.Parameters.AddWithValue("@Nombre", autor.Nombre);
                command.Parameters.AddWithValue("@Nacionalidad", autor.Nacionalidad);

                filasAfectadas = command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                ErrorConsulta(e);
                throw new Exception($"Error al ejecutar la consulta. {e}");
            }
            finally
            {
                CerrarConexion();
            }
            return $"Auto actualizado. {FilasAfectadas(filasAfectadas)}";
        }


        // Delete
        public string EliminarAutor(int id)
        {
            int filasAfectadas = 0;
            try
            {
                string queryString = "DELETE FROM Autor WHERE IdAutor = @IdAutor;";

                using SqlCommand command = new(queryString, con);
                command.Parameters.AddWithValue("@IdAutor", id);

                filasAfectadas = command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                ErrorConsulta(e);
                throw new Exception($"Error al ejecutar la consulta. {e}");
            }
            finally
            {
                CerrarConexion();
            }
            return $"Autor eliminado. {FilasAfectadas(filasAfectadas)}";
        }

        public void ErrorConsulta(Exception e) => Console.WriteLine($"Error al ejecutar la consulta. {e}");

        public string FilasAfectadas(int filasAfectadas) => $"No. de Filas afectadas {filasAfectadas}";

    }
}
