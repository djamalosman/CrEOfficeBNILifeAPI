using System.ComponentModel.DataAnnotations;

namespace EOfficeBNILAPI.Models.Table
{
    public class Tr_Document_Log_Table
    {
        [Key]
        public Guid ID_LOG_DOCUMENT { get; set; }
        public Guid ID_DOCUMENT { get; set; }
        public string DESCRIPTION { get; set; }
        public string? COMMENT { get; set; }
        public DateTime? CREATED_ON { get; set; }
        public Guid CREATED_BY { get; set; }
        public DateTime? MODIFIED_ON { get; set; }
        public Guid MODIFIED_BY { get; set; }

    }
}
