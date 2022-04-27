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
    public partial class FrmProductList : Form
    {
        public FrmProductList()
        {
            InitializeComponent();
        }
        ProductBLL bll = new ProductBLL();
        ProductDTO dto = new ProductDTO();
        ProductDetailDTO detail = new ProductDetailDTO();
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (detail.ProductID == 0)
                MessageBox.Show("Please select a product from the table.");
            else
            {
                DialogResult result = MessageBox.Show("Are you sure?", "Warning", MessageBoxButtons.YesNo);
                
                if (result == DialogResult.Yes)
                {
                    if (bll.Delete(detail))
                    {
                        MessageBox.Show("Product has been deleted.");
                        bll = new ProductBLL();
                        dto = bll.Select();
                        dataGridView1.DataSource = dto.Products;
                        cmbCateogry.DataSource = dto.Categories;
                        CleanFilters();
                    }
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (detail.ProductID == 0)
                MessageBox.Show("Please select a product from table");
            else
            {
                FrmProduct frm = new FrmProduct();
                frm.IsUpdate = true;
                frm.detail = detail;
                frm.dto = dto;
                this.Hide();
                frm.ShowDialog();
                this.Visible = true;
                bll = new ProductBLL();
                dto = bll.Select();
                dataGridView1.DataSource = dto.Products;
                CleanFilters();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FrmProduct frm = new FrmProduct();
            frm.dto = dto;

            this.Hide();
            frm.ShowDialog();
            this.Visible = true;
            dto = bll.Select();
            dataGridView1.DataSource = dto.Products;
            CleanFilters();
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.IsNumber(e);
        }

        private void txtStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.IsNumber(e);
        }

        private void FrmProductList_Load(object sender, EventArgs e)
        {
            dto = bll.Select();
            cmbCateogry.DataSource = dto.Categories;
            cmbCateogry.DisplayMember = "CategoryName";
            cmbCateogry.ValueMember = "ID";
            cmbCateogry.SelectedIndex = -1;
            dataGridView1.DataSource = dto.Products;
            dataGridView1.Columns[0].HeaderText = "Unknown";
            dataGridView1.Columns[1].HeaderText = "Product Name";
            dataGridView1.Columns[2].HeaderText = "Category Name";
            dataGridView1.Columns[3].HeaderText = "Stock Amount";
            dataGridView1.Columns[4].HeaderText = "Price";
            dataGridView1.Columns[5].HeaderText = "Product ID";
            dataGridView1.Columns[6].HeaderText = "Category ID";

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<ProductDetailDTO> list = dto.Products;
            if (txtProductName.Text.Trim() != "")
                list = list.Where(x => x.ProductName.Contains(txtProductName.Text)).ToList();
            if (cmbCateogry.SelectedIndex != -1)
                list = list.Where(x => x.CategoryID == Convert.ToInt32(cmbCateogry.SelectedValue)).ToList();
            if(txtPrice.Text.Trim()!="")
            {
                if (rbPriceEqual.Checked)
                    list = list.Where(x => x.Price == Convert.ToInt32(txtPrice.Text)).ToList();
                else if (rbPriceLess.Checked)
                    list = list.Where(x => x.Price < Convert.ToInt32(txtPrice.Text)).ToList();
                else if (rbPriceMore.Checked)
                    list = list.Where(x => x.Price > Convert.ToInt32(txtPrice.Text)).ToList();
                else
                    MessageBox.Show("Please select a criterion from price group. (Check radio buttons.)");
            }
            if (txtStock.Text.Trim() != "")
            {
                if (rbStockEqual.Checked)
                    list = list.Where(x => x.StockAmount == Convert.ToInt32(txtStock.Text)).ToList();
                else if (rbStockLess.Checked)
                    list = list.Where(x => x.StockAmount < Convert.ToInt32(txtStock.Text)).ToList();
                else if (rbStockMore.Checked)
                    list = list.Where(x => x.StockAmount  > Convert.ToInt32(txtStock.Text)).ToList();
                else
                    MessageBox.Show("Please select a criterion from stock group. (Check radio buttons.");
            }
            dataGridView1.DataSource = list;


        }

        
        private void btnClear_Click(object sender, EventArgs e)
        {
            CleanFilters();
        }

        private void CleanFilters()
        {
            txtPrice.Clear();
            txtProductName.Clear();
            txtStock.Clear();
            cmbCateogry.SelectedIndex = -1;
            rbPriceEqual.Checked = false;
            rbPriceLess.Checked = false;
            rbPriceMore.Checked = false;
            rbStockEqual.Checked = false;
            rbStockLess.Checked = false;
            rbStockMore.Checked = false;
            dataGridView1.DataSource = dto.Products;
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            detail = new ProductDetailDTO();
            detail.ProductID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[5].Value);
            
            detail.ProductName = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            detail.CategoryID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[6].Value);
            detail.Price = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[4].Value);

 
        }
    }
}
