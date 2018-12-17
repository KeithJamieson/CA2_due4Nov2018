//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RELICDBLibrary
{
    using System;
    using System.Collections.Generic;
    
    public partial class Competition
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Competition()
        {
            this.Entries = new HashSet<Entry>();
            this.Leaderboards = new HashSet<Leaderboard>();
        }
    
        public int competition_id { get; set; }
        public string competition_name { get; set; }
        public int club_id { get; set; }
        public string venue { get; set; }
        public System.DateTime competition_date { get; set; }
        public int airc_id { get; set; }
        public string competition_type { get; set; }
        public string competition_status { get; set; }
    
        public virtual Club Club { get; set; }
        public virtual Member Member { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Entry> Entries { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Leaderboard> Leaderboards { get; set; }
    }
}
