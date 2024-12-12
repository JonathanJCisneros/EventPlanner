<template>
    <div class="site-wrapper">
        <Header />
        <main>
            <RouterView></RouterView>
        </main>
    </div>    
    <Footer />
</template>

<script setup lang="ts">
    import { RouterView } from 'vue-router';
    import Header from './components/layouts/Header.vue';
    import Footer from './components/layouts/Footer.vue';
</script>
