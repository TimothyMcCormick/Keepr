<template>
  <div class="row m-0 p-4">
    <div class="col-md-3 p-0 d-flex justify-content-center">
      <img class="rounded" :src="profile.picture" alt="" />
    </div>

    <div class="col-md-9 d-flex flex-column">
      <h1 class="name-font-size">{{ profile.name }}</h1>
      <h5>Vaults: {{ profileVaults.length }}</h5>
      <h5>Keeps: {{ profileKeeps.length }}</h5>
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
      <Vault v-for="pv in profileVaults" :key="pv.id" :vault="pv" />
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
      <Keep v-for="pk in profileKeeps" :key="pk.id" :keep="pk" />
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
import { profilesService } from "../services/ProfilesService"
export default {
  name: 'Profile',
  setup() {
    const route = useRoute()
    onMounted(async () => {
      try {
        await profilesService.getProfile(route.params.id)
        await profilesService.getProfileVaults(route.params.id)
        await profilesService.getProfileKeeps(route.params.id)
      } catch (error) {
        logger.error(error)
        Pop.toast(error.message, 'error')
      }
    })
    return {
      profile: computed(() => AppState.profile),
      profileVaults: computed(() => AppState.profileVaults),
      profileKeeps: computed(() => AppState.profileKeeps)
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
.name-font-size {
  font-size: 52px;
}
@media all and (max-width: 768px) {
  .masonry-frame {
    columns: 2;

    div {
      break-inside: avoid;
    }
  }
}
@media all and (max-width: 768px) {
  .name-font-size {
    font-size: 36px;
  }
}
</style>
