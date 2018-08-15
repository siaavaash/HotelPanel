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
    
    public partial class Privilege
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Privilege()
        {
            this.Privilege1 = new HashSet<Privilege>();
            this.RolePrivileges = new HashSet<RolePrivilege>();
        }
    
        public long PrivilegeID { get; set; }
        public Nullable<long> PrivilegeParentID { get; set; }
        public string Title { get; set; }
        public string EngTitle { get; set; }
        public Nullable<System.Guid> PrivilegeGuid { get; set; }
        public Nullable<System.Guid> PrivilegeParentIGuid { get; set; }
        public string ControllerName { get; set; }
        public Nullable<long> PrivilegeCode { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Privilege> Privilege1 { get; set; }
        public virtual Privilege Privilege2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RolePrivilege> RolePrivileges { get; set; }
    }
}
