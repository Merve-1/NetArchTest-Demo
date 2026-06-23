using System.Collections.Immutable;
using ArcTestsData.Entities;
using ArcTestsData.Interfaces;
using ArcTestsData.Repositories;
using ArcTestsServices.Interfaces;
using ArcTestsServices.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
