using System.ComponentModel.DataAnnotations;

namespace EOfficeBNILAPI.Models.Table
{
    public class Tr_Procurement_Table
    {
        [Key]
        public Guid ID_PR { get; set; }
        public Guid ID_PROCUREMENT { get; set; }
        public Guid ID_LETTER { get; set; }
        public decimal? NOMINAL { get; set; }
        
        public DateTime CREATED_ON { get; set; }
        public Guid CREATED_BY { get; set; }
        public DateTime MODIFIED_ON { get; set; }

        public Guid MODIFIED_BY { get; set; }
    }
}
