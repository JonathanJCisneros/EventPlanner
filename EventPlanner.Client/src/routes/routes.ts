import {
    createRouter,
    createWebHistory,
    type NavigationGuardNext,
    type RouteLocationNormalizedGeneric,
    type RouteLocationNormalizedLoadedGeneric,
    type Router
} from 'vue-router';

interface Meta {
    title: string,
    description: string,
    keywords: string[]
}

const router: Router = createRouter({
    history: createWebHistory(),
    routes: [
        {
            path: '/',
            name: 'Home',
            component: () => import('../views/home/Home.vue'),
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
            component: () => import('../views/services/Weddings.vue'),
            meta: {
                authorize: false,
                layout: {
                    header: 'standard',
                    footer: 'standard'
                },
                description: 'Our wedding planning is top notch! We will help with anything from choosing the venue with our partners to helping cater!',
                keywords: ['weddings, wedding planning']
            }
        },
        {
            path: '/services/birthdays',
            name: 'Birthdays',
            component: () => import('../views/services/Birthdays.vue'),
            meta: {
                authorize: false,
                layout: {
                    header: 'standard',
                    footer: 'standard'
                },
                description: 'Our birthday planning is top notch! We will help with anything from choosing the venue with our partners to helping cater!',
                keywords: ['birthdays, birthday planning']
            }
        },
        {
            path: '/services/business',
            name: 'Business',
            component: () => import('../views/services/Business.vue'),
            meta: {
                authorize: false,
                layout: {
                    header: 'standard',
                    footer: 'standard'
                },
                description: 'Our business planning is top notch! We will help with anything from choosing the venue with our partners to helping cater!',
                keywords: ['weddings, wedding planning']
            }
        },
        {
            path: '/events',
            name: 'Events',
            component: () => import('../views/events/Events.vue'),
            meta: {
                authorize: true,
                layout: {
                    header: 'standard',
                    footer: 'standard'
                }
            }
        },
        {
            path: '/events/new',
            name: 'New Event',
            component: () => import('../views/events/NewEvent.vue'),
            meta: {
                authorize: true,
                layout: {
                    header: 'standard',
                    footer: 'standard'
                }
            }
        },
        {
            path: '/events/:id',
            name: 'View Event',
            component: () => import('../views/events/ViewEvent.vue'),
            meta: {
                authorize: true,
                layout: {
                    header: 'standard',
                    footer: 'standard'
                }
            }
        },
        {
            path: '/events/:id/edit',
            name: 'Edit Event',
            component: () => import('../views/events/EditEvent.vue'),
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
            component: () => import('../views/user/Login.vue'),
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
            component: () => import('../views/user/Register.vue'),
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
            component: () => import('../views/user/Account.vue'),
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
            component: () => import('../views/Contact.vue'),
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
            component: () => import('../views/home/Error.vue'),
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

const defaultMeta: Meta = {
    title: 'Event Planner',
    description: 'Welcome to Event Planner! The easiest way to plan events and coordinate with multiple hosts and your guest list.',
    keywords: ['event planner', 'event planning']
}

router.beforeEach(async (to: RouteLocationNormalizedGeneric, from: RouteLocationNormalizedLoadedGeneric, next: NavigationGuardNext): Promise<void> => {
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

    let title: string = defaultMeta.title;

    if (to.name) {
        title = `${to.name as string} - ${title}`;
    }

    document.title = title;

    let description: string = defaultMeta.description;

    if (to.meta.hasOwnProperty('description')) {
        description = to.meta.description as string;
    }

    const descriptionTag: Element | null = document.querySelector('meta[name="description"]');

    if (descriptionTag) {
        descriptionTag.setAttribute('content', description);
    }

    let keywords: string = defaultMeta.keywords.join(', ');

    if (to.meta.hasOwnProperty('keywords')) {
        keywords = (to.meta.keywords as string[]).join(', ');
    }

    const keywordsTag: Element | null = document.querySelector('meta[name="keywords"]');

    if (keywordsTag) {
        keywordsTag.setAttribute('content', keywords);
    }

    next();
});

export default router;
