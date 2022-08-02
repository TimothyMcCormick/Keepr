<template>
  <div
    data-bs-target="#keep-modal"
    data-bs-toggle="modal"
    class="
      m-3
      p-2
      img-fluid
      keeps-card
      bg-img
      rounded
      elevation-2
      d-flex
      align-items-end
      selectable
    "
    @click="setActive"
    :style="`background-image: url(${keep.img})`"
  >
    <h3 class="text-light">
      <b>{{ keep.name }}</b>
    </h3>
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
  background-size: cover;
  background-position: center;
}
.keeps-card {
  height: 20vh;
  width: 18rem;
}
</style>