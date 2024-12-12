<template>
    <header>
        <RouterLink to="/">
            <img alt="Vue logo" class="logo" src="../assets/img/logo.svg" />
        </RouterLink>        
    </header>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import { useRoute, RouterLink } from 'vue-router';

    export default defineComponent({
        computed: {
            route: () => useRoute()
        }
    });
</script>

<style scoped>
    header {
        display: flex;
        align-items: center;
        gap: 15px;
    }

    img {
        width: 50px;
        height: 50px;
    }
</style>
