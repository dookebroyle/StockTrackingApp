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
    public partial class FrmCustomer : Form
    {
        public FrmCustomer()
        {
            InitializeComponent();
        }

        CustomerBLL bll = new CustomerBLL();
        public CustomerDetailDTO detail = new CustomerDetailDTO();
        public bool IsUpdate = false;


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtCustomerName.Text.Trim() == "")
                MessageBox.Show("Customer name is empty");
            else
            {
                if (!IsUpdate)
                {
                    CustomerDetailDTO customer = new CustomerDetailDTO();
                    customer.CustomerName = txtCustomerName.Text.ToUpper();
                    if (bll.Insert(customer))
                    {
                        MessageBox.Show("Customer was added.");
                        txtCustomerName.Clear();
                    }
                }
                else
                {
                    if (detail.CustomerName == txtCustomerName.Text)
                        MessageBox.Show("There is no change.");
                    else
                    {
                        detail.CustomerName = txtCustomerName.Text;
                        if (bll.Update(detail))
                        {
                            MessageBox.Show("Customer has been updated.");
                            this.Close();
                        }
                    }
                }

            }
        }

        private void FrmCustomer_Load(object sender, EventArgs e)
        {
            if (IsUpdate)
                txtCustomerName.Text = detail.CustomerName;
        }
    }
}
