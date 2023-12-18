namespace EOfficeBNILAPI.Models
{
    public class DeliveryLetterOutput
    {
        public string letterNumber { get; set; }
        public DateTime? receiveDate { get; set; }
        public string sender { get; set; }
        public string senderDivision { get; set; }
        public string receiver { get; set; }
        public string destination_receiver_name { get; set; }
        public string receiverPhone { get; set; }
        public int drafterReadStatus { get; set; }
        public int senderReadStatus { get; set; }
        public string receiverAddress { get; set; }
        public string? status { get; set; }
        public int? statusCode { get; set; }
        public int? shippingType { get; set; }
        public int? deliveryType { get; set; }
        public Guid idLetter { get; set; }
        public Guid? idDelivery { get; set; }
        public DateTime? modifiedOn { get; set; }
        public Guid letterMaker { get; set; }
        public string? deliveryNumber { get; set; }
        public string? receiptNumber { get; set; }

    }
    public class ParamInsertDelivery
    {
        public string idLetter { get; set; }
        public int? shippingType { get; set; }
        public DateTime? receiveDate { get; set; }
        public string? expedition { get; set; }
        public string? referenceNumber { get; set; }
        public string? receiptNumber { get; set; }
        public string? address { get; set; }
        public int? deliveryType { get; set; }
        public int? status { get; set; }
        public string? receiverName { get; set; }
        public string destination_receiver_name { get; set; }
        public string receiverPhone { get; set; }
        public int drafterReadStatus { get; set; }
        public int senderReadStatus { get; set; }

    }
    public class InsertListLetterDelivery
    {
        public Guid idLetter { get; set; }
    }
    public class ParamGenerateDeliveryNumber
    {
        public string? delivNumber { get; set; }
        public int delivType { get; set; }
        public Guid idDeliv { get; set; }
    }
    public class DeliveryDetailOutput
    {
        public Guid? idDelivery { get; set; }
        public string? deliveryNumber { get; set; }
        public int? shippingTypeCode { get; set; }
        public string shippingTypeCodeValue { get; set; }
        public string? expedition { get; set; }
        public string? referenceNumber { get; set; }
        public string? receiptNumber { get; set; }
        public string address { get; set; }
        public int? deliveryTypeCode { get; set; }
        public string deliveryTypeCodeValue { get; set; }
        public int? statusCode { get; set; }
        public string statusCodeValue { get; set; }
        public string receiverName { get; set; }
        public string destination_receiver_name { get; set; }
        public string receiverPhone { get; set; }
        public int drafterReadStatus { get; set; }
        public int senderReadStatus { get; set; }
        public DateTime? createdOn { get; set; }
        public DateTime? receiveDate { get; set; }
        public List<DeliveryLetterOutput> letter { get; set; }

    }
    public class ParamUpdateDelivery
    {
        public int saveType { get; set; }
        public Guid idDelivery { get; set; }
        public string idLetter { get; set; }
        public int? shippingType { get; set; }
        public string? expedition { get; set; }
        public string? referenceNumber { get; set; }
        public string? receiptNumber { get; set; }
        public string? address { get; set; }
        public int? deliveryType { get; set; }
        public int? status { get; set; }
        public string? receiverName { get; set; }
        public string? destination_receiver_name { get; set; }
        public string receiverPhone { get; set; }
        public int drafterReadStatus { get; set; }
        public int senderReadStatus { get; set; }
        public string? deliveryNumber { get; set; }
        public DateTime? receiveDate { get; set; }
    }
    public class ParamGetReportEkspedisiEoffice
    {
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public int statusElse { get; set; }

    }

    public class ParamGetReportSeacrhoutGoing
    {
      
        public int statusElse { get; set; }

        public string trackingNumber { get; set; }


    }
    public class DeliveryReportOutput
    {
        public string letterNumber { get; set; }
        public DateTime? receiveDate { get; set; }
        public string sender { get; set; }
        public string senderDivision { get; set; }
        public string receiver { get; set; }
        public string destination_receiver_name { get; set; }
        public int drafterReadStatus { get; set; }
        public int senderReadStatus { get; set; }
        public string receiverAddress { get; set; }
        public string receiverPhone { get; set; }
        public int? shippingTypeCode { get; set; }
        public string shippingTypeCodeValue { get; set; }
        public int? deliveryTypeCode { get; set; }
        public string deliveryTypeCodeValue { get; set; }
        public int? statusCode { get; set; }
        public string statusCodeValue { get; set; }
        public Guid? idDelivery { get; set; }
        public DateTime? modifiedOn { get; set; }
        public string? deliveryNumber { get; set; }
        public string? referenceNumber { get; set; }
        public string? receiptNumber { get; set; }
        public string? expedition { get; set; }

    }
    public class ParamUploadEkspedisiEoffice
    {
        public string letterNumber { get; set; }
        public string sender { get; set; }
        public string senderDivision { get; set; }
        public string? expedition { get; set; }
        public string? referenceNumber { get; set; }
        public string? receiptNumber { get; set; }
        public string receiver { get; set; }
        public string destination_receiver_name { get; set; }
        public int drafterReadStatus { get; set; }
        public int senderReadStatus { get; set; }
        public string receiverAddress { get; set; }
        public string shippingTypeCodeValue { get; set; }
        public string? deliveryTypeCodeValue { get; set; }
        public DateTime? receiveDate { get; set; }
        public string StatusCodeValue { get; set; }
    }
    public class ParamUploadEkspedisiEofficeString
    {
        public string jsonDataString { get; set; }
    }
    public class UploadEkspedisiEofficeOutput
    {
        public string letterNumber { get; set; }
        public string sender { get; set; }
        public string senderDivision { get; set; }
        public string? expedition { get; set; }
        public string? referenceNumber { get; set; }
        public string? receiptNumber { get; set; }
        public string receiver { get; set; }
        public string destination_receiver_name { get; set; }
        public int drafterReadStatus { get; set; }
        public int senderReadStatus { get; set; }
        public string receiverAddress { get; set; }
        public string shippingTypeCodeValue { get; set; }
        public string? deliveryTypeCodeValue { get; set; }
        public string statusCodeValue { get; set; }
        public string statusUpload { get; set; }
    }
    public class DeliveryReportOutputDashboard
    {
        public string letterNumber { get; set; }
        public DateTime? receiveDate { get; set; }
        public string sender { get; set; }
        public string senderDivision { get; set; }
        public string receiver { get; set; }
        public string destination_receiver_name { get; set; }
        public int drafterReadStatus { get; set; }
        public int senderReadStatus { get; set; }
        public string receiverAddress { get; set; }
        public int? shippingTypeCode { get; set; }
        public string shippingTypeCodeValue { get; set; }
        public int? deliveryTypeCode { get; set; }
        public string deliveryTypeCodeValue { get; set; }
        public int? statusCode { get; set; }
        public string statusCodeValue { get; set; }
        public Guid? idDelivery { get; set; }
        public DateTime? modifiedOn { get; set; }
        public string? deliveryNumber { get; set; }
        public string? referenceNumber { get; set; }
        public string? receiptNumber { get; set; }
        public string? expedition { get; set; }
        public Guid createdBy { get; set; }
        public Guid sendBy { get; set; }

    }
    public class ParamGetSKDelivery
    {
        public string type { get; set; }
        public Guid? idDelivery { get; set; }
    }
}
