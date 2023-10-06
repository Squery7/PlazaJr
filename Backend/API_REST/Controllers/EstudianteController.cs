using API_REST.Dao;
using API_REST.Factory;
using API_REST.Models;
using Microsoft.AspNetCore.Mvc;

namespace API_REST.Controllers
{

    [ApiController]
    [Route("estudiante")]
    public class EstudianteController : ControllerBase
    {
        private readonly EstudianteDao estudianteDao;

        public EstudianteController()
        {
            var factory = new ConnectionFactory();
            estudianteDao = new EstudianteDao(factory.RecuperaConexion());
        }

        [HttpGet]
        [Route("listar")] // Listar todos los estudiantes
        public dynamic ListarEstudiantes()
        {
            var datos = estudianteDao.ListarEstudiantes();
            return new { 
                Mensaje = datos
            };
        }

        [HttpGet]
        [Route("listar/id/{id}")] // Listar un estudiante por su id
        public dynamic ListarEstudiantePorId(int id)
        {
            var datos = estudianteDao.ListarEstudiantePorId(id);
            if (datos == null)
            {
                return new
                {
                    Error = true,
                    Mensaje = "No se encontró el estudiante"
                };
            }
            else
            {
                return new
                {
                    Error = false,
                    Mensaje = $"Bienvenid@ {datos.Nombre}"
                };
            }
        }

        [HttpGet]
        [Route("listar/nombre/{nombre}")] // Listar un estudiante por su nombre
        public dynamic ListarEstudiantePorNombre(string nombre)
        {
            var datos = estudianteDao.ListarEstudiantePorNombre(nombre);
            if (datos == null )
            {
                return new
                {
                    Error = true,
                    Mensaje = "No se encontró el estudiante"
                };
            }
            else
            {
                return new
                {
                    Error = false,
                    Mensaje = $"Su Id es: {datos.IdLector}"
                };
            }
        }

        [HttpPost]
        [Route("guardar")] // Guardar un estudiante
        public dynamic? GuardarEstudiante([FromBody] Estudiante estudiante)
        {
            var datos = estudianteDao.CrearEstudiante(estudiante);
            return datos;
        }

        [HttpPut]
        [Route("actualizar")] // Actualizar un estudiante
        public dynamic? ActualizarEstudiante([FromBody] Estudiante estudiante)
        {
            var datos = estudianteDao.ActualizarEstudiante(estudiante);
            return datos;
        }

        [HttpDelete]
        [Route("eliminar/{id}")] // Eliminar un estudiante
        public dynamic? EliminarEstudiante(int id)
        {
            var datos = estudianteDao.EliminarEstudiante(id);
            return datos;
        }

    }
}
