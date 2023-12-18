using System;
using System.ComponentModel.DataAnnotations;

namespace EOfficeBNILAPI.Models.Table
{
	public class Tm_Content_Template
	{
		[Key]
		public Guid ID_CONTENT_TEMPLATE { get; set; }
		public string TEMPLATE_NAME { get; set; }
		public string TEMPLATE_CONTENT { get; set; }
		public string KODE { get; set; }
		public Guid ID_UNIT{ get; set; }
		public int? IS_DELETED { get; set; }
        public DateTime? CREATED_ON { get; set; }
        public Guid CREATED_BY { get; set; }
        public DateTime? MODIFIED_ON { get; set; }
        public Guid MODIFIED_BY { get; set; }

    }
}

