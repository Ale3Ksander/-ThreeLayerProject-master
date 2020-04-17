﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NinjaCore2.Data.DataContext;
using NinjaCore2.Data.Entities;
using NinjaCore2.Data.Repositories.Abstract;

namespace NinjaCore2.Data.Repositories.Concrete
{
    public class UserRepository : IUserRepository
    {
        private ApplicationDataContext context;

        public UserRepository(ApplicationDataContext context)
        {
            this.context = context;
        }

        public IEnumerable<User> GetUserList()
        {
            return context.Users;
        }

        public User GetUser(int id)
        {
            return context.Users.Find(id);
        }

        public void Create(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
        }

        public void Update(User user)
        {
            context.Users.AsNoTracking().FirstOrDefault(x => x.Id == user.Id);           
            context.Entry(user).State = EntityState.Modified;            
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            User user = context.Users.Find(id);
            if (user != null)
                context.Users.Remove(user);
            context.SaveChanges();
        }

        //public void Save()
        //{
        //    context.SaveChanges();
        //}
    }
}
