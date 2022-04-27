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
    public partial class FrmSales : Form
    {
        public FrmSales()
        {
            InitializeComponent();
        }

        public SalesDTO dto = new SalesDTO();
        bool combofull = false;
        public SalesDetailDTO detail = new SalesDetailDTO();
        SalesBLL bll = new SalesBLL();
        public bool IsUpdate = false;



        private void txtProductSalesAmount_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtProductSalesAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.IsNumber(e);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmSales_Load(object sender, EventArgs e)
        {
            cmbCateogry.DataSource = dto.Categories;
            cmbCateogry.DisplayMember = "CategoryName";
            cmbCateogry.ValueMember = "ID";
            cmbCateogry.SelectedIndex = -1;
            if (!IsUpdate)
            {
                gridProducts.DataSource = dto.Products;
                gridProducts.Columns[0].Visible = false;
                gridProducts.Columns[1].HeaderText = "Product Name";
                gridProducts.Columns[2].HeaderText = "Category Name";
                gridProducts.Columns[3].HeaderText = "Stock Amount";
                gridProducts.Columns[4].HeaderText = "Price";
                gridProducts.Columns[5].Visible = false;
                gridProducts.Columns[6].Visible = false;
                gridCustomers.DataSource = dto.Customers;
                gridCustomers.Columns[0].Visible = false;
                gridCustomers.Columns[1].HeaderText = "Customer Name";
                gridCustomers.Columns[2].Visible = false;
                gridCustomers.Columns[3].Visible = false;
                if (dto.Categories.Count > 0)
                    combofull = true;
            }
            else
            {
                panel1.Hide();
                txtCustomerName.Text = detail.CustomerName;
                txtProductName.Text = detail.ProductName;
                txtProductPrice.Text = detail.Price.ToString();
                txtProductSalesAmount.Text = detail.SalesAmount.ToString();
                ProductDetailDTO product = dto.Products.First(x => x.ProductID == detail.ProductID);
                detail.StockAmount = product.StockAmount;
                txtStock.Text = detail.StockAmount.ToString();
            }

        }

        private void cmbCateogry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combofull)
            {
                List<ProductDetailDTO> list = dto.Products;
                list = list.Where(x => x.CategoryID == Convert.ToInt32(cmbCateogry.SelectedValue)).ToList();
                gridProducts.DataSource = list;
                if(list.Count == 0)
                {
                    txtProductPrice.Clear();
                    txtProductName.Clear();
                    txtStock.Clear();
                }
            }
        }

        private void txtCustomerSerach_TextChanged(object sender, EventArgs e)
        {
            List<CustomerDetailDTO> list = dto.Customers;
            list = list.Where(x => x.CustomerName.Contains(txtCustomerSerach.Text.ToUpper())).ToList();
            gridCustomers.DataSource = list;
            if (list.Count == 0)
                txtCustomerName.Clear();
        }

        private void gridProducts_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            detail.ProductName = gridProducts.Rows[e.RowIndex].Cells[1].Value.ToString();
            detail.Price = Convert.ToInt32(gridProducts.Rows[e.RowIndex].Cells[4].Value);
            detail.StockAmount = Convert.ToInt32(gridProducts.Rows[e.RowIndex].Cells[3].Value);
            detail.ProductID = Convert.ToInt32(gridProducts.Rows[e.RowIndex].Cells[5].Value);
            detail.CategoryID = Convert.ToInt32(gridProducts.Rows[e.RowIndex].Cells[6].Value);
            txtProductName.Text = detail.ProductName;
            txtProductPrice.Text = detail.Price.ToString();
            txtStock.Text = detail.StockAmount.ToString();
        }

        private void gridCustomers_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            detail.CustomerName = gridCustomers.Rows[e.RowIndex].Cells[1].Value.ToString();
            detail.CustomerID = Convert.ToInt32(gridCustomers.Rows[e.RowIndex].Cells[0].Value);
            txtCustomerName.Text = detail.CustomerName;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (detail.ProductID == 0)
                MessageBox.Show("Please select a product from the product table.");
            
            else
            {
                if (!IsUpdate)
                {
                    if (detail.CustomerID == 0)
                        MessageBox.Show("Please select a customer from the customer table.");
                    else if (txtProductSalesAmount.Text.Trim() == "")
                        MessageBox.Show("Please enter a stock amount for purchase.");
                    else if (detail.StockAmount < Convert.ToInt32(txtProductSalesAmount.Text))
                        MessageBox.Show("Not enough product for sale.");
                    else {
                        detail.SalesAmount = Convert.ToInt32(txtProductSalesAmount.Text);
                        detail.SalesDate = DateTime.Today;
                        if (bll.Insert(detail))
                        {
                            MessageBox.Show("Sale has been added.");
                            bll = new SalesBLL();
                            dto = bll.Select();
                            gridProducts.DataSource = dto.Products;
                            gridCustomers.DataSource = dto.Customers;
                            combofull = false;
                            cmbCateogry.DataSource = dto.Categories;
                            if (dto.Products.Count > 0)
                                combofull = true;
                            txtProductSalesAmount.Clear();
                        }
                    }   
                }
                else
                {
                    if (detail.SalesAmount == Convert.ToInt32(txtProductSalesAmount.Text))
                        MessageBox.Show("There is no change.");
                    else
                    {
                        int temp = detail.StockAmount + detail.SalesAmount;
                        if (temp < Convert.ToInt32(txtProductSalesAmount.Text))
                        {
                            MessageBox.Show("Not enough product for sale. Check Inventory.");
                        }
                        else
                        {
                            detail.SalesAmount = Convert.ToInt32(txtProductSalesAmount.Text);
                            detail.StockAmount = temp - detail.SalesAmount;
                           
                            if (bll.Update(detail))
                            {
                                MessageBox.Show("Sale has been updated.");
                                this.Close();

                            }
                        }
                    }
                }
            }
        }
    }
}
