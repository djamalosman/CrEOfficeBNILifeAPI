using System.ComponentModel.DataAnnotations;

namespace EOfficeBNILAPI.Models.Table
{
    public class Tr_Level_Employee
    {
        [Key]
        public Guid ID_EMP_LEVEL { get; set; }
        public Guid ID_USER { get; set; }
        public int ID_LEVEL { get; set; }
        public Guid ID_UNIT { get; set; }
        public DateTime CREATED_ON { get; set; }
        public Guid CREATED_BY { get; set; }
        public DateTime MODIFIED_ON { get; set; }
        public Guid MODIFIED_BY { get; set; }
        public int STATUS_CODE { get; set; }



    }
}
