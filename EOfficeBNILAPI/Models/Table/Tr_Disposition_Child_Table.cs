using System.ComponentModel.DataAnnotations;

namespace EOfficeBNILAPI.Models.Table
{
    public class Tr_Disposition_Child_Table
    {
        [Key]
        public Guid ID_DISPOSITION_CHILD { get; set; }
        public Guid ID_DISPOSITION_HEADER { get; set; }
        public Guid ID_USER { get; set; }
        public Guid ID_POSITION { get; set; }
        public string NOTES { get; set; }
        public int READ_STATUS { get; set; }
        public DateTime? CREATED_ON { get; set; }
        public Guid CREATED_BY { get; set; }
        public DateTime? MODIFIED_ON { get; set; }
        public Guid MODIFIED_BY { get; set; }
    }
}
