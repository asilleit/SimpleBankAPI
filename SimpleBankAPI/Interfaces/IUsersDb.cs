﻿using SimpleBankAPI.Models;

namespace SimpleBankAPI.Interfaces
{
    public interface IUsersDb 
    {
        Task<User> Create(User user);

    }
}
