using FastEndpoints;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using test_TicketMaster.Api.DTO.Requests;
using test_TicketMaster.Api.DTO.Responses;
using test_TicketMaster.Api.Model;

namespace test_TicketMaster.Api.Endpoints
{
    public class GetPersonByIdRoleEndpoint : Endpoint<GetPersonByIdRequest, GetPersonByIdRoleResponse>
    {
        public override void Configure()
        {
            Get("/person");
            AllowAnonymous();
        }

        public override async Task HandleAsync(GetPersonByIdRequest req, CancellationToken ct)
        {
            var persons = Person.GetPersonByIdRole(req.RoleId);

            await SendAsync(new GetPersonByIdRoleResponse { Persons = persons});
        }
    }
}
