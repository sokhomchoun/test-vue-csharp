import { createRouter, createWebHistory } from 'vue-router';
import type { RouteRecordRaw } from 'vue-router';
import homeRoute from './modules/homeRoute';
import loginRoute from './modules/loginRoute';
import registerRoute from './modules/registerRoute';

const routes: Array<RouteRecordRaw> = [
    homeRoute,
    loginRoute,
    registerRoute
]
const router = createRouter({
    history: createWebHistory(),
    routes
});

router.beforeEach((to, from, next) => {
    const isAuthenticated = !!sessionStorage.getItem('authToken');
    
    if (to.meta.requiresAuth && !isAuthenticated) {
        next({ name: 'LoginComponent' });
    } else {
        next();
    }
});

export default router
