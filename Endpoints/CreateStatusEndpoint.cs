using FastEndpoints;
using test_TicketMaster.Api.DTO.Requests;
using test_TicketMaster.Api.DTO.Responses;
using test_TicketMaster.Api.Model;

namespace test_TicketMaster.Api.Endpoints
{
    public class CreateStatusEndpoint : Endpoint<CreateStatusRequest,CreateStatusResponse>
    {
        public override void Configure()
        {
            Post("/status");
            AllowAnonymous();
        }
        public override async Task HandleAsync(CreateStatusRequest req, CancellationToken ct)
        {
            Status.CreateStatus(req.Name);
            await SendAsync(new CreateStatusResponse { Message = "Nouveau statut ajouté !" });
        }
    }
}
