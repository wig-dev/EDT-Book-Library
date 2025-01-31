using EDT_Book_Library;
using EDT_Book_Library.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BookLibraryDBContext>(opt => opt.UseInMemoryDatabase("BookLibrary"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.AddServiceDefaults();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("GetPostDelete",
        builder =>
        builder.AllowAnyOrigin().WithMethods("GET", "POST", "DELETE")
        .AllowAnyHeader());
});


var app = builder.Build();


//Seed some data without adding complexity of migrations
using(var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<BookLibraryDBContext>();
    var seeder = new DataSeeder(context);
    await seeder.SeedData();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseCors("GetPostDelete");
    //We are using .NET9 OpenAPI for API documentation generation instead of SwaggerGen. We are then pointing SwaggerUI at this. 
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
    });

}


//Setup Minimal API Endpoints
app.MapGet("/books", async (BookLibraryDBContext db) =>
    await db.Books.ToListAsync());

app.MapGet("/books/{id}", async (int id, BookLibraryDBContext db) =>
    await db.Books.FindAsync(id)
        is Book book
            ? Results.Ok(book)
            : Results.NotFound());

app.MapPost("/books", async (Book book, BookLibraryDBContext db) =>
{
    db.Books.Add(book); 
    await db.SaveChangesAsync();

    return Results.Created($"/books/{book.Id}", book);
});


app.MapPost("/checkout/{bookid}/{customerid}", async (int bookId, int customerId, BookLibraryDBContext db) =>
{
    var book = await db.Books.FindAsync(bookId);
    if (book is null) return Results.NotFound();
    var customer = await db.Customers.FindAsync(customerId);
    if(customer is null) return Results.NotFound();

    if (book.IsAvailable)
    {
        db.BookCheckout.Add(new BookCheckout { BookId = book.Id, CustomerId = customer.Id });
        book.IsAvailable = false;
        await db.SaveChangesAsync();
        return Results.NoContent();
    }
    return Results.Problem("Book Unavailable");

   
});
app.MapPost("/checkin/{id}", async (int id, BookLibraryDBContext db) =>
{
    var bookCheckout = await db.BookCheckout.FindAsync(id);
    if (bookCheckout is null) return Results.NotFound();
    var book = await db.Books.FindAsync(id);
    if (book is null) return Results.NotFound();

    bookCheckout.IsReturned = true;
    book.IsAvailable = true;

    await db.SaveChangesAsync();

    return Results.NoContent();
});


app.MapDelete("/books/{id}", async (int id, BookLibraryDBContext db) =>
{
    if (await db.Books.FindAsync(id) is Book book)
    {
        if (book.IsAvailable)
        {
            db.Books.Remove(book);
            await db.SaveChangesAsync();
            return Results.NoContent();
        }
        return Results.Problem("Book Unavailable");
    }

    return Results.NotFound();
});


app.Run();


//Needed for the Program class to be seen by the Test project WebApplicationFactory.
public partial class Program { }