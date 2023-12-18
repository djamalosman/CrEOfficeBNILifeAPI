using System.ComponentModel.DataAnnotations;

namespace EOfficeBNILAPI.Models.Table
{
    public class Tr_Document_Number_Table
    {
        [Key]
        public Guid ID_DOC_NUMBER { get; set; }
        public int NUMBER_TYPE { get; set; }
        public string USER_CODE { get; set; }
        public int YEAR { get; set; }
        public int MONTH { get; set; }
        public int DATE { get; set; }
        public int NUMBER { get; set; }
        public DateTime? CREATED_ON { get; set; }
        public Guid CREATED_BY { get; set; }
        public DateTime? MODIFIED_ON { get; set; }
        public Guid MODIFIED_BY { get; set; }

    }
}
