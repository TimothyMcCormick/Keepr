<template>
  <div class="container">
    <div class="col-md-12 d-flex justify-content-between">
      <div>
        <h1 class="mt-4">
          {{ activeVault.name }}
        </h1>
        <h5 class="ms-1">Keeps: {{ myKeeps.length }}</h5>
      </div>
      <div v-if="activeVault.creatorId == account.id">
        <button class="btn btn-outline-danger m-4" @click="deleteVault">
          DELETE VAULT
        </button>
      </div>
    </div>
    <div></div>
    <div class="masonry-frame mt-3 ms-2">
      <div v-for="mk in myKeeps" :key="mk.id">
        <VaultKeep :keep="mk" />
      </div>
    </div>
  </div>
</template>


<script>
import { computed, onMounted, watch, watchEffect } from "@vue/runtime-core"
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

        await vaultsService.getVaultById(route.params.id)
        await vaultsService.getVaultKeeps(route.params.id)

      } catch (error) {
        router.push({ name: 'Home' })
        logger.error(error)
        Pop.toast('Private Vault', 'error')
      }
    })
    return {
      account: computed(() => AppState.account),
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
@media all and (max-width: 768px) {
  .masonry-frame {
    columns: 2;

    div {
      break-inside: avoid;
    }
  }
}
</style>