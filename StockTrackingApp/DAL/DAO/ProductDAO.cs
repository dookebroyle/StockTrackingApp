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
            try
            {
                if (entity.ID != 0)
                {
                    PRODUCT product = db.PRODUCTs.First(x => x.ID == entity.ID);
                    product.isDeleted = true;
                    product.DeletedDate = DateTime.Today;
                    db.SaveChanges();
                }
                else if (entity.CateogryID != 0)
                {
                    List<PRODUCT> list = db.PRODUCTs.Where(x => x.CateogryID == entity.CateogryID).ToList();
                    foreach (var item in list)
                    {
                        item.isDeleted = true;
                        item.DeletedDate = DateTime.Today;
                        List<SALE> sales = db.SALEs.Where(x => x.ProductID == item.ID).ToList();
                        foreach (var item2 in sales)
                        {
                            item2.isDeleted = true;
                            item2.DeletedDate = DateTime.Today;
                        }
                        db.SaveChanges();

                    }
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool GetBack(int ID)
        {
            PRODUCT product = db.PRODUCTs.First(x => x.ID == ID);
            product.isDeleted = false;
            product.DeletedDate = null;
            db.SaveChanges();
            return true;
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
                var list = (from p in db.PRODUCTs.Where(x => x.isDeleted == false)
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
        public List<ProductDetailDTO> Select(bool IsDeleted)
        {
            try
            {
                List<ProductDetailDTO> products = new List<ProductDetailDTO>();
                var list = (from p in db.PRODUCTs.Where(x => x.isDeleted == true)
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

                foreach (var item in list)
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
