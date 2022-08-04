<template>
  <header>
    <Navbar />
  </header>
  <main>
    <router-view />
    <KeepModal />
    <CreateKeepModal />
    <CreateVaultModal />
    <VaultKeepModal />
    <EditAccountModal />
  </main>
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
export default {
  name: 'App',
  setup() {
    return {
      appState: computed(() => AppState)
    }
  }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";
</style>
