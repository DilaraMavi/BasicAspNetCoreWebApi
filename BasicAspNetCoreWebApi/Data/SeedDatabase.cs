using BasicAspNetCoreWebApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicAspNetCoreWebApi.Data
{
    public class SeedDatabase
    {   // İlk verileri dbye static ekliyorum.
        public static void Seed(DbContext context)
        {
            if (!context.Database.GetPendingMigrations().Any())
            {
                if (context is DataContext)
                {
                    DataContext _context = context as DataContext;
                    if (!_context.Users.Any())
                    {
                        _context.Users.AddRange(Users);
                    }
                }
            }
            context.SaveChanges();
        }

        private static User[] Users =
        {
           new User(){ Name="Name1", Email="test1@test.com", Department="Department1" /*,UserName="UserName1", Password="testpass"*/},
           new User(){ Name="Name2", Email="test2@test.com", Department="Department2" /*,UserName="UserName3", Password="testpass"*/},
           new User(){ Name="Name3", Email="test3@test.com", Department="Department3" /*,UserName="UserName3", Password="testpass"*/}
        };
    }
}
