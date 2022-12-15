using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using Repository;
using System.Net;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TechStoreController : ControllerBase
    {
        private readonly ILogger<TechStoreController> _logger;

        private readonly IMobileRepository _mobileRepository;

        public TechStoreController(ILogger<TechStoreController> logger, IMobileRepository mobileRepository)
        {
            _logger = logger;
            _mobileRepository = mobileRepository;
        }

        [HttpPost("PostMobile/{brand},{price},{date},{mobiledatagen},{camerares},{storage}", Name = "PostMobile")]
        public ActionResult<Devices.Mobile_Phone> PostMobile(Devices.EBrand brand, float price, DateTime date, Devices.EMobileDataGen mobile_data_gen, short camera_res, Devices.EMobileStorage storage)
        {
            _mobileRepository.BeginTransaction();
            var mobile = _mobileRepository.CreateMobile(Enum.GetValues<Devices.EBrand>()[(int)brand], price, date, Enum.GetValues<Devices.EMobileDataGen>()[(int)mobile_data_gen], camera_res , Enum.GetValues<Devices.EMobileStorage>()[(int)storage]);
            _mobileRepository.CommitTransaction();
            if (mobile == null)
            {
                _logger.LogError($"{nameof(TechStoreController.PostMobile)} -> cannot create mobile");
                return NotFound();
            }
            return mobile;
        }

        [HttpGet("GetMobiles", Name = "GetMobiles")]
        public ActionResult<IEnumerable<Devices.Mobile_Phone>> GetMobiles()
        {
            _mobileRepository.BeginTransaction();
            var mobile = _mobileRepository.GetMobiles();
            _mobileRepository.CommitTransaction();
            if (mobile == null)
            {
                _logger.LogError($"{nameof(TechStoreController.GetMobiles)} -> mobile not found");
                return NotFound();
            }
            return mobile;
        }
    }
}