using System.ComponentModel.DataAnnotations;

namespace EOfficeBNILAPI.Models.Table
{
    public class Tr_Dummy_Table
    {
        [Key]
        public Guid ID_DUMMY { get; set; }
        public string NAMA { get; set; }
        public DateTime INSERT_DATE { get; set; }
        public DateTime? CREATED_ON { get; set; }
        public Guid CREATED_BY { get; set; }
        public DateTime? MODIFIED_ON { get; set; }
        public Guid MODIFIED_BY { get; set; }
        public Guid ID_LETTER { get; set; }
        public Guid ID_USER { get; set; }
        public Guid ID_POSITION { get; set; }

    }
}
