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
        public bool Delete(CustomerDetailDTO entity)
        {
            throw new NotImplementedException();
        }

        public bool GetBack(CustomerDetailDTO entity)
        {
            throw new NotImplementedException();
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

        public bool Update(CustomerDetailDTO entity)
        {
            throw new NotImplementedException();
        }
    }
}
