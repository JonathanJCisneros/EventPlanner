<template>
    <header>
        <div>
            <RouterLink class="home" to="/">
                <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24">
                    <path d="M7 1V3H3C2.44772 3 2 3.44772 2 4V20C2 20.5523 2.44772 21 3 21H10.7546C9.65672 19.6304 9 17.8919 9 16C9 11.5817 12.5817 8 17 8C18.8919 8 20.6304 8.65672 22 9.75463V4C22 3.44772 21.5523 3 21 3H17V1H15V3H9V1H7ZM23 16C23 19.3137 20.3137 22 17 22C13.6863 22 11 19.3137 11 16C11 12.6863 13.6863 10 17 10C20.3137 10 23 12.6863 23 16ZM16 12V16.4142L18.2929 18.7071L19.7071 17.2929L18 15.5858V12H16Z"></path>
                </svg>
                <h1>Event Planner!</h1>
            </RouterLink>            
            <nav>
                <div :class="{ 'opened': opened }">
                    <a href="javascript:void(0)" class="dropdown" v-on:click.prevent="handleDropdown">
                        Services
                        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 320 512">
                            <path d="M137.4 374.6c12.5 12.5 32.8 12.5 45.3 0l128-128c9.2-9.2 11.9-22.9 6.9-34.9s-16.6-19.8-29.6-19.8L32 192c-12.9 0-24.6 7.8-29.6 19.8s-2.2 25.7 6.9 34.9l128 128z" />
                        </svg>
                    </a>
                    <ul>
                        <li>
                            <RouterLink class="dropdown-item" to="/services/weddings">Weddings</RouterLink>
                        </li>
                        <li>
                            <RouterLink class="dropdown-item" to="/services/birthdays">Birthdays</RouterLink>
                        </li>
                        <li>
                            <RouterLink class="dropdown-item" to="/services/business">Business</RouterLink>
                        </li>
                    </ul>
                </div>
                <RouterLink to="/events">Upcoming Events</RouterLink>
                <RouterLink to="/events/new" v-if="isLoggedIn">New Plan</RouterLink>
                <RouterLink to="/contact">Contact Us</RouterLink>
            </nav>
        </div>
        <nav>
            <template v-if="isLoggedIn">
                <Notifications />
                <RouterLink to="/user/account">My Account</RouterLink>
                <a href="javascript:void(0)" v-on:click.prevent="logout">Logout</a>
            </template>
            <RouterLink to="/user/login" v-else>Login</RouterLink>            
        </nav>            
    </header>
</template>

<script lang="ts">
    import { defineComponent, defineAsyncComponent } from 'vue';
    import { RouterLink } from 'vue-router';
    import router from '../../routes/routes.ts';

    interface Data {
        opened: boolean
    }

    export default defineComponent({
        data(): Data {
            return {
                opened: false
            };
        },
        components: {
            Notifications: defineAsyncComponent(() => import('../../components/Notifications.vue'))
        },
        computed: {
            isLoggedIn: function (): boolean {
                return localStorage.getItem('user_auth') && localStorage.getItem('jwt');
            }
        },
        mounted(): void {
            document.addEventListener('click', this.close)
        },
        beforeDestroy(): void {
            document.removeEventListener('click', this.close)
        },
        methods: {
            close(e): void {
                if (this.opened && !['opened', 'dropdown', 'dropdown-item'].some(x => e.target.classList.contains(x))) {
                    this.opened = false
                }
            },
            handleDropdown(): void {
                this.opened = !this.opened;
            },
            logout(): void {
                localStorage.removeItem('user_auth');
                localStorage.removeItem('jwt');

                router.push('/user/login');
            }
        }
    });
</script>

<style scoped>
    header {
        display: flex;
        align-items: center;
        justify-content: space-between;
        background-color: #4CAF50;
        color: white;
        padding: 20px;
    }

        header > div {
            display: flex;
            align-items: center;
            gap: 15px;
        }

    .home {
        display: flex;
        align-items: center;
        gap: 7px;
        color: white;
        text-decoration-line: none;
    }

        .home:hover {
            text-decoration-line: none;
        }

    svg {
        width: 50px;
        height: 50px;
        fill: white;
    }

    nav {
        background: #333;
        overflow: hidden;
        display: flex;
        align-items: center;
        flex-wrap: wrap;
    }

        nav a {
            color: white;
            padding: 14px;
            text-decoration: none;
            display: block;
        }

            nav a:hover {
                background-color: #575757;
            }

        .opened svg {
            transform: rotate(180deg);
            transition: all 250ms ease-in-out;
        }

        .opened ul {
            display: block;
            position: absolute;
            background-color: #333;
            z-index: 999;
            margin-block: 0;
            padding-inline: 0;
        }

        .dropdown {
            display: flex;
            align-items: center;
            gap: 7px;
        }

            .dropdown svg {
                height: 1rem;
                width: auto;
                transition: all 250ms ease-in-out;
            }

            ul {
                display: none;
            }

            li {
                list-style-type: none;
                color: black;            
            }        
</style>
