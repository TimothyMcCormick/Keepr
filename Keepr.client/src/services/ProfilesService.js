import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class ProfilesService {
  // async GetProfile(profileId) {
  //   const res = await api.get(`api/profiles/${profileId}`)
  //   logger.log(res.data)
  // }
}


export const profilesService = new ProfilesService()