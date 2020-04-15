using System;
using System.Linq;
using BusServiceApp.EF.Models;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace SenderApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ApplicationContext db = new ApplicationContext();

            User user1 = new User{ FirstName = "Artem", LastName = "Vatsyk", Age = "23"};
            db.Users.Add(user1);
            db.SaveChanges();
            Console.WriteLine("I don't know, better to check!");

            var users = db.Users.ToList();
            foreach (User i in users)
            {
                Console.WriteLine(i.FirstName);
            }
        }
    }
}
