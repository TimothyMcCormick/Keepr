<template>
  <div class="container">
    <div class="col-md-12 d-flex justify-content-between">
      <div>
        <h1 class="mt-4">
          {{ activeVault.name }}
        </h1>
        <h5 class="ms-1">Keeps: {{ myKeeps.length }}</h5>
      </div>
      <div>
        <button class="btn btn-outline-danger m-4" @click="deleteVault">
          DELETE VAULT
        </button>
      </div>
    </div>
    <div></div>
    <div class="masonry-frame mt-3 ms-2">
      <div v-for="mk in myKeeps" :key="mk.id">
        <Keep :keep="mk" />
      </div>
    </div>
  </div>
</template>


<script>
import { computed, onMounted } from "@vue/runtime-core"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { vaultsService } from "../services/VaultsService"
import { useRoute, useRouter } from "vue-router"
import { AppState } from "../AppState"
export default {
  setup() {
    const route = useRoute()
    const router = useRouter()

    onMounted(async () => {
      try {
        await vaultsService.getVaultKeeps(route.params.id)
        await vaultsService.getVaultById(route.params.id)
      } catch (error) {
        logger.error(error)
        Pop.toast(error.message, 'error')
      }
    })
    return {

      myKeeps: computed(() => AppState.myVaultKeeps),
      activeVault: computed(() => AppState.activeVault),
      async deleteVault() {
        try {
          await vaultsService.deleteVault(route.params.id)
          router.push({ name: "Account" })
          Pop.toast('Vault Deleted', 'error')
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.masonry-frame {
  columns: 4;

  div {
    break-inside: avoid;
  }
}
</style>