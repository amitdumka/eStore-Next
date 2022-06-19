using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AKS.Payroll.Forms.Inventory
{
    public partial class SalesForm : Form
    {
        private InvoiceType InvoiceType;

        public SalesForm()
        {
            InitializeComponent();
            InvoiceType = InvoiceType.ManualSale;
        }
        public SalesForm(InvoiceType type)
        {
            InitializeComponent();
            InvoiceType = type;
        }

        private void SetupForm()
        {
            switch (InvoiceType)
            {
                case InvoiceType.Sales:
                    
                    break;
                case InvoiceType.SalesReturn:
                    break;
                case InvoiceType.ManualSale:
                    break;
                case InvoiceType.ManualSaleReturn:
                    break;
                default:
                    break;
            }
        }

        private void SalesForm_Load(object sender, EventArgs e)
        {
            
            
        }
    }
}
