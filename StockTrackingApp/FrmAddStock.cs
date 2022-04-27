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
    public partial class FrmAddStock : Form
    {
        public FrmAddStock()
        {
            InitializeComponent();
        }

        ProductBLL bll = new ProductBLL();
        ProductDTO dto = new ProductDTO();
        bool combofull = false;
        ProductDetailDTO detail = new ProductDetailDTO();

        private void txtStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.IsNumber(e);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmAddStock_Load(object sender, EventArgs e)
        {
            dto = bll.Select();
            dataGridView1.DataSource = dto.Products;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "Product Name";
            dataGridView1.Columns[2].HeaderText = "Category Name";
            dataGridView1.Columns[3].HeaderText = "Stock Amount";
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            cmbCateogry.DataSource = dto.Categories;
            cmbCateogry.DisplayMember = "CategoryName";
            cmbCateogry.ValueMember = "ID";
            cmbCateogry.SelectedIndex = -1;
            if (dto.Categories.Count > 0)
                combofull = true;

        }
        
        private void cmbCateogry_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            if (combofull)
            {
                List<ProductDetailDTO> List = dto.Products;
                List = List.Where(x => x.CategoryID == Convert.ToInt32(cmbCateogry.SelectedValue)).ToList();
                dataGridView1.DataSource = List;
                if(List.Count == 0)
                {
                    ClearFilters();
                }
            }
        }

        private void ClearFilters()
        {
            txtPrice.Clear();
            txtProductName.Clear();
            txtStock.Clear();
 
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            detail.ProductID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            detail.ProductName = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            detail.CategoryName = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            detail.StockAmount = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[3].Value);
            detail.Price = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[4].Value);
            detail.ProductID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[5].Value);
            
            txtProductName.Text = detail.ProductName;
            txtPrice.Text = detail.Price.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtProductName.Text.Trim() == "" || txtStock.Text.Trim() == "")
                MessageBox.Show("Please choose a product from the list.");
            else if (txtStock.Text.Trim() == "")
                MessageBox.Show("Please give stock amount.");
            else
            {
                int sumstock = detail.StockAmount + Convert.ToInt32(txtStock.Text);
                detail.StockAmount = sumstock;
                if(bll.Update(detail))
                  {
                        MessageBox.Show("Stock has been updated.");
                        bll = new ProductBLL();
                        dto = bll.Select();
                        dataGridView1.DataSource = dto.Products;
                        ClearFilters();
                    }
            }
        }
    }
}
