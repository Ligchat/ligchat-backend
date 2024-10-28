namespace tests_.src.Application.Services
{
    using global::tests_.src.Application.Common.Utilities;
    using global::tests_.src.Domain.Entities;
    using MongoDB.Driver;
    using System.Collections.Generic;
    using System.Threading.Tasks;


    namespace tests_.src.Application.Services
    {
        public class FlowWhatsappService
        {
            private readonly IMongoCollection<FlowWhatsapp> _flowsWhatsapp;

            public FlowWhatsappService(MongoDbContext mongoDbContext)
            {
                _flowsWhatsapp = mongoDbContext.FlowsWhatsapp;
            }

            public async Task<List<FlowWhatsapp>> GetAllFlowsAsync()
            {
                return await _flowsWhatsapp.Find(_ => true).ToListAsync();
            }

            public async Task<FlowWhatsapp> GetFlowByIdAsync(string id)
            {
                return await _flowsWhatsapp.Find(flow => flow.Id == id).FirstOrDefaultAsync();
            }

            public async Task CreateFlowAsync(FlowWhatsapp flow)
            {
                await _flowsWhatsapp.InsertOneAsync(flow);
            }

            public async Task UpdateFlowAsync(string id, FlowWhatsapp flowIn)
            {
                await _flowsWhatsapp.ReplacLigChateAsync(flow => flow.Id == id, flowIn);
            }

            public async Task RemoveFlowAsync(string id)
            {
                await _flowsWhatsapp.DeletLigChateAsync(flow => flow.Id == id);
            }
        }
    }

}
