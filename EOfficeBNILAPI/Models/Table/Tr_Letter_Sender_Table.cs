using System.ComponentModel.DataAnnotations;

namespace EOfficeBNILAPI.Models.Table
{
    public class Tr_Letter_Sender_Table
    {
        [Key]
        public Guid ID_LETTER_SENDER { get; set; }
        public Guid ID_LETTER { get; set; }
        public Guid ID_USER_SENDER { get; set; }
        public Guid ID_POSITION_SENDER { get; set; }
        public Guid ID_UNIT_SENDER { get; set; }
        public int ID_LEVEL_SENDER { get; set; }
        public int IS_MAIN { get; set; }
        public DateTime? CREATED_ON { get; set; }
        public Guid CREATED_BY { get; set; }
        public DateTime? MODIFIED_ON { get; set; }
        public Guid MODIFIED_BY { get; set; }
        public string? SENDER_POSITION_NAME { get; set; }
    }
}
