using Core.Controllers.Base;
using Core.Identity.Models;
using Core.Models.Configuration;
using Core.Services;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Buffers.Text;
using Services.Admin.Horisontal;

namespace VdnhApi.Controllers
{
    [ApiController]
    [Route("api/horisontal")]
    public class HorisontalController(
    IHorisontal horisontal,
    ILogger<BaseController> logger,
    UserManager<ApplicationUser> userManager,
    IResponceBuilder responceBuilder,
    IUserService userInfoService,
    IOptions<AppSettings> config
    ) : BaseController(logger, userManager, responceBuilder, userInfoService, config)
    {
        [Route("{horisontalId}"), HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetIdHorisontal(long? horisontalId)
        {
            return await GetAnswerAsync(async () => await horisontal.GetHorisontal(horisontalId));
        }

        [Route("all"), HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetIdHorisontalAll ()
        {
            return await GetAnswerAsync(async () => await horisontal.GetHorisontalAll());
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetListHorisontal()
        {
            return await GetAnswerAsync(async () => await horisontal.GetHorisontalList());
        }

        [HttpPost]
        public async Task<IActionResult> PostUpdateHorisontal([FromForm] Domain.RequestModel.HorisontalRequestModel? request)
        {
            return await GetAnswerAsync(async () => await horisontal.UpdateHorisontal(request));
        }

        [HttpPut]
        [AllowAnonymous]
        public async Task<IActionResult> PutInsertHorisontal([FromForm] Domain.RequestModel.HorisontalRequestModel? request)
        {
            return await GetAnswerAsync(async () => await horisontal.InsertHorisontal(request));
        }

        [Route("{horisontalId}"), HttpDelete]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteHorisontal(long? horisontalId)
        {
            return await GetAnswerAsync(async () => await horisontal.DeleteHorisontal(horisontalId));
        }
    }
}
