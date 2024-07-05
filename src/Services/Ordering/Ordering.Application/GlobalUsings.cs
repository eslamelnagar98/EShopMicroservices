﻿global using System.Diagnostics.CodeAnalysis;
global using System.Runtime.CompilerServices;
global using BuildingBlocks.CQRS;
global using BuildingBlocks.Exceptions;
global using BuildingBlocks.Messaging.Events.BasketCheckout;
global using BuildingBlocks.Pagination;
global using FluentValidation;
global using MassTransit;
global using MediatR;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Logging;
global using Ordering.Application.Data;
global using Ordering.Application.Dtos;
global using Ordering.Application.Exceptions;
global using Ordering.Application.Extensions;
global using Ordering.Application.Orders.Command.CreateOrder;
global using Ordering.Application.Pagination;
global using Ordering.Domain.Enums;
global using Ordering.Domain.Events;
global using Ordering.Domain.Models;
global using Ordering.Domain.ValueObjects;
