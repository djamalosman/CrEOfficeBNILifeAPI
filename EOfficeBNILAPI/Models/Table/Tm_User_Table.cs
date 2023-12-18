using System.ComponentModel.DataAnnotations;

namespace EOfficeBNILAPI.Models.Table
{
    public class Tm_User_Table
    {
        [Key]
        public Guid ID_USER { get; set; }
        public Guid ID_POSITION { get; set; }
        public string NIP { get; set; }
        public string FULLNAME { get; set; }
        public string USERNAME { get; set; }
        public string PASSWORD { get; set; }
        public string EMAIL { get; set; }
        public Int64 PHONE { get; set; }
        public string ID_GROUP { get; set; }
        public DateTime CREATED_ON { get; set; }
        public Guid CREATED_BY { get; set; }
        public DateTime MODIFIED_ON { get; set; }
        public Guid MODIFIED_BY { get; set; }
        public int STATUS_CODE { get; set; }
        public Guid? RECOVERY_TOKEN { get; set; }
        public Guid? ID_ONE_SIGNAL { get; set; }
    }
}
