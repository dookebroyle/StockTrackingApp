using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockTrackingApp.DAL.DTO;

namespace StockTrackingApp.DAL.DAO
{
    class CustomerDAO :StockContext, IDAO<CUSTOMER, CustomerDetailDTO>
    {
        public bool Delete(CUSTOMER entity)
        {
            throw new NotImplementedException();
        }

        public bool GetBack(int ID)
        {
            throw new NotImplementedException();
        }

        public bool Insert(CUSTOMER entity)
        {
            try
            {
                db.CUSTOMERs.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<CustomerDetailDTO> Select()
        {
            try
            {
                List<CustomerDetailDTO> Customers = new List<CustomerDetailDTO>();
                var list = db.CUSTOMERs;
                foreach(var item in list)
                {
                    CustomerDetailDTO dto = new CustomerDetailDTO();
                    dto.ID = item.ID;
                    dto.CustomerName = item.CustomerName;
                    dto.IsDeleted = item.isDeleted;
                    dto.DeletedDate = item.DeletedDate;
                    Customers.Add(dto);
                }
                return Customers;
            }
            catch (Exception)
            {

                throw;
            }        }

        public bool Update(CUSTOMER entity)
        {
            try
            {
                CUSTOMER customer = db.CUSTOMERs.First(x => x.ID == entity.ID);
                customer.CustomerName = entity.CustomerName;
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
