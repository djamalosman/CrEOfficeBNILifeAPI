using System.ComponentModel.DataAnnotations;

namespace EOfficeBNILAPI.Models.Table
{
    public class Tr_Tbl_Status_Table
    {
        [Key]
        public Guid ID_TBL_STATUS { get; set; }
        public Guid ID_LETTER { get; set; }
        public Guid ID_USER { get; set; }
        public Guid ID_POSITION { get; set; }
        public int READ_STATUS { get; set; }
        public DateTime SEND_DATE { get; set; }
        public DateTime? CREATED_ON { get; set; }
        public Guid CREATED_BY { get; set; }
        public DateTime? MODIFIED_ON { get; set; }
        public Guid MODIFIED_BY { get; set; }
    }
}
