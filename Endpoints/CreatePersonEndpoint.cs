using FastEndpoints;
using test_TicketMaster.Api.DTO.Requests;
using test_TicketMaster.Api.DTO.Responses;
using test_TicketMaster.Api.Model;

namespace test_TicketMaster.Api.Endpoints
{
    public class CreatePersonEndpoint : Endpoint<CreatePersonRequest,CreatePersonResponse>
    {
        public override void Configure()
        {
            Post("/person");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CreatePersonRequest req, CancellationToken ct)
        {
            Person.CreatePerson(req.Firstname, req.Lastname, req.Birthday, req.Adress, req.PostalCode, req.City, req.LocalPhoneNumber, req.PortablePhoneNumber, req.Mail, req.Password, req.IdRole, req.IdCountry);
            await SendAsync(new CreatePersonResponse { Message = "Person added !" });
        }
    }
}
