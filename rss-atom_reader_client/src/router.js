import Vue from 'vue';
import Router from 'vue-router';
import NewsFeed from './components/NewsFeed.vue';

Vue.use(Router);

export default new Router({
  mode: 'history',
  base: process.env.BASE_URL,
  routes: [
    {
      path: '/',
      name: 'newsFeed',
      component: NewsFeed,
    },
  ],
});
