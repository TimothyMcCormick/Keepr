<template>
  <!-- Button trigger modal -->

  <!-- Modal -->
  <div
    class="modal fade"
    id="create-keep-modal"
    tabindex="-1"
    role="dialog"
    aria-labelledby="modelTitleId"
    aria-hidden="true"
  >
    <div class="modal-dialog modal-dialog-centered" role="document">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title">New Keep</h5>
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
              <label for="Title">Title</label>
              <input class="form-control" type="text" v-model="editable.name" />
            </div>
            <div class="col-md-12">
              <label for="Img Url">Img Url</label>
              <input class="form-control" type="text" v-model="editable.img" />
            </div>
            <div class="col-md-12">
              <label for="Description">Description</label>
              <textarea
                class="form-control"
                name="description"
                id="description"
                cols="30"
                rows="10"
                v-model="editable.description"
              ></textarea>
            </div>
            <div class="col-md-12 d-flex justify-content-end">
              <button
                type="button"
                class="m-2 btn btn-primary"
                @click="createKeep"
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
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { keepsService } from "../services/KeepsService"
import { Modal } from "bootstrap"

export default {
  setup() {
    const editable = ref({})
    return {
      editable,
      async createKeep() {
        try {
          Modal.getOrCreateInstance(document.getElementById('create-keep-modal')).hide()
          await keepsService.createKeep(editable.value)
          Pop.toast('Created Keep!', 'success')
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