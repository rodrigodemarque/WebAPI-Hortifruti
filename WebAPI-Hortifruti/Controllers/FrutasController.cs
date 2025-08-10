using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using WebAPI_Hortifruti.Repositories;
using WebAPI_Hortifruti.Utils.Log;

namespace WebAPI_Hortifruti.Controllers
{
    public class FrutasController : ApiController
    {
        readonly ILogger logger;
        readonly IRepository<Models.Fruta> repository;
        public FrutasController()
        {
            logger = new Logger(Configurations.Config.GetLogPath());
            repository = new Repositories.SQLServer.Fruta(Configurations.Config.GetConnectionStringSQLServer());
            repository.CacheExpirationTime = Configurations.Config.GetCacheExpirationTimeInSeconds("cacheExpirationTimeInSeconds");
            
        }
        // GET: api/Frutas
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                return Ok(await repository.GetAllAsync());
            }
            catch (Exception ex)
            {
                await logger.Log(ex);

                return InternalServerError();
            }

        }

        // GET: api/Frutas/5
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                Models.Fruta fruta = (await repository.GetByIdAsync(id));
                if (fruta.Id == 0)
                    return NotFound();
                return Ok(fruta);
                    
            }
            catch (Exception ex)
            {
                await logger.Log(ex);

                return InternalServerError();
            }
        }


        // GET: api/Frutas/nome
        [Route("api/Frutas/{nome:alpha}")]
        public async Task<IHttpActionResult> Get(string nome)
        {
            try
            {
                List<Models.Fruta> frutas = (await repository.GetByNameAsync(nome));
                if (frutas.Count == 0)
                    return NotFound();
                return Ok(frutas);
                //return Ok(await repository.GetByNameAsync(nome));
            }
            catch (Exception ex)
            {
                await logger.Log(ex);

                return InternalServerError();
            }
        }

        // POST: api/Frutas
        public async Task<IHttpActionResult> Post([FromBody] Models.Fruta fruta)
        {
            if (!ModelState.IsValid || fruta == null)
                return BadRequest("Os dados da Fruta não foram enviados corretamente");

            try
            {
                await repository.AddASync(fruta); 
                return Ok(fruta);
            }
            catch (Exception ex)
            {
                await logger.Log(ex);
                return InternalServerError();
            }
        }

        // PUT: api/Frutas/5
        public async Task<IHttpActionResult> Put(int id, [FromBody] Models.Fruta fruta)
        {
            if (!ModelState.IsValid || fruta == null)
                return BadRequest("Os dados da Fruta não foram enviados corretamente");

            if (id != fruta.Id)
                return BadRequest("O id da rota não corresponde ao Id da fruta");

            try
            {   
                bool resposta = await repository.UpdateAsync(fruta);
                if (!resposta)
                    return NotFound();

                return Ok(fruta);
            }
            catch (Exception ex)
            {
                await logger.Log(ex);
                return InternalServerError();
            }
        }

        // DELETE: api/Frutas/5
        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
                bool resposta = await repository.DeleteAsync(id);
                if (!resposta)
                    return NotFound();

                return Ok();
            }
            catch (Exception ex)
            {
                await logger.Log(ex);
                return InternalServerError();
            }
        }
    }
}
