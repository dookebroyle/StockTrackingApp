﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockTrackingApp.DAL.DTO;
using StockTrackingApp.DAL.DAO;
using StockTrackingApp.DAL;

namespace StockTrackingApp.BLL
{
    public class CategoryBLL : IBLL<CategoryDetailDTO, CategoryDTO>
    {
        CategoryDAO dao = new CategoryDAO();
        public bool Delete(CategoryDetailDTO entity)
        {
            throw new NotImplementedException();
        }

        public bool GetBack(CategoryDetailDTO entity)
        {
            throw new NotImplementedException();
        }

        public bool Insert(CategoryDetailDTO entity)
        {
            CATEGORY category = new CATEGORY();
            category.CategoryName = entity.CategoryName;
            return dao.Insert(category);

        }

        public CategoryDTO Select()
        {
            CategoryDTO dto = new CategoryDTO();
            dto.Categories = dao.Select();
            return dto;
        }

        public bool Update(CategoryDetailDTO entity)
        {
            throw new NotImplementedException();
        }
    }
}
