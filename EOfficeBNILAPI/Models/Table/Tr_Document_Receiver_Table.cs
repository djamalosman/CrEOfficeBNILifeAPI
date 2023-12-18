using System.ComponentModel.DataAnnotations;

namespace EOfficeBNILAPI.Models.Table
{
    public class Tr_Document_Receiver_Table
    {
        [Key]
        public Guid ID_DOCUMENT_RECEIVER { get; set; }
        public Guid ID_DOCUMENT { get; set; }
        public Guid ID_UNIT { get; set; }
        public Guid? ID_USER_TU { get; set; }
        public int STATUS_CODE { get; set; }
        public int? RECEIVED_DOCUMENT { get; set; }
        public int? RETURNED_DOCUMENT { get; set; }
        public string? RETURN_NUMBER { get; set; }
        public DateTime? RECEIVED_DATE { get; set; }
        public DateTime? CREATED_ON { get; set; }
        public Guid CREATED_BY { get; set; }
        public DateTime? MODIFIED_ON { get; set; }
        public Guid MODIFIED_BY { get; set; }


    }
}
