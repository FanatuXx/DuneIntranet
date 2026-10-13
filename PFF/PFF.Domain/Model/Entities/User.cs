using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace PFF.Domain.Model.Entities
{
    public class User (int id, string username, string role)
    {
        public int Id { get; } = id;
        public string Username { get; } = username;
        public string Role { get; } = role;
        public string? RefreshToken { get; set; }
    }
}
