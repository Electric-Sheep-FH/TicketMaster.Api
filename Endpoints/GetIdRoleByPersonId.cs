using FastEndpoints;
using test_TicketMaster.Api.DTO.Requests;
using test_TicketMaster.Api.DTO.Responses;
using test_TicketMaster.Api.Model;

namespace test_TicketMaster.Api.Endpoints
{
    public class GetIdRoleByPersonId : Endpoint<GetIdRoleByPersonIdRequest,GetIdRoleByPersonIdResponse>
    {
        public override void Configure()
        {
            Get("/persons");
            AllowAnonymous();
        }

        public override async Task HandleAsync(GetIdRoleByPersonIdRequest req, CancellationToken ct)
        {
            int searchedID = Person.GetIdRoleByPersonId(req.PersonId);
            await SendAsync(new GetIdRoleByPersonIdResponse { Message = $"ID role of the person = {searchedID}" });
        }
    }
}
