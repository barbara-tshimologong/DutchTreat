﻿using DutchTreat.Data;
using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace DutchTreat.Controllers
{
    [Route("api/[Controller")]
    [ApiController]
    [Produces("application/json")]
    public class ProductsController : ControllerBase
    {
        private readonly IDutchRepository _repository;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IDutchRepository repository, ILogger<ProductsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(200)]

        public ActionResult <IEnumerable<Product>> Get()
        {
            try
            {
                return Ok(_repository.GetAllProducts());

            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Failed to get products: {ex}");
                return BadRequest($"Failed to get products: {ex}");
            }
        }
    }
}
