//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class accepted_servises
    {
        public int order { get; set; }
        public int service { get; set; }
        public double result { get; set; }
        public System.DateTime finished { get; set; }
        public bool accepted { get; set; }
        public int status { get; set; }
        public int analyzer { get; set; }
        public int user { get; set; }
    
        public virtual accepted_services_statuses accepted_services_statuses { get; set; }
        public virtual analyzers analyzers { get; set; }
        public virtual orders orders { get; set; }
        public virtual services services { get; set; }
        public virtual users users { get; set; }
    }
}