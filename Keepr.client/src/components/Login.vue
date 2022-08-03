<template>
  <span class="navbar-text">
    <button
      class="btn selectable text-success lighten-30 text-uppercase my-2 my-lg-0"
      @click="login"
      v-if="!user.isAuthenticated"
    >
      Login
    </button>
    <router-link
      :to="{ name: 'Account' }"
      class="btn text-success lighten-30 selectable"
    >
      <div class="bg-login rounded">
        <div v-if="account.picture">
          <img
            :src="account.picture"
            alt="account photo"
            height="40"
            class="rounded"
          />
          <span class="mx-3 text-white lighten-30">{{ account.name }}</span>
        </div>
      </div>
    </router-link>
  </span>
</template>


<script>
import { computed } from "@vue/reactivity";
import { AppState } from "../AppState";
import { AuthService } from "../services/AuthService";
export default {
  setup() {
    return {
      user: computed(() => AppState.user),
      account: computed(() => AppState.account),
      async login() {
        AuthService.loginWithPopup();
      },
      async logout() {
        AuthService.logout({ returnTo: window.location.origin });
      },
    };
  },
};
</script>


<style lang="scss" scoped>
.dropdown-menu {
  user-select: none;
  display: block;
  transform: scale(0);
  transition: all 0.15s linear;
}
.dropdown-menu.show {
  transform: scale(1);
}
.hoverable {
  cursor: pointer;
}
.bg-login {
  background-color: rgb(96, 96, 96);
}
</style>
