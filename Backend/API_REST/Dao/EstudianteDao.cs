using API_REST.Models;
using System.Data.SqlClient;

namespace API_REST.Dao
{
    public class EstudianteDao
    {

        private SqlConnection con;

        public EstudianteDao(SqlConnection con)
        {
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

        public List<Estudiante> ListarEstudiantes()
        {
            List<Estudiante> lista = new();
            try
            {
                string queryString = "SELECT * FROM Estudiante;";

                using SqlCommand command = new(queryString, con);

                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var estudiante = new Estudiante
                    {
                        IdLector = reader.GetInt32(0),
                        CI = reader.GetString(1),
                        Nombre = reader.GetString(2),
                        Direccion = reader.GetString(3),
                        Carrera = reader.GetString(4),
                        Edad = reader.GetInt32(5)

                    };
                    lista.Add(estudiante);
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

        // Create
        public string CrearEstudiante(Estudiante estudiante)
        {
            int filasAfectadas = 0;
            try
            {
                string queryString = "INSERT INTO Estudiante (CI, Nombre, Direccion, Carrera, Edad) " +
                    "VALUES (@CI, @Nombre, @Direccion, @Carrera, @Edad);";

                using SqlCommand command = new(queryString, con);
                command.Parameters.AddWithValue("@CI", estudiante.CI);
                command.Parameters.AddWithValue("@Nombre", estudiante.Nombre);
                command.Parameters.AddWithValue("@Direccion", estudiante.Direccion);
                command.Parameters.AddWithValue("@Carrera", estudiante.Carrera);
                command.Parameters.AddWithValue("@Edad", estudiante.Edad);

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
            return $"Estudiante creado. {FilasAfectadas(filasAfectadas)}";
        }

        // Read
        public Estudiante? ListarEstudiantePorId(int id)
        {
            Estudiante? estudiante = null;
            try
            {
                string queryString = "SELECT * FROM Estudiante WHERE IdLector = @IdLector;";

                using SqlCommand command = new(queryString, con);
                command.Parameters.AddWithValue("@IdLector", id);

                using SqlDataReader reader = command.ExecuteReader();

                reader.Read();

                if (reader.HasRows)
                {
                    estudiante = new Estudiante
                    {
                        IdLector = reader.GetInt32(0),
                        CI = reader.GetString(1),
                        Nombre = reader.GetString(2),
                        Direccion = reader.GetString(3),
                        Carrera = reader.GetString(4),
                        Edad = reader.GetInt32(5)
                    };
                }
                else
                {
                    estudiante = null;
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
            return estudiante;
        }

        public Estudiante? ListarEstudiantePorNombre (string nombre)
        {
            Estudiante? estudiante = null;
            try
            {
                string queryString = "SELECT TOP 1 * FROM Estudiante WHERE Nombre = @Nombre ;";
                
                using SqlCommand command = new(queryString, con);
                command.Parameters.AddWithValue("@Nombre", nombre);

                using SqlDataReader reader = command.ExecuteReader();
                
                if (reader.HasRows)
                {
                    reader.Read();
                    estudiante = new Estudiante
                    {
                        IdLector = reader.GetInt32(0),
                        CI = reader.GetString(1),
                        Nombre = reader.GetString(2),
                        Direccion = reader.GetString(3),
                        Carrera = reader.GetString(4),
                        Edad = reader.GetInt32(5)
                    };
                }
                else
                {
                    estudiante = null;
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
            return estudiante;
        }

        // Update
        public string ActualizarEstudiante(Estudiante estudiante)
        {
            int filasAfectadas;
            try
            {
                string queryString = "UPDATE Estudiante "+
                    "SET Ci = @CI, "+
                    "Nombre = @Nombre, "+
                    "Direccion = @Direccion, " +
                    "Carrera = @Carrera, " +
                    "Edad = @Edad " +
                    "WHERE IdLector = @IdLector;";

                using SqlCommand command = new(queryString, con);
                command.Parameters.AddWithValue("@IdLector", estudiante.IdLector);
                command.Parameters.AddWithValue("@CI", estudiante.CI);
                command.Parameters.AddWithValue("@Nombre", estudiante.Nombre);
                command.Parameters.AddWithValue("@Direccion", estudiante.Direccion);
                command.Parameters.AddWithValue("@Carrera", estudiante.Carrera);
                command.Parameters.AddWithValue("@Edad", estudiante.Edad);

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
            return $"Estudiante actualizado. {FilasAfectadas(filasAfectadas)}";
        }

        // Delete
        public string EliminarEstudiante(int id)
        {
            int filasAfectadas;
            try
            {
                string queryString = "DELETE FROM Estudiante WHERE IdLector = @IdLector;";
                
                using SqlCommand command = new(queryString, con);
                command.Parameters.AddWithValue("@IdLector", id);

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
            return $"Estudiante eliminado. {FilasAfectadas(filasAfectadas)}";
        }

        public void ErrorConsulta(Exception e) => Console.WriteLine($"Error al ejecutar la consulta. {e}");

        public string FilasAfectadas(int filasAfectadas) => $"No. de Filas afectadas {filasAfectadas}";

    }
}
