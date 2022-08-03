<template>
  <Modal id="keep-modal">
    <template #modal-body>
      <div class="container">
        <div class="row">
          <div class="col-md-6 order-2">
            <img
              class="img-fluid keep-img rounded"
              :src="keep.img"
              alt="https://thiscatdoesnotexist.com"
            />
          </div>
          <div class="col-md-6 order-1 order-md-2">
            <div class="ms-4 d-flex flex-column justify-content-center">
              <div class="text-end">
                <button
                  v-if="keep.creatorId == account.id"
                  type="button"
                  class="btn-close p-2"
                  data-bs-dismiss="modal"
                  aria-label="Close"
                ></button>
              </div>
              <div class="row">
                <span class="d-flex align-items-center justify-content-center">
                  <i class="mdi mdi-eye f-18 text-primary"> </i>
                  <p>{{ keep.views }}</p>

                  <i class="mdi mdi-alpha-k-box-outline f-18 text-primary"> </i>
                  <p>{{ keep.kept }}</p>
                </span>
              </div>
              <!-- <a class="close text-end" aria-label="Close">
                <span aria-hidden="true"
                  ><i
                    data-dismiss="modal"
                    class="selectable mdi mdi-close f-18"
                  ></i
                ></span>
              </a> -->
              <div class="row description">
                <h2 class="p-2 text-center">Keep {{ keep.name }}</h2>

                <p>
                  {{ keep.description }}
                </p>
              </div>
            </div>
            <div class="row">
              <span class="d-flex align-items-center justify-content-between">
                <select
                  class="selectable"
                  name="vaults"
                  id="vaults"
                  @change="setActiveVault"
                  v-model="activeVaultId"
                >
                  <option value="select">ADD TO VAULT</option>
                  <option v-for="v in vaults" :key="v.id" :value="v.id">
                    {{ v.name }}
                  </option>
                </select>
                <i
                  class="button mdi mdi-delete f-24 selectable"
                  @click="deleteKeep()"
                ></i>

                <div class="rounded p-2">
                  <div>
                    <span class="mx-3 text-dark lighten-30">
                      <b>{{ keep.creator?.name }}</b></span
                    >
                    <img
                      :src="account.picture"
                      alt="account photo"
                      height="40"
                      class="rounded"
                    />
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
import { useRoute } from "vue-router"
import { Modal } from "bootstrap"
export default {

  setup() {
    const activeVaultId = ref(0)
    return {
      activeVaultId,
      async setActiveVault() {
        try {
          await vaultKeepsService.getByVaultId(activeVaultId.value)
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      },
      async deleteKeep() {
        try {
          Modal.getOrCreateInstance(document.getElementById('keep-modal')).hide()
          await keepsService.deleteKeep(this.keep.id)
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      },
      keep: computed(() => AppState.activeKeep),
      account: computed(() => AppState.account),
      vaults: computed(() => AppState.myVaults)
    }
  }
}
</script>


<style lang="scss" scoped>
.keep-img {
  width: 100%;
  object-fit: cover;
}
select {
  margin-bottom: 10px;
  margin-top: 10px;
  font-family: cursive, sans-serif;
  outline: 3px;
  background: #ffffff;
  color: #34ffeb;
  border: 3px solid #34ffeb;
  padding: 4px;
  border-radius: 9px;
}
</style>