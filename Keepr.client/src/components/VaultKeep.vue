<template>
  <div
    class="mb-4 ms-2 me-2 rounded img-container elevation-4 selectable"
    @click="setActiveVaultKeep"
  >
    <img class="rounded" :src="keep.img" alt="" style="width: 100%" />
    <div class="bottom-left">
      <h3>
        <b>{{ keep.name }}</b>
      </h3>
    </div>
    <div class="bottom-right">
      <img
        :title="`View ${keep.creator?.name}'s Profile`"
        class="small-profile-img"
        :src="keep.creator?.picture"
        alt=""
        @click.stop="goToProfile"
      />
    </div>
  </div>
</template>


<script>
import { computed } from "@vue/reactivity"
import { keepsService } from "../services/KeepsService"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { AppState } from "../AppState"
import { useRouter } from "vue-router"
import { Modal } from "bootstrap"
import { vaultKeepsService } from "../services/VaultKeepsService"
export default {
  props: {
    keep: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    const router = useRouter()
    return {
      account: computed(() => AppState.account),
      async setActiveVaultKeep() {
        try {
          Modal.getOrCreateInstance(document.getElementById('vault-keep-modal')).show()
          await vaultKeepsService.setActiveVaultKeep(props.keep)
          // const viewCount = props.keep.views++
          // await keepsService.editKeepViews(props.keep.id, viewCount)
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      },
      goToProfile() {
        router.push({ name: "Profile", params: { id: props.keep.creatorId } })
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.bg-img {
  background-position: center;
  background-size: cover;
  width: 100%;
  height: 150px;
}
.small-profile-img {
  width: 35px;
  height: 35px;
  object-fit: cover;
  border-radius: 50em;
}
.img-container {
  position: relative;
  text-align: center;
  color: white;
}
.bottom-left {
  position: absolute;
  bottom: 4px;
  left: 16px;
  color: white;
}
.bottom-right {
  position: absolute;
  bottom: 12px;
  right: 16px;
}
h3 {
  text-shadow: 3px 2px #0000008e;
}
</style>