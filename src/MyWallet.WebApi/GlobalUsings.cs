global using Azure.Extensions.AspNetCore.Configuration.Secrets;
global using Azure.Identity;
global using Azure.Security.KeyVault.Secrets;
global using FluentValidation.AspNetCore;
global using Serilog;
global using Serilog.Events;
global using System.Text;
global using System.Text.Json.Serialization;
global using System.Net.Mime;
global using System.Security.Claims;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.Versioning;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.Extensions.Caching.Memory;
global using Microsoft.Extensions.Options;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.IdentityModel.Tokens;
global using Microsoft.OpenApi.Models;
global using MyWallet.Common;
global using MyWallet.Common.Dto;
global using MyWallet.Core.Dal;
global using MyWallet.Core.Models;
global using MyWallet.Resources;
global using MyWallet.Services;
global using MyWallet.Services.Interfaces;
global using MyWallet.WebApi.Configurations;
global using MyWallet.WebApi.Controllers.Base;
global using MyWallet.WebApi.Helpers;
global using MyWallet.WebApi.Models;
global using MyWallet.WebApi.Services;
