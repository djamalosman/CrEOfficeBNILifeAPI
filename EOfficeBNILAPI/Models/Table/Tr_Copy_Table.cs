using System.ComponentModel.DataAnnotations;

namespace EOfficeBNILAPI.Models.Table
{
    public class Tr_Copy_Table
    {
        [Key]
        public Guid ID_COPY { get; set; }
        public Guid ID_LETTER { get; set; }
        public Guid ID_USER_COPY { get; set; }
        public Guid ID_POSITION_COPY { get; set; }
        public DateTime? CREATED_ON { get; set; }
        public Guid CREATED_BY { get; set; }
        public DateTime? MODIFIED_ON { get; set; }
        public Guid MODIFIED_BY { get; set; }
    }
}
