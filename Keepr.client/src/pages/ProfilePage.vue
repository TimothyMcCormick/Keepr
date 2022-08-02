<template>
  <div class="profile">
    <h1>This is the profile page</h1>
  </div>
</template>

<script>
import { onMounted } from "@vue/runtime-core"
import { vaultsService } from "../services/VaultsService"
import { profilesService } from "../services/ProfilesService"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { useRoute } from "vue-router"
export default {
  name: 'ProfilePage',
  setup() {
    // route = useRoute()
    onMounted(async () => {
      try {
        // await profilesService.GetProfile(route.params.id)
        // await vaultsService.GetProfileVaults()
      } catch (error) {
        logger.error(error)
        Pop.toast(error.message, 'error')
      }
      return {

      }
    })
  }
}
</script>
