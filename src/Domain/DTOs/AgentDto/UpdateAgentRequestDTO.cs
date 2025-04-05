namespace LigChat.Backend.Domain.DTOs.AgentDto
{
    public class UpdateAgentRequestDTO
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string ApiKey { get; set; }
        public string Model { get; set; }
        public bool Status { get; set; }
        public string? Prompt { get; set; }
    }
} 