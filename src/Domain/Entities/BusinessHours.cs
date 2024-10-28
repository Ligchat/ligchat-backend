namespace tests_.src.Domain.Entities
{
    public class BusinessHours
    {
        public int Id { get; set; }

        public string DayOfWeek { get; set; }

        public string OpeningTime { get; set; } 

        public string ClosingTime { get; set; }

        public bool IsOpen { get; set; }

        public int SectorId { get; set; }
    }
}
