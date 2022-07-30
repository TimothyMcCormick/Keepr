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
  public class VaultsController : ControllerBase
  {
    private readonly VaultsService _vaultserv;

    public VaultsController(VaultsService vaultserv)
    {
      _vaultserv = vaultserv;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Vault>> Create([FromBody] Vault vaultData)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        vaultData.CreatorId = userInfo.Id;
        vaultData.Creator = userInfo;
        Vault newVault = _vaultserv.Create(vaultData);
        return Ok(newVault);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}")]
    public ActionResult<Vault> Get(int id)
    {
      try
      {

        Vault vault = _vaultserv.Get(id);
        return Ok(vault);

      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Vault>> Edit([FromBody] Vault vaultData, int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        vaultData.CreatorId = userInfo.Id;
        vaultData.Id = id;
        Vault update = _vaultserv.Edit(vaultData);
        return Ok(update);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<Vault>> Delete(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        _vaultserv.Delete(id);
        return Ok("Deleted");
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}