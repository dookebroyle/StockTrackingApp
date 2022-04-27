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
    class CustomerBLL : IBLL<CustomerDetailDTO, CustomerDTO>
    {
        CustomerDAO dao = new CustomerDAO();
        SalesDAO salesdao = new SalesDAO();
        public bool Delete(CustomerDetailDTO entity)
        {
            CUSTOMER customer = new CUSTOMER();
            customer.ID = entity.ID;
            dao.Delete(customer);
            SALE sale = new SALE();
            sale.CustomerID = entity.ID;
            salesdao.Delete(sale);
            return true;

        }

        public bool GetBack(CustomerDetailDTO entity)
        {
            return dao.GetBack(entity.ID);
        }

        public bool Insert(CustomerDetailDTO entity)
        {
            CUSTOMER customer = new CUSTOMER();
            customer.CustomerName = entity.CustomerName;
            return dao.Insert(customer);
        }

        public CustomerDTO Select()
        {
            CustomerDTO dto = new CustomerDTO();
            dto.Customers = dao.Select();
            return dto;
        }

        public CustomerDTO Select(bool IsDeleted)
        {
            CustomerDTO dto = new CustomerDTO();
            dto.Customers = dao.Select(IsDeleted);
            return dto;
        }

        public bool Update(CustomerDetailDTO entity)
        {
            CUSTOMER customer = new CUSTOMER();
            customer.ID = entity.ID;
            customer.CustomerName = entity.CustomerName;
            return dao.Update(customer);
        }
    }
}
