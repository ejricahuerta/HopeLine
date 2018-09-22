using HopeLine.DataAccess.Entities;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace HopeLine.DataAccess.DatabaseContexts
{
    public class ChatDbContext : DbContext
    {
        public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options)
        {
        }

        public DbSet<Message> Messages { get; set; }
    }
}