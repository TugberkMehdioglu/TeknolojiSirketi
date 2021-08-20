using Project.BLL.DesignPatterns.GenericRepository.BaseRep;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.DesignPatterns.GenericRepository.ConcRep
{
    public class ProductRep : BaseRep<Product>
    {
        public void UpdateStockRange(List<Product> item)
        {
            foreach (Product element in item)
            {
                UpdateStock(element);
            }
        }

        public void UpdateStock(Product item)
        {
            item.Status = ENTITIES.Enums.DataStatus.Updated;
            item.ModifiedDate = DateTime.Now;
            Product toBeUpdated = Find(item.ID);
            toBeUpdated.UnitInStock = item.UnitInStock;
            Save();
        }
    }
}
