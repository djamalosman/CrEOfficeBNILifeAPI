using System.ComponentModel.DataAnnotations;

namespace EOfficeBNILAPI.Models.Table
{
    public class Tr_Content_Table
    {
        [Key]
        public Guid ID_CONTENT { get; set; }
        public Guid ID_LETTER { get; set; }
        public string? LETTER_CONTENT { get; set; }
        public DateTime? CREATED_ON { get; set; }
        public Guid CREATED_BY { get; set; }
        public DateTime? MODIFIED_ON { get; set; }
        public Guid MODIFIED_BY { get; set; }

        public string? SUMMARY { get; set; }
    }
}
