global using Shared.DDD;
global using Shared.GenericRootModule;
global using Shared.GenericRootModule.Seed;
global using Shared.GenericRootModule.Interceptor;
global using Shared.CQRS;
global using Shared.GenericRootModule.Features;

global using Werhouse;
global using Werhouse.Data;
global using Werhouse.Data.Repositories;
global using Werhouse.Items;


global using System.Linq.Expressions;
global using System.Reflection;

global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Logging;
global using Microsoft.EntityFrameworkCore.Diagnostics;

global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Routing;

global using Mapster;
global using MediatR;
global using Carter;
global using FluentValidation;

global using Shared.Behaviors;
global using Shared.Exceptions;
global using Shared.Paginations;
global using Shared.Enums;
global using Microsoft.EntityFrameworkCore.Migrations;
global using Microsoft.EntityFrameworkCore.Infrastructure;
global using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
global using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
global using Microsoft.AspNetCore.Mvc;
global using Shared.Communicate;

public static class Constants
{
	public static readonly HttpController WerhouseController = new HttpController("http://localhost", 5000);

}