//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CA2_due4NOV2018
{
    using System;
    using System.Collections.Generic;
    
    public partial class Member
    {
        public int airc_id { get; set; }
        public int club_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string member_status { get; set; }
        public string role { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string DR { get; set; }
        public string SJ { get; set; }
        public string XC { get; set; }
    
        public virtual Club Club { get; set; }
        public virtual Grade Grade { get; set; }
        public virtual Grade Grade1 { get; set; }
        public virtual Grade Grade2 { get; set; }
        public virtual User User { get; set; }
    }
}