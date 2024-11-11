using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Query.Wrapper;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TestODataExample.Model;

namespace TestODataExample.Controller;

[Route("odata/Products")]
public class ProductODataController(AppDbContext context) : ODataController
{
    [HttpGet("get1")]
    [EnableQuery]
    public IActionResult Get1()
    {
        return Ok(context.Products.AsQueryable());
    }
    
    [HttpGet("get2")]
    public async Task<IActionResult> Get2(ODataQueryOptions<Product> options)
    {
        var query = options.ApplyTo(context.Products.AsQueryable(), new ODataQuerySettings());

        var queryProduct = query as IQueryable<Product>;

        var results = await queryProduct.ToListAsync();
        return Ok(results);
    }
    
    [HttpGet("get3")]
    public async Task<IActionResult> Get3(ODataQueryOptions<Product> options)
    {
        var query = options.ApplyTo(context.Products.AsNoTracking().AsQueryable()).Cast<ISelectExpandWrapper>();
        var results = await query.Select(wrapper => wrapper.ToDictionary()).ToListAsync();
        var serialized = JsonConvert.SerializeObject(results);
        var deserialized = JsonConvert.DeserializeObject<List<Product>>(serialized);
        return Ok(deserialized);
    }
}