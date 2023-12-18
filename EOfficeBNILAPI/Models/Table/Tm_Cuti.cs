using System.ComponentModel.DataAnnotations;

namespace EOfficeBNILAPI.Models.Table
{
    public class Tm_Cuti_Table
    {
        [Key]
        public Guid ID_CUTI { get; set; }
        public string NIP { get; set; }
        public string DESCRIPTION { get; set; }
        public int STATUS { get; set; }
        public DateTime STARTDATE { get; set; }
        public DateTime ENDDATE { get; set; }
    }
}
