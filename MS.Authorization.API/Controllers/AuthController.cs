using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MS.Application.Authorization.Common.Models;
using MS.Application.Authorization.Features.AuthFeatures.Commands.UpdateRefreshToken;
using MS.Application.Authorization.Features.UserFeatures.Queries.LoginUser;
using MS.Authorization.API.Services;
using MS.Domain.Authorization.Entities;

namespace MS.Authorization.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IMediator mediator, CookieService cookieService) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly CookieService _cookieService = cookieService;

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<LoginUserResponse>> Login(LoginUserRequest request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);

            if (!response.IsSuccess)
            {
                return BadRequest(response.Error);
            }

            _cookieService.SetTokenCookies(response.Value!.Token, response.Value.RefreshToken);

            return Ok(response.Value);
        }

        [HttpPost]
        public async Task<ActionResult<Result<UpdateRefreshTokenResponse>>> Refresh(UpdateRefreshTokenRequest request, CancellationToken cancellationToken)
        {

        }
    }
}
