<template>
  <div
    class="mb-3 rounded img-container elevation-4 selectable"
    @click="setActive"
  >
    <img class="rounded" :src="keep.img" alt="" style="width: 100%" />
    <div class="bottom-left">
      <h3 class="shadow-1">
        <b>{{ keep.name }}</b>
      </h3>
    </div>
    <div class="bottom-right">
      <img
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
      async setActive() {
        try {
          Modal.getOrCreateInstance(document.getElementById('keep-modal')).show()
          await keepsService.setActiveKeep(props.keep)
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