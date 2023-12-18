using System.ComponentModel.DataAnnotations;

namespace EOfficeBNILAPI.Models.Table
{
    public class Tr_Memo_Type_Table
    {
        [Key]
        public Guid ID_MEMO_TYPE { get; set; }
        public Guid ID_STRINGMAP { get; set; }

        public string MEMO_TYPE_NAME { get; set; }

        public DateTime? CREATED_ON { get; set; }
        public Guid CREATED_BY { get; set; }
        public DateTime? MODIFIED_ON { get; set; }
        public Guid MODIFIED_BY { get; set; }

        public int STATUS_CODE { get; set; }

        public string MEMO_TYPE_CODE { get; set; }
    }
}
