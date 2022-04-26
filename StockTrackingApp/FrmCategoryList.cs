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
    public partial class FrmCategoryList : Form
    {
        public FrmCategoryList()
        {
            InitializeComponent();
        }

        CategoryDTO dto = new CategoryDTO();
        CategoryBLL bll = new CategoryBLL();


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmCateogryList_Load(object sender, EventArgs e)
        {
            dto = bll.Select();
            dataGridView1.DataSource = dto.Categories;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "Category Name";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FrmCategory frm = new FrmCategory();
            this.Hide();
            frm.ShowDialog();
            this.Visible = true;
            dto = bll.Select();
            dataGridView1.DataSource = dto.Categories;
        }

        private void txtCategory_TextChanged(object sender, EventArgs e)
        {
            List<CategoryDetailDTO> list = dto.Categories;
            list = list.Where(x =>x.CategoryName.Contains(txtCategory.Text.ToUpper())).ToList();
            dataGridView1.DataSource = list;
        }
    }
}
