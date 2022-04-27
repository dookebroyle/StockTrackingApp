﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTrackingApp.DAL.DTO
{
    public class ProductDetailDTO
    {
        public int ID { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public int StockAmount { get; set; }
        public int Price { get; set; }
        public int ProductID { get; set; }

        public int CategoryID { get; set; }
        
    }
}
