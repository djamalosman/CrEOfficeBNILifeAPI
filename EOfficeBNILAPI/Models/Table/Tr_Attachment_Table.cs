using System.ComponentModel.DataAnnotations;

namespace EOfficeBNILAPI.Models.Table
{
    public class Tr_Attachment_Table
    {
        [Key]
        public Guid ID_ATTACHMENT { get; set; }
        public Guid ID_LETTER { get; set; }
        public string FILENAME { get; set; }
        public int? STATUS_CODE { get; set; }
        public int? IS_DOC_LETTER { get; set; }
        public DateTime? CREATED_ON { get; set; }
        public Guid CREATED_BY { get; set; }
        public DateTime? MODIFIED_ON { get; set; }
        public Guid MODIFIED_BY { get; set; }
    }
}
