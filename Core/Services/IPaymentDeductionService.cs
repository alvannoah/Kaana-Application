using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IPaymentDeductionService
    {
        Task AddPaymentDeduction(PaymentDeduction paymentDeduction);
        Task<PaymentDeduction> GetPaymentDeductionById(long id);
        Task<List<PaymentDeduction>> GetPaymentDeductions();
        Task UpdatePaymentDeduction(PaymentDeduction paymentDeduction);
    }
}
