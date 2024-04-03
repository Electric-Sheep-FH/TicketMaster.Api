using FastEndpoints;
using test_TicketMaster.Api.DTO.Requests;
using test_TicketMaster.Api.DTO.Responses;
using test_TicketMaster.Api.Model;

namespace test_TicketMaster.Api.Endpoints
{
    public class DeletePersonByIdEndpoint : Endpoint<DeletePersonByIdRequest, DeletePersonByIdResponse>
    {
        public override void Configure()
        {
            Delete("/person");
            AllowAnonymous();
        }
        public override async Task HandleAsync(DeletePersonByIdRequest req, CancellationToken ct)
        {
            Person.DeletePersonById(req.IdToDelete);
            await SendAsync(new DeletePersonByIdResponse { Message = "Id deleted !" });
        }
    }
}