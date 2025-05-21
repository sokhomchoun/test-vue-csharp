import { createApp } from 'vue';
import App from './App.vue';
import './assets/tailwind.css';
import { store } from './store';
import router from './router';
import 'toastify-js/src/toastify.css';
import 'flowbite';


const app = createApp(App);

app.use(router);
app.use(store);

app.mount('#app')
