//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DefaultWpfApp.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Users
    {
        public Users()
        {
            this.Participants = new HashSet<Participants>();
        }
    
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public Nullable<System.DateTime> BirthDate { get; set; }
        public string AboutMe { get; set; }
        public Nullable<int> RolesId { get; set; }
        public string Image { get; set; }
    
        public virtual ICollection<Participants> Participants { get; set; }
        public virtual Roles Roles { get; set; }
    }
}