﻿using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IUserRepository
    {
        Task<User> AddUserAsync(User user);
    }
}
