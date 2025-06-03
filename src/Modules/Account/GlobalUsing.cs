
global using Shared.DDD;
global using Shared.GenericRootModule;
global using Shared.GenericRootModule.Seed;
global using Shared.GenericRootModule.Interceptor;
global using Shared.CQRS;
global using Shared.GenericRootModule.Features;


global using System.ComponentModel.DataAnnotations;
global using System.Net.Mail;
global using System.Linq.Expressions;
global using System.Reflection;

global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Logging;
global using Microsoft.EntityFrameworkCore.Diagnostics;
global using Microsoft.AspNetCore.Mvc;
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

global using Account.Data;
global using Account.Users.Models;
global using Account.Users.Events;
global using Account.Users.Dtos;
global using Shared.Communicate;
global using System.Net;
global using System.Security.Cryptography;
global using System.Text;
global using System.Text.Json;

public static class Constants
{
	public static readonly HttpController AccountController = new HttpController("http://localhost", 5000);

}

