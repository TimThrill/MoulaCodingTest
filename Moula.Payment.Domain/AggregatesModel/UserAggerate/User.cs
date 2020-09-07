using System;
using System.Collections.Generic;
using System.Text;

namespace Moula.Payment.Domain.AggregatesModel.UserAggerate
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset Dob { get; set; }
    }
}
