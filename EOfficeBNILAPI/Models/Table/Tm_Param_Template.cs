using System.ComponentModel.DataAnnotations;

namespace EOfficeBNILAPI.Models.Table
{
    public class Tm_Param_Template_Table
    {
        [Key]
        public Guid ID_PARAM_TEMPLATE { get; set; }
        public string NAMA_PARAMETER { get; set; }
        public int STATUS { get; set; }
        public string KODE { get; set; }
        public DateTime CREATED_ON { get; set; }
        public Guid CREATED_BY { get; set; }
        public DateTime MODIFIED_ON { get; set; }
        public Guid MODIFIED_BY { get; set; }
    }
}
