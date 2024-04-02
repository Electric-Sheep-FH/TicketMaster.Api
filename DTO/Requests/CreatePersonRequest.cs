namespace test_TicketMaster.Api.DTO.Requests
{
    public class CreatePersonRequest
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateOnly Birthday { get; set; }
        public string Adress { get; set; }
        public int PostalCode { get; set; }
        public string City { get; set; }
        public string LocalPhoneNumber { get; set; }
        public string PortablePhoneNumber { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public int IdRole { get; set; }
        public int IdCountry { get; set; }
    }
}
