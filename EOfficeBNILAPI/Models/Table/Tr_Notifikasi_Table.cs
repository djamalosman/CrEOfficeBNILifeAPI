using System.ComponentModel.DataAnnotations;

namespace EOfficeBNILAPI.Models.Table
{
    public class Tr_Notifikasi_Table
    {
        [Key]
        public Guid ID_NOTIFIKASI { get; set; }
        public Guid ID_LETTER { get; set; }
        public string STATUS_DOC { get; set; }
        public Guid ID_USER { get; set; }
        public Guid ID_USER_APPROVAL { get; set; }
        public string NOTIFIKASI { get; set; }
        public DateTime? CREATED_ON { get; set; }
        public int STATUS_READ { get; set; }


    }
}
