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
  public class VaultsController : ControllerBase
  {
    private readonly VaultsService _vaultserv;

    private readonly VaultKeepsService _vaultkeepserv;

    public VaultsController(VaultsService vaultserv, VaultKeepsService vaultkeepserv)
    {
      _vaultserv = vaultserv;
      _vaultkeepserv = vaultkeepserv;
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
    public async Task<ActionResult<Vault>> Get(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        Vault vault = _vaultserv.Get(id, userInfo?.Id);
        return Ok(vault);

      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    // [HttpGet("{id}")]
    // public async Task<ActionResult<Vault>> GetVaultById(int id)
    // {
    //   try
    //   {
    //     Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
    //     Vault vault = _vaultkeepserv.GetVaultById(id);
    //     return Ok(vault);

    //   }
    //   catch (System.Exception e)
    //   {
    //     return BadRequest(e.Message);
    //   }
    // }

    [HttpGet("{id}/keeps")]

    public async Task<ActionResult<List<Keep>>> GetKeeps(int id)
    {
      try
      {

        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();

        List<VaultKeepViewModel> keeps = _vaultkeepserv.Get(id, userInfo?.Id);
        return Ok(keeps);
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
        Vault update = _vaultserv.Edit(vaultData, userInfo.Id);
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
        _vaultserv.Delete(id, userInfo.Id);
        return Ok("Deleted");
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}