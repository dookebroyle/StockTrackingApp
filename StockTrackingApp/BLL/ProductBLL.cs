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
    public class ProductBLL : IBLL<ProductDetailDTO, ProductDTO>
    {
        CategoryDAO categorydao = new CategoryDAO();
        ProductDAO productdao = new ProductDAO();
        SalesDAO saledao = new SalesDAO();
        public bool Delete(ProductDetailDTO entity)
        {
            PRODUCT product = new PRODUCT();
            product.ID = entity.ProductID;
            productdao.Delete(product);
            SALE sale = new SALE();
            sale.ProductID = entity.ProductID;
            saledao.Delete(sale);
            return true;
            
        }

        public bool GetBack(ProductDetailDTO entity)
        {
            return productdao.GetBack(entity.ID);
        }

        public bool Insert(ProductDetailDTO entity)
        {
            PRODUCT product = new PRODUCT();
            product.ProductName = entity.ProductName;
            product.Price = entity.Price;
            product.CateogryID = entity.CategoryID;
            return productdao.Insert(product);
        }

        public ProductDTO Select()
        {
            ProductDTO dto = new ProductDTO();
            dto.Categories = categorydao.Select();
            dto.Products = productdao.Select();
            return dto;
        }
        public ProductDTO Select(bool isDeleted)
        {
            ProductDTO dto = new ProductDTO();
            dto.Categories = categorydao.Select(isDeleted);
            dto.Products = productdao.Select(isDeleted);
            return dto;
        }

        public bool Update(ProductDetailDTO entity)
        {
            PRODUCT product = new PRODUCT();
            product.ID = entity.ProductID;
            product.Price = entity.Price;
            product.ProductName = entity.ProductName;
            product.StockAmount = entity.StockAmount;
            product.CateogryID = entity.CategoryID;
            return productdao.Update(product);
        }
    }
}
