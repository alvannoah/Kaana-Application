using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Enums
{
    public enum RecordStatus
    {
        ACTIVE,
        INACTIVE,
        DELETED,
    }

    public enum ExpenseType
    {
        Transport,
        Maintenance,
        Salary
    }

    public enum PaymentMethod
    {
        Cash,
        MobileMoney,
        BankTransfer
    }

    public enum MilkQuality
    {
        GradeA,
        GradeB,
        Rejected
    }
}
