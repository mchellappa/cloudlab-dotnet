using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kitchen.Api.Entities
{
    public class Cupcake
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]        
        public int Id {get; set;}
        
        public string Name {get; set;}
        
        public string Description {get; set;}
        
        public decimal Price {get; set;}

        
        public string Image {get; set;}
    }
}