using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IPaymentService
    {
        Task AddPayment(Payment farmer);
        Task<Payment> GetPaymentById(long id);
        Task<List<Payment>> GetPayments();
        Task UpdatePayment(Payment payment);
        Task Delete(long id);
        Task<decimal> GetTotalLitresDelivered(long farmerId, long collectionPeriodId);
    }
}
