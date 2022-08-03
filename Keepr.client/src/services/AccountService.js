import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class AccountService {
  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = res.data
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }
  async getMyVaults() {
    const res = await api.get('/account/vaults')
    logger.log(res.data)
    AppState.myVaults = res.data
  }
  async getMyKeeps() {
    const res = await api.get('api/keeps')
    logger.log(res.data)
    res.data.filter(k => k.creatorId != AppState.account.id)
    AppState.myKeeps = res.data
  }
}

export const accountService = new AccountService()
