using API_REST.Dao;
using API_REST.Factory;
using Microsoft.AspNetCore.Mvc;

namespace API_REST.Controllers
{

    [ApiController]
    [Route("libAut")]
    public class LibAutController
    {
        private readonly LibAutDao libAutDao;

        public LibAutController()
        {
            var factory = new ConnectionFactory();
            libAutDao = new LibAutDao(factory.RecuperaConexion());
        }


    }
}
