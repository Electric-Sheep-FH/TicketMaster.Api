using FastEndpoints;
using test_TicketMaster.Api.DTO.Requests;
using test_TicketMaster.Api.DTO.Responses;
using test_TicketMaster.Api.Model;

namespace test_TicketMaster.Api.Endpoints
{
    public class CreateTicketHistoryEndpoint : Endpoint<CreateTicketHistoryRequest,CreateTicketHistoryResponse>
    {
        public override void Configure()
        {
            Post("/ticket");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CreateTicketHistoryRequest req, CancellationToken ct)
        {
            TicketHistory.CreateHistoriedTicket(req.DysfunctionId, req.EmergencyId, req.ClientId, req.Observation, req.UserId);

            await SendAsync(new CreateTicketHistoryResponse { Message = "Ticket created and historied !" });
        }
    }
}
