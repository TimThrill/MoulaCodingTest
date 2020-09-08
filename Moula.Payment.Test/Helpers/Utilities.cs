using Moula.Payment.Domain.AggregatesModel.UserAggerate;
using Moula.Payment.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moula.Payment.Test.Helpers
{
    public static class Utilities
    {
        public static void InitializeDbForTests(PaymentContext db)
        {
            db.Users.AddRange(GetSeedingUsers());
            db.SaveChanges();
        }

        public static void ReinitializeDbForTests(PaymentContext db)
        {
            InitializeDbForTests(db);
        }

        public static List<User> GetSeedingUsers()
        {
            return new List<User>()
            {
                new User()
                {
                    FirstName = "Cong",
                    LastName = "Ma",
                    Dob = new DateTimeOffset(1989, 12, 2, 0, 0, 0, TimeSpan.Zero)
                },
            };
        }
    }
}
