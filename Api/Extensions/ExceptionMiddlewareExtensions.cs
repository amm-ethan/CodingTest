using Contracts.Services;
using Entities.ErrorModel;
using Entities.Exceptions;
using Entities.Exceptions.BadRequest;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using Shared.DataTransferObjects;
using System.Net;

namespace Api.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this WebApplication app, ILoggerManager logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        context.Response.StatusCode = contextFeature.Error switch
                        {
                            BadRequestException => StatusCodes.Status400BadRequest,
                            InvalidFileException => StatusCodes.Status400BadRequest,
                            InvalidOperationException => StatusCodes.Status400BadRequest,
                            CsvHelper.MissingFieldException => StatusCodes.Status400BadRequest,
                            _ => StatusCodes.Status500InternalServerError
                        };
                        logger.LogError($"Something went wrong: {contextFeature.Error}");

                        var objectToReturn = contextFeature.Error switch
                        {
                            TranscationValidationBadRequestException => new ErrorDetails()
                            {
                                StatusCode = context.Response.StatusCode,
                                Message = JsonConvert.DeserializeObject<TransactionErrorDto>(contextFeature.Error.Message)
                            },
                            InvalidOperationException => new ErrorDetails()
                            {
                                StatusCode = context.Response.StatusCode,
                                Message = "Input File is invalid or empty"
                            },
                            CsvHelper.MissingFieldException => new ErrorDetails()
                            {
                                StatusCode = context.Response.StatusCode,
                                Message = "Input File is invalid or empty"
                            },
                            _ => new ErrorDetails()
                            {
                                StatusCode = context.Response.StatusCode,
                                Message = contextFeature.Error.Message,
                            }
                        };
                        await context.Response.WriteAsync(objectToReturn.ToString());
                    }
                });
            });
        }
    }
}