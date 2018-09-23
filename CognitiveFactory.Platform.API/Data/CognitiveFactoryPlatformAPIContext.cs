using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CognitiveFactory.Platform.API.Models;

namespace CognitiveFactory.Platform.API.Data
{
    public class CognitiveFactoryPlatformAPIContext : DbContext
    {
        public CognitiveFactoryPlatformAPIContext (DbContextOptions<CognitiveFactoryPlatformAPIContext> options)
            : base(options)
        {
        }

        public DbSet<CognitiveFactory.Platform.API.Models.Solution> Solutions { get; set; }
    }
}
