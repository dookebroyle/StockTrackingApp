using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockTrackingApp.DAL.DTO;
using StockTrackingApp.DAL.DAO;
using StockTrackingApp.DAL;

namespace StockTrackingApp.BLL
{
    public class SalesBLL : IBLL<SalesDetailDTO, SalesDTO>
    {
        SalesDAO salesdao = new SalesDAO();
        ProductDAO productdao = new ProductDAO();
        CategoryDAO categorydao = new CategoryDAO();
        CustomerDAO customerdao = new CustomerDAO();
        public bool Delete(SalesDetailDTO entity)
        {
            SALE sale = new SALE();
            sale.ID = entity.SaleID;
            salesdao.Delete(sale);
            PRODUCT product = new PRODUCT();
            product.ID = entity.ProductID;
            product.StockAmount = entity.StockAmount;
            productdao.Update(product);

            return true;
        }

        public bool GetBack(SalesDetailDTO entity)
        {
            throw new NotImplementedException();
        }

        public bool Insert(SalesDetailDTO entity)
        {
            SALE sale = new SALE();
            sale.CategoryID = entity.CategoryID;
            sale.ProductID = entity.ProductID;
            sale.CustomerID = entity.CustomerID;
            sale.ProductSalesPrice = entity.Price;
            sale.ProductSalesAmount = entity.SalesAmount;
            sale.SalesDate = entity.SalesDate;
            salesdao.Insert(sale);
            PRODUCT product = new PRODUCT();
            product.ID = entity.ProductID;
            int temp = entity.StockAmount - entity.SalesAmount;
            product.StockAmount = temp;
            productdao.Update(product);
            return true;

        }

        public SalesDTO Select()
        {
            SalesDTO dto = new SalesDTO();
            dto.Products = productdao.Select();
            dto.Customers = customerdao.Select();
            dto.Categories = categorydao.Select();
            dto.Sales = salesdao.Select();
            return dto;


        }

        public bool Update(SalesDetailDTO entity)
        {
            SALE sale = new SALE();
            sale.ID = entity.SaleID;
            sale.ProductSalesAmount = entity.SalesAmount;
            salesdao.Update(sale);
            PRODUCT product = new PRODUCT();
            product.ID = entity.ProductID;
            product.StockAmount = entity.StockAmount;
            productdao.Update(product);
            return true;
        }
    }
}
