using System;
using System.Diagnostics;
using System.Web.Http.ExceptionHandling;

namespace TriggerFish.AddressParser.WebApi.ExceptionHandling
{
    //TODO: unit
    public class UnexpectedExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            Trace.TraceError(context.Exception.Message);

            //TODO: Log error, app insight or local file storage

            context.Result = new UnexpectedErrorResult
            {
                Request = context.Request
            };
        }
    }
}