using System.ComponentModel.DataAnnotations;

namespace EOfficeBNILAPI.Models.Table
{
    public class Tm_Delegasi_Table
    {
        [Key]
        public Guid ID_DELEGASI { get; set; }
        public Guid ID_POSITION { get; set; }
        public string NIP_PENGAJUAN { get; set; }
        public string NAMA_PENGAJUAN { get; set; }
        public string NIP_DELEGASI { get; set; }
        public string NAMA_DELEGASI { get; set; }
        public int STATUS { get; set; }
        public DateTime STARTDATE { get; set; }

        public DateTime ENDDATE { get; set; }
    }
}
