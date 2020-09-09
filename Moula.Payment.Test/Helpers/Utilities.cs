using Moula.Payment.Domain.AggregatesModel.UserAggerate;
using Moula.Payment.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moula.Payment.Test.Helpers
{
    public static class Utilities
    {
        public static decimal InitialBalance = 10000;
        public static decimal DefaultPaymentAmount = new decimal(199.99);
        public static int DefaultUserId = 1;
        public static Domain.AggregatesModel.PaymentAggerate.Payment PendingPaymentForCancel =
            new Domain.AggregatesModel.PaymentAggerate.Payment
            {
                Id = new Guid("00000000-0000-0000-0000-000000000001"),
                UserId = DefaultUserId,
                CreatedDate = DateTimeOffset.UtcNow,
                Amount = DefaultPaymentAmount,
                Status = Domain.PaymentStatus.Pending
            };
        public static Domain.AggregatesModel.PaymentAggerate.Payment PendingPaymentForProcess =
            new Domain.AggregatesModel.PaymentAggerate.Payment
            {
                Id = new Guid("00000000-0000-0000-0000-000000000002"),
                UserId = DefaultUserId,
                CreatedDate = DateTimeOffset.UtcNow,
                Amount = DefaultPaymentAmount,
                Status = Domain.PaymentStatus.Pending
            };
        public static Domain.AggregatesModel.PaymentAggerate.Payment ClosedPayment =
            new Domain.AggregatesModel.PaymentAggerate.Payment
            {
                Id = new Guid("00000000-0000-0000-0000-000000000003"),
                UserId = DefaultUserId,
                CreatedDate = DateTimeOffset.UtcNow,
                Amount = DefaultPaymentAmount,
                Status = Domain.PaymentStatus.Closed
            };
        public static Domain.AggregatesModel.PaymentAggerate.Payment ProcessedPayment =
            new Domain.AggregatesModel.PaymentAggerate.Payment
            {
                Id = new Guid("00000000-0000-0000-0000-000000000004"),
                UserId = DefaultUserId,
                CreatedDate = DateTimeOffset.UtcNow,
                Amount = DefaultPaymentAmount,
                Status = Domain.PaymentStatus.Processed
            };

        private static readonly object _lock = new object();
        private static bool _hasInstance = false;

        public static void InitializeDbForTests(PaymentContext db)
        {
            // Use a lock to avoid this method calling twice
            // Ref: https://github.com/dotnet/aspnetcore/issues/20307
            lock (_lock)
            {
                if(!_hasInstance)
                {
                    db.Users.AddRange(GetSeedingUsers());
                    db.SaveChanges();
                    foreach(var user in db.Users)
                    {
                        db.UserAccounts.Add(GetSeedingAccountForUser(user.Id));
                        db.Payments.AddRange(GetSeedingPaymentsForUser(user.Id));
                    }
                    db.SaveChanges();

                    _hasInstance = true;
                }
            }
        }

        public static void ReinitializeDbForTests(PaymentContext db)
        {
            InitializeDbForTests(db);
        }

        public static List<User> GetSeedingUsers()
        {
            var users = new List<User>()
            {
                new User()
                {
                    Id = DefaultUserId,
                    FirstName = "Cong",
                    LastName = "Ma",
                    Dob = new DateTimeOffset(1989, 12, 2, 0, 0, 0, TimeSpan.Zero)
                }
            };

            return users;
        }

        public static UserAccount GetSeedingAccountForUser(int userId)
        {
            return new UserAccount
            {
                UserId = userId,
                Balance = InitialBalance
            };
        }

        public static ICollection<Domain.AggregatesModel.PaymentAggerate.Payment> GetSeedingPaymentsForUser(int userId)
        {
            List<Moula.Payment.Domain.AggregatesModel.PaymentAggerate.Payment> payments = new List<Domain.AggregatesModel.PaymentAggerate.Payment>();
            payments.Add(PendingPaymentForCancel);
            payments.Add(PendingPaymentForProcess);
            payments.Add(ClosedPayment);
            payments.Add(ProcessedPayment);

            return payments;
        }
    }
}
