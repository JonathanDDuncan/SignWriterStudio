//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SWSEF.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DictionaryGloss
    {
        public long IDDictionaryGloss { get; set; }
        public Nullable<long> IDDictionary { get; set; }
        public Nullable<long> IDCulture { get; set; }
        public string gloss { get; set; }
        public string glosses { get; set; }
    
        public virtual Dictionary Dictionary { get; set; }
    }
}
