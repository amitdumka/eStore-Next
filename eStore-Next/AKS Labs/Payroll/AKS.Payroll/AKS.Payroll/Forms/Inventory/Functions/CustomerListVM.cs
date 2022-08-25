using System.ComponentModel.DataAnnotations;

namespace AKS.Payroll.Forms.Inventory.Functions
{
    public class CustomerListVM
    {
        public string CustomerName { get; set; }

        [Key]
        public string MobileNo { get; set; }
    }
}