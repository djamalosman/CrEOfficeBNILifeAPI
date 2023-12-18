using System.ComponentModel.DataAnnotations;

namespace EOfficeBNILAPI.Models.Table
{
    public class Tr_Document_Table
    {
        [Key]
        public Guid ID_DOCUMENT { get; set; }
        public int DOCUMENT_TYPE { get; set; }
        public string TRACKING_NUMBER { get; set; }
        public int QTY_TOTAL { get; set; }
        public string SENDER_NAME { get; set; }
        public string? DOC_RECEIVER { get; set; }
        public DateTime RECEIVED_DATE { get; set; }
        public DateTime? DISTRIBUTION_TIME { get; set; }
        public int STATUS_CODE { get; set; }
        public DateTime? CREATED_ON { get; set; }
        public Guid CREATED_BY { get; set; }
        public DateTime? MODIFIED_ON { get; set; }
        public Guid MODIFIED_BY { get; set; }

    }
}
