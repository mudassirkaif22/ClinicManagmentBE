using System.ComponentModel.DataAnnotations;

namespace ClinicManagment.Model
{
    public class Signup
    {
        
        public int Id { get; set; }
        public string name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
       
    }
}
