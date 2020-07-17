namespace TestTaskNotissimus
{
    public class OfferEntity
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Url { get; set; }
        public int Price { get; set; }
        public string CurrencyId { get; set; }
        public int CategoryId { get; set; }
        public string Picture { get; set; }
        public bool? Delivery { get; set; }
        public int? LocalDeliveryCost { get; set; }
        public string Description { get; set; }
        public string CustomFields { get; set; }
    }
}