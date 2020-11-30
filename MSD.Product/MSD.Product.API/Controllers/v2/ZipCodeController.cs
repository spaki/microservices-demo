﻿using Microsoft.AspNetCore.Mvc;
using MSD.Product.API.Controllers.Common;
using MSD.Product.API.Models;
using MSD.Product.Domain.Dtos.ZipCode;
using MSD.Product.Domain.Interfaces.Services;
using MSD.Product.Infra.Warning;
using System.Threading.Tasks;
using System.Web;

namespace MSD.Product.API.Controllers.V2
{
    [ApiVersion("2")]
    public class ZipCodeController : RootController
    {
        private readonly IZipCodeService zipCodeService;

        public ZipCodeController(
            WarningManagement warningManagement,
            IZipCodeService zipCodeService
        ) : base(warningManagement)
        {
            this.zipCodeService = zipCodeService;
        }

        /// <summary>
        /// Search for address info in the Brazillian Correios Post Service
        /// This Endpoint keep 20 seconds of Redis Cache
        /// (Internal communication it will be performed by WCF)
        /// </summary>
        /// <param name="zipCode">Zip Code, example: 80020000</param>
        /// <returns>Default API Response with address info</returns>
        [HttpGet("{zipCode}")]
        public async Task<ActionResult<ApiDefaultResponse<Address>>> GetByExternalIdAsync(string zipCode) => Response(await zipCodeService.GetAddressByZipCodeV2Async(HttpUtility.UrlDecode(zipCode)).ConfigureAwait(false));
    }
}
