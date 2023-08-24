using ChifrWithDB;
using ConsoleApp1;
using Domain.Entity;
using Domain.Repositories;
using System;


namespace Infrastucture;
internal class UserRepository: IUserRepository
{
    private readonly ApplicationContext _context;

    public UserRepository( ApplicationContext context )
    {
        _context = context;
    }

    public void AddUser(User user)
    {
        _context.Users.Add(user);
    }

    public List<User> GetUsers()
    {
        Chifr chifr = new Chifr();

        var users = _context.Users.ToList();

        foreach (var u in users)
        {
            u.message =  chifr.DeChifrator(u.message, u.bias);
        }

        return users;

        //return _context.Users.ToList();

    }
}
