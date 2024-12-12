<template>
    <header>
        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24">
            <path d="M7 1V3H3C2.44772 3 2 3.44772 2 4V20C2 20.5523 2.44772 21 3 21H10.7546C9.65672 19.6304 9 17.8919 9 16C9 11.5817 12.5817 8 17 8C18.8919 8 20.6304 8.65672 22 9.75463V4C22 3.44772 21.5523 3 21 3H17V1H15V3H9V1H7ZM23 16C23 19.3137 20.3137 22 17 22C13.6863 22 11 19.3137 11 16C11 12.6863 13.6863 10 17 10C20.3137 10 23 12.6863 23 16ZM16 12V16.4142L18.2929 18.7071L19.7071 17.2929L18 15.5858V12H16Z"></path>
        </svg>
        <h1>Event Planner!</h1>
        <nav>
            <RouterLink to="/">Home</RouterLink>
            <RouterLink to="/plans">Plans</RouterLink>
            <RouterLink to="/plans/new">New Plan</RouterLink>
            <RouterLink to="/contact">Contact Us</RouterLink>
        </nav>
    </header>
</template>

<script lang="ts">
    import { RouterLink } from 'vue-router';
</script>

<style scoped>
    header {
        display: flex;
        align-items: center;
        gap: 15px;
        background-color: #4CAF50;
        color: white;
        padding: 20px;
    }

    svg {
        width: 50px;
        height: 50px;
        fill: white;
    }

    nav {
        background: #333;
        overflow: hidden;
    }

        nav a {
            color: white;
            padding: 14px 20px;
            text-decoration: none;
            display: block;
            float: left;
        }

            nav a:hover {
                background-color: #575757;
            }
</style>
