using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockTrackingApp.DAL.DTO;

namespace StockTrackingApp.DAL.DAO
{
    public class CategoryDAO:StockContext, IDAO<CATEGORY, CategoryDetailDTO>
    {
        public bool Delete(CATEGORY entity)
        {
            try
            {
                CATEGORY category = db.CATEGORies.First(x => x.ID == entity.ID);
                Console.WriteLine(entity.ID);
                category.isDeleted = true;
                category.DeletedDate = DateTime.Today;
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
            try
            {
                CATEGORY category = db.CATEGORies.First(x => x.ID == ID);
                category.isDeleted = false;
                category.DeletedDate = null;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Insert(CATEGORY entity)
        {
            try
            {
                db.CATEGORies.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<CategoryDetailDTO> Select()
        {
            List<CategoryDetailDTO> categories = new List<CategoryDetailDTO>();
            var list = db.CATEGORies.Where(x => x.isDeleted == false);
            foreach(var item in list)
            {
                CategoryDetailDTO dto = new CategoryDetailDTO();
                dto.ID = item.ID;
                dto.CategoryName = item.CategoryName;
                categories.Add(dto);
            }
            return categories;
        }
        public List<CategoryDetailDTO> Select(bool IsDeleted)
        {
            List<CategoryDetailDTO> categories = new List<CategoryDetailDTO>();
            var list = db.CATEGORies.Where(x => x.isDeleted == true);
            foreach (var item in list)
            {
                CategoryDetailDTO dto = new CategoryDetailDTO();
                dto.ID = item.ID;
                dto.CategoryName = item.CategoryName;
                categories.Add(dto);
            }
            return categories;
        }

        public bool Update(CATEGORY entity)
        {
            try
            {
                CATEGORY category = db.CATEGORies.First(x => x.ID == entity.ID);
                category.CategoryName = entity.CategoryName;
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
