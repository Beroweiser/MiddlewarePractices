var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//app.Run();
//app.Run(async context => Console.WriteLine("Middleware 1."));
//app.Run(async context => Console.WriteLine("Middleware 2."));

// app.Use();
//app.Use(async (context, next) =>
//{
//    Console.WriteLine("Middleware 1 baþladý");
//    await next.Invoke();
//    Console.WriteLine("Middleware 1 sonlandýrýlýyor..");
//});
//app.Use(async (context, next) =>
//{
//    Console.WriteLine("Middleware 2 basladý")
//    ;
//    await next.Invoke();
//    Console.WriteLine("Middleware 2 sonlandýrýlýyor..");
//});
//app.Use(async (context, next) =>
//{
//    Console.WriteLine("Middleware 3 basladý")
//    ;
//    await next.Invoke();
//    Console.WriteLine("Middleware 3 sonlandýrýlýyor..");
//});

app.Use(async (context, next) =>
{
    Console.WriteLine("Use Middleware tetiklendi");
    await next.Invoke();
    
});

// app.Map();
app.Map("/example", internalApp => internalApp.Run(async context =>
{
    Console.WriteLine("/example middleware tetiklendi");
    await context.Response.WriteAsync("/example middleware tetiklendi.");
}));
// app.MapWhen();
app.MapWhen(x => x.Request.Method == "Get" , internalApp =>
{
    internalApp.Run(async context =>
    {
        
        Console.WriteLine("Middleware tetiklendi");
        await context.Response.WriteAsync("MapWhen Middleware tetiklendi");

    });
});

app.Run();

