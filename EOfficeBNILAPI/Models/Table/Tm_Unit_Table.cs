using System.ComponentModel.DataAnnotations;

namespace EOfficeBNILAPI.Models.Table
{
    public class Tm_Unit_Table
    {
        [Key]
        public Guid ID_UNIT { get; set; }
        public Guid ID_BRANCH { get; set; }
        public string UNIT_NAME { get; set; }
        public Guid? PARENT_ID { get; set; }
        public int STATUS_CODE { get; set; }
        public string UNIT_TYPE { get; set; }
        public string UNIT_CODE { get; set; }
        public DateTime CREATED_ON { get; set; }
        public Guid CREATED_BY { get; set; }
        public DateTime MODIFIED_ON { get; set; }
        public Guid MODIFIED_BY { get; set; }
        //public string UNIT_CODE_SUPERIOR { get; set; }
        //public string UNIT_CODE_OLD { get; set; }
    }
}
