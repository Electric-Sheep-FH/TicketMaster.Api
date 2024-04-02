using FastEndpoints;
using test_TicketMaster.Api.DTO.Requests;
using test_TicketMaster.Api.DTO.Responses;
using test_TicketMaster.Api.Model;

namespace test_TicketMaster.Api.Endpoints
{
    public class CreateCountryEndpoint : Endpoint<CreateCountryRequest,CreateCountryResponse>
    {
        public override void Configure()
        {
            Post("/country");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CreateCountryRequest req, CancellationToken ct)
        {
            Country.CreateCountry(req.CountryName);
            await SendAsync(new CreateCountryResponse { Message = "Country added !"});
        }
    }
}
