<template>
  <div class="about text-center">
    <h1>Welcome {{ account.name }}</h1>
    <img class="rounded" :src="account.picture" alt="" />
    <p>{{ account.email }}</p>
  </div>
  <div>
    <Vault v-for="v in myVaults" :key="v.id" :vault="v" />
  </div>
</template>

<script>
import { computed, onMounted } from 'vue'
import { AppState } from '../AppState'
import Pop from "../utils/Pop"
import { logger } from "../utils/Logger"
import { accountService } from "../services/AccountService"
import { useRoute } from "vue-router"
export default {
  name: 'Account',
  setup() {
    // route = useRoute()
    onMounted(async () => {
      try {
        await accountService.getMyVaults()
      } catch (error) {
        logger.error(error)
        Pop.toast(error.message, 'error')
      }
    })
    return {
      account: computed(() => AppState.account),
      myVaults: computed(() => AppState.myVaults)
    }
  }
}
</script>

<style scoped>
img {
  max-width: 100px;
}
</style>
