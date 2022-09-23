using System;

namespace eStore.Dev.Models
{
    public class Item
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; } = DateTime.Today;
        public DateTime EndTime { get; set; } = DateTime.Now;
        public double Value { get; set; }
    }
}