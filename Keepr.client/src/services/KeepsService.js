import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class KeepsService {
  async GetKeeps() {
    const res = await api.get('api/keeps')
    AppState.keeps = res.data
  }

  async setActive(keep) {
    AppState.activeKeep = keep

  }

  async createKeep(keepData) {
    const res = await api.post('api/keeps', keepData)

    AppState.keeps.push(res.data)
  }

  async deleteKeep(keepId) {
    const res = await api.delete(`api/keeps/${keepId}`)
    logger.log(res.data)
    AppState.keeps.filter(k => k.id != keepId)
  }
}

export const keepsService = new KeepsService()