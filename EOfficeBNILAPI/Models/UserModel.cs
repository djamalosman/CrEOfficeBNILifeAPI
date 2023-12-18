namespace EOfficeBNILAPI.Models
{
    public class ParamGetUsertWeb
    {
        public string draw { get; set; }
        public int start { get; set; }
        public string sortColumn { get; set; }
        public string sortColumnDirection { get; set; }
        public string? searchValue { get; set; }
        public int pageSize { get; set; }

    }
    public class DataOuputUser
    {
        public Guid Iduser { get; set; }
        public string Nip { get; set; }
        public string Fullname { get; set; }

        public string Email { get; set; }

        public Int64 Phone { get; set; }

        public int statusCode { get; set; }

        public string statusCodeValue { get; set; }
        public string IDgroup { get; set; }

        public Guid IdPosition { get; set; }

        public string Position_name { get; set; }

        public string Unit_name { get; set; }

    }
    public class ParamGetDetailUser
    {
        public Guid Iduser { get; set; }
        public Guid Idunit { get; set; }
        public Guid IdBod { get; set; }
    }
    public class UserOutputWeb
    {
        public string draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<DataOuputUser> data { get; set; }

    }

    public class ParamUpdateUser
    {
        public Guid Iduser { get; set; }

        public string IDgroup { get; set; }

        public Guid idposition { get; set; }


    }
    public class ParamUpdatePassword
    {
        public string passLama { get; set; }
        public string newPassword { get; set; }

    }
    public class ParamGetUserByUnit
    {
        public Guid idUnit { get; set; }

    }
    public class ParamUpdateAdminDivisi
    {
        public Guid idUnit { get; set; }
        public Guid idUser { get; set; }

    }

    #region model api User
    public class ParamSyncHCIS
    {
        public InnerSyncHCIS JSON { get; set; }
    }
    public class InnerSyncHCIS
    {
        public List<DataSyncHCIS> data { get; set; }
    }
    public class DataSyncHCIS
    {
        public string EmployeeNo { get; set; }
        public string EmployeeName { get; set; }
        public string PositionCode { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? WorklocationName { get; set; }
        public int Status { get; set; }
    }
    public class OutputSyncHCIS
    {
        public string EmployeeNo { get; set; }
        public string EmployeeName { get; set; }
        public string PositionCode { get; set; }
        public string? Email { get; set; }
        public string? PhoneUser { get; set; }
        public string? WorklocationName { get; set; }
        public int Status { get; set; }
        public string StatusSync { get; set; }
    }
    public class ParamAuthentication
    {
        public string token { get; set; }
        public ParamSyncHCIS jsonData { get; set; }
    }
    #endregion

    #region model api divisi
    public class ParamSyncDivisiHCIS
    {
        public InnerSyncDivisiHCIS JSON { get; set; }
    }
    public class InnerSyncDivisiHCIS
    {
        public List<DataSyncDivisiHCIS> data { get; set; }
    }
    public class DataSyncDivisiHCIS
    {
        public string unit_code { get; set; }
        public string unit_name { get; set; }
        public string unit_type { get; set; }
        public string code_atasan { get; set; }
        public string unit_code_old { get; set; }
        public int Status { get; set; }
    }
    public class OutputSyncDivisiHCIS
    {
        public string unit_code { get; set; }
        public string unit_name { get; set; }
        public string unit_type { get; set; }
        public string code_atasan { get; set; }
        public string unit_code_old { get; set; }
        public int Status { get; set; }
        public string StatusSync { get; set; }
    }
    public class ParamAuthenticationDivsi
    {
        public string token { get; set; }
        public ParamSyncDivisiHCIS jsonData { get; set; }
    }
    #endregion

    #region model api position
    public class ParamSyncPositionHCIS
    {
        public InnerSyncPositionHCIS JSON { get; set; }
    }
    public class InnerSyncPositionHCIS
    {
        public List<DataSyncPositionHCIS> data { get; set; }
    }
    public class DataSyncPositionHCIS
    {
        public string position_code { get; set; }
        public string position_name { get; set; }
        public int level { get; set; }
        public string code_atasan { get; set; }
        public string position_code_old { get; set; }
        public string unit_code { get; set; }
        public int Status { get; set; }
    }
    public class OutputSyncPositionHCIS
    {
        public string position_code { get; set; }
        public string position_name { get; set; }
        public int level { get; set; }
        public string code_atasan { get; set; }
        public string position_code_old { get; set; }
        public int Status { get; set; }
        public string unit_code { get; set; }
        public string StatusSync { get; set; }
    }
    public class ParamAuthenticationPosition
    {
        public string token { get; set; }
        public ParamSyncPositionHCIS jsonData { get; set; }
    }
    #endregion

    #region model api Cuti
    public class ParamSyncCutiHCIS
    {
        public InnerSyncCutiHCIS JSON { get; set; }
    }
    public class InnerSyncCutiHCIS
    {
        public List<DataSyncCutiHCIS> data { get; set; }
    }
    public class DataSyncCutiHCIS
    {
        public string nip { get; set; }
        public int Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
    }
    public class OutputSyncCutiHCIS
    {
        public string nip { get; set; }
        public int Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public string StatusSync { get; set; }

    }
    public class ParamAuthenticationCuti
    {
        public string token { get; set; }
        public ParamSyncCutiHCIS jsonData { get; set; }
    }
    #endregion

    #region model api Delegasi
    public class ParamSyncDelegasiHCIS
    {
        public InnerSyncDelegasiHCIS JSON { get; set; }
    }
    public class InnerSyncDelegasiHCIS
    {
        public List<DataSyncDelegasiHCIS> data { get; set; }
    }
    public class DataSyncDelegasiHCIS
    {
        public string nip_pengajuan { get; set; }
        public string nama_pengajuan { get; set; }
        public string nip_delegasi { get; set; }
        public string nama_delegasi { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int Status { get; set; }
    }
    public class OutputSyncDelegasiHCIS
    {
        public string nip_pengajuan { get; set; }
        public string nama_pengajuan { get; set; }
        public string nip_delegasi { get; set; }
        public string nama_delegasi { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Status { get; set; }
        public string StatusSync { get; set; }

    }
    public class ParamAuthenticationDelegasi
    {
        public string token { get; set; }
        public ParamSyncDelegasiHCIS jsonData { get; set; }
    }
    #endregion


    #region model api Absen
    public class ParamSyncAbsenHCIS
    {
        public InnerSyncAbsenHCIS JSON { get; set; }
    }
    public class InnerSyncAbsenHCIS
    {
        public List<DataSyncAbsenHCIS> data { get; set; }
    }
    public class DataSyncAbsenHCIS
    {
        public string nip { get; set; }
        public int Status { get; set; }
        public DateTime Date { get; set; }
    }
    public class OutputSyncAbsenHCIS
    {
        public string nip { get; set; }
        public int Status { get; set; }
        public DateTime Date { get; set; }
        public string StatusSync { get; set; }

    }
    public class ParamAuthenticationAbsen
    {
        public string token { get; set; }
        public ParamSyncAbsenHCIS jsonData { get; set; }
    }
    #endregion

    public class ParamForgotPassword
    {
        public string token { get; set; }

        public string email { get; set; }
    }
    public class ParamUpdateForgotPassword
    {
        public Guid recoveryToken { get; set; }
        public string newPassword { get; set; }
    }
    public class ParamUpdatePasswordLogin
    {
      
        public string nip { get; set; }
        public string newPassword { get; set; }
    }
    public class DataOuputUserPGA
    {
        public Guid Iduser { get; set; }
        public string Nip { get; set; }
        public string Fullname { get; set; }

        public string IDgroup { get; set; }
    }

    public class DataOuputUserDirektur
    {
        public Guid IdDirektur { get; set; }

        public Guid idPositionDirektur { get; set; }
        public Guid idUnit { get; set; }
        public string Nip { get; set; }
        public string Fullname { get; set; }
        public string Position_Name { get; set; }
        public string unitName { get; set; }

        public Guid Dirkom { get; set; }

    }

    public class DataOuputUserSeketaris
    {
        public Guid Iduser { get; set; }
        public Guid idPositionSeketaris { get; set; }
        public Guid idUnit { get; set; }
        public string Nip { get; set; }
        public string Fullname { get; set; }
        public string Position_Name { get; set; }
        public string unitName { get; set; }

    }

    //public class UserSekdirOutputWeb
    //{
    //    public string draw { get; set; }
    //    public int recordsTotal { get; set; }
    //    public int recordsFiltered { get; set; }
    //    public List<DataOuputSekdir> data { get; set; }

    //}

    public class DataOuputSekdir
    {
        public Guid IduserSeketaris { get; set; }
        public Guid idPositionDirektur { get; set; }
        public Guid idPositionSeketaris { get; set; }
        public string FullnameDirektur { get; set; }
        public string Position_NameDirektur { get; set; }

        public string FullnameSeketaris { get; set; }
        public string Position_NameSeketaris { get; set; }

        public Guid ID_SETDIRKOM { get; set; }

    }

    public class ParamUpdateUserSekdirWeb
    {
        public Guid Iduser { get; set; }

        public Guid IdDirektur { get; set; }
        public Guid idPositionDirektur { get; set; }


    }

    public class ParamUpdateSekdirWeb
    {
        public Guid IduserSeketaris { get; set; }

        public Guid ID_SETDIRKOM { get; set; }

    }


    public class GetOuputIdSekdir
    {
        public Guid IduserSeketaris { get; set; }
        public Guid idPositionSeketaris { get; set; }
        public Guid iduserDirektur { get; set; }
    }


    public class DataOuputSuperUser
    {
        public Guid Iduser { get; set; }
        public string Nip { get; set; }
        public string Fullname { get; set; }
        public string Position_Name { get; set; }
        public string unitName { get; set; }

    }

    public class SuperUserOutputWeb
    {
        public string draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<DataOuputSuperUserWeb> data { get; set; }

    }

    public class DataOuputSuperUserWeb
    {
        public Guid IdBod { get; set; }
        public Guid Iduser { get; set; }
        public Guid Idunit { get; set; }
        public string Nip { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public Int64 Phone { get; set; }
        public int Status { get; set; }
        public string IDgroup { get; set; }
        public Guid IdPosition { get; set; }
        public string Position_name { get; set; }
        public string Unit_name { get; set; }

        public string Unit_code { get; set; }

        public string password { get; set; }
    }


    public class AdminHCTOutputWeb
    {
        public string draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<DataOuputAdminHCT> data { get; set; }

    }

    public class DataOuputAdminHCT
    {
        public Guid Iduser { get; set; }
        public string Nip { get; set; }
        public string Fullname { get; set; }
        public string Position_Name { get; set; }
        public string unitName { get; set; }

    }


    public class DataOuputPosition
    {
        public Guid idPosition { get; set; }
        public string Position_Name { get; set; }
    }

    public class ParamUpdateLevelemp
    {
        public Guid idemplevel { get; set; }
        public Guid idUnit { get; set; }
        public Guid idUser { get; set; }
        public int idLevel { get; set; }
        public int statuscode { get; set; }
    }


    public class OuputLevelemp
    {
        public Guid idemplevel { get; set; }
        public Guid idUnit { get; set; }
        public string unitName { get; set; }
        public Guid idUser { get; set; }
        public string userName { get; set; }

        public string levelName { get; set; }

    }
    public class ParamOneSignal
    {
        public Guid idUser { get; set; }
        public Guid IdOneSignal { get; set; }
    }

    public class ImageUploadTTD
    {
        public Guid? idUser { get; set; }
        public string? NameImage { get; set; }
        public string? TypeImage { get; set; }
        public int? LenghtImage { get; set; }
    }

    public class OuputSignature
    {
        public Guid idMg { get; set; }
        public Guid idUser { get; set; }
        public string? nip { get; set; }
        public string? fullname { get; set; }
        public string? NameImage { get; set; }
        public string? PositionName { get; set; }
        public string? UnitName { get; set; }
        public string? idgroup { get; set; }
        public string? TypeImage { get; set; }
        public int? LenghtImage { get; set; }
        public int? status_code { get; set; }
    }

    public class OutputDetailApprovalSignature
    {
        public OuputSignature Signature { get; set; }
    }

    public class ParamJsonStirngSiganture
    {
        public string jsonDataString { get; set; }
    }
    public class ParamGetApprovalSignature
    {
        public Guid idMg { get; set; }
        public bool isChecked { get; set; }
    }
    public class ParamGetApprovalSignatureOnedata
    {
        public Guid idMg { get; set; }
    }


    public class AdminPengadaanOutputWeb
    {
        public string draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<DataOuputAdminPengadaan> data { get; set; }

    }


    public class SetPengadaanOutputWeb
    {
        public string draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<DataOuputSetPengadaan> data { get; set; }

    }

    public class DataOuputAdminPengadaan
    {
        public Guid Iduser { get; set; }
        public string Nip { get; set; }
        public string Fullname { get; set; }
        public string Position_Name { get; set; }
        public string unitName { get; set; }

    }


    public class DataOuputSetPengadaan
    {
        public Guid idUser { get; set; }
        public Guid Id { get; set; }
        public string NAME_PENGADAAN { get; set; }
        public string MIN_NOMINAL { get; set; }
        public string MAX_NOMINAL { get; set; }
        public string APPROVER { get; set; }
        public string APPROVER2 { get; set; }
        public string APPROVER_NAME { get; set; }
        public string APPROVER2_NAME { get; set; }
        public string POSITION1 { get; set; }
        public string POSITION2 { get; set; }
        public string JABATAN1 { get; set; }
        public string JABATAN2 { get; set; }

        public int STATUS_APPROVAL { get; set; }
        public string STATUS { get; set; }
    }

    public class ParamInsertPengadaan
    {
        public Guid Id { get; set; }
        public string name { get; set; }
        public string min_nominal { get; set; }
        public string max_nominal { get; set; }
        public string approver1 { get; set; }
        public string approver2 { get; set; }

    }

    public class SetDelegasiOutputWeb
    {
        public string draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<DataOuputSetDelegasi> data { get; set; }

    }

    public class DataOuputSetDelegasi
    {

        public Guid Id { get; set; }
        public Guid id_user { get; set; }
        public Guid id_user_delegasi { get; set; }
        public Guid id_user_approved { get; set; }
        public DateTime? startdate { get; set; }
        public string startdatestring { get; set; }

        public DateTime? enddate { get; set; }
        public string enddatestring { get; set; }
        public string user_name { get; set; }
        public string user_name_delegasi { get; set; }
        public string user_name_approved { get; set; }
        public string POSITION { get; set; }
        public string POSITION_DELEGASI { get; set; }
        public string POSITION_APPROVED { get; set; }

        public string JABATAN { get; set; }
        public string JABATAN_DELEGASI { get; set; }
        public string JABATAN_APPROVED { get; set; }
        public string Status_approver { get; set; }

        public string Approved_by { get; set; }


    }

    public class ParamInsertDelegasi
    {
        public Guid Id { get; set; }
        public Guid Id_user { get; set; }
        public Guid Id_user_delegasi { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Status_approver { get; set; }


    }
    public class ParamJsonStirngPengadaan
    {
        public string jsonDataString { get; set; }
    }

    public class ParamGetApprovalPengadaan
    {
        public Guid Id { get; set; }
        public bool isChecked { get; set; }
    }
    public class ParamGetPengadaanView
    {
        public Guid Id { get; set; }

    }

    public class ParamJsonStirngDelegasi
    {
        public string jsonDataString { get; set; }
    }

    public class ParamGetApprovalDelegasi

    {
        public Guid Id { get; set; }
        public bool isChecked { get; set; }
    }


}
