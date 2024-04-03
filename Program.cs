using FastEndpoints;
using System.Data.SqlClient;
using test_TicketMaster.Api.Database;
using test_TicketMaster.Api.Model;

namespace test_TicketMaster.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var bld = WebApplication.CreateBuilder();
            bld.Services.AddFastEndpoints();

            var app = bld.Build();
            app.UseFastEndpoints();

            //SeedDB.FeedDB();
            Person.DeleteByIdRole(2);
            app.Run();
        }
    }
}
