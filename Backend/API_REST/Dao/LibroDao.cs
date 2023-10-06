using System.Data.SqlClient;
using API_REST.Models;
using Microsoft.AspNetCore.Mvc;

namespace API_REST.Dao
{
    public class LibroDao
    {

        private SqlConnection con;

        public LibroDao(SqlConnection con)
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

        public List<Libro> ListarLibros()
        {
            List<Libro> lista = new();
            try
            {
                string queryString = "SELECT * FROM Libro;";

                using SqlCommand command = new(queryString, con);

                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var libro = new Libro
                    {
                        Id = reader.GetInt32(0),
                        Titulo = reader.GetString(1),
                        Editorial = reader.GetString(2),
                        Area = reader.GetString(3)
                    };
                    lista.Add(libro);
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

        [HttpGet]
        [Route("listar/id/{id}")] // Listar un libro por su id
        public Libro ListarLibroPorId(int id)
        {
            Libro? libro = null;
            try
            {
                string queryString = "SELECT * FROM Libro WHERE IdLibro = @IdLibro;";
                using SqlCommand command = new(queryString, con);
                command.Parameters.AddWithValue("@IdLibro", id);
                using SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                libro = new Libro
                {
                    Id = reader.GetInt32(0),
                    Titulo = reader.GetString(1),
                    Editorial = reader.GetString(2),
                    Area = reader.GetString(3)
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
            return libro;
        }

        [HttpGet]
        [Route("listar/titulo/{titulo}")] // Listar un libro por su nombre
        public Libro ListarLibroPorTitulo(string titulo)
        {
            Libro? libro = null;
            try
            {
                string queryString = "SELECT * FROM Libro WHERE Titulo = @Titulo;";
                using SqlCommand command = new(queryString, con);
                command.Parameters.AddWithValue("@Titulo", titulo);
                using SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                libro = new Libro
                {
                    Id = reader.GetInt32(0),
                    Titulo = reader.GetString(1),
                    Editorial = reader.GetString(2),
                    Area = reader.GetString(3)
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
            return libro;
        }

        // Create
        public string CrearLibro(Libro libro)
        {
            int filasAfectadas;
            try
            {
                string queryString = "INSERT INTO Libro (Titulo, Editorial, Area) " +
                    "VALUES (@Titulo, @Editorial, @Area);";

                using SqlCommand command = new(queryString, con);
                command.Parameters.AddWithValue("@Titulo", libro.Titulo);
                command.Parameters.AddWithValue("@Editorial", libro.Editorial);
                command.Parameters.AddWithValue("@Area", libro.Area);

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
            return $"Libro creado. {FilasAfectadas(filasAfectadas)}";
        }

        // Update
        public string ActualizarLibro(Libro libro)
        {
            int filasAfectadas;
            try
            {
                string queryString = "UPDATE Libro " +
                    "SET Titulo = @Titulo, " +
                    "Editorial = @Editorial, " +
                    "Area = @Area " +
                    "WHERE IdLibro = @IdLibro ;";

                using SqlCommand command = new(queryString, con);

                command.Parameters.AddWithValue("@Titulo", libro.Titulo);
                command.Parameters.AddWithValue("@Editorial", libro.Editorial);
                command.Parameters.AddWithValue("@Area", libro.Area);

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
            return $"Libro actualizado. {FilasAfectadas(filasAfectadas)}";
        }

        // Delete
        public string EliminarLibro(int id)
        {
            int filasAfectadas;
            try
            {
                string queryString = "DELETE FROM Libro WHERE IdLibro = @IdLibro;";

                using SqlCommand command = new(queryString, con);
                command.Parameters.AddWithValue("@IdLibro", id);

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
            return $"Libro eliminado. {FilasAfectadas(filasAfectadas)}";
        }

        public string PrestarLibro(int idLector, int idLibro, string fechaPrestamo, string fechaDevolucion, bool devuelto)
        {
            try
            {
                string queryString = "INSERT INTO Prestamo (IdLector, IdLibro, FechaPrestamo, FechaDevolucion, Devuelto) " +
                    "VALUES (@IdLector, @IdLibro, @FechaPrestamo, @FechaDevolucion, @Devuelto);";

                using SqlCommand command = new(queryString, con);
                command.Parameters.AddWithValue("@IdLector", idLector);
                command.Parameters.AddWithValue("@IdLibro", idLibro);
                command.Parameters.AddWithValue("@IdFechaPrestamo", fechaPrestamo);
                command.Parameters.AddWithValue("@IdFechaDevolucion", fechaDevolucion);
                command.Parameters.AddWithValue("@Devuelto", devuelto);

                var filasAfectadas = command.ExecuteNonQuery();

                return $"Libro prestado. No. de columnas afectadas {filasAfectadas}";
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al ejecutar la consulta. {e}");
                throw new Exception($"Error al ejecutar la consulta. {e}");
            }
        }

        public string DevolverLibro(int idLector, int idLibro, string fechaDevolucion, bool devuelto)
        {
            try
            {
                string queryString = "UPDATE Prestamo " +
                "SET Devuelto = @Devuelto, " +
                "FechaDevolucion = @FechaDevolucion " +
                "WHERE IdLector = @IdLector AND IdLibro = @IdLibro ;";
                using SqlCommand command = new(queryString, con);

                command.Parameters.AddWithValue("@IdLector", idLector);
                command.Parameters.AddWithValue("@IdLibro", idLibro);

                command.Parameters.AddWithValue("@fechaDevolucion", fechaDevolucion);
                command.Parameters.AddWithValue("@devuelto", devuelto);

                var filasAfectadas = command.ExecuteNonQuery();

                return $"Libro devuelto. No. de columnas afectadas {filasAfectadas}";
            }
            catch
            {
                throw new Exception("Error al devolver el libro");
            }
        }

        public void ErrorConsulta(Exception e) => Console.WriteLine($"Error al ejecutar la consulta. {e}");

        public string FilasAfectadas(int filasAfectadas) => $"No. de Filas afectadas {filasAfectadas}";
    }
}
