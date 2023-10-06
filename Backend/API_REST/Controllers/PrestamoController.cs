using API_REST.Dao;
using API_REST.Factory;
using API_REST.Models;
using Microsoft.AspNetCore.Mvc;

namespace API_REST.Controllers
{

    [ApiController]
    [Route("prestamo")]
    public class PrestamoController
    {

        private readonly PrestamoDao prestamoDao;

        public PrestamoController()
        {
            var factory = new ConnectionFactory();
            prestamoDao = new PrestamoDao(factory.RecuperaConexion());
        }

        [HttpGet]
        [Route("listar")] // Listar todos los libros
        public dynamic ListarPrestamos()
        {
            var datos = prestamoDao.ListarPrestamos();
            if (datos.Count() == 0)
            {
                return new
                {
                    Error = true,
                    Mensaje = "No hay prestamos registrados"
                };
            }
            return new
            {
                Error = false,
                Mensaje = datos
            };
        }

        [HttpGet]
        [Route("librosDisponibles")] // Listar todos los libros disponibles
        public dynamic ListarPrestamosDisponibles()
        {
            var datos = prestamoDao.ListarPrestamosDisponibles();
            if (datos == 0)
            {
                return new
                {
                    Error = true,
                    Mensaje = "No hay prestamos disponibles"
                };
            }
            return new
            {
                Error = false,
                Mensaje = datos
            };
        }


        [HttpGet]
        [Route("listar/id/{id}")] // Listar un libro por su id
        public dynamic ListarPrestamoPorId(int id)
        {
            var datos = prestamoDao.ListarPrestamoPorId(id);
            if (datos == null)
            {
                return new
                {
                    Error = true,
                    Mensaje = "No se encontró el prestamo"
                };
            }
            return new
            {
                Error = false,
                Mensaje = datos
            };
        }

        [HttpGet]
        [Route("listar/nombre/{nombre}")] // Listar un libro por su nombre
        public dynamic ListarPrestamoPorNombre(string nombre)
        {
            // TODO: Implementar 

            return new
            {
                Error = false,
                Mensaje = "Método aun no implementado"
            };
        }

        [HttpPost]
        [Route("guardar")] // Guardar un libro
        public dynamic GuardarPrestamo([FromBody] Prestamo prestamo)
        {
            var datos = prestamoDao.GuardarPrestamo(prestamo);

            return new
            {
                Error = false,
                Mensaje = datos
            };
        }

        [HttpPut]
        [Route("actualizar")] // Actualizar un prestamo
        public dynamic ActualizarPrestamo([FromBody] Prestamo prestamo)
        {
            var datos = prestamoDao.ActualizarPrestamo(prestamo);

            return new
            {
                Error = false,
                Mensaje = datos
            };
        }

        [HttpDelete]
        [Route("eliminar/id/{id}")] // Eliminar un prestamo por su id
        public dynamic? EliminarPrestamo(int id)
        {
            var datos = prestamoDao.EliminarPrestamo(id);

            return new
            {
                Error = false,
                Mensaje = datos
            };
        }

    }
}
