using System.ComponentModel.DataAnnotations;

namespace EOfficeBNILAPI.Models.Table
{
    public class Tm_Absen_Table
    {
        [Key]
        public Guid ID_ABSEN { get; set; }
        public string NIP { get; set; }
        public int STATUS { get; set; }
        public DateTime DATE { get; set; }
    }
}
