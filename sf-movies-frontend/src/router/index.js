import { createRouter, createWebHistory } from 'vue-router'
import Home from '../views/Home.vue'

const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home
  },
  {
    path: '/movies',
    name: 'Movies',
    component: () => import('../views/Movies.vue')
  },
  {
    path: '/locations',
    name: 'Locations',
    component: () => import('../views/Locations.vue')
  }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

export default router

