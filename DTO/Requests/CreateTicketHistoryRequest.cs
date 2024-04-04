namespace test_TicketMaster.Api.DTO.Requests
{
    public class CreateTicketHistoryRequest
    {
        public int DysfunctionId { get; set; }
        public int EmergencyId { get; set; }
        public int ClientId { get; set; }
        public string Observation { get; set; }
        public int UserId { get; set; }

    }
}
