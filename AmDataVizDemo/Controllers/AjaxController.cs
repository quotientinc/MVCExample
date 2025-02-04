﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using System.Collections.Specialized;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;

using AmDataVizDemo.Models;
using AmDataVizDemo.Models.ViewModels;
using AmDataVizDemo.Services;
using Newtonsoft.Json;


namespace AMIPWeb.Controllers
{
    public class AjaxController : Controller
    {
        private DataService dataService;
        private ForecastService forecastService;

        public AjaxController()
        {
            dataService = new DataService();
            forecastService = new ForecastService();
        }

        // GET: /Ajax/SupplierData
        public async Task<IActionResult> SupplierData(int? Start, int? Rows)
        {
            int start = 0;
            int rows = 10;

            if (Start != null)
            {
                start = (int)Start;
            }
            if (Rows != null)
            {
                rows = (int)Rows;
            }

            string UserName = User.Identity.Name;
            string IpAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();

            List<SupplierDatum> supplierData = await dataService.GetSupplierData(start, rows);
            return Json(supplierData);
        }

        // GET: /Ajax/ForecastArima
        public async Task<IActionResult> ForecastArima()
        {
            string UserName = User.Identity.Name;
            string IpAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();

            TimeSeriesPrediction timeSeries = await forecastService.PerformArima();
            return Json(timeSeries);
        }


        // GET: /Ajax/JobTestDataTotals
        public async Task<IActionResult> JobTestDataTotals()
        {
           return Json(dataService.GetJobTestDataTotals());
        }

        // GET: /Ajax/JobTestData
        public async Task<IActionResult> JobTestData()
        {
            return Json(dataService.GetJobTestData());
        }

        // GET: /Ajax/ProductionData
        public async Task<IActionResult> ProductionData()
        {
            return Json(dataService.GetProductionData());
        }
    } // controller

} // namespace
