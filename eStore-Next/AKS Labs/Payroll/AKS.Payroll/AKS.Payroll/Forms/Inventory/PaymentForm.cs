using AKS.Payroll.Forms.Inventory.Functions;
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
    public partial class PaymentForm : Form
    {
        string Invoice;
        decimal Amount;
        public PaymentDetail Pd;
        public PaymentForm()
        {
            InitializeComponent();
            Invoice = "";
            Amount = 0;
        }
        public PaymentForm(string invNo, decimal amt)
        {
            InitializeComponent();
        }

        private void PaymentForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            cbxPaymentMode.Items.AddRange(Enum.GetNames(typeof(PaymentMode)));
            cbxCard.Items.AddRange(Enum.GetNames(typeof(Card)));
            cbxCardType.Items.AddRange(Enum.GetNames(typeof(CardType)));
            lbInv.Text = "Invoice No.: " + Invoice;
            lbAmount.Text = $"Amount: Rs. {Amount}";

        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //if (btnAdd.Text == "Add")
            //{
            //    btnAdd.Text = "Save";
            //}
            //else 
            if (btnAdd.Text == "Edit")
            {
                btnAdd.Text = "Save";
            }
            else if (btnAdd.Text == "Save")
            {
                Read();
                if (Pd != null)
                {
                    this.DialogResult = DialogResult.OK;
                    MessageBox.Show("Payment details add");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to read payment details!!!");
                }
            }
        }
        private PaymentDetail Read()
        {
            try
            {

                Pd = new PaymentDetail
                {
                    Amount = decimal.Parse(txtAmount.Text.Trim()),
                    InvoiceNumber = Invoice,
                    Mode = (PaymentMode)cbxPaymentMode.SelectedIndex,
                    RefNumber = txtRefNumber.Text.Trim(),
                    Id = $"ARD/INP/{DateTime.Now.Year}/{DateTime.Now.Month}/{DateTime.Now.Day}/{new Random().NextInt64(DateTime.Now.ToFileTime())}"
                };
                if (Pd.Mode == PaymentMode.Card)
                {
                    Pd.AuthCode = txtAuthCode.Text.Trim();
                    Pd.Card = (Card)cbxCard.SelectedIndex;
                    Pd.CardType = (CardType)cbxCardType.SelectedIndex;
                    Pd.LastFour = int.Parse(txtLastFour.Text.Trim());
                }
                return Pd;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return Pd;
            }
        }
    }
}


