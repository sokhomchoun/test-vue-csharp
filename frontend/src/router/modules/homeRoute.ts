export default {
    path: '/',
    name: 'HomeComponent',
    component: () => import('../../components/home/ListComponent.vue'),
    meta: { requiresAuth: true } 
}