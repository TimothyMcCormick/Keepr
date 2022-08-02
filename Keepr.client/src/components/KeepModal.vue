<template>
  <Modal id="keep-modal">
    <template #modal-body>
      <div class="container">
        <div class="row">
          <div class="col-md-6">
            <img
              class="img-fluid rounded"
              :src="keep.img"
              alt="https://thiscatdoesnotexist.com"
            />
          </div>
          <div class="col-md-6">
            <div class="ms-4 d-flex flex-column justify-content-center">
              <div class="text-end">
                <button
                  type="button"
                  class="btn-close"
                  data-bs-dismiss="modal"
                  aria-label="Close"
                ></button>
              </div>
              <div class="row">
                <span>
                  <div class="d-flex justify-content-around me-2 ms-2">
                    <i class="mdi mdi-eye f-18"> {{ keep.views }} </i>
                    <i class="mdi mdi-alpha-k-box-outline f-18">
                      {{ keep.kept }}
                    </i>
                  </div>
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
              <div class="row">
                <h2 class="p-2 text-center">Keep {{ keep.name }}</h2>

                <p>
                  Lorem ipsum dolor sit amet consectetur adipisicing elit. Quod
                  dignissimos saepe dolor soluta enim ipsum sed. Autem libero
                  molestiae alias excepturi obcaecati necessitatibus!
                </p>
              </div>
              <div class="row justify-content-center">
                <div class="col-md-10 border-bottom border-secondary"></div>
              </div>
              <div class="row">
                <span class="d-flex align-items-end">
                  <button class="btn btn-outline-primary">ADD TO VAULT</button>
                  <i class="mdi mdi-delete f-24 selectable"></i>

                  <img
                    class="img-fluid small-profile rounded selectable"
                    src="https://thiscatdoesnotexist.com"
                    alt=""
                  />
                  John Smith
                </span>
              </div>
            </div>
          </div>
        </div>
      </div>
    </template>
  </Modal>
</template>


<script>
import { computed } from "@vue/reactivity"
import { AppState } from "../AppState"
export default {
  setup() {
    return {
      keep: computed(() => AppState.activeKeep)
    }
  }
}
</script>


<style lang="scss" scoped>
.small-profile {
  height: 36px;
  width: 36px;
}
</style>