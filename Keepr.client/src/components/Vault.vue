<template>
  <div
    class="
      m-3
      p-2
      img-fluid
      vaults-card
      bg-img
      rounded
      elevation-2
      d-flex
      align-items-end
      selectable
    "
    @click="goToVaults"
    :style="`background-image: url(${vault.img})`"
  >
    <h3 class="text-light">
      <b>{{ vault.name }}</b>
    </h3>
  </div>
</template>


<script>
export default {
  props: {
    vault: {
      type: Object,
      required: true
    }
  },
  setup() {
    return {}
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
</style>