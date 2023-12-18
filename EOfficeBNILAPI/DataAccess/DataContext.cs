using Microsoft.EntityFrameworkCore;
using EOfficeBNILAPI.Models;
using EOfficeBNILAPI.Models.Table;

namespace EOfficeBNILAPI.DataAccess
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server with connection string from app settings
            options.UseSqlServer(Configuration.GetConnectionString("Dev"));
        }

        public DbSet<Tm_User_Table> tm_user { get; set; }

        public DbSet<Tm_Cuti_Table> tm_cuti { get; set; }
        public DbSet<Tm_Param_Template_Table> tm_param_template { get; set; }

        public DbSet<Tm_Delegasi_Table> tm_delegasi { get; set; }
        public DbSet<Tm_Position_Table> tm_position { get; set; }
        public DbSet<Tm_Unit_Table> tm_unit { get; set; }
        public DbSet<Tm_Absen_Table> tm_absen { get; set; }
        public DbSet<Tm_Branch_Table> tm_branch { get; set; }
        public DbSet<Tr_Document_Table> tr_document { get; set; }
        public DbSet<Tr_Document_Receiver_Table> tr_document_receiver { get; set; }
        public DbSet<Tr_Document_Log_Table> tr_document_log { get; set; }
        public DbSet<Tr_Document_Number_Table> tr_document_number { get; set; }
        public DbSet<Tm_StringMap_Table> tm_stringmap { get; set; }
        public DbSet<Tm_Menu_Table> tm_menu { get; set; }
        public DbSet<Tm_Letter_Table> tm_letter { get; set; }
        public DbSet<Tr_Attachment_Table> tr_attachment { get; set; }
        public DbSet<Tr_Receiver_Table> tr_receiver { get; set; }
        public DbSet<Tr_Copy_Table> tr_copy { get; set; }
        public DbSet<Tr_Content_Table> tr_content { get; set; }
        public DbSet<Tr_Letter_Number> tr_letter_number { get; set; }
        public DbSet<Tr_Log_Letter> tr_log_letter { get; set; }
        public DbSet<Tr_Tbl_Status_Table> tr_tbl_status { get; set; }
        public DbSet<Tr_Log_Sync_Table> tr_log_sync { get; set; }
        public DbSet<Tm_Nmbr_Table> tm_nmbr { get; set; }
        public DbSet<Tr_Disposition_Header_Table> tr_disposition_header { get; set; }
        public DbSet<Tr_Disposition_Child_Table> tr_disposition_child { get; set; }
        public DbSet<Tr_Dummy_Table> tr_dummy { get; set; }

        public DbSet<Tr_SettingSeketaris_Table> tr_setingseketaris { get; set; }

        public DbSet<Tr_Letter_Noneoffice_Table> tr_letter_noneoffice { get; set; }

        public DbSet<Tr_Level_Employee> tr_level_employee { get; set; }

        public DbSet<Tm_Content_Template> tm_content_template { get; set; }

        public DbSet<Tr_Checker_Table> tr_checker { get; set; }
        public DbSet<Tr_Outgoing_Recipient_Table> tr_outgoing_recipient { get; set; }
        public DbSet<Tr_Letter_Sender_Table> tr_letter_sender { get; set; }
        
        public DbSet<Tr_Img_Signature_Table> tr_img_signature { get; set; }
        public DbSet<Tr_Delivery_Table> tr_delivery { get; set; }
        public DbSet<Tr_Letter_Delivery> tr_letter_delivery { get; set; }

        public DbSet<Tm_Setting_Approval_Table> tm_setting_approval { get; set; }

        public DbSet<Tr_Memo_Type_Table> tr_memo_type { get; set; }

        public DbSet<Tr_Deliberation_Table> tr_deliberation { get; set; }

        public DbSet<Tm_Procurement_Table> tm_procurement { get; set; }

        public DbSet<Tm_Procurement_Detail_Table> tm_procurement_detail { get; set; }


        public DbSet<Tr_Delegasi_Table> tr_delegasi { get; set; }

        public DbSet<Tr_Procurement_Table> tr_procurement { get; set; }

        public DbSet<Tr_Notifikasi_Table> tr_notifikasi { get; set; }


    }
}
