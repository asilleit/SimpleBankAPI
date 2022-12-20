namespace SimpleBankAPI.Models.Response
{
    public class Movim
    {
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public static Movim FromMovementToMovim(Movement movement)
        {
            var movim = new Movim
            {
                Amount = movement.Amount,
                CreatedAt = movement.CreatedAt,
            };
            return movim;
        }
    }

}
