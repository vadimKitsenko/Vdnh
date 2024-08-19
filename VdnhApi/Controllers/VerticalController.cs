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
using Services.Admin.Vertical;

namespace VdnhApi.Controllers
{
    [ApiController]
    [Route("api/vertical")]
    public class VerticalController(
    IVertical vertical,
    ILogger<BaseController> logger,
    UserManager<ApplicationUser> userManager,
    IResponceBuilder responceBuilder,
    IUserService userInfoService,
    IOptions<AppSettings> config
    ) : BaseController(logger, userManager, responceBuilder, userInfoService, config)
    {
        [Route("{verticalId}/{periodId}"), HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetIdVertical(long? verticalId, long? periodId)
        {
            return await GetAnswerAsync(async () => await vertical.GetVertical(verticalId, periodId));
        }

        [Route("all"), HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetIdVerticalAll()
        {
            return await GetAnswerAsync(async () => await vertical.GetVerticalAll());
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetListVertical()
        {
            return await GetAnswerAsync(async () => await vertical.GetVerticalList());
        }

        [HttpPost]
        public async Task<IActionResult> PostUpdateVertical([FromForm] Domain.RequestModel.VerticalRequestModel? request)
        {
            return await GetAnswerAsync(async () => await vertical.UpdateVertical(request));
        }

        [HttpPut]
        [AllowAnonymous]
        public async Task<IActionResult> PutInsertVertical([FromForm] Domain.RequestModel.VerticalRequestModel? request)
        {
            return await GetAnswerAsync(async () => await vertical.InsertVertical(request));
        }

        [Route("{verticalId}"), HttpDelete]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteVertical(long? verticalId)
        {
            return await GetAnswerAsync(async () => await vertical.DeleteVertical(verticalId));
        }
    }
}
