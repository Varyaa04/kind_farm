//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace kind_farm.Conn_DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class products_table
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public products_table()
        {
            this.cart_table = new HashSet<cart_table>();
            this.order_manager_table = new HashSet<order_manager_table>();
        }
    
        public int id_product { get; set; }
        public string name_product { get; set; }
        public int id_type_product { get; set; }
        public int id_kind_product { get; set; }
        public double weight { get; set; }
        public int id_unit { get; set; }
        public double cost { get; set; }
        public string picture { get; set; }
        public string description { get; set; }
        public Nullable<int> id_allergens { get; set; }

        public string currentPhoto
        {
            get
            {
                if (string.IsNullOrEmpty(picture) || string.IsNullOrWhiteSpace(picture))
                {
                    return "/img/none.png";
                }
                else
                {
                    return "/img/" + picture;
                }
            }
        }

        public virtual allergens_table allergens_table { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<cart_table> cart_table { get; set; }
        public virtual kind_product_table kind_product_table { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<order_manager_table> order_manager_table { get; set; }
        public virtual type_product_table type_product_table { get; set; }
        public virtual unit_table unit_table { get; set; }
    }
}
