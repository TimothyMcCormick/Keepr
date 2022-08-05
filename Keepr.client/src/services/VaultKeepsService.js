import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultKeepsService {

  async setActiveVaultKeep(keep) {
    AppState.activeVaultKeep = keep

    logger.log(keep)
  }
  async createVaultKeep(newVK) {
    const res = await api.post('api/vaultkeeps', newVK)
    // logger.log(res.data)
    AppState.myVaultKeeps.push(res.data)
  }

  async deleteVaultKeep(keepId) {
    const res = await api.delete(`api/vaultkeeps/${keepId}`)
    // logger.log(res.data)
    AppState.myVaultKeeps = AppState.myVaultKeeps.filter(mvk => mvk.id != keepId)
  }
}


export const vaultKeepsService = new VaultKeepsService()