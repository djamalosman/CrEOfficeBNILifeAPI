using System.ComponentModel.DataAnnotations;

namespace EOfficeBNILAPI.Models.Table
{
    public class Tr_Img_Signature_Table
    {
        [Key]
        public Guid ID_IMG { get; set; }
        public Guid ID_USER { get; set; }
        public string NAME_IMAGE { get; set; }
        public string TYPE_IMAGE { get; set; }
        public int? LENGHT_IMAGE { get; set; }
        public int STATUS_CODE { get; set; }

        public DateTime CREATED_ON { get; set; }
        public Guid CREATED_BY { get; set; }
        public DateTime MODIFIED_ON { get; set; }
        public Guid MODIFIED_BY { get; set; }
        public int READ_STATUS { get; set; }

        public string? APPROVAL_REQUEST { get; set; }

    }
}
