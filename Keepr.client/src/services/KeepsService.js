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
}

export const keepsService = new KeepsService()