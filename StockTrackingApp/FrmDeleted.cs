using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StockTrackingApp.BLL;
using StockTrackingApp.DAL.DTO;

namespace StockTrackingApp
{
    public partial class FrmDeleted : Form
    {
        public FrmDeleted()
        {
            InitializeComponent();
        }

        SalesDTO dto = new SalesDTO();
        SalesBLL bll = new SalesBLL();
        SalesDetailDTO salesdetail = new SalesDetailDTO();
        ProductDetailDTO productdetail = new ProductDetailDTO();
        CategoryDetailDTO categorydetail = new CategoryDetailDTO();
        CustomerDetailDTO customerdetail = new CustomerDetailDTO();
        CategoryBLL categoryBLL = new CategoryBLL();
        ProductBLL productBLL = new ProductBLL();
        CustomerBLL customerBLL = new CustomerBLL();
        private void FrmDeleted_Load(object sender, EventArgs e)
        {
            cmbDeletedData.Items.Add("Category");
            cmbDeletedData.Items.Add("Customer");
            cmbDeletedData.Items.Add("Product");
            cmbDeletedData.Items.Add("Sales");
            dto = bll.Select(true);
            dataGridView1.DataSource = dto.Sales;
            dataGridView1.Columns[0].HeaderText = "Customer Name";
            dataGridView1.Columns[1].HeaderText = "Product Name";
            dataGridView1.Columns[2].HeaderText = "Category Name";
            dataGridView1.Columns[3].HeaderText = "Customer ID";
            dataGridView1.Columns[4].HeaderText = "Product ID";
            dataGridView1.Columns[5].HeaderText = "Category ID";
            dataGridView1.Columns[6].HeaderText = "Sales Amount";
            dataGridView1.Columns[7].HeaderText = "Price";
            dataGridView1.Columns[8].HeaderText = "Sales Date";
            dataGridView1.Columns[9].HeaderText = "Stock Amount";
            dataGridView1.Columns[10].HeaderText = "Sale ID";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
        }


        


        private void cmbDeletedData_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDeletedData.SelectedIndex == 0){
                dataGridView1.DataSource = dto.Categories;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Category Name";
            }
            else if (cmbDeletedData.SelectedIndex == 1)
            {
                dataGridView1.DataSource = dto.Customers;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Customer Name";
                dataGridView1.Columns[2].Visible = false;
                dataGridView1.Columns[3].Visible = false;
            }
            else if (cmbDeletedData.SelectedIndex == 2)
            {
                dataGridView1.DataSource = dto.Products;
                dataGridView1.Columns[0].HeaderText = "Unknown";
                dataGridView1.Columns[1].HeaderText = "Product Name";
                dataGridView1.Columns[2].HeaderText = "Category Name";
                dataGridView1.Columns[3].HeaderText = "Stock Amount";
                dataGridView1.Columns[4].HeaderText = "Price";
                dataGridView1.Columns[5].HeaderText = "Product ID";
                dataGridView1.Columns[6].HeaderText = "Category ID";
            }
            else if (cmbDeletedData.SelectedIndex == 3)
            {
                dataGridView1.DataSource = dto.Sales;

                dataGridView1.Columns[0].HeaderText = "Customer Name";
                dataGridView1.Columns[1].HeaderText = "Product Name";
                dataGridView1.Columns[2].HeaderText = "Category Name";
                dataGridView1.Columns[3].HeaderText = "Customer ID";
                dataGridView1.Columns[4].HeaderText = "Product ID";
                dataGridView1.Columns[5].HeaderText = "Category ID";
                dataGridView1.Columns[6].HeaderText = "Sales Amount";
                dataGridView1.Columns[7].HeaderText = "Price";
                dataGridView1.Columns[8].HeaderText = "Sales Date";
                dataGridView1.Columns[9].HeaderText = "Stock Amount";
                dataGridView1.Columns[10].HeaderText = "Sale ID";

            }
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if(cmbDeletedData.SelectedIndex == 0)
            {
                categorydetail = new CategoryDetailDTO();
                categorydetail.ID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                categorydetail.CategoryName = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            }
            else if (cmbDeletedData.SelectedIndex == 1)
            {
                customerdetail = new CustomerDetailDTO();
                customerdetail.ID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                customerdetail.CustomerName = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            }
           else if (cmbDeletedData.SelectedIndex == 2)
            {
                productdetail = new ProductDetailDTO();
                productdetail.ProductID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[5].Value);
                productdetail.ProductName = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                productdetail.CategoryID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[6].Value);
                productdetail.Price = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[4].Value);
            }
            else if (cmbDeletedData.SelectedIndex == 3)
            {
                salesdetail = new SalesDetailDTO();
                salesdetail.SaleID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[10].Value);
                salesdetail.ProductID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[4].Value);
                salesdetail.CustomerName = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                salesdetail.ProductName = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                salesdetail.Price = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[7].Value);
                salesdetail.SalesAmount = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[6].Value);
            }
        }

        private void btnGetBack_Click(object sender, EventArgs e)
        {
            if (cmbDeletedData.SelectedIndex == 0)
            {
                if (categoryBLL.GetBack(categorydetail))
                {
                    dto = bll.Select(true);
                    dataGridView1.DataSource = dto.Categories;
                }
            }
            else if (cmbDeletedData.SelectedIndex == 1)
            {
                if (customerBLL.GetBack(customerdetail))
                {
                    dto = bll.Select(true);
                    dataGridView1.DataSource = dto.Customers;
                }

            }
            else if (cmbDeletedData.SelectedIndex == 2)
            {
                if (productBLL.GetBack(productdetail))
                {
                    dto = bll.Select(true);
                    dataGridView1.DataSource = dto.Products;
                }

            }
            else if (cmbDeletedData.SelectedIndex == 3)
            {
                if (bll.GetBack(salesdetail))
                {
                    dto = bll.Select(true);
                    dataGridView1.DataSource = dto.Sales;
                }


            }
        }
    }
}
