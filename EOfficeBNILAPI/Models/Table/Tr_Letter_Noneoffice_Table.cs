using System.ComponentModel.DataAnnotations;

namespace EOfficeBNILAPI.Models.Table
{
    public class Tr_Letter_Noneoffice_Table
    {
        [Key]
        public Guid ID_MAILING_NON_EOFFICE { get; set; }

        public string? LETTER_NUMBER { get; set; }
        public int DELIVERY_TYPE { get; set; }

        public int REPORT_TYPE { get; set; }

        public string? NO_AWB { get; set; }

        public DateTime RECEIPTDATE { get; set; }

        public string? EXPEDITION_NAME { get; set; }

        public string? SENDER_NAME { get; set; }

        public string? NIP { get; set; }

        public Guid ID_UNIT { get; set; }

        public string? DOC_RECEIVER { get; set; }

        public string? REFERENCE_NUMBER { get; set; }

        public string? ADDRESS { get; set; }

        public int STATUS_SENDER { get; set; }

        public DateTime? CREATED_ON { get; set; }
        public Guid CREATED_BY { get; set; }
        public DateTime? MODIFIED_ON { get; set; }
        public Guid MODIFIED_BY { get; set; }

        public int STATUS_CODE { get; set; }
        public int? READ_STATUS { get; set; }

        public string? PURPOSE_NAME { get; set; }

        public string? PHONE_NUMBER { get; set; }

        public DateTime? DATE_UNTIL { get; set; }

        public string? QRCODE_NUMBER { get; set; }
    }
}
