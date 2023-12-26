using LPS.DocumentUploader.Application.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace LPS.DocumentUploader.Application.Helpers
{
    public class Requests
    {
        public static IActionResult Response(ControllerBase Controller,
            ApiStatus statusCode, object dataValue, string msg)
        {
            var e = new ApiStatus(500);

            var returnController = new
            {
                status = e.StatusCode,
                error = true,
                detail = "",
                message = e.StatusDescription,
                data = dataValue
            };

            if (statusCode.StatusCode != 200)
            {
                returnController = new
                {
                    status = statusCode.StatusCode,
                    error = true,
                    detail = msg,
                    message = statusCode.StatusDescription,
                    data = dataValue
                };
            }
            else
            {
                returnController = new
                {
                    status = statusCode.StatusCode,
                    error = false,
                    detail = msg,
                    message = statusCode.StatusDescription,
                    data = dataValue
                };
            }

            return Controller.StatusCode(statusCode.StatusCode, returnController);
        }
    }
}
