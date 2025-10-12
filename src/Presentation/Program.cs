using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Inyeccion de dependencias de servicios
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IWorkoutPlanService, WorkoutPlanService>();
builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddScoped<IAttendanceService, AttendanceService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IWorkoutClassService, WorkoutClassService>();

// Inyeccion de dependencias de repositorios
builder.Services.AddScoped<IRepositoryBase<User>, RepositoryBase<User>>();
builder.Services.AddScoped<IRepositoryBase<Employee>, RepositoryBase<Employee>>();
builder.Services.AddScoped<IRepositoryBase<Admin>, RepositoryBase<Admin>>();
builder.Services.AddScoped<IRepositoryBase<WorkoutPlan>, RepositoryBase<WorkoutPlan>>();
builder.Services.AddScoped<IRepositoryBase<Member>, RepositoryBase<Member>>();
builder.Services.AddScoped<IRepositoryBase<Attendance>, RepositoryBase<Attendance>>();
builder.Services.AddScoped<IRepositoryBase<Payment>, RepositoryBase<Payment>>();
builder.Services.AddScoped<IRepositoryBase<WorkoutClass>, RepositoryBase<WorkoutClass>>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IWorkoutPlanRepository, WorkoutPlanRepository>();
builder.Services.AddScoped<IAttendanceRepository, AttendanceRepository>();
builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddScoped<IWorkoutClassRepository, WorkoutClassRepository>();

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

app.Run();
