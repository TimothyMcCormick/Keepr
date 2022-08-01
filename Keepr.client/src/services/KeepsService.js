import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class KeepsService {
  async GetKeeps() {
    const res = await api.Get('api/keeps')
    logger.log(res.data)
  }
}

export const keepsService = new KeepsService()