using System.Collections.Generic;
using Kitchen.Api.Entities;

namespace Kitchen.Api.Services
{
    public interface ICupcakeRepository
    {
        IEnumerable<Cupcake> GetCupcakes();

        Cupcake GetCupcake(int id);
        
    }
}