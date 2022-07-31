using System.Collections.Generic;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{

  [ApiController]
  [Route("api/[controller]")]
  public class ProfilesController : ControllerBase
  {
    private readonly ProfilesService _profserv;
    private readonly VaultsService _vaultserv;
    private readonly KeepsService _keepserv;

    public ProfilesController(ProfilesService profserv, VaultsService vaultserv, KeepsService keepserv)
    {
      _profserv = profserv;
      _vaultserv = vaultserv;
      _keepserv = keepserv;
    }

    [HttpGet("{id}")]
    public ActionResult<Profile> GetById(string id)
    {
      try
      {

        Profile profile = _profserv.GetProfileById(id);
        return Ok(profile);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}/keeps")]
    public ActionResult<List<Keep>> GetKeeps(string id)
    {
      try
      {
        // Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        // userInfo.Id = id;
        List<Keep> keeps = _keepserv.GetKeepsByCreatorId(id);
        return Ok(keeps);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}/vaults")]
    public ActionResult<List<Keep>> GetVaults(string id)
    {
      try
      {
        // Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        // userInfo.Id = id;
        List<Vault> vaults = _vaultserv.GetVaultsByCreatorId(id);
        return Ok(vaults);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}