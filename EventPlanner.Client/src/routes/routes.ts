import {
    createRouter,
    createWebHistory,
    type NavigationGuardNext,
    type RouteLocationNormalizedGeneric,
    type RouteLocationNormalizedLoadedGeneric,
    type Router
} from 'vue-router';

import Home from '../views/Home.vue';
import Plans from '../views/plans/Plans.vue';
import NewPlan from '../views/plans/NewPlan.vue';
import ViewPlan from '../views/plans/ViewPlan.vue';
import Contact from '../views/Contact.vue';
import EditPlan from '../views/plans/EditPlan.vue';

const router: Router = createRouter({
    history: createWebHistory(),
    routes: [
        {
            path: '/',
            name: 'Home',
            component: Home
        },
        {
            path: '/plans',
            name: 'Plans',
            component: Plans
        },
        {
            path: '/plans/new',
            name: 'New Plan',
            component: NewPlan
        },
        {
            path: '/plans/:id',
            name: 'View Plan',
            component: ViewPlan
        },
        {
            path: '/plans/:id/edit',
            name: 'Edit Plan',
            component: EditPlan
        },
        {
            path: '/contact',
            name: 'Contact',
            component: Contact
        }
    ]
});

router.beforeEach((to: RouteLocationNormalizedGeneric, from: RouteLocationNormalizedLoadedGeneric, next: NavigationGuardNext) => {
    let title: string = 'Event Panner';

    if (to.name && typeof to.name === 'string') {
        title = `${to.name} - ${title}`;
    }

    document.title = title;

    next();
});

export default router;
