import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  user: {},
  account: {},
  keeps: [],
  activeKeep: {},
  myVaults: [],
  myKeeps: [],
  vaults: [],
  activeVaultId: 0,
  myVaultKeeps: [],
  activeVault: {},
  profileVaults: [],
  profileKeeps: [],
  profile: {}
})
