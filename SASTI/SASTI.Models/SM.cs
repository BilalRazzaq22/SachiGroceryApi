//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SASTI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SM
    {
        public int SMS_ID { get; set; }
        public Nullable<int> SENDER { get; set; }
        public string TEXT { get; set; }
        public string RESPONSE { get; set; }
        public Nullable<System.DateTime> SENT_ON { get; set; }
        public Nullable<int> SMS_TYPE_ID { get; set; }
    }
}
