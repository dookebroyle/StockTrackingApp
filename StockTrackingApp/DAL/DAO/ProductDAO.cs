using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockTrackingApp.DAL;
using StockTrackingApp.DAL.DTO;

namespace StockTrackingApp.DAL.DAO
{
    public class ProductDAO : StockContext, IDAO<PRODUCT, ProductDetailDTO>
    {
        public bool Delete(PRODUCT entity)
        {
            throw new NotImplementedException();
        }

        public bool GetBack(int ID)
        {
            throw new NotImplementedException();
        }

        public bool Insert(PRODUCT entity)
        {
            try
            {
                db.PRODUCTs.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ProductDetailDTO> Select()
        {
            try
            {
                List<ProductDetailDTO> products = new List<ProductDetailDTO>();
                var list = (from p in db.PRODUCTs
                            join c in db.CATEGORies on p.CateogryID equals c.ID
                            select new
                            {
                                ProductName = p.ProductName,
                                CategoryName = c.CategoryName,
                                StockAmount = p.StockAmount,
                                Price = p.Price,
                                ProductID = p.ID,
                                CategoryID = c.ID
                            }).OrderBy(x => x.ProductName).ToList(); 

                foreach(var item in list)
                {
                    ProductDetailDTO dto = new ProductDetailDTO();
                    dto.ProductName = item.ProductName;
                    dto.CategoryName = item.CategoryName;
                    dto.StockAmount = item.StockAmount;
                    dto.Price = item.Price;
                    dto.ProductID = item.ProductID;
                    dto.CategoryID = item.CategoryID;
                    products.Add(dto);
                }
                return products;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Update(PRODUCT entity)
        {
            try
            {
                PRODUCT product = db.PRODUCTs.First(x => x.ID == entity.ID);
                if (entity.CateogryID == 0)
                {
                    product.StockAmount = entity.StockAmount;
                    
                }
                else
                {
                    product.ProductName = entity.ProductName;
                    product.Price = entity.Price;
                    product.CateogryID = entity.CateogryID;
                }
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
