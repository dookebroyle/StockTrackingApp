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
    public partial class FrmCategory : Form
    {
        public FrmCategory()
        {
            InitializeComponent();
        }

        CategoryBLL bll = new CategoryBLL();
        public CategoryDetailDTO detail = new CategoryDetailDTO();
        public bool IsUpdate = false;

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void FrmCategory_Load(object sender, EventArgs e)
        {
            if (IsUpdate)
                txtCategory.Text = detail.CategoryName;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(txtCategory.Text.Trim() == "")
                MessageBox.Show("Category name is empty");
            else
            {
                if (!IsUpdate)
                {
                    CategoryDetailDTO category = new CategoryDetailDTO();
                    category.CategoryName = txtCategory.Text.ToUpper();
                    if (bll.Insert(category))
                    {
                        MessageBox.Show("Category was added.");
                        txtCategory.Clear();
                    }
                }
                else if(IsUpdate)
                {
                    if (detail.CategoryName == txtCategory.Text.Trim())
                        MessageBox.Show("There is no change.");
                    else
                    {
                        detail.CategoryName = txtCategory.Text;
                        if (bll.Update(detail))
                        {
                            MessageBox.Show("Category has been updated.");
                            this.Close();
                        }
                    }
                    
                }
            }
           
        }
    }
}
