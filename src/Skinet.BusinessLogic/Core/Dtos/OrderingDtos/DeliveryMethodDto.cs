namespace Skinet.BusinessLogic.Core.Dtos.OrderingDtos
{
    public class DeliveryMethodDto
    {
        public string ShortName { get; set; }
        public string DeliveryTime { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public MetaDataDto MetaData { get; set; }
    }
}