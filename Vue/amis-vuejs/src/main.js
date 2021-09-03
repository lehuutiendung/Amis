import Vue from 'vue'
import App from './App.vue'
import 'v2-datepicker/lib/index.css';
import V2Datepicker from 'v2-datepicker';
import axios from 'axios'
import VueAxios from 'vue-axios'
export const EventBus = new Vue();

Vue.use(VueAxios, axios);
Vue.use(V2Datepicker);
Vue.config.productionTip = false

new Vue({
  render: h => h(App),
}).$mount('#app')

