//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Data.DataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Question
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Question()
        {
            this.Answers = new HashSet<Answer>();
        }
    
        public long Question_id { get; set; }
        public Nullable<long> Language_id { get; set; }
        public Nullable<long> Question_statuse_code { get; set; }
        public Nullable<long> User_id { get; set; }
        public string Email_address { get; set; }
        public string Questioner_Name { get; set; }
        public string Question_Text { get; set; }
        public string Answer_text { get; set; }
        public Nullable<System.DateTime> Date_Asked { get; set; }
        public Nullable<System.DateTime> Date_answered { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<long> CompanyID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual Company1 Company1 { get; set; }
        public virtual Language Language { get; set; }
        public virtual Question_status Question_status { get; set; }
        public virtual User1 User1 { get; set; }
    }
}
