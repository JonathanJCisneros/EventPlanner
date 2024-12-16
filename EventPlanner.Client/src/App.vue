<template>
    <div class="site-wrapper">        
        <Header v-if="layout === undefined || layout.header === 'standard'" />
        <MinimalHeader v-else-if="layout.header === 'minimal'" />
        <main>
            <RouterView></RouterView>
        </main>
    </div>    
    <Footer v-if="layout === undefined || layout.footer === 'standard'" />
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import { RouterView } from 'vue-router';
    import router from './routes/routes.ts';
    import Header from './components/layouts/Header.vue';
    import MinimalHeader from './components/layouts/MinimalHeader.vue';
    import Footer from './components/layouts/Footer.vue';

    type Layout = {
        header: string,
        footer: string
    }

    export default defineComponent({
        components: {
            Header,
            MinimalHeader,
            Footer
        },
        computed: {
            layout: (): Layout => router.currentRoute.value.meta.layout
        }      
    });
</script>
