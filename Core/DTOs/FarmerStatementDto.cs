using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class FarmerStatementDto
    {
        public long FarmerId { get; set; }
        public string FarmerName { get; set; }
        public decimal TotalLitres { get; set; }
        public decimal RatePerLitre { get; set; }
        public decimal AdvanceIssued { get; set; }
        public decimal AdvanceRepaid { get; set; }
        public decimal AdvanceBalance { get; set; }
        public decimal AdvanceRecovered { get; set; }
        public decimal NetAfterAdvances { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal TotalDeductions { get; set; }
        public decimal NetAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal Balance { get; set; }
    }
}
