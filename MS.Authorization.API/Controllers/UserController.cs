using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MS.Application.Authorization.Features.UserFeatures.Commands.CreateUser;
using MS.Application.Authorization.Features.UserFeatures.Queries.GetAllUser;
using System.Security.Claims;

namespace MS.Authorization.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        public async Task<ActionResult<CreateUserResponse>> Create(CreateUserRequest request,
        CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);

            if (!response.IsSuccess)
            {
                return BadRequest(response.Error);
            }

            return Ok(response.Value);
        }

        [HttpGet]
        public async Task<ActionResult<List<GetAllUserResponse>>> GetAll(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetAllUserRequest(), cancellationToken);
            var user = HttpContext.User.Identity as ClaimsIdentity;
            return Ok(response);
        }
    }
}
