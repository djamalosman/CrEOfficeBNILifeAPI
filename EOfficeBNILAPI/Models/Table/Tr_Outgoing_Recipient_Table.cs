using System.ComponentModel.DataAnnotations;

namespace EOfficeBNILAPI.Models.Table
{
    public class Tr_Outgoing_Recipient_Table
    {
        [Key]
        public Guid ID_OUTGOING_RECIPIENT { get; set; }
        public Guid ID_LETTER { get; set; }
        public string? RECIPIENT_NAME { get; set; }
        public string? RECIPIENT_ADDRESS { get; set; }
        public string? RECEPIENT_COMPANY { get; set; }
        public string? RECIPIENT_NUMBER { get; set; }
        public string? DESCRIPTION { get; set; }
        public DateTime? CREATED_ON { get; set; }
        public Guid CREATED_BY { get; set; }
        public DateTime? MODIFIED_ON { get; set; }
        public Guid MODIFIED_BY { get; set; }
    }
}
