using System.ComponentModel.DataAnnotations;

namespace EOfficeBNILAPI.Models.Table
{
    public class Tr_Deliberation_Table
    {
        [Key]
       public Guid ID_DELIBERATION { get; set; }
       public Guid ID_LETTER { get; set; }
       public Guid ID_USER { get; set; }
        public Guid ID_POSITION { get; set; }
        public int STATUS_CODE { get; set; }
        public string? COMMENT { get; set; }

        public Guid ID_USER_DELEGASI { get; set; }
    }
}
