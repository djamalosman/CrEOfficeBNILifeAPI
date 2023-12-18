using System.ComponentModel.DataAnnotations;

namespace EOfficeBNILAPI.Models.Table
{
    public class Tm_Procurement_Table
    {
        [Key]
        public Guid ID { get; set; }
        public int STATUS_APPROVE { get; set; }
        public string NAME { get; set; }
        public decimal MIN_NOMINAL { get; set; }
        public decimal MAX_NOMINAL { get; set; }
        public string APPROVER { get; set; }
        public string APPROVER2 { get; set; }
        public DateTime CREATED_ON { get; set; }
        public Guid CREATED_BY { get; set; }
        public DateTime MODIFIED_ON { get; set; }
        public Guid MODIFIED_BY { get; set; }

        public int DELETE_STATUS { get; set; }
        
    }
}
