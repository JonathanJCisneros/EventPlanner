<template>
    <div class="notifications" :class="{ 'opened': notificationsOpen }">
        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 448 512" :class="{ 'active': notificationsOpen }" v-on:click="toggleNotifications">
            <path d="M224 0c-17.7 0-32 14.3-32 32l0 19.2C119 66 64 130.6 64 208l0 18.8c0 47-17.3 92.4-48.5 127.6l-7.4 8.3c-8.4 9.4-10.4 22.9-5.3 34.4S19.4 416 32 416l384 0c12.6 0 24-7.4 29.2-18.9s3.1-25-5.3-34.4l-7.4-8.3C401.3 319.2 384 273.9 384 226.8l0-18.8c0-77.4-55-142-128-156.8L256 32c0-17.7-14.3-32-32-32zm45.3 493.3c12-12 18.7-28.3 18.7-45.3l-64 0-64 0c0 17 6.7 33.3 18.7 45.3s28.3 18.7 45.3 18.7s33.3-6.7 45.3-18.7z" />
        </svg>
        <span v-if="!loading && notifications.length > 0">{{ notifications.length }}</span>
        <div v-if="notificationsOpen">
            <template v-if="notifications.length > 0">
                <div class="actions">
                    <a href="javascript:void(0)" v-on:click.prevent="markAllRead">Mark All as Read</a>
                    <RouterLink to="/notifications">View All</RouterLink>
                </div>
                <div class="items">
                    <div v-for="notification in notifications">

                    </div>
                </div>
            </template>
            <template v-else>
                You have no unread notifications at this time.
            </template>
        </div>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import { RouterLink } from 'vue-router';

    type Notification = {
        id: string,
        type: string,
        title: string,
        description: string,
        status: string,
        createdDate: Date,
        updatedDate: Date
    }

    interface Data {
        notificationsOpen: boolean,
        notifications: Notification[],
        stateMessage: null | string,
        loading: boolean
    }

    export default defineComponent({
        data(): Data {
            return {
                notificationsOpen: false,
                notifications: [],
                stateMessage: null,
                loading: true
            };
        },
        async created(): Promise<void> {
            await this.getNotifications();
        },
        methods: {
            async getNotifications(attempts: number = 0): Promise<void> {
                this.loading = true;

                await fetch('https://localhost:7134/api/notification/getunread', {
                    method: 'GET',
                    headers: {
                        'Authorization': `Bearer ${localStorage.getItem('user_token') as string}`,
                        'Accept': 'application/json',
                        'Content-Type': 'application/json'
                    }
                })
                    .then(async (response: Response): Promise<null | Notification[]> => {
                        if (!response.ok) {
                            return null;
                        }

                        return await response.json() as Notification[];
                    })
                    .then(async (data: null | Notification[]): Promise<void> => {
                        if (!data) {
                            await this.handleFail(attempts);

                            return;
                        }

                        this.loading = false;
                        this.notifications = data as Notification[];
                    })
                    .catch(async (error: Error): Promise<void> => {
                        console.log(error);

                        await this.handleFail(attempts);
                    });
            },
            async handleFail(attempts: number): Promise<void> {
                attempts++;

                if (attempts < 2) {
                    await this.getNotifications(attempts);
                }  
            },
            toggleNotifications(): void {
                this.notificationsOpen = !this.notificationsOpen
            }
        }
    });
</script>

<style scoped>
    .notifications {
        min-width: 2.5rem;
        height: 100%;
        display: flex;
        align-items: center;
        justify-content: center;
    }

    .notifications > span {
        position: absolute;
        top: 3px;
        right: 3px;
    }

    .notifications > div {
        position: absolute;
        right: 220px;
        bottom: -40px;
        background: #333;
        padding: 7px;
        text-align: center;
        width: 300px;
        min-height: 70px;
        max-height: 120px;
        overflow-y: auto;
    }

    svg {
        height: 1.2rem;
        width: 1.2rem;
        fill: #333;
        transition: all 250ms ease-in-out;
    }

        svg:hover {
            fill: white;
            cursor: pointer;
        }
</style>
