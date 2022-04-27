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
    public partial class FrmProduct : Form
    {
        public FrmProduct()
        {
            InitializeComponent();
        }

        public ProductDTO dto = new ProductDTO();
        ProductBLL bll = new ProductBLL();
        public ProductDetailDTO detail = new ProductDetailDTO();
        public bool IsUpdate = false;

        private void txtCategory_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.IsNumber(e);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtProductName.Text.Trim() == "")
                MessageBox.Show("Product name is missing.");
            if (cmbCateogry.SelectedIndex == -1)
                MessageBox.Show("Product category is missing.");
            if (txtPrice.Text.Trim() == "")
                MessageBox.Show("Product price is missing.");
            else
            {
                if (!IsUpdate)
                {
                    ProductDetailDTO product = new ProductDetailDTO();
                    product.ProductName = txtProductName.Text.ToUpper();
                    product.CategoryID = Convert.ToInt32(cmbCateogry.SelectedValue);
                    product.Price = Convert.ToInt32(txtPrice.Text);
                    if (bll.Insert(product))
                    {
                        MessageBox.Show("Product was added.");
                        txtPrice.Clear();
                        txtProductName.Clear();
                        cmbCateogry.SelectedIndex = -1;
                    }
                }
                else
                {
                    if (detail.ProductName == txtProductName.Text &&
                        detail.CategoryID == Convert.ToInt32(cmbCateogry.SelectedValue) &&
                        detail.Price == Convert.ToInt32(txtPrice.Text))
                        MessageBox.Show("THere is no change.");
                    else
                    {
                        detail.ProductName = txtProductName.Text;
                        detail.CategoryID = Convert.ToInt32(cmbCateogry.SelectedValue);
                        detail.Price = Convert.ToInt32(txtPrice);
                        if (bll.Update(detail))
                        {
                            MessageBox.Show("Product has been updated.");
                            this.Close();
                        }
                    }
                }


            }
        }

        private void FrmProduct_Load(object sender, EventArgs e)
        {
            cmbCateogry.DataSource = dto.Categories;
            cmbCateogry.DisplayMember = "CategoryName";
            cmbCateogry.ValueMember = "ID";
            if (IsUpdate)
            {
                txtProductName.Text = detail.ProductName;
                txtPrice.Text = detail.Price.ToString();
                cmbCateogry.SelectedValue = detail.CategoryID;
            }
            else
            {

                cmbCateogry.SelectedIndex = -1;
            }
        }
    }
}
