namespace EOfficeBNILAPI.Models
{
    public class LoginParam
    {
        public string Nip { get; set; }
        public string Password { get; set; }
    }

    public class LoginResponse
    {
        public string Token { get; set; }
        public Guid IdUser { get; set; }
        public string Nip { get; set; }
        public string Fullname { get; set; }
        public string Password { get; set; }
        public Guid IdPosition { get; set; }
        public string PositionName { get; set; }
        public Guid? parentIdPosition { get; set; }
        public Guid? parentIdUser { get; set; }
        public string IdGroup { get; set; }
        public Guid IdUnit { get; set; }
        public string UnitName { get; set; }
        public string unitCode { get; set; }
        public Guid IdBranch { get; set; }
        public string BranchName { get; set; }
        public string email { get; set; }
        public Int64 phone { get; set; }
        public Guid directorIdUnit { get; set; }
        public string directorUnitName { get; set; }
        public string directorUnitCode { get; set; }
    }
}
