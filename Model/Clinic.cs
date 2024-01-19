using System.ComponentModel.DataAnnotations;

namespace ClinicManagment.Model
{
    public class Clinic
    {
      
        public  int ID { get; set; }
        public  string UserId { get; set; }
        public string Name { get; set; }
        public string Symtopms { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
      

    }
}
