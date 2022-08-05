<template>
  <div
    class="modal fade"
    id="edit-account-modal"
    tabindex="-1"
    role="dialog"
    aria-labelledby="modelTitleId"
    aria-hidden="true"
  >
    <div class="modal-dialog modal-dialog-centered" role="document">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title">Edit Account</h5>
          <button
            type="button"
            class="btn-close"
            data-bs-dismiss="modal"
            aria-label="Close"
          ></button>
        </div>
        <div class="modal-body">
          <form type="submit" action="">
            <div class="col-md-12">
              <label for="Title">Name</label>
              <input class="form-control" type="text" v-model="editable.name" />
            </div>
            <div class="col-md-12">
              <label for="Img Url">Picture</label>
              <input
                class="form-control"
                type="text"
                v-model="editable.picture"
              />
            </div>

            <div class="col-md-12 d-flex justify-content-end">
              <button
                type="button"
                class="m-2 btn btn-primary"
                @click="editAccount"
              >
                Save
              </button>
            </div>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { ref } from "@vue/reactivity"
import { watchEffect } from "@vue/runtime-core"
import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { accountService } from "../services/AccountService"
import { Modal } from "bootstrap"
export default {
  setup() {
    const editable = ref({})
    watchEffect(() => {
      AppState.account
      editable.value = { ...AppState.account };
    })
    return {
      editable,
      async editAccount() {
        try {
          Modal.getOrCreateInstance(document.getElementById('edit-account-modal')).hide()
          await accountService.editAccount(editable.value)
          Pop.toast('Account Updated!')
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
</style>