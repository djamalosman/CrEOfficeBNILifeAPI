using System.ComponentModel.DataAnnotations;

namespace EOfficeBNILAPI.Models.Table
{
    public class Tr_Delivery_Table
    {
        [Key]
        public Guid ID_DELIVERY { get; set; }
        public int? SHIPPING_TYPE_CODE { get; set; }
        public string? EXPEDITION { get; set; }
        public string? REFERENCE_NUMBER { get; set; }
        public string? RECEIPT_NUMBER { get; set; }
        public string? ADDRESS { get; set; }
        public int? DELIVERY_TYPE_CODE { get; set; }
        public int? STATUS_CODE { get; set; }
        public string? RECEIVER_NAME { get; set; }
        public DateTime? CREATED_ON { get; set; }
        public Guid CREATED_BY { get; set; }
        public DateTime? MODIFIED_ON { get; set; }
        public Guid MODIFIED_BY { get; set; }
        public string? DELIVERY_NUMBER { get; set; }
        public DateTime? RECEIVE_DATE { get; set; }
        public string? DESTINATION_RECEIVER_NAME { get; set; }
        public int DRAFTER_READ_STATUS { get; set; }
        public int SENDER_READ_STATUS { get; set; }
        public string? RECEIVER_PHONE { get; set; }
    }
}
