using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.Application.Authorization.Features.RoleFeatures.Commands.CreateRole;
using MS.Application.Authorization.Features.RoleFeatures.Queries.GetAllRole;
using MS.Application.Authorization.Features.RoleFeatures.Queries.GetUserRole;

namespace MS.Authorization.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<CreateRoleResponse>> Create(CreateRoleRequest request,
        CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);

            if(!result.IsSuccess) 
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<GetAllRoleResponse>>> GetAll(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetAllRoleRequest(), cancellationToken);
            return Ok(response);
        }

        [Authorize]
        [HttpGet("/user/roles")]
        public async Task<ActionResult<List<GetUserRoleResponse>>> GetUserRoles(Guid userId, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetUserRoleRequest(userId), cancellationToken);
            return Ok(response);
        }
    }
}
