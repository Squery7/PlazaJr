using API_REST.Dao;
using API_REST.Factory;
using API_REST.Models;
using Microsoft.AspNetCore.Mvc;

namespace API_REST.Controllers
{
    [ApiController]
    [Route("libro")]
    public class LibroController
    {
        private readonly LibroDao libroDao;

        public LibroController()
        {
            var factory = new ConnectionFactory();
            libroDao = new LibroDao(factory.RecuperaConexion());
        }

        [HttpGet]
        [Route("listar")] // Listar todos los libros
        public dynamic ListarLibros()
        {
            var datos = libroDao.ListarLibros();

            if (datos.Count() == 0)
            {
                return new
                {
                    Error = true,
                    Mensaje = "No hay libros registrados"
                };
            } else
            {
                return new
                {
                    Error = false,
                    Mensaje = datos
                };
            }
        }

        [HttpGet]
        [Route("listar/id/{id}")] // Listar un libro por su id
        public dynamic ListarLibroPorId(int id)
        {
            var datos = libroDao.ListarLibroPorId(id);
            return datos;
        }

        [HttpGet]
        [Route("listar/nombre/{nombre}")] // Listar un libro por su nombre
        public dynamic ListarLibroPorNombre(string nombre)
        {
            var datos = libroDao.ListarLibroPorTitulo(nombre);
            return datos;
        }

        [HttpPost]
        [Route("guardar")] // Guardar un libro
        public string GuardarLibro([FromBody] Libro libro)
        {
            var datos = libroDao.CrearLibro(libro);
            return datos;
        }

        [HttpPut]
        [Route("actualizar")] // Actualizar un libro
        public dynamic? ActualizarLibro([FromBody] Libro libro)
        {
            var datos = libroDao.ActualizarLibro(libro);
            return datos;
        }

        [HttpDelete]
        [Route("eliminar/{id}")] // Eliminar un libro
        public dynamic? EliminarLibro(int id)
        {
            var datos = libroDao.EliminarLibro(id);
            return datos;
        }

    }
}
