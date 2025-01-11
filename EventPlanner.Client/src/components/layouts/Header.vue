<template>
    <header>
        <RouterLink class="home" to="/">
            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24">
                <path d="M7 1V3H3C2.44772 3 2 3.44772 2 4V20C2 20.5523 2.44772 21 3 21H10.7546C9.65672 19.6304 9 17.8919 9 16C9 11.5817 12.5817 8 17 8C18.8919 8 20.6304 8.65672 22 9.75463V4C22 3.44772 21.5523 3 21 3H17V1H15V3H9V1H7ZM23 16C23 19.3137 20.3137 22 17 22C13.6863 22 11 19.3137 11 16C11 12.6863 13.6863 10 17 10C20.3137 10 23 12.6863 23 16ZM16 12V16.4142L18.2929 18.7071L19.7071 17.2929L18 15.5858V12H16Z"></path>
            </svg>
            <h1>Event Planner!</h1>
        </RouterLink>            
        <nav :class="{ 'active': menuOpened }">
            <div>
                <div :class="{ 'opened': dropdownOpened }">
                    <a href="javascript:void(0)" class="dropdown" v-on:click.prevent="toggleDropdown">
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
                <RouterLink to="/events/new" v-if="isLoggedIn">New Event</RouterLink>
                <RouterLink to="/contact">Contact Us</RouterLink>
            </div>
            <div>
                <template v-if="isLoggedIn">
                    <Notifications />
                    <RouterLink to="/user/account">My Account</RouterLink>
                    <a href="javascript:void(0)" v-on:click.prevent="logout">
                        <template v-if="!loggingOut">
                            Logout
                        </template>
                        <template v-else>
                            <span class="loader"></span>
                        </template>
                    </a>
                </template>
                <RouterLink to="/user/login" v-else>Login</RouterLink>
            </div>
        </nav>
        <button type="button"
                :class="{ 'active': menuOpened }"
                v-on:click="toggleMenu"
                aria-label="Mobile Menu Icon">
            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 448 512">
                <path d="M0 96C0 78.3 14.3 64 32 64l384 0c17.7 0 32 14.3 32 32s-14.3 32-32 32L32 128C14.3 128 0 113.7 0 96zM0 256c0-17.7 14.3-32 32-32l384 0c17.7 0 32 14.3 32 32s-14.3 32-32 32L32 288c-17.7 0-32-14.3-32-32zM448 416c0 17.7-14.3 32-32 32L32 448c-17.7 0-32-14.3-32-32s14.3-32 32-32l384 0c17.7 0 32 14.3 32 32z" />
            </svg>
        </button>
    </header>
</template>

<script lang="ts">
    import { defineComponent, defineAsyncComponent } from 'vue';
    import { RouterLink } from 'vue-router';
    import router from '../../routes/routes.ts';
    import cookies from '../../assets/js/cookies.ts';

    interface Data {
        dropdownOpened: boolean,
        menuOpened: boolean,
        loggingOut: boolean
    }

    export default defineComponent({
        data(): Data {
            return {
                dropdownOpened: false,
                menuOpened: false,
                loggingOut: false
            };
        },
        components: {
            Notifications: defineAsyncComponent(() => import('../../components/Notifications.vue'))
        },
        computed: {
            isLoggedIn: function (): boolean {
                return cookies.get('user_token') !== null;
            }
        },
        mounted(): void {
            document.addEventListener('click', this.close);
        },
        beforeDestroy(): void {
            document.removeEventListener('click', this.close);
        },
        methods: {
            close(event): void {
                if (this.dropdownOpened && !['opened', 'dropdown', 'dropdown-item'].some(x => event.target.classList.contains(x))) {
                    this.dropdownOpened = false;
                }
            },
            toggleDropdown(): void {
                this.dropdownOpened = !this.dropdownOpened;
            },
            toggleMenu(): void {
                this.menuOpened = !this.menuOpened;
            },
            async logout(): Promise<void> {
                this.dropdownOpened = false;
                this.loggingOut = true;

                await fetch('https://localhost:7134/api/user/logout', {
                    method: 'GET',
                    headers: {
                        'Authorization': `Bearer ${cookies.get('user_token') as string}`,
                        'Accept': 'application/json',
                        'Content-Type': 'application/json'
                    }
                })
                    .then(async (response: Response): Promise<boolean> => response.ok)
                    .then(async (success: boolean): Promise<void> => {
                        if (success) {
                            cookies.delete('user_token');

                            router.push('/user/login');                            
                        }
                    })
                    .catch((error: Error): void => {
                        console.log(error);
                    });

                this.loggingOut = false;
            }
        }
    });
</script>

<style scoped>
    header {
        display: flex;
        align-items: center;
        background-color: #4CAF50;
        color: white;
        padding: 20px;
        gap: 15px;
        position: relative;
    }

    .loader {
        height: 14px;
        width: 14px;
        border-width: 1.5px;
    }

        .loader::after {
            height: 1.3rem;
            width: 1.3rem;
            border-width: 1.5px;
        }

    .home {
        display: flex;
        align-items: center;
        gap: 7px;
        color: white;
        white-space: nowrap;
        text-decoration-line: none;
    }

        .home:hover {
            text-decoration-line: none;
        }

        .home svg {
            width: 50px;
            height: 50px;
            fill: white;
        }

    nav {        
        overflow: hidden;
        display: flex;
        align-items: center;
        justify-content: space-between;
        width: 100%;
    }

        nav > div {            
            display: flex;
            align-items: center;
        }

        nav a {
            color: white;
            padding: 14px;
            text-decoration: none;
            background: #333;
            display: block;
            border: 3px solid #333;
        }

            nav a:hover {
                background-color: #575757;
                border-color: #575757;
            }

            nav a.router-link-active {
                background-color: white;
                color: #333;
                font-weight: 600;
                pointer-events: none;
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
        transition: all 250ms ease-in-out;
    }

        .dropdown {
            display: flex;
            align-items: center;
            gap: 7px;
        }

            .dropdown svg {
                height: 1rem;
                fill: white;
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

    button {
        display: none;
        background: #333;
        padding: 5px;
        border: none;
        transition: all 250ms ease-in-out;
    }

        button:hover {
            background-color: white;
            cursor: pointer;
        }

        button:hover > svg {
            fill: #333;
        }

        button.active {
            background-color: white;
        }

            button.active > svg {
                fill: #333;
            }

        button > svg {
            height: 2rem;
            width: 2rem;
            fill: white;
            transition: all 250ms ease-in-out;
        }

    @media screen and (max-width: 1159px) {
        header {
            flex-direction: column;
            gap: 10px;
            align-items: center;
        }

        nav {
            height: 0;
            overflow: hidden;
            flex-direction: column;
            gap: 15px;
            transition: all 350ms ease-in-out;
        }

            nav.active {
                height: unset;
                transition: all 350ms ease-in-out;
            }

            nav > div {
                flex-direction: column;
            }            

            nav > div > a {
                background: none;
                border: none;
            }

        .dropdown {
            background: #333;
            border: 2px solid #333;
        }

        button {
            display: block;
            position: absolute;
            right: 10px;
            top: 37px;
        }
    }
</style>
