namespace test_TicketMaster.Api.DTO.Requests
{
    public class UpdateExistingTicketByIdRequest
    {
        public int TicketId { get; set; }
        public int StateId { get; set; }
        public int UserId { get; set; }
        public string Observation { get; set; }
    }
}
