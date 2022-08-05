<template>
  <div
    title="View Vault"
    class="
      m-3
      p-2
      img-fluid
      vaults-card
      bg-img
      rounded
      elevation-4
      d-flex
      align-items-end
      selectable
    "
    @click="goToVault"
    :style="`background-image: url(${vault.img})`"
  >
    <h3 class="text-light">
      <b>{{ vault.name }}</b>
    </h3>
  </div>
</template>


<script>
import { computed } from "@vue/reactivity"
import { useRouter } from "vue-router"
import { AppState } from "../AppState"
import { vaultsService } from "../services/VaultsService"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
export default {
  props: {
    vault: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    const router = useRouter()
    return {
      account: computed(() => AppState.account),
      async goToVault() {
        try {

          router.push({ name: "Vault", params: { id: props.vault.id } })

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
.bg-img {
  background-size: cover;
  background-position: center;
}
.vaults-card {
  height: 20vh;
  width: 18rem;
}
h3 {
  text-shadow: 3px 2px #0000008e;
}
</style>