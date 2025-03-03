using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Auth.Core.Domain.IdentityEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
	{
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
