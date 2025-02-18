﻿global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using System.Reflection;
global using Ordering.Domain.Enums;
global using Ordering.Application.Dtos;
global using BuildingBlocks.CQRS;
global using FluentValidation;
global using Microsoft.Extensions.Logging;
global using MediatR;
global using Ordering.Application.Data;
global using Ordering.Domain.ValueObjects;
global using BuildingBlocks.Exceptions;
global using Ordering.Application.Exceptions;
global using Microsoft.EntityFrameworkCore;
global using BuildingBlocks.Behaviors;
global using Ordering.Domain.Events;
global using Ordering.Application.Extensions;
global using BuildingBlocks.Pagination;
global using Ordering.Domain.Models;
global using BuildingBlocks.Messaging.Events;
global using MassTransit;
global using BuildingBlocks.Messaging.MassTransit;
global using Microsoft.FeatureManagement;
