using FastEndpoints;
using test_TicketMaster.Api.DTO.Requests;
using test_TicketMaster.Api.DTO.Responses;
using test_TicketMaster.Api.Model;

namespace test_TicketMaster.Api.Endpoints
{
    public class UpdateExistingTicketByIdEndpoint : Endpoint<UpdateExistingTicketByIdRequest,UpdateExistingTicketByIdResponse>
    {
        public override void Configure()
        {
            Post("/update-ticket");
            AllowAnonymous();
        }

        public override async Task HandleAsync(UpdateExistingTicketByIdRequest req, CancellationToken ct)
        {
            TicketHistory.UpdateExistingTicketById(req.TicketId, req.Observation, req.StateId, req.UserId);

            await SendAsync(new UpdateExistingTicketByIdResponse { Message = "Ticket updating is done !" });
        }
    }
}
