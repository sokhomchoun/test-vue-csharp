import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'

export default defineConfig({
  plugins: [vue()],
  resolve: {
    alias: {
      vuex: 'vuex/dist/vuex.esm-bundler.js'
    }
  }
})
