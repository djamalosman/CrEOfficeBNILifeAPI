using System.ComponentModel.DataAnnotations;

namespace EOfficeBNILAPI.Models.Table
{
    public class Tm_Nmbr_Table
    {
        [Key]
        public Guid ID_BRCD { get; set; }
        public int TYPE_BRCD { get; set; }
        public string NMR_BRCD { get; set; }

    }
}
