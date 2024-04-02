using FastEndpoints;
using test_TicketMaster.Api.DTO.Requests;
using test_TicketMaster.Api.DTO.Responses;
using test_TicketMaster.Api.Model;

namespace test_TicketMaster.Api.Endpoints
{
    public class CreateRoleEndpoint : Endpoint<CreateRoleRequest,CreateRoleResponse>
    {
        public override void Configure()
        {
            Post("/role");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CreateRoleRequest req, CancellationToken ct)
        {
            Role.CreateRole(req.Name);
            await SendAsync(new CreateRoleResponse { Message = "Response = Added !" });
        }
    }
}
