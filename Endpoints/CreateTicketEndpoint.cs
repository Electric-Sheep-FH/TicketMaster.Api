using FastEndpoints;
using test_TicketMaster.Api.Model;
using test_TicketMaster.Api.DTO.Requests;
using test_TicketMaster.Api.DTO.Responses;

namespace test_TicketMaster.Api.Endpoints
{
    public class CreateTicketEndpoint : Endpoint<CreateTicketRequest,CreateTicketResponse>
    {
        public override void Configure()
        {
            Post("/tickets");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CreateTicketRequest req, CancellationToken ct)
        {
            var ticket = Ticket.CreateTicket(req.Created, req.Observation);
            await SendAsync(new CreateTicketResponse { Id = ticket });
        }
    }
}
