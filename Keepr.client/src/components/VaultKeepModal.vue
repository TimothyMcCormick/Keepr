<template>
  <Modal id="vault-keep-modal">
    <template #modal-body>
      <div class="container">
        <div class="row">
          <div class="col-md-6 order-1">
            <img
              class="img-fluid keep-img rounded"
              :src="keep.img"
              alt="https://thiscatdoesnotexist.com"
            />
          </div>
          <div class="col-md-6 order-2 order-md-1">
            <div class="ms-4 d-flex flex-column justify-content-center">
              <div class="text-end">
                <button
                  title="Close Modal"
                  type="button"
                  class="btn-close p-2"
                  data-bs-dismiss="modal"
                  aria-label="Close"
                ></button>
              </div>
              <div class="row justify-content-center hide-view-kept">
                <div class="col-md-6">
                  <div class="row justify-content-center">
                    <div class="col-md-2 p-0 d-flex justify-content-end">
                      <i class="mdi mdi-eye f-22 text-primary"></i>
                    </div>
                    <div
                      class="
                        col-md-2
                        d-flex
                        justify-content-center
                        align-items-center
                        p-0
                      "
                    >
                      <p class="text-dark m-0">{{ keep.views }}</p>
                    </div>
                    <div class="col-md-2 p-0 d-flex justify-content-end">
                      <i
                        class="mdi mdi-alpha-k-box-outline f-22 text-primary"
                      ></i>
                    </div>
                    <div
                      class="
                        col-md-2
                        d-flex
                        justify-content-center
                        align-items-center
                        p-0
                      "
                    >
                      <p class="text-dark m-0">{{ keep.kept }}</p>
                    </div>
                  </div>
                </div>
              </div>
              <!-- <a class="close text-end" aria-label="Close">
                <span aria-hidden="true"
                  ><i
                    data-dismiss="modal"
                    class="selectable mdi mdi-close f-18"
                  ></i
                ></span>
              </a> -->
              <h2 class="p-2 text-center">Keep {{ keep.name }}</h2>
              <div class="row description scrollable-y">
                <p>
                  {{ keep.description }}
                </p>
              </div>
            </div>
            <div class="row">
              <span class="d-flex align-items-center justify-content-between">
                <div class="d-flex align-items-center">
                  <button
                    class="remove-button"
                    @click="deleteVaultKeep"
                    v-if="keep.creatorId == account.id"
                  >
                    REMOVE FROM VAULT
                  </button>
                </div>

                <div class="rounded p-2 d-flex align-items-center">
                  <div
                    :title="`View ${keep.creator?.name}'s Profile`"
                    class="selectable"
                    @click="goToProfile"
                  >
                    <img
                      :src="account.picture"
                      alt="account photo"
                      height="40"
                      class="rounded"
                    />
                    <span class="mx-3 text-dark lighten-30 hide-name">
                      <b>{{ keep.creator?.name }}</b></span
                    >
                  </div>
                </div>
              </span>
            </div>
          </div>
        </div>
      </div>
    </template>
  </Modal>
</template>


<script>
import { computed, ref } from "@vue/reactivity"
import { AppState } from "../AppState"
import Pop from "../utils/Pop"
import { logger } from "../utils/Logger"
import { keepsService } from "../services/KeepsService"
import { vaultKeepsService } from "../services/VaultKeepsService"
import { useRoute, useRouter } from "vue-router"
import { Modal } from "bootstrap"
export default {

  setup() {
    const router = useRouter()
    const vaultId = ref(0)
    return {
      vaultId,
      vaults: computed(() => AppState.myVaults),
      async createVaultKeep() {
        try {
          const newVaultKeep = {
            vaultId: vaultId.value,
            keepId: this.keep.id
          }
          Modal.getOrCreateInstance(document.getElementById('vault-keep-modal')).hide()
          await vaultKeepsService.createVaultKeep(newVaultKeep)
          router.push({ name: "Vault", params: { id: vaultId.value } })
          Pop.toast('Added to Vault!', 'success')
          vaultId.value = 0
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      },
      async deleteVaultKeep() {
        try {
          if (await Pop.confirm()) {
            Modal.getOrCreateInstance(document.getElementById('vault-keep-modal')).hide()
            await vaultKeepsService.deleteVaultKeep(this.keep.vaultKeepId)
            Pop.toast('Deleted From Vault', 'error')
          }
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      },
      goToProfile() {
        Modal.getOrCreateInstance(document.getElementById('vault-keep-modal')).hide()
        router.push({ name: "Profile", params: { id: this.keep.creatorId } })
      },
      keep: computed(() => AppState.activeVaultKeep),
      account: computed(() => AppState.account),

    }
  }
}
</script>


<style lang="scss" scoped>
.keep-img {
  width: 100%;
  object-fit: cover;
  height: 60vh;
}
.description {
  height: 35vh;
}
.top-row {
  height: 5vh;
}
.remove-button {
  margin-bottom: 10px;
  margin-top: 10px;
  font-family: cursive, sans-serif;
  outline: 3px;
  background: #ffffff;
  color: #fd0000;
  border: 3px solid #fd0000;
  padding: 4px;
  border-radius: 9px;
}
@media all and (max-width: 1200px) {
  .hide-name {
    display: none;
  }
}
@media all and (max-width: 1000px) {
  .hide-view-kept {
    display: none;
  }
}

p {
  font-family: Verdana, Geneva, Tahoma, sans-serif;
  font-size: 20px;
  color: rgb(37, 37, 37);
}
</style>