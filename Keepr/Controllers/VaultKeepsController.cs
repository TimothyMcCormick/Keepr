using Keepr.Services;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{

  [ApiController]
  [Route("api/[controller]")]
  public class VaultKeepsController
  {


    private readonly VaultKeepsService _vaultkeepserv;

    public VaultKeepsController(VaultKeepsService vaultkeepserv)
    {
      _vaultkeepserv = vaultkeepserv;
    }
  }
}