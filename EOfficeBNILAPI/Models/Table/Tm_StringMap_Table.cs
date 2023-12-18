using System.ComponentModel.DataAnnotations;

namespace EOfficeBNILAPI.Models.Table
{
    public class Tm_StringMap_Table
    {
        [Key]
        public Guid ID_STRINGMAP { get; set; }
        public string OBJECTNAME { get; set; }
        public string ATTRIBUTENAME { get; set; }
        public int ATTRIBUTEVALUE { get; set; }
        public string VALUE { get; set; }
        public int STATUS_CODE { get; set; }
    }
}
