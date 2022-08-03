<template>
  <div
    data-bs-target="#keep-modal"
    data-bs-toggle="modal"
    class="m-3 p-2 bg-img rounded elevation-4 d-flex align-items-end selectable"
    @click="setActive"
    :style="`background-image: url(${keep.img})`"
  >
    <div class="row">
      <span>
        <h3 class="text-light">
          <b>{{ keep.name }}</b>
        </h3>
        <img class="small-profile-img" :src="keep.creator?.picture" alt="" />
      </span>
    </div>
  </div>
</template>


<script>
import { keepsService } from "../services/KeepsService"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
export default {
  props: {
    keep: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    return {
      async setActive() {
        try {
          await keepsService.setActive(props.keep)
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
  background-position: center;
  background-size: cover;
  width: 100%;
  height: 150px;
}
.small-profile-img {
  width: 30px;
  height: 30px;
  object-fit: cover;
  border-radius: 50em;
}
</style>