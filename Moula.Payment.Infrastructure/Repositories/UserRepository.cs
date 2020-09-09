﻿using Microsoft.EntityFrameworkCore;
using Moula.Payment.Domain;
using Moula.Payment.Domain.AggregatesModel.UserAggerate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Moula.Payment.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        private readonly PaymentContext _context;

        public UserRepository(PaymentContext context)
        {
            _context = context;
        }
        public async Task<UserAccount> GetUserAccountAsync(int userId)
        {
            return await _context.UserAccounts.FirstOrDefaultAsync(ac => ac.UserId == userId);
        }
    }
}
