using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockTrackingApp.DAL;
using StockTrackingApp.DAL.DTO;


namespace StockTrackingApp.DAL.DAO
{
    public class SalesDAO :StockContext, IDAO<SALE, SalesDetailDTO>
    {
        public bool Delete(SALE entity)
        {
            try
            {
                SALE sale = db.SALEs.First(x => x.ID == entity.ID);
                sale.isDeleted = true;
                sale.DeletedDate = DateTime.Today;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool GetBack(int ID)
        {
            throw new NotImplementedException();
        }

        public bool Insert(SALE entity)
        {
            try
            {
                db.SALEs.Add(entity);
                db.SaveChanges();
                return true;
                
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<SalesDetailDTO> Select()
        {
            try
            {
                List<SalesDetailDTO> sales = new List<SalesDetailDTO>();
                var list = (from s in db.SALEs.Where(x => x.isDeleted == false || x.isDeleted==null)
                            join p in db.PRODUCTs on s.ProductID equals p.ID
                            join c in db.CUSTOMERs on s.CustomerID equals c.ID
                            join cat in db.CATEGORies on s.CategoryID equals cat.ID
                            select new
                            {
                                ProductName = p.ProductName,
                                CustomerName = c.CustomerName,
                                ProductID = s.ProductID,
                                CustomerID = s.CustomerID,
                                SalesID = s.ID,
                                CategoryID = s.CategoryID,
                                CategoryName = cat.CategoryName,
                                SalesPrice = s.ProductSalesPrice,
                                SalesAmount = s.ProductSalesAmount,
                                SalesDate = s.SalesDate,
                                StockAmount = p.StockAmount
                            }).OrderBy(x => x.SalesDate).ToList();
                foreach(var item in list)
                {
                    SalesDetailDTO dto = new SalesDetailDTO
                    {
                        ProductName = item.ProductName,
                        CustomerName = item.CustomerName,
                        ProductID = item.ProductID,
                        CustomerID = item.CustomerID,
                        SaleID = item.SalesID,
                        CategoryID = item.CategoryID,
                        CategoryName = item.CategoryName,
                        Price = item.SalesPrice,
                        SalesAmount = item.SalesAmount,
                        SalesDate = item.SalesDate,
                        StockAmount = item.StockAmount
                        
                    };
                    sales.Add(dto);
                }
                return sales;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Update(SALE entity)
        {
            try
            {
                SALE sales = db.SALEs.First(x => x.ID == entity.ID);
                sales.ProductSalesAmount = entity.ProductSalesAmount;
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
