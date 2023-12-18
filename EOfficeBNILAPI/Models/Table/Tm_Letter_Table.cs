using System.ComponentModel.DataAnnotations;

namespace EOfficeBNILAPI.Models.Table
{
    public class Tm_Letter_Table
    {
        [Key]
        public Guid ID_LETTER { get; set; }
        public Guid? ID_DOCUMENT { get; set; }
        public string LETTER_NUMBER { get; set; }
        public DateTime LETTER_DATE { get; set; }
        public string? ABOUT { get; set; }
        public string? ATTACHMENT_DESC { get; set; }
        public string? PRIORITY { get; set; }
        public int LETTER_TYPE_CODE { get; set; }
        public string? SENDER_NAME { get; set; }
        public string? SENDER_ADDRESS { get; set; }
        public DateTime? LETTER_DATE_IN { get; set; }
        public string? LETTER_NUMBER_IN { get; set; }
        public int? DOCUMENT_TYPE_CODE { get; set; }
        public int STATUS_CODE { get; set; }
        public int? OUTBOX_TYPE_CODE { get; set; }
        public int? RESULT_TYPE_CODE { get; set; }
        public int? SIGNATURE_TYPE_CODE { get; set; }
        public Guid CREATED_BY_POSITION_ID { get; set; }
        public DateTime? CREATED_ON { get; set; }
        public Guid CREATED_BY { get; set; }
        public DateTime? MODIFIED_ON { get; set; }
        public Guid MODIFIED_BY { get; set; }
        public Guid? MEMO_TYPE_ID { get; set; }
        public string? LETTER_DELIBERATION_NUMBER { get; set; }
    }
}
