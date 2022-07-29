using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultsService
  {
    private readonly VaultsRepository _vaultrepo;

    public VaultsService(VaultsRepository vaultrepo)
    {
      _vaultrepo = vaultrepo;
    }
  }
}