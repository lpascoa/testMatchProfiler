import { createApp } from 'vue';
import App from './App.vue';
import router from './router/';

import { createVuetify } from 'vuetify';

// Importa estilos padrão do Vuetify
import 'vuetify/dist/vuetify.min.css';

// Cria a instância do aplicativo Vue
const app = createApp(App);

// Usa o Vue Router e Vuex store no aplicativo
app.use(router);

// Cria uma instância do Vuetify
const vuetify = createVuetify();

// Usa o Vuetify no aplicativo
app.use(vuetify);

// Monta o aplicativo na div com o id "app" do arquivo index.html
app.mount('#app');
