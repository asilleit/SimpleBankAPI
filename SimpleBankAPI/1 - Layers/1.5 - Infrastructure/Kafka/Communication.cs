using SimpleBankAPI.Models;

namespace SimpleBankAPI.Infrastructure.Kafka
{
    public class Communication
    {
        public string Body { get; set; }
        public string Subject { get; set; }
        public User User { get; set; }
    }
}
