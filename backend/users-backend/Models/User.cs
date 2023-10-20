using users_backend;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;
namespace users_backend.Models
{
    public class User
    {
        public Guid Id { get; } = Guid.NewGuid();

        public required string UserId { get; set; }

        public required string UserName { get; set; }

        public string? Email { get; set; }

        public DateTime BirthDate { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public Gender? Gender { get; set; }

        public string? Phone { get; set; }

        public bool IsDeleted { get; set; }
    }

    public enum Gender
    {
        Male,
        Female
    }


public static class UserEndpoints
{
	public static void MapUserEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/User").WithTags(nameof(User));

        group.MapGet("/", async (UsersDbContext db) =>
        {
            return await db.Users.ToListAsync();
        })
        .WithName("GetAllUsers")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<User>, NotFound>> (Guid id, UsersDbContext db) =>
        {
            return await db.Users.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is User model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetUserById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (Guid id, User user, UsersDbContext db) =>
        {
            var affected = await db.Users
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.Id, user.Id)
                  .SetProperty(m => m.UserId, user.UserId)
                  .SetProperty(m => m.UserName, user.UserName)
                  .SetProperty(m => m.Email, user.Email)
                  .SetProperty(m => m.BirthDate, user.BirthDate)
                  .SetProperty(m => m.Gender, user.Gender)
                  .SetProperty(m => m.Phone, user.Phone)
                );

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateUser")
        .WithOpenApi();

        group.MapPost("/", async (User user, UsersDbContext db) =>
        {
            db.Users.Add(user);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/User/{user.Id}",user);
        })
        .WithName("CreateUser")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (Guid id, UsersDbContext db) =>
        {
            var affected = await db.Users
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteUser")
        .WithOpenApi();
    }
}}
