using EOfficeBNILAPI.Models;

namespace EOfficeBNILAPI.DataAccess
{
    public interface IDataAccessProvider
    {
        LoginResponse GetDetailUserLoginToken(string nip, string password);
        GeneralOutputModel GetDataDocument(int pageNumber, Guid id_unit, Guid id_branch);
        GeneralOutputModel GetDataDocumentWeb(SessionUser sessionUser, ParamGetDocumentWeb pr);
        GeneralOutputModel GetDocumentByTrackingNumber(string trackingNumber);
        GeneralOutputModel GetAdminDivision();
        GeneralOutputModel GetPosition();
        GeneralOutputModel InsertDataDocument(ParamInsertDocument pr, SessionUser sessionUser);
        GeneralOutputModel GetDetailDocument(ParamGetDetailDocument pr, SessionUser sessionUser);
        GeneralOutputModel GetDetailDocumentDistribution(ParamGetDetailDocumentReceiver pr, SessionUser sessionUser);
        GeneralOutputModel UpdateDataDocument(ParamUpdateDocument pr, SessionUser sessionUser);
        GeneralOutputModel ReceiveDocument(ParamReceiveDocument pr, SessionUser sessionUser);
        GeneralOutputModel UploadDataDocument(ParamUploadDocumentString pr, SessionUser sessionUser);
        bool InsertLogDocument(Guid idDocument, SessionUser sessionUser, string description, string comment);
        string GetStringMapping(string objectName,string attributeName, int attributeValue);
        GeneralOutputModel GetStringMap(ParamGetStringmap pr);
        string GenerateNoDoc(int type, string userCode, SessionUser sessionUser);
        GeneralOutputModel GetDataMenu(string idGroup);

        GeneralOutputModel GetUserDataWeb(SessionUser sessionUser, ParamGetUsertWeb pr);

        GeneralOutputModel GetDetailUser(ParamGetDetailUser pr, SessionUser sessionUser);

        GeneralOutputModel UpdateDatauserSetting(ParamUpdateUser pr, SessionUser sessionUser);

        GeneralOutputModel GetDataMenuMobile();
        GeneralOutputModel GetDataPenerima(string keyword, SessionUser sessionUser);
        GeneralOutputModel InsertDataAttachmentLetter(ParamInsertAttachment pr, SessionUser sessionUser);
        GeneralOutputModel InsertLetter(ParamInsertLetter pr, SessionUser sessionUser);
        GeneralOutputModel GetDetailReportDocument(ParamGetDetailReportDocument pr, SessionUser sessionUser);
        GeneralOutputModel GetLetterDraft(ParamGetLetterWeb pr, SessionUser sessionUser);
        GeneralOutputModel GetInboxLetter(ParamGetLetterWeb pr, SessionUser sessionUser);
        GeneralOutputModel GetOutboxLetter(ParamGetLetterWeb pr, SessionUser sessionUser);
        GeneralOutputModel DeleteLetter(ParamDeleteLetter pr, SessionUser sessionUser);
        GeneralOutputModel GetDetailLetterSM(ParamGetDetailLetter pr, SessionUser sessionUser);
        GeneralOutputModel ResetPassword(ParamUpdatePassword pr, SessionUser sessionUser);
        GeneralOutputModel DeleteAttachment(ParamDeleteAttachment pr, SessionUser sessionUser);
        GeneralOutputModel ReceiveCheckedDoc(ParamReceiveCheckedDoc pr, SessionUser sessionUser);
        GeneralOutputModel GetAllDivision(SessionUser sessionUser);
        GeneralOutputModel GetAllUserAdminDivision(SessionUser sessionUser);
        GeneralOutputModel GetUserByUnit(ParamGetUserByUnit pr, SessionUser sessionUser);
        GeneralOutputModel UpdateAdminDivisi(ParamUpdateAdminDivisi pr, SessionUser sessionUser);

        GeneralOutputModel DeleteAdminDivisi(ParamUpdateAdminDivisi pr, SessionUser sessionUser);

        GeneralOutputModel PreviewNoDoc(string letterType, SessionUser sessionUser);
        
        GeneralOutputModel GenerateRecoveryToken(ParamForgotPassword pr);
        GeneralOutputModel GetSearchDetailReportDocument(ParamGetDetailSearchoutgoingDocument pr, SessionUser sessionUser);
        GeneralOutputModel GetDashboardContent(SessionUser sessionUser);
        string InsrtBrcd(string BarodeCode,int type, SessionUser sessionUser);
        GeneralOutputModel GetValidationNoresi(ParamGetDetailNoresi pr, SessionUser sessionUser);
        GeneralOutputModel InsertDisposition(ParamInsertLetterDisposition pr, SessionUser sessionUser);
        GeneralOutputModel UpdateForgotPasswordUser(ParamUpdateForgotPassword pr);
        GeneralOutputModel UpdatePasswordLoginUser_(ParamUpdatePasswordLogin pr);
        
           GeneralOutputModel GetAllPGAUser (SessionUser sessionUser);
        GeneralOutputModel GetAllDirekturUser(SessionUser sessionUser);
        GeneralOutputModel GetUserBySeketaris(ParamUpdateUserSekdirWeb pr, SessionUser sessionUser);
 
        GeneralOutputModel GetUserSekDirDataWeb(SessionUser sessionUser);

        GeneralOutputModel ValidasiDatauserSeketaris(ParamUpdateUserSekdirWeb pr, SessionUser sessionUser);

        GeneralOutputModel UpdateDatauserSeketaringSetting(ParamUpdateUserSekdirWeb pr, SessionUser sessionUser);

        GeneralOutputModel DeleteDatauserSekdirSetting(ParamUpdateSekdirWeb pr, SessionUser sessionUser);

        GeneralOutputModel GetAllSuperUser(SessionUser sessionUser);

        GeneralOutputModel GetSuperUserDataWeb(SessionUser sessionUser, ParamGetUsertWeb pr);

        GeneralOutputModel UpdateDataSuperuser(ParamGetDetailUser pr, SessionUser sessionUser);

        GeneralOutputModel DeleteDataSuperUser(ParamGetDetailUser pr, SessionUser sessionUser);

        GeneralOutputModel GetAllDwopdownAdminHct(SessionUser sessionUser);

        GeneralOutputModel GetAllDataAdminHctWeb(SessionUser sessionUser, ParamGetUsertWeb pr);

        GeneralOutputModel UpdateDataAdminHct(ParamGetDetailUser pr, SessionUser sessionUser);

        GeneralOutputModel DeleteDataAdminHct(ParamGetDetailUser pr, SessionUser sessionUser);

        GeneralOutputModel GetAllDwopdownEmloyee(SessionUser sessionUser);

        GeneralOutputModel GetAllDwopdownDataposition(SessionUser sessionUser);
        
        GeneralOutputModel GetDataEmpByFilter(ParamGetDetailUser pr, SessionUser sessionUser);

        GeneralOutputModel UpdateDataPositionEmp(ParamUpdateUser pr, SessionUser sessionUser);

     
        GeneralOutputModel GetDataSuratKeluarNon();
        GeneralOutputModel GetAdminDivisionNonEoffice();

        GeneralOutputModel InsertDataLetterNonEoffice(ParamInsertNonEoffice pr, SessionUser sessionUser);

        GeneralOutputModel UpdateDataLetterNonOffice_(UpdateNonEoffice pr, SessionUser sessionUser);

        GeneralOutputModel InsertDataUsingUploadEkspedisiNonOffice_(ParamUploadLetterNonOfficeString pr, SessionUser sessionUser);

        GeneralOutputModel GetDataEkspedisiNonEoffice(string keyword);

        GeneralOutputModel GetDetailsEkspedisiNonEoffic(getByIdNonEoffice pr, SessionUser sessionUser);

        GeneralOutputModel GetDetailsKurirNonEoffice(getByIdNonEoffice pr, SessionUser sessionUser);

        GeneralOutputModel GetDataKurirNameNonEoffice(string keyword);

        string GenerateNonDocoutgoingmail(int type, string userCode, SessionUser sessionUser);

        GeneralOutputModel GetByIdDocoutgoingmail(getByIdNonEoffice pr, SessionUser sessionUser);

        GeneralOutputModel InsertLevelEmpl(ParamUpdateLevelemp pr, SessionUser sessionUser);

        GeneralOutputModel GetAllUserAdminDivisionHct(SessionUser sessionUser);

        GeneralOutputModel GetAllEmpLevel(SessionUser sessionUser);

        GeneralOutputModel DeleteEplLevel(ParamUpdateLevelemp pr, SessionUser sessionUser);


        GeneralOutputModel GetSearchReportDocumentNonEoffice(ParamReportNonOuboxLetter pr, SessionUser sessionUser);

        GeneralOutputModel GetSearchReportDocumentNonEofficeByUser(ParamReportNonOuboxLetter pr, SessionUser sessionUser);

        GeneralOutputModel ExportUpdateNonEofficeEkspedisi_(ParamReportNonOuboxLetter pr, SessionUser sessionUser);

        GeneralOutputModel GetContentTemplate(SessionUser sessionUser);

        GeneralOutputModel AddContentTemplate(ParamInsertContentTemplate pr, SessionUser sessionUser);
        GeneralOutputModel AddContentTemplateBulk(ParamInsertContentTemplateBulk pr, SessionUser sessionUser);

        GeneralOutputModel UpdateContentTemplate(ParamUpdateContentTemplate pr, SessionUser sessionUser);

        GeneralOutputModel InsertLetterOutbox(ParamInsertLetterOutbox pr, SessionUser sessionUser);

        GeneralOutputModel GetSearchReportOutgoingMailEksnKur(ParamReportOutgoingMailEksnKurir pr, SessionUser sessionUser);

        GeneralOutputModel GetDetailLetterSK(ParamGetDetailLetter pr, SessionUser sessionUser);
        GeneralOutputModel ApprovalLetter(ParamApprovalLetter pr, SessionUser sessionUser);
        
        GeneralOutputModel UpdateOneSignalId(ParamOneSignal pr, SessionUser sessionUser);

        GeneralOutputModel InsertDataUserSignature(ImageUploadTTD pr, SessionUser sessionUser);

        GeneralOutputModel GetAllSignatureUser(SessionUser sessionUser);

        GeneralOutputModel GetAllApprovalSignatureUser(SessionUser sessionUser);
        GeneralOutputModel GetOuputApprovalRejectSignatureUser(SessionUser sessionUser);

        //GeneralOutputModel ApprovalSigantureDataUser(ParamGetApprovalSignature pr, SessionUser sessionUser);
        GeneralOutputModel ApprovalSigantureDataUser(ParamJsonStirngSiganture pr, SessionUser sessionUser);
        //GeneralOutputModel RejectSigantureDataUser(ParamGetApprovalSignature pr, SessionUser sessionUser);
        GeneralOutputModel RejectSigantureDataUser(ParamJsonStirngSiganture pr, SessionUser sessionUser);

        #region kurir non eoffice
        GeneralOutputModel GetDataKurirSuratKeluarNon();
        GeneralOutputModel UpdateDataKurirLetterNonOffice_(UpdateNonEoffice pr, SessionUser sessionUser);

        GeneralOutputModel GetSearchKurirReportDocumentNonEoffice(ParamReportNonOuboxLetter pr, SessionUser sessionUser);

        #endregion


        GeneralOutputModel DeleteUserProfileSignature(OuputSignature pr, SessionUser sessionUser);

        GeneralOutputModel DetailUserProfileSignature(OuputSignature pr, SessionUser sessionUser);

        GeneralOutputModel DetailApprovalUserProfileSignature(OuputSignature pr, SessionUser sessionUser);

        GeneralOutputModel UpdateUploadDocNonOffice_(ParamUploadLetterNonOfficeString pr, SessionUser sessionUser);

        GeneralOutputModel DetailNotifNonEoffice_(getByIdNonEoffice pr, SessionUser sessionUser);


        GeneralOutputModel GetDetailsViewEkspedisi_(SessionUser sessionUser, ParamGetDetailsView pr);

        GeneralOutputModel GetDetailsViewKurir_(SessionUser sessionUser, ParamGetDetailsView pr);

        GeneralOutputModel ApprovalSignatureOneDataWeb_(ParamGetApprovalSignatureOnedata pr, SessionUser sessionUser);

        GeneralOutputModel RejectSignatureOneDataWeb_(ParamGetApprovalSignatureOnedata pr, SessionUser sessionUser);

        #region ekspedisi surat keluar
        GeneralOutputModel GetSKEskpedisi(ParamGetSKDelivery pr, SessionUser sessionUser);
        GeneralOutputModel InsertDelivery(ParamInsertDelivery pr, SessionUser sessionUser);
        #endregion


        GeneralOutputModel GetUserDropDownSetUserBod(SessionUser sessionUser);

        GeneralOutputModel GetDataUserBodAll(SessionUser sessionUser, ParamGetUsertWeb pr);

        GeneralOutputModel CreateDataUserBod(ParamGetDetailUser pr, SessionUser sessionUser);

        GeneralOutputModel DeleteDataSettingApprovalBod(ParamGetDetailUser pr, SessionUser sessionUser);

        GeneralOutputModel LetterNotifSecretary(ParamGetDetailLetter pr, SessionUser sessionUser);
        GeneralOutputModel GetDetailDeliveryEoffice(ParamGetDetailDelivery pr, SessionUser sessionUser);
        GeneralOutputModel UpdateDeliveryEoffice(ParamUpdateDelivery pr, SessionUser sessionUser);
        GeneralOutputModel GetReportEkspedisiEoffice(ParamGetReportEkspedisiEoffice pr, SessionUser sessionUser);
        GeneralOutputModel UploadExpeditionEoffice(ParamUploadEkspedisiEofficeString pr, SessionUser sessionUser);


        #region Memo
        GeneralOutputModel GetStringMapMemo_(ParamGetStringmap pr, SessionUser sessionUser);
        GeneralOutputModel InsertDataAttachmentMemo(ParamInsertAttachmentMemo pr, SessionUser sessionUser);

        GeneralOutputModel InsertMemo(ParamInsertMemo pr, SessionUser sessionUser);

        GeneralOutputModel GetDistribusiMemo(ParamGetMemoWeb pr, SessionUser sessionUser);


        GeneralOutputModel GetMemoDraft(ParamGetMemoWeb pr, SessionUser sessionUser);

        GeneralOutputModel GetDetailsMemo(ParamGetDetailMemo pr, SessionUser sessionUser);

        GeneralOutputModel ApprovalLetterMemo(ParamApprovalLetterMemo pr, SessionUser sessionUser);


        GeneralOutputModel MemoLetterNotifSecretary(ParamGetDetailLetter pr, SessionUser sessionUser);


        GeneralOutputModel DeleteAttachmentMemo(ParamDeleteAttachmentMemo pr, SessionUser sessionUser);
        #endregion



        GeneralOutputModel UpdateQrcodeKurirNonEoffice(ParamInsertGenerateNoDocNonEoffice pr, SessionUser sessionUser);

        #region
        GeneralOutputModel GetAllPengadaan_(SessionUser sessionUser);
        GeneralOutputModel GetPengadaanById_(ParamGetMemoPengadaanById pr, SessionUser sessionUser);
        #endregion

        GeneralOutputModel GetMemoTypeById_(ParamGetMemoTypeById pr, SessionUser sessionUser);


        GeneralOutputModel ApprovalDelebrationMemo_(ParamApprovalLetterMemo pr, SessionUser sessionUser);

        GeneralOutputModel InsertMemoBackDate(ParamInsertMemoBackDate pr, SessionUser sessionUser);



        GeneralOutputModel GetDetailsMemoBackDate(ParamGetDetailMemo pr, SessionUser sessionUser);

        GeneralOutputModel GetDataNominalMinMaxById(ParamCheckMinMaxNomial pr);


        GeneralOutputModel GetApprovalPengadaanById_(ParamGetMemoPengadaanById pr, SessionUser sessionUser);

        GeneralOutputModel GetAllDwopdownAdminPengadaan(SessionUser sessionUser);

        GeneralOutputModel GetAllDataAdminPengadaanWeb(SessionUser sessionUser, ParamGetUsertWeb pr);

        GeneralOutputModel UpdateDataAdminPengadaan(ParamGetDetailUser pr, SessionUser sessionUser);

        GeneralOutputModel DeleteDataAdminPengadaan(ParamGetDetailUser pr, SessionUser sessionUser);


        GeneralOutputModel GetAllDataSetPengadaanWeb(SessionUser sessionUser, ParamGetUsertWeb pr);

        GeneralOutputModel GetAllDwopdownSetPengadaan(SessionUser sessionUser);

        GeneralOutputModel InsertDataPengadaan(ParamInsertPengadaan pr, SessionUser sessionUser);

        GeneralOutputModel GetAllDataSetDelegasiWeb(SessionUser sessionUser, ParamGetUsertWeb pr);
        GeneralOutputModel GetAllDwopdownSetDelegasi(SessionUser sessionUser);

        GeneralOutputModel InsertDataDelegasi(ParamInsertDelegasi pr, SessionUser sessionUser);
               
        GeneralOutputModel GetDataPengadaanAll(SessionUser sessionUser);
        GeneralOutputModel GetDataPengadaanApproval(SessionUser sessionUser);

        GeneralOutputModel ApprovalJenisPengadaan(ParamJsonStirngPengadaan pr, SessionUser sessionUser);
        GeneralOutputModel GetDataPengadaanView(ParamGetPengadaanView pr, SessionUser sessionUser);

        GeneralOutputModel GetDataDelegasi(SessionUser sessionUser);
        GeneralOutputModel GetDataDelegasiApproval(SessionUser sessionUser);
        GeneralOutputModel ApprovalDelegasiUser(ParamJsonStirngDelegasi pr, SessionUser sessionUser);

        GeneralOutputModel InsertMemoPengadaan(ParamInsertMemoPengadaan pr, SessionUser sessionUser);

        GeneralOutputModel GetDetailsMemoPengadaan(ParamGetDetailMemo pr, SessionUser sessionUser);


        GeneralOutputModel ApprovalMemoPengadaan_(ParamApprovalLetterMemo pr, SessionUser sessionUser);

        GeneralOutputModel GetDetailMemoWebPriview_(ParamGetDetailMemo pr, SessionUser sessionUser);


        GeneralOutputModel GetDataPemeriksaDiv_(string keyword, SessionUser sessionUser);

        GeneralOutputModel GetDataPemeriksaDivLainya(string keyword, SessionUser sessionUser);

        GeneralOutputModel GetDetailsMemoPengadaanWebPriview_(ParamGetDetailMemo pr, SessionUser sessionUser);

        GeneralOutputModel GetStringMapMemoSrch_(ParamGetStringmap pr, SessionUser sessionUser);



        GeneralOutputModel InsertLetterOutboxBackdate(ParamInsertLetterOutboxBackdate pr, SessionUser sessionUser);

        GeneralOutputModel GetDetailLetterSKBackdate(ParamGetDetailLetter pr, SessionUser sessionUser);

        GeneralOutputModel UpdateResetPassword(ParamUpdateAdminDivisi pr, SessionUser sessionUser);

        GeneralOutputModel GetStringMapSearchInbox(ParamGetStringmap pr);


        GeneralOutputModel GetStringmapLevelStgJbtn_(ParamGetStringmap pr);

        GeneralOutputModel GetStringMapSearchOutbox_(ParamGetStringmap pr);


        GeneralOutputModel UpdateStatusNotif(ParamUpdateNotif pr, SessionUser sessionUser);

        GeneralOutputModel PushNotifikasiToMobile_(ParamPushNotifikasi pr, SessionUser sessionUser);

        GeneralOutputModel GetStringMapSearchInboxWM_(ParamGetStringmap pr);


        GeneralOutputModel GetDetailLetterSKMobile_(ParamPriviewMobile pr, SessionUser sessionUser);
        GeneralOutputModel GetDataPriviewMobile_(ParamPriviewMobile pr, SessionUser sessionUser);

        GeneralOutputModel GetDetailsMemoPengadaanMobilePriview_(ParamPriviewMobile pr, SessionUser sessionUser);

        GeneralOutputModel GetSearchOutgoingLetterNumber(ParamGetReportSeacrhoutGoing pr, SessionUser sessionUser);


        GeneralOutputModel SearchSuratKeluarKurirNonEoffice(ParamReportNonOuboxLetter pr, SessionUser sessionUser);

        GeneralOutputModel SearchSuratKeluarEkspedisiNonEoffice(ParamReportNonOuboxLetter pr, SessionUser sessionUser);

        // api
        GeneralOutputModel SyncUserHCIS(ParamSyncHCIS jsonData);
        //GeneralOutputModel SyncDivisiHCIS(ParamSyncDivisiHCIS jsonData);

        //GeneralOutputModel SyncPositionHCIS(ParamSyncPositionHCIS jsonData);
        
        //GeneralOutputModel SyncCutiHCIS(ParamSyncCutiHCIS jsonData);
        //GeneralOutputModel SyncDelegasiHCIS(ParamSyncDelegasiHCIS jsonData);
        
        //GeneralOutputModel SyncAbsenHCIS(ParamSyncAbsenHCIS jsonData);

    }
}
