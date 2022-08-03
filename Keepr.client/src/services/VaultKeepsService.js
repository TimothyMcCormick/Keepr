const { logger } = require("../utils/Logger")
const { api } = require("./AxiosService")

class VaultKeepsService {
  async getByVaultId(id) {
    const res = await api.get(`api/vaults/${id}/keeps`)
    logger.log(res.data)
  }
}


const vaultKeepsService = new VaultKeepsService()