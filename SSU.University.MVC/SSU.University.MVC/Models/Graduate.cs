//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SSU.University.MVC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Graduate
    {
        public int Id { get; set; }
        public int IdStudent { get; set; }
        public System.DateTime DateGraduates { get; set; }
    
        public virtual Student Student { get; set; }
    }
}
