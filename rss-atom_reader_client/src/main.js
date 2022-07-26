import Vue from 'vue';
import vueResource from 'vue-resource';
import jQuery from 'jquery';
import App from './App.vue';
import router from './router';

import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap-vue/dist/bootstrap-vue.css';

Vue.config.productionTip = false;

global.jQuery = jQuery;
require('bootstrap');

Vue.use(vueResource);

new Vue({
  router,
  render: h => h(App),
}).$mount('#app');
