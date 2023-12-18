namespace EOfficeBNILAPI.Models
{
    public class ProcurementInsertParam
    {
        public string NAME { get; set; }

        public string MIN_NOMINAL { get; set; }
        public string MAX_NOMINAL { get; set; }
        public string APPROVER { get; set; }
        public string APPROVER2 { get; set; }

    }
    public class ProcurementEditParam
    {
        public Guid ID { get; set; }
        public string MIN_NOMINAL { get; set; }
        public string MAX_NOMINAL { get; set; }
        public string APPROVER { get; set; }
        public string APPROVER2 { get; set; }
    }

    public class ProcurementOutput
    {
        public Guid ID_USER { get; set; }
        public string MIN_NOMINAL { get; set; }
        public string MAX_NOMINAL { get; set; }
        public string APPROVER { get; set; }
        public string APPROVER2 { get; set; }

    }

}
