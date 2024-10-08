using Microsoft.EntityFrameworkCore;
using VivesBlog.Core;
using VivesBlog.Services;
using VivesBlog.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<VivesBlogDbContext>(options =>
{
	options.UseInMemoryDatabase(nameof(VivesBlogDbContext));
});


builder.Services.AddScoped<IArticleService ,ArticleService>();
builder.Services.AddScoped<IPersonService ,PersonService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();

	using var scope = app.Services.CreateScope();
	var vivesBlogDbContext = scope.ServiceProvider.GetRequiredService<VivesBlogDbContext>();
	if (vivesBlogDbContext.Database.IsInMemory())
	{
		vivesBlogDbContext.Seed();
	}
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
