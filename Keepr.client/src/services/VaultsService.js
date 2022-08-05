
import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultsService {
  async createVault(vaultData) {
    const res = await api.post('api/vaults', vaultData)

    AppState.vaults.push(res.data)
  }

  async getVaultKeeps(vaultId) {
    const res = await api.get(`api/vaults/${vaultId}/keeps`)
    logger.log(res.data)
    AppState.myVaultKeeps = res.data
  }

  // async goToVault(vaultId) {
  //   const res = await api.get(`api/vaults/${vaultId}/keeps`)
  //   logger.log(res.data)
  //   AppState.vaultKeeps = res.data
  // }

  async getVaultById(vaultId) {

    const res = await api.get(`api/vaults/${vaultId}`)
    // logger.log(res.data)
    AppState.activeVault = res.data

  }

  async deleteVault(vaultId) {
    const res = await api.delete(`api/vaults/${vaultId}`)
    // logger.log(res.data)
    AppState.vaults = AppState.vaults.filter(v => v.id != vaultId)

  }

}


export const vaultsService = new VaultsService()