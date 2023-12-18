using System.ComponentModel.DataAnnotations;

namespace EOfficeBNILAPI.Models.Table
{
    public class Tr_SettingSeketaris_Table
    {
        [Key]
        public Guid ID_SETDIRKOM { get; set; }
        public Guid ID_UNIT_SEKETARIS { get; set; }
        public Guid ID_POSITION_SEKETARIS { get; set; }
        public Guid ID_SEKETARIS { get; set; }
        public Guid ID_UNIT_DIRKOM { get; set; }
        public Guid ID_POSITION_DIRKOM { get; set; }
        public Guid ID_DIRKOM { get; set; }

        public int STATUS_CODE { get; set; }
        public DateTime CREATED_ON { get; set; }
        public Guid CREATED_BY { get; set; }
        public DateTime MODIFIED_ON { get; set; }
        public Guid MODIFIED_BY { get; set; }
    }
}
