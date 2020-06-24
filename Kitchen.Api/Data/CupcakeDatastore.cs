using System.Collections.Generic;
using Kitchen.Api.Models;

namespace Kitchen.Api.Data
{
    public class CupcakeDatastore
    {
        public static CupcakeDatastore Current {get;} = new CupcakeDatastore();

        public List<CupcakeDto> Cupcakes {get; set; }
        public CupcakeDatastore(){
            Cupcakes = new List<CupcakeDto>(){
                new CupcakeDto(){
                    Id = 1,
                    Name = "Chocolate",
                    Description = "Chocolatey and delicious",
                    Price = 4.95m,
                    Image = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRgsnbhtyxwu83esiCshNY7-YCypRKUHikL58EuaX-DUq-A698-FA"
                },
                new CupcakeDto(){
                    Id = 2,
                    Name = "Vanilla",
                    Description = "Plain, boring vanilla cupcake",
                    Price = 4.95m,
                    Image = "https://www.lifeloveandsugar.com/wp-content/uploads/2017/01/Moist-Vanilla-Cupcakes2.jpg"
                },
                new CupcakeDto(){
                    Id = 3,
                    Name = "Red Velvet",
                    Description = "Is it chocolate? I don't know! It looks red and tastes delicious",
                    Price = 5.5m,
                    Image = "https://therecipecritic.com/wp-content/uploads/2017/01/RedVelvetCupcakes2-667x1000.jpg"
                },
                new CupcakeDto(){
                    Id = 4,
                    Name = "Pumpkin Spice",
                    Description = "Seasonal classic that tastes like everyone's favorite squash",
                    Price = 6.99m,
                    Image = "https://cdn.crownmediadev.com/4a/1e/9808ba9b4487a12d0ccccc84f61c/home-family-pumpkin-spice-latte-cupcakes.jpg"
                }
            };

        }
    }
}