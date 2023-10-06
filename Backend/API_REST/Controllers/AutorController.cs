using API_REST.Dao;
using API_REST.Factory;
using API_REST.Models;
using Microsoft.AspNetCore.Mvc;

namespace API_REST.Controllers
{
    [ApiController]
    [Route("autor")]
    public class AutorController
    {

        private readonly AutorDao autorDao;

        public AutorController()
        {
            var factory = new ConnectionFactory();
            autorDao = new AutorDao(factory.RecuperaConexion());
        }

        [HttpGet]
        [Route("listar")] // Listar todos los autores
        public List<Autor> ListarAutores()
        {
            var datos = autorDao.ListarAutores();
            return datos;
        }

        [HttpGet]
        [Route("listar/id/{id}")] // Listar un autor por su id
        public Autor ListarAutorPorId(int id)
        {
            var datos = autorDao.ListarAutorPorId(id);
            return datos;
        }

        [HttpGet]
        [Route("listar/nombre/{nombre}")] // Listar un autor por su nombre
        public Autor ListarAutorPorNombre(string nombre)
        {
            var datos = autorDao.ListarAutorPorNombre(nombre);
            return datos;
        }

        [HttpPost]
        [Route("guardar")] // Guardar un autor
        public string GuardarAutor([FromBody] Autor autor)
        {
            var datos = autorDao.CrearAutor(autor);
            return datos;
        }

        [HttpPut]
        [Route("actualizar")] // Actualizar un autor
        public string ActualizarAutor([FromBody] Autor autor)
        {
            var datos = autorDao.ActualizarAutor(autor);
            return datos;
        }

        [HttpDelete]
        [Route("eliminar/id/{id}")] // Eliminar un autor por su id
        public string EliminarAutor(int id)
        {
            var datos = autorDao.EliminarAutor(id);
            return datos;
        }

        
    }
}
