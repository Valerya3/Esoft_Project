//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Esoft_Project
{
    using System;
    using System.Collections.Generic;
    
    public partial class SupplySet
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SupplySet()
        {
            this.DealSet = new HashSet<DealSet>();
        }
    
        public int ID { get; set; }
        public int IDRieltor { get; set; }
        public int IDClient { get; set; }
        public int IDRealEstate { get; set; }
        public long Prise { get; set; }
    
        public virtual ClientsSet ClientsSet { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DealSet> DealSet { get; set; }
        public virtual RealEstateSet RealEstateSet { get; set; }
        public virtual RieltorSet RieltorSet { get; set; }
    }
}
