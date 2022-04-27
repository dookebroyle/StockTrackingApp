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
            try
            {
                CUSTOMER customer = db.CUSTOMERs.First(x => x.ID == entity.ID);
                customer.isDeleted = true;
                customer.DeletedDate = DateTime.Today;
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
            CUSTOMER customer = db.CUSTOMERs.First(x => x.ID == ID);
            customer.isDeleted = false;
            customer.DeletedDate = null;
            db.SaveChanges();
            return true;
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
                var list = db.CUSTOMERs.Where(x=>x.isDeleted == false).ToList();
                foreach(var item in list)
                {
                    CustomerDetailDTO dto = new CustomerDetailDTO();
                    dto.ID = item.ID;
                    dto.CustomerName = item.CustomerName;
                    dto.IsDeleted = item.isDeleted;
                    dto.DeletedDate = Convert.ToDateTime(item.DeletedDate);
                    Customers.Add(dto);
                }
                return Customers;
            }
            catch (Exception)
            {

                throw;
            }        }

        public List<CustomerDetailDTO> Select(bool IsDeleted)
        {
            try
            {
                List<CustomerDetailDTO> Customers = new List<CustomerDetailDTO>();
                var list = db.CUSTOMERs.Where(x => x.isDeleted == true).ToList();
                foreach (var item in list)
                {
                    CustomerDetailDTO dto = new CustomerDetailDTO();
                    dto.ID = item.ID;
                    dto.CustomerName = item.CustomerName;
                    dto.IsDeleted = item.isDeleted;
                    dto.DeletedDate = Convert.ToDateTime(item.DeletedDate);
                    Customers.Add(dto);
                }
                return Customers;
            }
            catch (Exception)
            {

                throw;
            }
        }

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
