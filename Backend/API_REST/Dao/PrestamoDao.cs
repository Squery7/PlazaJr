using API_REST.Models;
using System.Data.SqlClient;

namespace API_REST.Dao
{
    public class PrestamoDao
    {
        private SqlConnection con;

        public PrestamoDao(SqlConnection con)
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

        // Read
        public List<Prestamo> ListarPrestamos()
        {
            string queryString = "SELECT * FROM Prestamo;";

            List<Prestamo> lista = new();

            try
            {
                using SqlCommand command = new(queryString, con);

                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var prestamo = new Prestamo
                    {
                        Id = reader.GetInt32(0),
                        IdLector = reader.GetInt32(1),
                        IdLibro = reader.GetInt32(2),
                        FechaPrestamo = reader.GetDateTime(3).ToString(),
                        FechaDevolucion = reader.GetDateTime(4).ToString(),
                        Devuelto = reader.GetBoolean(5)
                    };
                    lista.Add(prestamo);
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

        public Prestamo ListarPrestamoPorId(int id)
        {
            Prestamo? prestamo = null;
            try
            {
                string queryString = "SELECT * FROM Prestamo WHERE Id = @Id;";
                using SqlCommand command = new(queryString, con);
                command.Parameters.AddWithValue("@Id", id);

                using SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                prestamo = new Prestamo
                {
                    IdLector = reader.GetInt32(0),
                    IdLibro = reader.GetInt32(1),
                    FechaPrestamo = reader.GetDateTime(3).ToString(),
                    FechaDevolucion = reader.GetDateTime(4).ToString(),
                    Devuelto = reader.GetBoolean(5)
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
            return prestamo;
        }

        // Create
        public string GuardarPrestamo(Prestamo prestamo)
        {
            string queryString = "INSERT INTO Prestamo (IdLector, IdLibro, FechaPrestamo, FechaDevolucion, Devuelto) VALUES (@IdLector, @IdLibro, @FechaPrestamo, @FechaDevolucion, @Devuelto);";

            string mensaje = "";

            try
            {
                using SqlCommand command = new(queryString, con);
                command.Parameters.AddWithValue("@IdLector", prestamo.IdLector);
                command.Parameters.AddWithValue("@IdLibro", prestamo.IdLibro);
                command.Parameters.AddWithValue("@FechaPrestamo", prestamo.FechaPrestamo);
                command.Parameters.AddWithValue("@FechaDevolucion", prestamo.FechaDevolucion);
                command.Parameters.AddWithValue("@Devuelto", prestamo.Devuelto);

                int filasAfectadas = command.ExecuteNonQuery();
                mensaje = FilasAfectadas(filasAfectadas);
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
            return mensaje;
        }

        // Update
        public string ActualizarPrestamo(Prestamo prestamo)
        {
            string queryString = "UPDATE Prestamo SET " +
                "IdLector = @IdLector, " +
                "IdLibro = @IdLibro, " +
                "FechaPrestamo = @FechaPrestamo, " +
                "FechaDevolucion = @FechaDevolucion, " +
                "Devuelto = @Devuelto " +
                "WHERE Id = @Id;";

            int filasAfectadas;

            try
            {
                using SqlCommand command = new(queryString, con);
                command.Parameters.AddWithValue("@Id", prestamo.Id);
                command.Parameters.AddWithValue("@IdLector", prestamo.IdLector);
                command.Parameters.AddWithValue("@IdLibro", prestamo.IdLibro);
                command.Parameters.AddWithValue("@FechaPrestamo", prestamo.FechaPrestamo);
                command.Parameters.AddWithValue("@FechaDevolucion", prestamo.FechaDevolucion);
                command.Parameters.AddWithValue("@Devuelto", prestamo.Devuelto);

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
            return $"Prestamo creado. {FilasAfectadas(filasAfectadas)}";
        }

        // Delete
        public string EliminarPrestamo(int id)
        {
            int filasAfectadas;
            try
            {
                string queryString = "DELETE FROM Prestamo WHERE Id = @Id;";
                using SqlCommand command = new(queryString, con);
                command.Parameters.AddWithValue("@Id", id);
                filasAfectadas = command.ExecuteNonQuery();
            } catch (Exception e)
            {
                ErrorConsulta(e);
                throw new Exception($"Error al ejecutar la consulta. {e}");
            } finally
            {
                CerrarConexion();
            }
            return $"Prestamo eliminado. {FilasAfectadas(filasAfectadas)}";
        }

        public int ListarPrestamosDisponibles()
        {
            int cantidad = 0;
            try
            {
                string queryString = new("SELECT COUNT(l.IdLibro) AS total_libros, " +
                    "COUNT(p.Id) AS total_prestamos, " +
                    "SUM(CASE WHEN p.Devuelto = 0 THEN 1 ELSE 0 END) AS prestamos_no_devueltos, " +
                    "COUNT(l.IdLibro) - SUM(CASE WHEN p.Devuelto = 0 THEN 1 ELSE 0 END) AS libros_disponibles, " +
                    "CASE WHEN COUNT(p.Id) < COUNT(l.IdLibro) - SUM(CASE WHEN p.Devuelto = 0 THEN 1 ELSE 0 END) THEN 'Puede realizar un nuevo préstamo' ELSE 'No puede realizar un nuevo préstamo' END AS estado_prestamo " +
                    "FROM Libro l " +
                    "LEFT JOIN " +
                    "Prestamo p ON l.IdLibro = p.IdLibro;");
                using SqlCommand command = new(queryString, con);

                using SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                cantidad = reader.GetInt32(3);
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
            return cantidad;
        }



        public void ErrorConsulta(Exception e) => Console.WriteLine($"Error al ejecutar la consulta. {e}");

        public string FilasAfectadas(int filasAfectadas) => $"No. de Filas afectadas {filasAfectadas}";

        
    }
}
