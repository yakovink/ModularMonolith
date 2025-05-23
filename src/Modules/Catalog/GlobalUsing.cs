global using Shared.DDD;
global using Shared.GenericRootModule;
global using Shared.GenericRootModule.Seed;
global using Shared.GenericRootModule.Interceptor;
global using Shared.CQRS;
global using Shared.GenericRootModule.Features;

global using Catalog.Products.Models;
global using Catalog.Products.Events;
global using Catalog.Data;
global using Catalog.Data.Seed;
global using Catalog.Products.Dtos;


global using System.Linq.Expressions;
global using System.Reflection;

global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Logging;

global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Routing;

global using Mapster;
global using MediatR;
global using Carter;
global using FluentValidation;

global using Microsoft.EntityFrameworkCore.Diagnostics;
global using Shared.Behaviors;
global using Shared.Exceptions;
