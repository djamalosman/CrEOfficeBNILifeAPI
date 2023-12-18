namespace EOfficeBNILAPI.Models
{
    public class UserInsertParam
    {
        public string NIP { get; set; }
        public string FULLNAME { get; set; }
        public string PASSWORD { get; set; }
        public Guid ID_POSITION { get; set; }
    }
    public class UserEditParam
    {
        public Guid ID_USER { get; set; }
        public string NIP { get; set; }
        public string FULLNAME { get; set; }
        public string PASSWORD { get; set; }
        public Guid ID_POSITION { get; set; }
        public int STATUS_CODE { get; set; }
    }
    public class UserOutput
    {
        public Guid ID_USER { get; set; }
        public Guid ID_POSITION { get; set; }
        public string POSITION_NAME { get; set; }
        public string NIP { get; set; }
        public string FULLNAME { get; set; }
        public string USERNAME { get; set; }
        public string PASSWORD { get; set; }
        public string EMAIL { get; set; }
        public Int64 PHONE { get; set; }
        public string ID_GROUP { get; set; }
        public int STATUS_CODE { get; set; }
    }
}
