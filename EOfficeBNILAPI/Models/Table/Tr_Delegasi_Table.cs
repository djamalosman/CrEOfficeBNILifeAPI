using System.ComponentModel.DataAnnotations;

namespace EOfficeBNILAPI.Models.Table
{
    public class Tr_Delegasi_Table
    {
        [Key]
        public Guid ID { get; set; }
        public int STATUS_APPROVER { get; set; }
        public Guid ID_USER { get; set; }
        public Guid ID_USER_DELEGASI { get; set; }

        public DateTime STARTDATE { get; set; }
        public DateTime ENDDATE { get; set; }

        public DateTime? CREATED_ON { get; set; }
        public Guid CREATED_BY { get; set; }
        public DateTime? MODIFIED_ON { get; set; }
        public Guid MODIFIED_BY { get; set; }


    }
}
