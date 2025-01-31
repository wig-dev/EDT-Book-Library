using EDT_Book_Library;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibraryTests
{
    public class BookLibraryWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(static services =>
            {
                var dbContextDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<BookLibraryDBContext>));

                services.Remove(dbContextDescriptor!);

                services.AddDbContext<BookLibraryDBContext>(opt => opt.UseInMemoryDatabase("BookLibraryTests"));
            });

            builder.UseEnvironment("development");
        }
    }
}
