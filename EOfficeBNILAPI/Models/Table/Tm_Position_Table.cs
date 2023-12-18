using System.ComponentModel.DataAnnotations;

namespace EOfficeBNILAPI.Models.Table
{
    public class Tm_Position_Table
    {
        [Key]
        public Guid ID_POSITION { get; set; }
        public Guid ID_UNIT { get; set; }
        public string POSITION_NAME { get; set; }
        public Guid? PARENT_ID { get; set; }
        public string? POSITION_CODE { get; set; }
        public int STATUS_CODE { get; set; }
        //public int LEVEL { get; set; }
        public DateTime CREATED_ON { get; set; }
        public Guid CREATED_BY { get; set; }
        public DateTime MODIFIED_ON { get; set; }
        public Guid MODIFIED_BY { get; set; }
        public  Guid? FLAG { get; set; }

        //public string? POSITION_CODE_SUPERIOR { get; set; }
        //public string? POSITION_CODE_OLD { get; set; }
        //public string? UNIT_CODE { get; set; }
    }
}
