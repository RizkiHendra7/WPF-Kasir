//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WPF_CURD_TOKO_HARDWARE_SOFTWARE
{
    using System;
    using System.Collections.Generic;
    
    public partial class Barang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Barang()
        {
            this.ItemPembelian = new HashSet<ItemPembelian>();
            this.ItemPenjualan = new HashSet<ItemPenjualan>();
        }
    
        public int IdBarang { get; set; }
        public int IdSupplier { get; set; }
        public int IdAdmin { get; set; }
        public string NamaBarang { get; set; }
        public int Stok { get; set; }
        public int HargaSatuan { get; set; }
        public bool IsDelete { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> DeleteDate { get; set; }
    
        public virtual Supplier Supplier { get; set; }
        public virtual UserAdmin UserAdmin { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ItemPembelian> ItemPembelian { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ItemPenjualan> ItemPenjualan { get; set; }
    }
}
