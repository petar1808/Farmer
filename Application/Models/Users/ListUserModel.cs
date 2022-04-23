using Application.Mappings;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Users
{
    public class ListUserModel 
    {
        public string Id { get; init; } = default!;

        public string Email { get; init; } = default!;

        public string Role { get; init; } = default!;

        public bool Active { get; init; } = default!;
    }
}
