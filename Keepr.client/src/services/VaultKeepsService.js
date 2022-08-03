import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultKeepsService {
  async createVaultKeep(newVK) {
    const res = await api.post('api/vaultkeeps', newVK)
    logger.log(res.data)
  }
}


export const vaultKeepsService = new VaultKeepsService()