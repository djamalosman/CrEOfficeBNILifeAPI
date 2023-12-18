using System.ComponentModel.DataAnnotations;

namespace EOfficeBNILAPI.Models.Table
{
    public class Tr_Log_Sync_Table
    {
        [Key]
        public Guid ID_LOG_SYNC { get; set; }
        public string REQUEST_JSON { get; set; }
        public string RESPONSE_JSON { get; set; }
        public string METHOD { get; set; }
        public int SYNC_TYPE_CODE { get; set; }
        public DateTime? CREATED_ON { get; set; }
        public DateTime? MODIFIED_ON { get; set; }
    }
}
