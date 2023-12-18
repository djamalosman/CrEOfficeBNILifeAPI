using System.ComponentModel.DataAnnotations;

namespace EOfficeBNILAPI.Models.Table
{
    public class Tm_Menu_Table
    {
        [Key]
        public Guid ID_MENU { get; set; }
        public string MENU { get; set; }
        public string AUTHORIZATIONS { get; set; }
        public string LINK { get; set; }
        public Guid? PARENT_ID { get; set; }
        public int MENU_ORDER { get; set; }
        public char HAS_CHILD { get; set; }
        public string? ICON { get; set; }
        public int STATUS_CODE { get; set; }
        public DateTime? CREATED_ON { get; set; }
        public Guid CREATED_BY { get; set; }
        public DateTime? MODIFIED_ON { get; set; }
        public Guid MODIFIED_BY { get; set; }
        public string? LINK_M { get; set; }
        public string? EXCLUDE_M { get; set; }
        public string? ICON_M { get; set; }

        public string MENU_EN { get; set; }
    }
}
