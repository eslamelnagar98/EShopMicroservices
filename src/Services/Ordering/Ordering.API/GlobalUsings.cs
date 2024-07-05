﻿global using System.Reflection;
global using BuildingBlocks.Behaviors.LoggingPipeline;
global using BuildingBlocks.Exceptions.Handler;
global using BuildingBlocks.Extensions;
global using BuildingBlocks.Messaging.Extensions;
global using BuildingBlocks.Messaging.Options;
global using BuildingBlocks.Pagination;
global using Carter;
global using FluentValidation;
global using FluentValidation.AspNetCore;
global using HealthChecks.UI.Client;
global using Mapster;
global using MediatR;
global using Microsoft.AspNetCore.Diagnostics.HealthChecks;
global using Microsoft.Extensions.Options;
global using NLog;
global using NLog.Web;
global using Ordering.API.Extensions;
global using Ordering.Application.Dtos;
global using Ordering.Application.Orders.Command.CreateOrder;
global using Ordering.Application.Orders.Command.DeleteOrder;
global using Ordering.Application.Orders.Command.UpdateOrder;
global using Ordering.Application.Orders.Queries.GetOrders;
global using Ordering.Application.Orders.Queries.GetOrdersByCustomer;
global using Ordering.Application.Orders.Queries.GetOrdersByName;
global using Ordering.Application.Pagination;
global using Ordering.Infrastructure.Data;
global using Ordering.Infrastructure.Data.Extensions;
global using Ordering.Infrastructure.Options;
