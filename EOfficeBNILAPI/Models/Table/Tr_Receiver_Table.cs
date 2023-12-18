using System.ComponentModel.DataAnnotations;

namespace EOfficeBNILAPI.Models.Table
{
    public class Tr_Receiver_Table
    {
        [Key]
        public Guid ID_RECEIVER { get; set; }
        public Guid ID_LETTER { get; set; }
        public Guid ID_USER_RECEIVER { get; set; }
        public Guid ID_POSITION_RECEIVER { get; set; }
        public DateTime? CREATED_ON { get; set; }
        public Guid CREATED_BY { get; set; }
        public DateTime? MODIFIED_ON { get; set; }
        public Guid MODIFIED_BY { get; set; }
    }

}
