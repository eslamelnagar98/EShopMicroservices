﻿global using System.Linq.Expressions;
global using System.Reflection;
global using Basket.API.Data;
global using Basket.API.Data.BasketData;
global using Basket.API.Data.BasketDbData;
global using Basket.API.Data.Interceptors;
global using Basket.API.Dtos.BasketCheckout;
global using Basket.API.Exceptions;
global using Basket.API.Extensions;
global using Basket.API.Models;
global using Basket.API.Options;
global using Basket.API.Options.Validations;
global using BuildingBlocks.Behaviors.LoggingPipeline;
global using BuildingBlocks.CQRS;
global using BuildingBlocks.Exceptions;
global using BuildingBlocks.Exceptions.Handler;
global using BuildingBlocks.Extensions;
global using BuildingBlocks.Message;
global using BuildingBlocks.Messaging.Events.BasketCheckout;
global using BuildingBlocks.Messaging.Extensions;
global using BuildingBlocks.Messaging.Options;
global using Carter;
global using Discount.gRPC;
global using FluentValidation;
global using HealthChecks.UI.Client;
global using Mapster;
global using Marten;
global using Marten.Schema;
global using Marten.Services;
global using MassTransit;
global using MediatR;
global using Microsoft.AspNetCore.Diagnostics.HealthChecks;
global using Microsoft.Extensions.Caching.Distributed;
global using Microsoft.Extensions.Options;
global using Newtonsoft.Json;
global using NLog;
global using NLog.Web;
