namespace buckstore.orders.service.application.DTOs
{
    public class MakePurchaseDto
    {
        public string CardNumber { get; set; }
        public decimal PurchaseValue { get; set; }

        public MakePurchaseDto(string cardNumber, decimal purchaseValue)
        {
            CardNumber = cardNumber;
            PurchaseValue = purchaseValue;
        }
    }
}