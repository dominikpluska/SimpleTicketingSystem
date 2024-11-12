﻿using DataAccess.Repository.Interface;
using DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using TicketsAPI.Data;

namespace TicketsAPI.UnitOfWork
{
    public class UnitOfWorkCategory : IUnitOfWorkCategory
    {
        public ICategoryRepository CategoryRepository { get; private set; }
        private readonly DbContext _context;

        public UnitOfWorkCategory(ApplicationDbContext context)
        {
            _context = context;
            CategoryRepository = new CategoryRepository(_context);
        }

        public async void SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}