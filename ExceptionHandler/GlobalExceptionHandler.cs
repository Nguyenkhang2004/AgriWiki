using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace AgriWiki_Project.ExceptionHandler
{
    public class GlobalExceptionHandler : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;
        private readonly ITempDataDictionaryFactory _tempDataFactory;

        public GlobalExceptionHandler(
            ILogger<GlobalExceptionHandler> logger,
            ITempDataDictionaryFactory tempDataFactory)
        {
            _logger = logger;
            _tempDataFactory = tempDataFactory;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi toàn cục");

                var tempData = _tempDataFactory.GetTempData(context);
                tempData["ErrorMessage"] = ex.Message;

                context.Response.OnStarting(() =>
                {
                    tempData.Save();
                    return Task.CompletedTask;
                });

                context.Response.Redirect(context.Request.Path);
            }
        }
    }
}