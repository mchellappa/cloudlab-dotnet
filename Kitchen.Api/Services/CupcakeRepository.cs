using System;
using System.Collections.Generic;
using System.Linq;
using Kitchen.Api.Data;
using Kitchen.Api.Entities;

namespace Kitchen.Api.Services
{
    public class CupcakeRepository : ICupcakeRepository
    {
        private readonly InventoryContext _context;

        public CupcakeRepository(InventoryContext context){
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
        public Cupcake GetCupcake(int id)
        {
            return _context.Cupcakes.Where(
                c => c.Id == id).FirstOrDefault();
            
        }

        public IEnumerable<Cupcake> GetCupcakes()
        {
            return _context.Cupcakes.OrderBy(c => c.Name).ToList();
        }
    }
}