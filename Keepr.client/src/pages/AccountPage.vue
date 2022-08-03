<template>
  <div class="row m-0">
    <div class="col-md-3 d-flex justify-content-center">
      <img class="rounded" :src="account.picture" alt="" />
    </div>
    <div class="col-md-9 d-flex flex-column justify-content-center">
      <h1>{{}}</h1>
      <p>{{ account.email }}</p>
    </div>
  </div>
  <div class="container">
    <h1 class="p-4">
      Vaults
      <i
        class="mdi mdi-plus text-primary selectable"
        data-bs-toggle="modal"
        data-bs-target="#create-vault-modal"
      ></i>
    </h1>
    <div class="row">
      <Vault v-for="v in myVaults" :key="v.id" :vault="v" />
    </div>
  </div>
  <div class="container">
    <h1 class="p-4">
      Keeps
      <i
        class="mdi mdi-plus text-primary selectable"
        data-bs-toggle="modal"
        data-bs-target="#create-keep-modal"
      ></i>
    </h1>
    <div class="masonry-frame">
      <Keep v-for="k in keeps" :key="k.id" :keep="k" />
    </div>
  </div>
</template>

<script>
import { computed, onMounted, watch, watchEffect } from 'vue'
import { AppState } from '../AppState'
import Pop from "../utils/Pop"
import { logger } from "../utils/Logger"
import { accountService } from "../services/AccountService"
import { useRoute } from "vue-router"
export default {
  name: 'Account',
  setup() {
    // route = useRoute()
    watchEffect(async () => {
      try {
        await accountService.getMyVaults()
        await accountService.getMyKeeps()
      } catch (error) {
        logger.error(error)
        Pop.toast(error.message, 'error')
      }
    })
    return {
      account: computed(() => AppState.account),
      myVaults: computed(() => AppState.myVaults),
      keeps: computed(() => AppState.myKeeps)
    }
  }
}
</script>

<style lang="scss" scoped>
img {
  width: 80%;
}
.masonry-frame {
  columns: 4;
  div {
    break-inside: avoid;
  }
}
</style>
