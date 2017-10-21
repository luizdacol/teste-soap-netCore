using System;
using Microsoft.AspNetCore.Mvc;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace TesteSoap.Controllers
{

    [Route("api/[controller]")]
    public class SoapController : Controller
    {

        [HttpGet]
        public string Get()
        {
            try
            {

                var binding = new BasicHttpBinding();
                var endpoint = new EndpointAddress(new Uri("URI_Servico"));
                var channelFactory = new ChannelFactory<IPingService>(binding, endpoint);
                var serviceClient = channelFactory.CreateChannel();
                var result = serviceClient.Ping("Ping");
                channelFactory.Close();

                return "ChannelFactory";
            }
            catch (Exception ex)
            {
                return $"Ocorreu um erro. Detlhes -> ${ex.Message}";
            }
        }

        [HttpGet("HealthCheck")]
        public ActionResult HealthCheck()
        {
            return Ok("ok");
        }
    }

    [ServiceContract]
    public interface IPingService
    {
        [OperationContract]
        string Ping(string msg);
    }

}