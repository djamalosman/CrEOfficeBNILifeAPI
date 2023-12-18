using JetBrains.Annotations;

namespace EOfficeBNILAPI.Models
{
    public class GeneralOutputModel
    {
        public int PageCount { get; set; }
        public string Status { get; set; }
        public object Result { get; set; }
        public string Message { get; set; }
        public int PageNumber { get; set; }
    }
    public class SessionUser 
    {
        public Guid idUser { get; set; }
        public string nip { get; set; }
        public string nama { get; set; }
        public Guid idUnit { get; set; }
        public Guid idBranch { get; set; }
        public string unitCode { get; set; }
        public Guid idPosition { get; set; }
        public string idGroup { get; set; }
        public Guid directorIdUnit { get; set; }
    }
    public class StringmapOutput
    {
        
        public int attributeValue { get; set; }
        public string value { get; set; }
    }
    public class ParamGetStringmap
    {
        public string objectName { get; set; }
        public string attributeName { get; set; }
        //public int attributeValue { get; set; }
    }
    public class DashboardOutput
    {
        public int documentInCount { get; set; }
        public int inboxCount { get; set; }
        public int outboxCount { get; set; }
        public int deliveryCount { get; set; }

        public int MemoCount { get; set; }
        public int LainnyaCount { get; set; }



        public List<LetterOutput> listLetter { get; set; }
        public List<LetterOutput> listLetterOutbox { get; set; }
        public List<DeliveryReportOutputDashboard> listDelivery { get; set; }

        public List<MemoOutput> listLetterMemo { get; set; }

        public List<DocumentOutput> listDocument { get; set; }

        public int signatureInCount { get; set; }
        public List<OuputSignature> listSignature { get; set; }

        public int NonEofficeInCount { get; set; }
        public List<OutputletterNonEoffice> listNonEoffice { get; set; }
        public List<OutputNotifikasiLainnya> listlainnya { get; set; }

    }
    public class OutputContentTemplate
    {
        public Guid idContentTemplate { get; set; }
        public string templateName { get; set; }
        public string templateContent { get; set; }
        public int? isDeleted { get; set; }
    }
    public class ParamInsertContentTemplate
    {
        public string templateName { get; set; }
        public string templateContent { get; set; }
    }
    public class ParamUpdateContentTemplate
    {
        public Guid idContentTemplate { get; set; }
        public string templateName { get; set; }
        public string templateContent { get; set; }
        public int? isDeleted { get; set; }
    }


    public class ParamInsertContentTemplateBulk
    {
        public string templateName { get; set; }
        public string templateContent { get; set; }
        public string parameter { get; set; }
    }
    public class ListParameter
    {
        public string parameter { get; set; }
    }
    public class OutputNotifikasiLainnya
    {
        public Guid ID_NOTIFIKASI { get; set; }
        public Guid ID_LETTER { get; set; }
        public string PERIHAL { get; set; }
        public string STATUS_DOC { get; set; }
        public Guid ID_USER { get; set; }
        public string NAME_USER { get; set; }

        public Guid ID_USER_APPROVAL { get; set; }
        public string USER_APPROVAL { get; set; }

        public string NOTIFIKASI { get; set; }
        public DateTime? CREATED_ON { get; set; }
        public int STATUS_READ { get; set; }

    }


    public class ParamPushNotifikasi
    {
        public Guid idletter { get; set; }

        public Guid? idOneSignal { get; set; }

    }

    public class GetDataPushNotifikasi
    {
        

        public Guid? idOneSignal { get; set; }

        public int? lettertypeCode { get; set; }

        public string? about { get; set; }

        public string? fullname { get; set; }


    }
    public class ParamPriviewMobile
    {
        public Guid idLetter { get; set; }
        public int? lettertypeCode { get; set; }
    }
}
