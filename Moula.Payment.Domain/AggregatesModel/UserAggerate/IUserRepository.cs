﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Moula.Payment.Domain.AggregatesModel.UserAggerate
{
    public interface IUserRepository: IRepository<User>
    {
        Task<UserAccount> GetUserAccountAsync(int userId);
    }
}
