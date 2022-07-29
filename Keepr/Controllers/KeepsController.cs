using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{

  [ApiController]
  [Route("api/[controller]")]
  public class KeepsController : ControllerBase
  {
    private readonly KeepsService _keepserv;

    public KeepsController(KeepsService keepserv)
    {
      _keepserv = keepserv;
    }

    [HttpGet]
    public ActionResult<List<Keep>> Get()
    {
      try
      {


        List<Keep> keeps = _keepserv.Get();
        return Ok(keeps);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<List<Keep>>> Create([FromBody] Keep keepData)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        keepData.CreatorId = userInfo.Id;
        keepData.Creator = userInfo;
        Keep newKeep = _keepserv.Create(keepData);
        return Ok(newKeep);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}")]
    public ActionResult<Keep> Get(int id)
    {
      try
      {
        Keep keep = _keepserv.Get(id);
        return Ok(keep);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }


  }
}