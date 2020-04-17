using System;

namespace NinjaCore2.Data.Entities
{
    public class User
    {
        //InvalidCastException: Unable to cast object of type 'System.Int32' to type 'System.Guid'.
        public int Id { get; set; }       
        public string FirstName { get; set; }
        public string LastName { get; set; }       
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
