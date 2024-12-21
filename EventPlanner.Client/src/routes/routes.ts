import {
    createRouter,
    createWebHistory,
    type NavigationGuardNext,
    type RouteLocationNormalizedGeneric,
    type RouteLocationNormalizedLoadedGeneric,
    type Router
} from 'vue-router';

import Home from '../views/home/Home.vue';
import Error from '../views/home/Error.vue';

import Weddings from '../views/services/Weddings.vue';
import Birthdays from '../views/services/Birthdays.vue';
import Business from '../views/services/Business.vue';

import Plans from '../views/plans/Plans.vue';
import NewPlan from '../views/plans/NewPlan.vue';
import ViewPlan from '../views/plans/ViewPlan.vue';
import EditPlan from '../views/plans/EditPlan.vue';

import Login from '../views/user/Login.vue';
import Register from '../views/user/Register.vue';
import Account from '../views/user/Account.vue';

import Contact from '../views/Contact.vue';

const router: Router = createRouter({
    history: createWebHistory(),
    routes: [
        {
            path: '/',
            name: 'Home',
            component: Home,
            meta: {
                authorize: false,
                layout: {
                    header: 'standard',
                    footer: 'standard'
                }
            }
        },
        {
            path: '/services/weddings',
            name: 'Weddings',
            component: Weddings,
            meta: {
                authorize: false,
                layout: {
                    header: 'standard',
                    footer: 'standard'
                }
            }
        },
        {
            path: '/services/birthdays',
            name: 'Birthdays',
            component: Birthdays,
            meta: {
                authorize: false,
                layout: {
                    header: 'standard',
                    footer: 'standard'
                }
            }
        },
        {
            path: '/services/business',
            name: 'Business',
            component: Business,
            meta: {
                authorize: false,
                layout: {
                    header: 'standard',
                    footer: 'standard'
                }
            }
        },
        {
            path: '/plans',
            name: 'Plans',
            component: Plans,
            meta: {
                authorize: true,
                layout: {
                    header: 'standard',
                    footer: 'standard'
                }
            }
        },
        {
            path: '/plans/new',
            name: 'New Plan',
            component: NewPlan,
            meta: {
                authorize: true,
                layout: {
                    header: 'standard',
                    footer: 'standard'
                }
            }
        },
        {
            path: '/plans/:id',
            name: 'View Plan',
            component: ViewPlan,
            meta: {
                authorize: true,
                layout: {
                    header: 'standard',
                    footer: 'standard'
                }
            }
        },
        {
            path: '/plans/:id/edit',
            name: 'Edit Plan',
            component: EditPlan,
            meta: {
                authorize: true,
                layout: {
                    header: 'standard',
                    footer: 'standard'
                }
            }
        },
        {
            path: '/user/login',
            name: 'Login',
            component: Login,
            meta: {
                hideFromAuth: true,
                layout: {
                    header: 'minimal',
                    footer: 'standard'
                }
            }
        },
        {
            path: '/user/register',
            name: 'Register',
            component: Register,
            meta: {
                hideFromAuth: true,
                layout: {
                    header: 'minimal',
                    footer: 'standard'
                }
            }
        },
        {
            path: '/user/account',
            name: 'Account',
            component: Account,
            meta: {
                authorize: true,
                layout: {
                    header: 'standard',
                    footer: 'standard'
                }
            }
        },
        {
            path: '/contact',
            name: 'Contact',
            component: Contact,
            meta: {
                authorize: false,
                layout: {
                    header: 'standard',
                    footer: 'standard'
                }
            }
        },
        {
            path: '/:catchAll(.*)',
            name: 'Error',
            component: Error,
            meta: {
                authorize: false,
                layout: {
                    header: 'minimal',
                    footer: 'none'
                }
            }
        }
    ]
});

router.beforeEach((to: RouteLocationNormalizedGeneric, from: RouteLocationNormalizedLoadedGeneric, next: NavigationGuardNext): void => {
    if (to.meta.hasOwnProperty('authorize') &&
        to.meta.authorize &&
        !localStorage.getItem('user_auth')
    ) {
        next('/user/login');
        return;
    }
    else if (
        to.meta.hasOwnProperty('hideFromAuth') &&
        to.meta.hideFromAuth &&
        localStorage.getItem('user_auth')
    ) {
        next('/user/account');
    }

    let title: string = 'Event Panner';

    if (to.name && typeof to.name === 'string') {
        title = `${to.name} - ${title}`;
    }

    document.title = title;

    next();
});

export default router;
